using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace JanusVagabond
{
  public enum MoveDirection
  {
    LEFT = 0,
    RIGHT = 1
  }

  namespace Tag
  {
    [Serializable]
    public struct MoveLeft : IComponentData
    {

    }

    [Serializable]
    public struct MoveRight : IComponentData
    {

    }
  }

  namespace Data
  {

    [Serializable]
    public struct Move : IComponentData
    {
      public float Speed;
      public float MaxSpeed;
      public float MinSpeed;
      // public MoveDirection Direction;
    }
  }

  namespace Authoring
  {

    [DisallowMultipleComponent]
    [RequiresEntityConversion]
    public class Move : MonoBehaviour, IConvertGameObjectToEntity
    {
      public float MaxSpeed = 5f;
      public float MinSpeed = .1f;

      void OnValidate() {
         if (MinSpeed >= MaxSpeed) {
          throw new ArithmeticException("Min Speed must be lower than Max Speed");
        }
      }

      public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
      {
        var data = new Data.Move
        {
          MinSpeed = MinSpeed,
          MaxSpeed = MaxSpeed
        };

        dstManager.AddComponentData(entity, data);
      }
    }
  }
}

