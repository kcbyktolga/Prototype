using Prototype.PlayerControls;
using UnityEngine;

namespace Prototype.MovementControls
{
    public class EnvironmentController : MonoBehaviour
    {
        [SerializeField] private MovementSettings[] _movementSettings;
        [SerializeField] private MovementSettings _environmentData;
        private PlayerController playerControllerScript;
        int currentIndex;
        void Start()
        {
            playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
            currentIndex = 0;
        }

    void Update()
        {
            if (playerControllerScript.startGame==true)
            { 
                currentIndex = 1;
                
                if (playerControllerScript.gameOver == false)
                {
                    transform.Translate(Vector3.forward * _movementSettings[currentIndex].EnvironmentSpeed*_environmentData.EnvironmentSpeed * Time.deltaTime);                   
                }
            }
            else
            {
                transform.Translate(Vector3.forward * _movementSettings[currentIndex].EnvironmentSpeed*_environmentData.EnvironmentSpeed * Time.deltaTime);
            }
                      
        }
    }
}

