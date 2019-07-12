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
    BeginInitializationEntityCommandBufferSystem m_EntityCommandBufferSystem;
    protected override void OnCreate()
    {
      // Cache the BeginInitializationEntityCommandBufferSystem in a field, so we don't have to create it every frame
      m_EntityCommandBufferSystem = World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>();
    }

    // [BurstCompile]
    struct SpawnJob : IJobForEachWithEntity<Data.DayNight, LocalToWorld>
    {
      // Add fields here that your job needs to do its work.
      // For example,
      //    public float deltaTime;

      public EntityCommandBuffer.Concurrent CommandBuffer;

      public void Execute(Entity entity, int index, [ReadOnly] ref Data.DayNight data,
          [ReadOnly] ref LocalToWorld location)
      {

        

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

