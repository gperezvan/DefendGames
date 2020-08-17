using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetlePatrol : MonoBehaviour
{
    public static bool isDead = false;
    public  bool isEating = false;
    public static bool isAttacking = false;
    public bool isWater = false;

    public float speed = 5f;
    public float directionChangeInterval = 1f; //cada segundo cambiará la dirección
    public float maxHeadingChange = 10f; //10 grados serán los que podrá cambiar el bicho

    Animator beetleAnimator;

    CharacterController controller;
    float heading; //dirección
    Vector3 targetRotation;

    private void Start()
    {
        beetleAnimator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        heading = Random.Range(0, 360);//cada bicho tendrá su dirección inicial aleatoria
        transform.eulerAngles = new Vector3(0, heading, 0);

        StartCoroutine(NewHeading());
    }
    private void Update()
    {
        if (!isDead && !isEating && !isAttacking && !isWater)
        {
            transform.eulerAngles = Vector3.Slerp(transform.eulerAngles, targetRotation, Time.deltaTime * directionChangeInterval);
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            controller.SimpleMove(forward * speed);
        }
    }
    IEnumerator NewHeading() //se encarga de hacer la rutina de girar
    {
        while (true) //en un void no sería válido, pero en una coroutine sí es permitido
        {
            NewHeadingRoutine();
            yield return new WaitForSeconds(directionChangeInterval);//esperará el segndo. Y lo hará siempre hasta que el objeto sea destruido
        }
    }

    void NewHeadingRoutine()//girar escarabajo
    {
        float floor = transform.eulerAngles.y - maxHeadingChange;
        float ceil = transform.eulerAngles.y + maxHeadingChange;
        heading = Random.Range(floor, ceil);
        targetRotation = new Vector3(0, heading, 0); //no giramos completamente aquí pk el cambio sería muy brisco, lo hacemos en el Update
    }

    public void ChangeIsWater(BeetlePatrol beetle1)
    {
        beetle1.isWater = !isWater;
    }
    public void ChangeIsEating(BeetlePatrol beetle1)
    {
        beetle1.isEating = !isEating;
    }



}
