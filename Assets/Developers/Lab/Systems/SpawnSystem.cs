using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace JanusVagabond
{

  [UpdateInGroup(typeof(SimulationSystemGroup))]
  public class SpawnSystem : JobComponentSystem
  {
    const int RANDOM_SEED = 999;
    const float GROUND_HEIGHT = -2f;
    static Random m_RandomPool = new Random(RANDOM_SEED);

    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;
    protected override void OnCreate()
    {
      // Cache the BeginInitializationEntityCommandBufferSystem in a field, so we don't have to create it every frame
      m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    // [BurstCompile]
    struct SpawnJob : IJobForEachWithEntity<Data.Spawn, LocalToWorld>
    {
      // Add fields here that your job needs to do its work.
      // For example,
      //    public float deltaTime;

      public EntityCommandBuffer.Concurrent CommandBuffer;

      public void Execute(Entity entity, int index, [ReadOnly] ref Data.Spawn spawnData,
          [ReadOnly] ref LocalToWorld location)
      {
        for (var i = 0; i < spawnData.Count; i++)
        {
          var instance = CommandBuffer.Instantiate(index, spawnData.Prefab);

          var position = math.transform(location.Value, new float3(m_RandomPool.NextFloat(spawnData.MinRange, spawnData.MaxRange) + spawnData.LocationX, GROUND_HEIGHT, 0));

          CommandBuffer.SetComponent(index, instance, new Translation { Value = position });
        }

        CommandBuffer.DestroyEntity(index, entity);
      }
    }

    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
      // Schedule job run:
      var job = new SpawnJob
      {
        CommandBuffer = m_EntityCommandBufferSystem.CreateCommandBuffer().ToConcurrent()
      }.Schedule(this, inputDependencies);

      // Add handle to play back the commands up the chain.
      m_EntityCommandBufferSystem.AddJobHandleForProducer(job);

      return job;
    }
  }
}

