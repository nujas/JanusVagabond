using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace JanusVagabond
{
  [Serializable]
  public struct WanderComponent : IComponentData
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

    public float Speed;
    public float DistanceLimit;
  }

  [DisallowMultipleComponent]
  [RequiresEntityConversion]
  public class Wander : MonoBehaviour, IConvertGameObjectToEntity
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

    public float Speed = 10f;

    public float DistanceLimit = 10f;
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
      var data = new WanderComponent
      {
        Speed = Speed,
        DistanceLimit = DistanceLimit
      };

      dstManager.AddComponentData(entity, data);
    }
  }
}
