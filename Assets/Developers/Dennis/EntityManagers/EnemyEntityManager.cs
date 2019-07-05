using System.Collections;
using System.Collections.Generic;
using TagComponents;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace EntityManagers
{
    public class EnemyEntityManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject EnemyEntityGameObject = null;

        void Start()
        {
            Instantiate(EnemyEntityGameObject);
        }
    }
}
