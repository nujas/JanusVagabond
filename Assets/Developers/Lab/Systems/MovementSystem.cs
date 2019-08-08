using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

using Random = Unity.Mathematics.Random;

namespace JanusVagabond
{
  public class MovementSystem : ComponentSystem
  {
    const int RANDOM_SEED = 99;

    static Random m_RandomPool = new Random(RANDOM_SEED);
    protected override void OnUpdate()
    {

      Entities.WithAllReadOnly<Data.Move, Tag.MoveLeft>().ForEach<Data.Move, Translation>(
          (Entity id, ref Data.Move mover, ref Translation translation) =>
          {
            var deltaTime = Time.deltaTime;

            translation = new Translation()
            {
              Value = new float3(translation.Value.x - deltaTime * mover.Speed, translation.Value.y, translation.Value.z)
            };

            // Components can only be added or removed using PostUpdateCommands.
          }
      );

      Entities.WithAllReadOnly<Data.Move, Tag.MoveRight>().ForEach<Data.Move, Translation>(
        (Entity id, ref Data.Move mover, ref Translation translation) =>
        {
          var deltaTime = Time.deltaTime;

          translation = new Translation()
          {
            Value = new float3(translation.Value.x + deltaTime * mover.Speed, translation.Value.y, translation.Value.z)
          };

          // Components can only be added or removed using PostUpdateCommands.
        }
      );

      // If wandered outside limit, move the other way
      Entities.WithAllReadOnly<Data.Wander>().ForEach<Data.Wander, Translation>(
          (Entity id, ref Data.Wander wanderer, ref Translation translation) =>
          {
            if (translation.Value.x < -wanderer.DistanceLimit)
            {
              PostUpdateCommands.RemoveComponent<Tag.MoveLeft>(id);
              PostUpdateCommands.AddComponent(id, new Tag.MoveRight());
            }

            if (translation.Value.x > wanderer.DistanceLimit)
            {
              PostUpdateCommands.RemoveComponent<Tag.MoveRight>(id);
              PostUpdateCommands.AddComponent(id, new Tag.MoveLeft());
            }
            // Components can only be added or removed using PostUpdateCommands.
          }
      );

      // If componenent has wandering but no move tag, give it a random one.
      Entities.WithNone<Tag.MoveLeft, Tag.MoveRight>().WithAllReadOnly<Data.Wander, Data.Move>().ForEach(
          (Entity id, ref Data.Move mover) =>
          {
            mover.Speed = m_RandomPool.NextFloat(mover.MinSpeed, mover.MaxSpeed);
            // NOTE: Quirky way of doing this, just wanna experiment xd
            if (m_RandomPool.NextBool())
            {
              PostUpdateCommands.AddComponent(id, new Tag.MoveLeft());
            }
            else
            {
              PostUpdateCommands.AddComponent(id, new Tag.MoveRight());
            }
          }
      );
    }
  }
}