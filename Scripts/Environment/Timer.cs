using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameObject Text;
    void Start()
    {
        InvokeRepeating("TextOff", 0,1);
        InvokeRepeating("TextOn", 0.5f,1);
    }
    void TextOn()
    {
        Text.SetActive(false);
    }
    void TextOff()
    {
        Text.SetActive(true);
    }
  
}
