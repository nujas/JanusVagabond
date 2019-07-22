using UnityEngine;

namespace JanusVagabond
{
    /// <summary>
    /// system to handle spawning NPCs at the designated spawn point
    /// </summary>
    public class SpawnEntityComponent : MonoBehaviour
    {
        private LocalSpawnComponent ls;
        
        void Start()
        {
            ls = this.GetComponent<LocalSpawnComponent>();

        }

        void SpawnNPC(GameObject npc)
        {
            Instantiate(npc, ls.LocalSpawn, Quaternion.identity);

            //TODO: add additional spawning behavior
        }
    }
}

