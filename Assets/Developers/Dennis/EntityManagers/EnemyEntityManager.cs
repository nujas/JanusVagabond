using UnityEngine;

namespace JANUS
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
