using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using System.Collections.Generic;
using UnityEngine;

namespace JanusVagabond
{
  namespace Data
  {
    [Serializable]
    public struct Spawn : IComponentData
    {
      public float LocationX;
      public float MinRange;
      public float MaxRange;
      public int Count;
      public Entity Prefab;
    }
  }

  namespace Authoring
  {

    [RequiresEntityConversion]
    public class Spawn : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
    {
      // Add fields to your component here. Remember that:
      //
      // * The purpose of this class is to store data for authoring purposes - it is not for use while the game is
      //   running.
      // 
      // * Traditional Unity serialization rules apply: fields must be public or marked with [SerializeField], and
      //   must be one of the supported types.
      //
      // For example,
      //    public float scale;
      public GameObject Prefab;

      public int Count;

      public float MinRange = -1f;
      public float MaxRange = 1f;

      public void DeclareReferencedPrefabs(List<GameObject> gameObjects)
      {
        gameObjects.Add(Prefab);
      }

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

        var data = new Data.Spawn
        {
          // The referenced prefab will be converted due to DeclareReferencedPrefabs.
          // So here we simply map the game object to an entity reference to that prefab.
          Prefab = conversionSystem.GetPrimaryEntity(Prefab),
          LocationX = transform.position.x,
          MinRange = MinRange,
          MaxRange = MaxRange,
          Count = Count
        };

        dstManager.AddComponentData(entity, data);
      }
    }
  }
}
