using UnityEngine;

namespace JanusVagabond
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
