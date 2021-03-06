using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.MovementControls
{
    [CreateAssetMenu(menuName ="Prototype/Movement/Movement Settings")]
    public class MovementSettings : ScriptableObject
    {
        public float JumpSpeed = 5f;
        public float HorizontalVelocity=5f;
        public float GravityModifier = 1;
        public float EnvironmentSpeed = 1f;
    }
}

