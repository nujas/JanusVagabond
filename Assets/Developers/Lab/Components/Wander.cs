using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace JanusVagabond
{
  namespace Data
  {

    [Serializable]
    public struct Wander : IComponentData
    {
      public float DistanceLimit;
    }
  }

  namespace Authoring
  {

    [DisallowMultipleComponent]
    [RequiresEntityConversion]
    public class Wander : MonoBehaviour, IConvertGameObjectToEntity
    {

      public float DistanceLimit = 10f;
      public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
      {
        var data = new Data.Wander
        {
          DistanceLimit = DistanceLimit
        };

        dstManager.AddComponentData(entity, data);
      }
    }
  }
}
