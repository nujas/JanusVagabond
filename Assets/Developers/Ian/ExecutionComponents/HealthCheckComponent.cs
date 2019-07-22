using UnityEngine;

namespace JanusVagabond
{

    /// <summary>
    /// system to handle checking the health of the structure
    /// </summary>
    public class HealthCheckComponent : MonoBehaviour
    {
        private LevelComponent lvl;
        private HealthComponent hCurrent;
        private float hStart;

        void Start()
        {
            hCurrent = this.GetComponent<HealthComponent>();
            hStart = hCurrent.Value;
            lvl = this.GetComponent<LevelComponent>();
        }

        void Update()
        {
            if (NeedsRepair())
            {
                // TODO: add functionality for repairing structures

                if (!IsAlive())
                {
                    // TODO: add functionality for destruction
                }
            }
        }

        bool IsAlive()
        {
            if (hCurrent.Value > 0)
            {
                return true;
            }
            return false;
        }

        bool NeedsRepair()
        {
            if (hCurrent.Value < hStart)
            {
                return true;
            }

            return false;
        }
    }
}
