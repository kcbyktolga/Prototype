using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Prototype.PlayerInput
{
    
    public abstract class AbstractInputData : ScriptableObject
    {
        public float Horizontal;
        public float Vertical;
        public float Jump;
        public abstract void ProcessInput();
    }
}
