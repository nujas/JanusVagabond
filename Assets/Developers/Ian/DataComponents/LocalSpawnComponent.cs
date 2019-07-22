using UnityEngine;

namespace JanusVagabond
{
    public class LocalSpawnComponent : MonoBehaviour
    {
        /// <summary>
        /// The local vector from the origin where the NPC will be spawned
        /// </summary>
        [SerializeField]
        public Vector2 LocalSpawn;
    }
}
