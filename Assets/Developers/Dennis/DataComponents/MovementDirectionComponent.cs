using UnityEngine;

namespace JanusVagabond
{
    public enum EMovementDirection
    {
        Left = -1,
        Right = 1,
    }

    public class MovementDirectionComponent : MonoBehaviour
    {
        public EMovementDirection value;
    }
}
