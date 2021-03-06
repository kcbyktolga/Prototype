using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.PlayerInput
{
    [CreateAssetMenu(menuName = "Prototype/Input/Input Data")]
    public class PlayerInputData : AbstractInputData
    {
        [Header("Axis Base Control")]
        [SerializeField] private bool _axisActive;
        [SerializeField] private string AxisNameHorizontal;
        [SerializeField] private string AxisNameVertical;

        [Header("Jump Base Control")]
        [SerializeField] private bool _jumpActive;
        [SerializeField] private string AxisNameJump;

        [Header("Key Base Control")]
        [SerializeField] private bool _keyBaseHorizontalActive;
        [SerializeField] private KeyCode PositiveHorizontalKeyCode;
        [SerializeField] private KeyCode NegativeHorizontalKeyCode;
        [SerializeField] private bool _keyBaseVerticalActive;
        [SerializeField] private KeyCode PositiveVerticalKeyCode;
        [SerializeField] private KeyCode NegativeVerticalKeyCode;
        [SerializeField] private float _increaseAmount = 0.015f;

        [Header("Jump Key Control")]
        [SerializeField] private bool _keyBaseJumpActive;
        [SerializeField] private KeyCode JumpKeyCode;
        [SerializeField] private float _jumpForce;

        public override void ProcessInput()
        {
            if (_axisActive)
            {
                Horizontal = Input.GetAxis(AxisNameHorizontal);
                Vertical = Input.GetAxis(AxisNameVertical);
            }
            else
            {
                if (_keyBaseHorizontalActive)
                {
                    KeyBaseAxisControl(ref Horizontal, PositiveHorizontalKeyCode, NegativeHorizontalKeyCode);
                }
                if (_keyBaseVerticalActive)
                {
                    KeyBaseAxisControl(ref Vertical, PositiveVerticalKeyCode, NegativeVerticalKeyCode);
                }             
            }

            if (_jumpActive)
            {
                Jump = Input.GetAxis(AxisNameJump);
            }
            else
            {
                KeyBaseJumpControl(ref Jump, JumpKeyCode);
            }
        }

        private void KeyBaseAxisControl(ref float value, KeyCode positive, KeyCode negative)
        {
            bool positiveActive = Input.GetKey(positive);
            bool negativeActive = Input.GetKey(negative);
           
            if (positiveActive)
            {
                value += _increaseAmount;
            }
            else if (negativeActive)
            {
                value -= _increaseAmount;
            }
            else
            {
                value = 0;
            }

            value = Mathf.Clamp(value, -1, 1);        
        }
        private void KeyBaseJumpControl(ref float value, KeyCode jump)
        {
            bool jumpActive = Input.GetKey(jump);

            if (jumpActive)
            {
                Debug.Log("zıpladı");
            }
        }
    }

}
