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

  [Serializable]
  public struct DayNightDataComponent : IComponentData
  {
    // Add fields to your component here. Remember that:
    //
    // * A component itself is for storing data and doesn't 'do' anything.
    //
    // * To act on the data, you will need a System.
    //
    // * Data in a component must be blittable, which means a component can
    //   only contain fields which are primitive types or other blittable
    //   structs; they cannot contain references to classes.
    //
    // * You should focus on the data structure that makes the most sense
    //   for runtime use here. Authoring Components will be used for 
    //   authoring the data in the Editor.

    public DayNightPhase Phase;

    public float TotalTimeSinceStart;

    public float TimeOfCurrentDay;


  }

  [RequiresEntityConversion]
  public class DayNight : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
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

      var spawnerData = new SpawnDataComponent
      {
        // The referenced prefab will be converted due to DeclareReferencedPrefabs.
        // So here we simply map the game object to an entity reference to that prefab.
        Prefab = conversionSystem.GetPrimaryEntity(Prefab),
        Count = Count
      };

      dstManager.AddComponentData(entity, spawnerData);
    }
  }
}
