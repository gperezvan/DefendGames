using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeetleNPC : MonoBehaviour
{
    Animator m_animator;
    public GameObject nextCarrotToDestroy;
    public bool cherryHit = false;
    public float smoothTime = 3f; //tiempo en hacer el ataque
    public Vector3 smoothVelocity = Vector3.zero;
    public bool hasReachThePlayer = false;
    public HealthManager healthManager;
    public BeetlePatrol beetle1;
    bool touch; //2n touch and destroy beetle

    public static BeetleManager1 manager;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        healthManager = GameObject.Find("Health_Slider").GetComponent<HealthManager>();
        if (manager == null)
        {
            manager = GameObject.Find("Beetles_Left_Value").GetComponent<BeetleManager1>();

        }
        manager.RecalculateBeetles();
        touch = false;
    }

    private void OnCollisionEnter(Collision collision) //cuando no se pueden atravesar, hay una colisión
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hasReachThePlayer = true;
            healthManager.ReduceHealth();


            if (!cherryHit) //nos ataca pk lo hemos tocado
            {
                
                GameObject thePlayer = collision.gameObject;
                Transform trans = thePlayer.transform;
                this.gameObject.transform.LookAt(trans); //mirar al jugador al tocarlo
                BeetlePatrol.isAttacking = true;
                m_animator.Play("Attack_OnGround");
                StartCoroutine(DestroyBeetle());
                manager.RecalculateBeetles();
            }
            else //nos ataca pk ha recibido golpe cereza
            {
                
                m_animator.Play("Attack_Standing");
                StartCoroutine(DestroyBeetleStandng());
                manager.RecalculateBeetles();
            }
           
        }
        if (collision.gameObject.CompareTag("Cherry"))
        {
            if (touch)
            {
                BeetlePatrol.isDead = true;
                StartCoroutine(DestroyBeetle());
            }
            touch = true;
            PointsManager.currentPoints += 50;
            BeetlePatrol.isAttacking = true;
            cherryHit = true;
            m_animator.Play("Stand");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Carrot"))
        {
            nextCarrotToDestroy = other.gameObject;
            beetle1.ChangeIsEating(beetle1);
            m_animator.Play("Eat_OnGround");
            StartCoroutine(DestroyCarrot());
        }

        if (other.gameObject.CompareTag("Water"))
        {
            beetle1.ChangeIsWater(beetle1);
            StartCoroutine(DestroyBeetle());
        }

    }
    IEnumerator DestroyCarrot()
    {
        yield return new WaitForSeconds(3.0f);
        Destroy(nextCarrotToDestroy.gameObject);
        beetle1.ChangeIsEating(beetle1);
        
    }

    IEnumerator DestroyBeetle()
    {
        yield return new WaitForSecondsRealtime(4.0f);//si se relantiza el tiemo, esta función va independiente del tiempo externo del jugo
        m_animator.Play("Die_OnGround");
        Destroy(this.gameObject, 2f);
        hasReachThePlayer = false;
        manager.RecalculateBeetles();
    }
    IEnumerator DestroyBeetleStandng()
    {
        yield return new WaitForSecondsRealtime(4.0f);//si se relantiza el tiemo, esta función va independiente del tiempo externo del jugo
        m_animator.Play("Die_Standing");
        Destroy(this.gameObject, 2f);
        cherryHit = false;
        hasReachThePlayer = false;
        manager.RecalculateBeetles();
    }
    private void Update()
    {
        if (cherryHit)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Transform transPlayer = player.transform;
            this.gameObject.transform.LookAt(transPlayer);
            if (!hasReachThePlayer)
            {
                m_animator.Play("Run_Standing");
            }
            
            transform.position = Vector3.SmoothDamp(transform.position,
                                                    transPlayer.position,
                                                    ref smoothVelocity, smoothTime);

        }
    }
}
