using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CherryManager : MonoBehaviour
{
    

    Text cheeriesTextCount;





    void Start()
    {
        cheeriesTextCount = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        cheeriesTextCount.text = PlayerManager.currentCherryCount.ToString();

    }
}
