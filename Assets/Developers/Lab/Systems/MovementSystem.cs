using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

using Random = Unity.Mathematics.Random;

namespace JanusVagabond
{
  public class MovementSystem : ComponentSystem
  {
    protected override void OnUpdate()
    {

      // If has MoveLeft, move it toward the left
      Entities.WithAllReadOnly<WanderComponent, MoveLeftComponent>().ForEach(
          (Entity id, ref Translation translation) =>
          {
            var deltaTime = Time.deltaTime;
            translation = new Translation()
            {
              Value = new float3(translation.Value.x - deltaTime, translation.Value.y, translation.Value.z)
            };

            // Components can only be added or removed using PostUpdateCommands.
            if (translation.Value.x < -10.0f)
              PostUpdateCommands.RemoveComponent<MoveLeftComponent>(id);
          }
      );

      // If has MoveLeft, move it toward the left
      Entities.WithAllReadOnly<WanderComponent, MoveRightComponent>().ForEach(
          (Entity id, ref Translation translation) =>
          {
            var deltaTime = Time.deltaTime;
            translation = new Translation()
            {
              Value = new float3(translation.Value.x + deltaTime, translation.Value.y, translation.Value.z)
            };

            // Components can only be added or removed using PostUpdateCommands.
            if (translation.Value.x > 10.0f)
              PostUpdateCommands.RemoveComponent<MoveLeftComponent>(id);
          }
      );

      // If componenent has wandering but no move tag, give it a random one.
      Entities.WithAllReadOnly<WanderComponent>().WithNone<MoveLeftComponent>().WithNone<MoveRightComponent>().ForEach(
          (Entity id, ref Translation translation) =>
          {
            var random = new Random(1);

            if (random.NextBool())
            {
              PostUpdateCommands.AddComponent(id, new MoveLeftComponent());
            }
            else
            {
              PostUpdateCommands.AddComponent(id, new MoveRightComponent());
            }
          }
      );
    }
  }
}