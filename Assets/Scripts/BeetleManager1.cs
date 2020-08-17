using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeetleManager1 : MonoBehaviour
{
    private string m_tag = "Beetle";
    public static int currentBeetlesCount = 0;
    Text beetleTextCount;

    public GameObject[] beetles;



    void Start()
    {
        beetleTextCount = GetComponent<Text>();
        currentBeetlesCount = 1;
        beetles = GameObject.FindGameObjectsWithTag(m_tag);
        RecalculateBeetles();
    }

    // Update is called once per frame

    public void RecalculateBeetles()
    {
        beetles = GameObject.FindGameObjectsWithTag(m_tag);
        currentBeetlesCount = beetles.Length;
        beetleTextCount.text = currentBeetlesCount.ToString();
    }
}
