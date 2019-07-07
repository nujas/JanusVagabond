using UnityEngine;

namespace JanusVagabond
{
    public class EnemyEntityManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject EnemyEntityGameObject;

        void Start()
        {
            Instantiate(EnemyEntityGameObject, Vector3.zero, Quaternion.identity);
        }
    }
}
