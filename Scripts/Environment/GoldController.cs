using UnityEngine;
using Prototype.PlayerControls;

public class GoldController : MonoBehaviour
{
    private PlayerController _player;
    private Transform _playerTransform;
    public float rotateSpeed = 10;
    public float speed = 10f;
    public float _distance = 10f;
    Vector3 startPos;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<PlayerController>();
        _playerTransform = GameObject.Find("Player").transform;
        startPos = transform.position;
        InvokeRepeating("Activator", 10, 10);
    }
    void Update()
    {
        GoldRotate();
        //MoveToPlayer();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
        }
    }
    void Activator()
    {
        if (gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
    }
    void GoldRotate()
    {
        transform.Rotate(Vector3.right * rotateSpeed * Time.deltaTime);
    }
    void MoveToPlayer()
    {
        float distance = Vector3.Distance(transform.position, _playerTransform.position);

        if (_player.magnet == true)
        {
            if (distance <= _distance)
            {
                transform.position = Vector3.MoveTowards(transform.position, _playerTransform.position, speed * Time.deltaTime);
            } 
        }
        if (distance <= 2)
        {
            gameObject.SetActive(false);
            transform.position = startPos;
        }
    }

   
}
