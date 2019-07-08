using UnityEngine;

namespace JanusVegabond
{
    public class BaseStructure : MonoBehaviour
    {
        // Fields
        // --------------------
        public uint Level { get; }
        public float HealthInitial { get; } // TODO: replace the health variables with a health component
        public float HealthCurrent { get; set; }
        public Vector2 SpawnPoint { get; } // the local vector at which the NPC will spawn from, could be different depending on the sprite
        // --------------------

        void Start()
        {
            HealthCurrent = this.HealthInitial;
        }

        // Methods
        // --------------------
        public bool NeedRepair()
        {
            if(this.HealthCurrent < this.HealthInitial)
            {
                return true;
            }

            return false;
        }

        public bool IsAlive()
        {
            if(this.HealthCurrent < 0)
            {
                return true;
            }

            return false;
        }
        // --------------------
    }
}

