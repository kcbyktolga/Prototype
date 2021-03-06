using Prototype.PlayerInput;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Prototype.MovementControls;

namespace Prototype.PlayerControls
{
    public class PlayerController : MonoBehaviour
    {   [Header("Scriptable Object")]
        [SerializeField] private MovementSettings _movementSettings;
        [SerializeField] private AbstractInputData _playerInputData;
        [SerializeField] private MovementSettings _environmentData;
        [Header("Boolens")]
        public bool isOnGround = true;
        public bool gameOver=false;
        public bool startGame = false;
        public bool magnet = false;
        [Header("Objects")]
        public Color targetColor;
        public ParticleSystem explosionParticle;
        public ParticleSystem dirtParticle;
        public AudioClip jumpSound;
        public AudioClip crashSound;
        public AudioClip goldSound;
        private Animator playerAnim;
        private Rigidbody playerRb;
        private AudioSource playerAudio;
        [Header("Values")]
        public int score = 0;
        public int highScore = 0;
        public int lastScore = 0;
        int _lastScore;
        [Header("UI Objects")]
        public GameObject[] evaluation;
        public GameObject scoreBoard;
        public GameObject intro;
        public GameObject buttons;
        private Image gamePanel;
        public Text scoreText;
        public Text lastScoreText;
        public Text highScoreText;
        public Text scoreBoardScoreText;
        int evaluationIndex;
        private Vector3 newPos=new Vector3(2.5f,0,10);
        Vector3 rightPos;
        Vector3 leftPos;
        void Start()
        {
            playerRb = GetComponent<Rigidbody>();
            playerAudio = GetComponent<AudioSource>();
            Physics.gravity = new Vector3(0, -9.81f * _movementSettings.GravityModifier, 0);
            _environmentData.EnvironmentSpeed = 1f;
            playerAnim = GetComponent<Animator>();
            gamePanel = GameObject.Find("DeathScene").GetComponent<Image>();
            highScore = PlayerPrefs.GetInt("High Score", 0);
            scoreBoard.SetActive(false);
            lastScore = PlayerPrefs.GetInt("LastScore");
            buttons.SetActive(false);
            playerAnim.SetBool("Static_b", true);
            playerAnim.SetFloat("Speed_f", 0.4f);
            InvokeRepeating("EnvironmentControl",10,20);
            //Debug.Log(Physics.gravity.y);
        }

        void FixedUpdate()
        {
            if (Input.anyKeyDown)
            {
                startGame = true;
            }
            if (startGame==true && !gameOver)
            {
                buttons.SetActive(true);
                intro.SetActive(false);
                playerAnim.SetFloat("Speed_f", 1);
                Jumping();
                Movement();  
            }
            else
            {
                dirtParticle.Stop();
            }
            Score();

        }
        void Movement()
        {   
            //if (_playerInputData.Horizontal < 0)
            //{
            //    transform.position = Vector3.Lerp(transform.position, new Vector3(-2.5f, transform.position.y, transform.position.z), _movementSettings.HorizontalVelocity);
            //}
            //if (_playerInputData.Horizontal > 0)
            //{
            //    transform.position = Vector3.Lerp(transform.position, new Vector3(2.5f, transform.position.y, transform.position.z), _movementSettings.HorizontalVelocity );
            //}

             rightPos = new Vector3(2.5f, 0, 10);
             leftPos = new Vector3(-2.5f, 0, 10);

            if(_playerInputData.Horizontal < 0)
            {
                newPos = leftPos;
            }
            if (_playerInputData.Horizontal > 0)
            {
                newPos = rightPos;
            }
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * _movementSettings.HorizontalVelocity);

            //if (Input.GetButtonDown("Fire1"))
            //{
            //    firstPos.x = Input.mousePosition.x;
            //}
            //if (Input.GetButtonUp("Fire1"))
            //{
            //    endPos.x = Input.mousePosition.x;
            //}
            //if (endPos.x < firstPos.x)
            //{
            //    transform.position = Vector3.Lerp(transform.position, new Vector3(-2.5f, transform.position.y, transform.position.z), _movementSettings.HorizontalVelocity);
            //}
            //if (endPos.x > firstPos.x)
            //{
            //    transform.position = Vector3.Lerp(transform.position, new Vector3(2.5f, transform.position.y, transform.position.z), _movementSettings.HorizontalVelocity);
            //}
        }
        void Jumping()
        {
            if (Input.GetButtonDown("Jump") && isOnGround)
            {
              //playerRb.AddForce(Vector3.up * _movementSettings.JumpForce, ForceMode.Impulse);
                playerRb.velocity = new Vector3(playerRb.velocity.x, _movementSettings.JumpSpeed, playerRb.velocity.z);
                isOnGround = false;
                playerAnim.SetTrigger("Jump_trig");
                dirtParticle.Stop();
                playerAudio.PlayOneShot(jumpSound, 1);
            }
        }
        public void JumpButton()
        {
            if (isOnGround && !gameOver)
            {
              //playerRb.AddForce(Vector3.up * _movementSettings.JumpForce, ForceMode.Impulse);
                playerRb.velocity = new Vector3(playerRb.velocity.x, _movementSettings.JumpSpeed, playerRb.velocity.z);
                isOnGround = false;
                playerAnim.SetTrigger("Jump_trig");
                dirtParticle.Stop();
                playerAudio.PlayOneShot(jumpSound, 1);
            }

        }
        public void RightButton()
        {
            // transform.position = Vector3.Lerp(transform.position, new Vector3(2.5f, transform.position.y, transform.position.z), _movementSettings.HorizontalVelocity*Time.deltaTime*200);
            newPos = rightPos;
        }
        public void LeftButton()
        {
            // transform.position = Vector3.Lerp(transform.position, new Vector3(-2.5f, transform.position.y, transform.position.z), _movementSettings.HorizontalVelocity*Time.deltaTime*200);
            newPos = leftPos;
        }
        IEnumerator LerpDarkScene(Color endColor, float duration)
        {
            float time = 0;
            Color startColor = gamePanel.color;
            while (time < duration)
            {
                gamePanel.color = Color.Lerp(startColor, endColor, time / duration);
                time += Time.deltaTime;
                yield return null; 
            }
            gamePanel.color = endColor;
            StartCoroutine(ScoreBoardActive());
        }
        IEnumerator ScoreBoardActive()
        {
            scoreBoard.SetActive(true);
            ScoreEvaluation();
            yield return new WaitForSeconds(6);
        }
        void EnvironmentControl()
        {
           _environmentData.EnvironmentSpeed += 0.1f;           
        }
        void AddScore()
        {
            score ++;          
        }
        void Score()
        {           
            if (score > PlayerPrefs.GetInt("High Score", 0))
            {
                PlayerPrefs.SetInt("High Score", score);
            }
            scoreText.text = score.ToString();
            highScoreText.text = "High Score: "+ highScore;
            scoreBoardScoreText.text = "Score: " + score;
            lastScoreText.text = "Last Score: " + lastScore;
            _lastScore = score;
            PlayerPrefs.SetInt("LastScore", _lastScore);
        }
        void ScoreEvaluation()
        {
            if (score < 100)
            {
                evaluationIndex = 0;
            }
            else if(score>=100 && score < 500)
            {
                evaluationIndex = 1;
            }
            else if (score >= 1000)
            {
                evaluationIndex = 2;
            }
            evaluation[evaluationIndex].SetActive(true);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isOnGround = true;
                dirtParticle.Play();
            }
            else if (collision.gameObject.CompareTag("Obstacle"))
            {
                gameOver = true;
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                StartCoroutine(LerpDarkScene(targetColor, 4));
                explosionParticle.Play();
                dirtParticle.Stop();
                playerAudio.PlayOneShot(crashSound, 1);
                buttons.SetActive(false);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Gold"))
            {
                if (!gameOver)
                {
                    AddScore();
                    playerAudio.PlayOneShot(goldSound, 1);
                }
            }

            //if (other.gameObject.CompareTag("Magnet"))
            //{
            //    if (!gameOver)
            //    {
            //        other.gameObject.SetActive(false);
            //        magnet = true;
            //        Invoke("MagnetModeOff", 10f);
            //    }
            //}
        }
        //void MagnetModeOff()
        //{
        //    magnet = false;

        //}
        public void Restart()
        {
            SceneManager.LoadScene("Game");
        }
    }
}

