using UnityEngine;

namespace JanusVagabond
{
    // at the heart of any enemy is the desire to go left or right.
    // in the future we want to be able to determine whether the enemy should move or not, but for now constant movement is at the core
    public class MoveEnemyComponent : MonoBehaviour
    {
        private MovementDirectionComponent md;
        private MovementSpeedComponent ms;
        private Transform tf;
        private float deltaTime;

        void Start()
        {
            md = this.GetComponent<MovementDirectionComponent>();
            ms = this.GetComponent<MovementSpeedComponent>();
            tf = this.GetComponent<Transform>();
        }

        void Update()
        {
            deltaTime = Time.deltaTime;
            tf.Translate((int)md.value * ms.Value * deltaTime, 0f, 0f);
        }
    }
}
