﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.PlayerInput
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private AbstractInputData[] _inputDataArray;

        private void Update()
        {
            for (int i = 0; i < _inputDataArray.Length; i++)
            {
                _inputDataArray[i].ProcessInput();
            }
        }
    }
}

