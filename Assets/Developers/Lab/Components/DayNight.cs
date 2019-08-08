using System;
using System.Collections.Generic;

using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


namespace JanusVagabond
{
  public enum DayNightPhase
  {
    DAY = 0,
    NIGHT = 1
  }

  namespace Data {
    [Serializable]
    public struct DayNight : IComponentData
    {
      public DayNightPhase Phase;

      public float TotalTimeSinceStart;

      public float TimeOfCurrentDay;
    }
  }

  namespace Authoring {

    [RequiresEntityConversion]
    public class DayNight : MonoBehaviour, IConvertGameObjectToEntity
    {

      public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
      {
        // Call methods on 'dstManager' to create runtime components on 'entity' here. Remember that:
        //
        // * You can add more than one component to the entity. It's also OK to not add any at all.
        //
        // * If you want to create more than one entity from the data in this class, use the 'conversionSystem'
        //   to do it, instead of adding entities through 'dstManager' directly.
        //
        // For example,
        //   dstManager.AddComponentData(entity, new Unity.Transforms.Scale { Value = scale });

        var data = new Data.DayNight
        {
          
        };

        dstManager.AddComponentData(entity, data);
      }
    }
  }
}
