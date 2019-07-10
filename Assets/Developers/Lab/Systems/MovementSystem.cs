using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

using Random = Unity.Mathematics.Random;

namespace JanusVagabond
{
  public class MovementSystem : ComponentSystem
  {
    float speed = 10f;
    float wanderLimit = 10f;
    protected override void OnUpdate()
    {

      // If has MoveLeft, move it toward the left
      Entities.WithAllReadOnly<WanderComponent, MoveLeftComponent>().ForEach(
          (Entity id, ref Translation translation) =>
          {
            if (translation.Value.x < -wanderLimit)
            {
              PostUpdateCommands.RemoveComponent<MoveLeftComponent>(id);
              PostUpdateCommands.AddComponent(id, new MoveRightComponent());
            }
            var deltaTime = Time.deltaTime;

            translation = new Translation()
            {
              Value = new float3(translation.Value.x - deltaTime * speed, translation.Value.y, translation.Value.z)
            };

            // Components can only be added or removed using PostUpdateCommands.
          }
      );

      // If has MoveLeft, move it toward the left
      Entities.WithAllReadOnly<WanderComponent, MoveRightComponent>().ForEach(
          (Entity id, ref Translation translation) =>
          {
            if (translation.Value.x > wanderLimit)
            {
              PostUpdateCommands.RemoveComponent<MoveRightComponent>(id);
              PostUpdateCommands.AddComponent(id, new MoveLeftComponent());
            }

            var deltaTime = Time.deltaTime;

            translation = new Translation()
            {
              Value = new float3(translation.Value.x + deltaTime * speed, translation.Value.y, translation.Value.z)
            };

            // Components can only be added or removed using PostUpdateCommands.
          }
      );

      // If componenent has wandering but no move tag, give it a random one.
      Entities.WithAllReadOnly<WanderComponent>().WithNone<MoveLeftComponent>().WithNone<MoveRightComponent>().ForEach(
          (Entity id, ref Translation translation) =>
          {
            // NOTE: Quirky way of do this, just wanna experiment xd
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