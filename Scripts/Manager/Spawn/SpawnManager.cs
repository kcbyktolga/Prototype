using Prototype.PlayerControls;
using System.Collections.Generic;
using UnityEngine;

namespace Prototype.Manager
{
    public class SpawnManager : MonoBehaviour
    {   
        private PlayerController playerControllerScript;
        [Header("GameObjects")]
        public GameObject[] obstacles;
        public GameObject[] gold;
      //public GameObject[] magnets;

        [Header("Positions")]
        public Vector3[] obstaclePosition;
        public Vector3[] goldPosition;

        [Header("Times")]
        public float goldStartDelay = 2f;
        public float goldRepeatRate = 1f;
        public float obstacleStartDelay = 2f;
        public float obstacleRepeatRate = 2f;
       // public float magnetStartDelay = 2f;
       // public float magnetRepatRate = 30f;

        [Header("Counts")]
        public int obstacleCount;
        public int goldCount;
      // public int magnetCount;

        [Header("Lists")]
        public List<GameObject> goldList;
        public List<GameObject> obstacleList;
       // public List<GameObject> magnetList;

        int obsIndex = 0;
        int goldIndex = 0;
      //int magnetIndex = 0;
        void Start()
        {
            playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

            goldList = new List<GameObject>();
            obstacleList = new List<GameObject>();
          //magnetList = new List<GameObject>();

            Spawner(gold, goldCount, goldList);
            Spawner(obstacles, obstacleCount, obstacleList);
          //Spawner(magnets, magnetCount, magnetList);

            InvokeRepeating("ObstacleActivate", obstacleStartDelay, obstacleRepeatRate);
            InvokeRepeating("GoldActivate", goldStartDelay, goldRepeatRate);
          //InvokeRepeating("MagnetActivate", magnetStartDelay, magnetRepatRate);
        }
        void Spawner(GameObject[] _gameObject, int count, List<GameObject> list)
        {
            for (int i = 0; i < count; i++)
            {   
                int objIndex = Random.Range(0, _gameObject.Length);
                GameObject newObj = Instantiate(_gameObject[objIndex]);
                newObj.SetActive(false);
                list.Add(newObj);
            }
        }
        void ObjectActivate(List<GameObject> listName, Vector3[] objectPosition, int indexName)
        {
            if (playerControllerScript.startGame == true)
            {
                if (playerControllerScript.gameOver == false)
                {
                    if (indexName < listName.Count)
                    {
                        listName[indexName].SetActive(true);
                        int posIndex = Random.Range(0, objectPosition.Length);
                        Vector3 obsPos = objectPosition[posIndex];
                        listName[indexName].transform.position = obsPos;
                    }
                }
            }
        }
        void ObstacleActivate()
        {
            ObjectActivate(obstacleList, obstaclePosition, obsIndex);
            obsIndex++;
            if (obsIndex == obstacleList.Count)
            {
                obsIndex = 0;
            }
        }
        void GoldActivate()
        {
            ObjectActivate(goldList, goldPosition, goldIndex);
            goldIndex++;
            if (goldIndex == goldList.Count)
            {
                goldIndex = 0;
            }
        }
        //void MagnetActivate()
        //{
        //    ObjectActivate(magnetList, goldPosition, magnetIndex);
        //    magnetIndex++;
        //    if (magnetIndex == magnetList.Count)
        //    {
        //        magnetIndex = 0;
        //    }
        //}
    }
}

