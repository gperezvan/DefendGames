using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static int currentCherryCount;//es static cuando quieres que sea una única instancia
    public int tempCurrentCherryCount;
    public bool isCollectingCherries;
    public static int livesRemaining;
    public static bool hasDead;

    public Transform[] spawningZomes;
    private void Awake()
    {
        currentCherryCount = 0;
        tempCurrentCherryCount = 0;
        isCollectingCherries = false;
        livesRemaining = 3;
        hasDead = false;
    }
    private void Update()//se ejecuta unas 60 veces/seg
    {
        if (isCollectingCherries)
        {
            if (tempCurrentCherryCount >= 60)//de esta forma cada segundo adquirirá una cereza.
            {
                currentCherryCount += 1;
                PointsManager.currentPoints += 5;
                tempCurrentCherryCount = 0;
            }
            else
            {
                tempCurrentCherryCount += 1;
            }
        }

        if (HealthManager.currentHealth <= 0 && !hasDead)
        {
            hasDead = true;
            livesRemaining--;
            if (livesRemaining == 2)
            {
                Destroy(GameObject.Find("Life 3"));
                GetComponent<Animator>().Play("CM_Die");
                StartCoroutine(RespawnPlayer());
            }
            if (livesRemaining == 1)
            {
                Destroy(GameObject.Find("Life 2"));
                GetComponent<Animator>().Play("CM_Die");
                StartCoroutine(RespawnPlayer());
            }
            if (livesRemaining == 0)
            {
                Destroy(GameObject.Find("Life 1"));
            }
        }
    }
    IEnumerator RespawnPlayer()
    {
        int randomPos = Random.Range(0,spawningZomes.Length);
        yield return new WaitForSecondsRealtime(4f);
        this.transform.position = spawningZomes[randomPos].transform.position;//movemos jugador a zona Spawning aleatoria
        GetComponent<Animator>().Play("CM_Idle");
        hasDead = false;
        HealthManager.currentHealth = 100;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CherryTree"))
        {
            isCollectingCherries = true;
            currentCherryCount += 1;
            PointsManager.currentPoints += 5;
        } 
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("CherryTree"))
        {
            isCollectingCherries = false;
        }
    }
}
