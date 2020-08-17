using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    // Start is called before the first frame update
  public static CameraFollower sharedInstance;

    public GameObject followTarget;

    public float movementSmoothness = 1.0f;
    public float rotationSmoothness = 1.0f; //1 por segundo

    public bool canFollow = true; //si el personaje está muerto no sigue el personaje.

    private void Awake()
    {
        sharedInstance = this;
    }

    private void LateUpdate() //lo último que se ejecuta en el último Frame
    {
        if(followTarget == null || canFollow == false){
            return;
        }

        transform.position = Vector3.Lerp(transform.position, followTarget.transform.position, Time.deltaTime * movementSmoothness);//factor de suavidad en función de los frames. Lerp= interpolacion en forma de línea

        transform.rotation = Quaternion.Slerp(transform.rotation, followTarget.transform.rotation, Time.deltaTime * rotationSmoothness);//Slerp= interpolacion en forma de esfera
    }
}
