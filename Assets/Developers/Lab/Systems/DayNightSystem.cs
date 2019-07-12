using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;

namespace JanusVagabond
{

  [UpdateInGroup(typeof(SimulationSystemGroup))]
  public class DayNightSystem : JobComponentSystem
  {
    // This declares a new kind of job, which is a unit of work to do.
    // The job is declared as an IJobForEach<Translation, Rotation>,
    // meaning it will process all entities in the world that have both
    // Translation and Rotation components. Change it to process the component
    // types you want.
    //
    // The job is also tagged with the BurstCompile attribute, which means
    // that the Burst compiler will optimize it for the best performance.
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;
    protected override void OnCreate()
    {
      // Cache the BeginInitializationEntityCommandBufferSystem in a field, so we don't have to create it every frame
      m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    // [BurstCompile]
    struct SpawnJob : IJobForEachWithEntity<SpawnDataComponent, LocalToWorld>
    {
      // Add fields here that your job needs to do its work.
      // For example,
      //    public float deltaTime;

      public EntityCommandBuffer.Concurrent CommandBuffer;

      public void Execute(Entity entity, int index, [ReadOnly] ref SpawnDataComponent spawnData,
          [ReadOnly] ref LocalToWorld location)
      {
        for (var i = 0; i < spawnData.Count; i++)
        {
          var instance = CommandBuffer.Instantiate(index, spawnData.Prefab);

          var position = math.transform(location.Value, new float3(i * 1.3F, noise.cnoise(new float2(1, 0) * 0.21F) * 2, 0));

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

