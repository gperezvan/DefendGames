using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryControl : MonoBehaviour
{
    public Rigidbody cherryRb;
    public float throwDistance = 1000f;
    public float timeToDestroy = 4f;

    private GameObject player;

    public MeshRenderer cherry;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cherry = GetComponent < MeshRenderer >();
        cherry.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            if (PlayerManager.currentCherryCount > 0)
            {
                ThrowCherry();
            }
        }
    }

    void ThrowCherry()
    {
        cherry.enabled = true;
        StartCoroutine (InstantiateCherry());
        

    }

    IEnumerator InstantiateCherry()
    {

        yield return new WaitForSecondsRealtime(0.8f);
        cherry.enabled = false;
        Rigidbody cherryClone = (Rigidbody)Instantiate(cherryRb, transform.position, transform.rotation);


        cherryClone.useGravity = true;
        cherryClone.constraints = RigidbodyConstraints.None;
        cherryClone.AddForce(player.transform.forward * throwDistance);
        Destroy(cherryClone.gameObject, timeToDestroy);

        PlayerManager.currentCherryCount -= 1;

    }

}
