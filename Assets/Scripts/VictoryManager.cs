using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    Text victoryText;
    void Start()
    {
        victoryText = GetComponent<Text>();
        victoryText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (BeetleManager1.currentBeetlesCount==0)
        {
            victoryText.text = "YOU WON! \nCongrats!";
        }
        if(CarrotManager.currentCarrotsCount==0 || PlayerManager.livesRemaining == 0)
        {
            victoryText.text = "GAME OVER! \nFeel Sorry!";
        }
    }
}
