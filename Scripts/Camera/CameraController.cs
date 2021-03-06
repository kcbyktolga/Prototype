using Prototype.PlayerControls;
using UnityEngine;

namespace Prototype.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CameraSettings[] _cameraSettings;
        [SerializeField] private Transform _targetTrasnform;
        [SerializeField] private Transform _cameraTransform;
        private PlayerController _playerController;

        private void Start()
        {
            _playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        private void LateUpdate()
        {
            CameraRotationFollow();
            CameraMovementFollow();
        }
        private void CameraRotationFollow()
        {
            if(_playerController.gameOver==false)
            {
                _cameraTransform.rotation = Quaternion.Lerp(_cameraTransform.rotation,
                            Quaternion.LookRotation(_targetTrasnform.position - _cameraTransform.position),
                            Time.deltaTime * _cameraSettings[0].RotationLerpSpeed);

                _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, _targetTrasnform.position + _cameraSettings[0].PositionOffset, Time.deltaTime * _cameraSettings[0].PositionLerp);
            }
            else
            {
                _cameraTransform.rotation = Quaternion.Lerp(_cameraTransform.rotation,
                           Quaternion.LookRotation(_targetTrasnform.position - _cameraTransform.position),
                           Time.deltaTime * _cameraSettings[1].RotationLerpSpeed);

                _cameraTransform.position = Vector3.Lerp(_cameraTransform.position, _targetTrasnform.position + _cameraSettings[1].PositionOffset, Time.deltaTime * _cameraSettings[1].PositionLerp);
            }
            

        }
        private void CameraMovementFollow()
        {
           

        }

    }
}

