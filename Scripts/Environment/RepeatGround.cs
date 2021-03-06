using Prototype.PlayerControls;
using UnityEngine;

namespace Prototype.MovementControls
{
    public class RepeatGround : MonoBehaviour
    {
        [SerializeField] private MovementSettings[] _movementSettings;
        [SerializeField] private MovementSettings _environmentData;
        private PlayerController playerControllerScript;
        private Vector3 startPos;
        public GameObject firstGround;
        public GameObject lastGround;
        Rigidbody firstRb;
        Rigidbody lastRb;
        float lenght;
        int currentIndex = 0;

        void Start()
        {
            //startPos = transform.position;
            playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
            firstRb = firstGround.GetComponent<Rigidbody>();
            lastRb = lastGround.GetComponent<Rigidbody>();
            lenght = firstGround.GetComponent<BoxCollider>().size.z;
        }
        void Update()
        {
            if (playerControllerScript.startGame == true)
            {
                currentIndex = 1;
                if (playerControllerScript.gameOver == false)
                {
                    RepeatingGround();
                }
                else
                {
                    firstRb.velocity = new Vector3(0, 0, 0);
                    lastRb.velocity = new Vector3(0, 0, 0);
                }
            }
            else
            {
                RepeatingGround();
            }          
        }
        void RepeatingGround()
        {
            firstRb.velocity = new Vector3(0, 0, _movementSettings[currentIndex].EnvironmentSpeed*_environmentData.EnvironmentSpeed);
            lastRb.velocity = new Vector3(0, 0, _movementSettings[currentIndex].EnvironmentSpeed*_environmentData.EnvironmentSpeed); 

            if (firstGround.transform.position.z <= -lenght/2)
            {
                firstGround.transform.position += new Vector3(0, 0, lenght * 2);
            }
            if (lastGround.transform.position.z <= -lenght/2)
            {
                lastGround.transform.position += new Vector3(0, 0, lenght * 2);
            }
        }
    }
}

