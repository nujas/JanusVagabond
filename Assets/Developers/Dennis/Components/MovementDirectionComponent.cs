﻿using UnityEngine;

namespace JANUS
{
    public enum EMovementDirection
    {
        Left = 0,
        Right = 1,
    }

    public class MovementDirection : MonoBehaviour
    {
        public EMovementDirection value;
    }
}