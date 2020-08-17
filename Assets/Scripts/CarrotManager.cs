using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarrotManager : MonoBehaviour
{
    private string m_tag = "Carrot";
    public static int currentCarrotsCount = 1;
    Text carrotTextCount;

    public GameObject[] carrots;



    void Awake()
    {
        carrotTextCount = GetComponent<Text>();
        currentCarrotsCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        carrots = GameObject.FindGameObjectsWithTag(m_tag);
        currentCarrotsCount = carrots.Length;
        carrotTextCount.text = currentCarrotsCount.ToString();

    }
}
