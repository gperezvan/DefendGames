using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public string idleState, walkState, runState, jumpState, throwState, dieState;
    bool isIdle, isWalking, isJumping, isRunning, isDead, forward, backward, left, right;
    Animator m_Animator;
    public AudioClip jumpClip, throwClip;
    
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        isIdle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!isRunning)
            {
                isWalking = true;
                isIdle = false;
                forward = true;
                m_Animator.SetBool(idleState, false);
                m_Animator.SetBool(walkState, true);
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (!isRunning)
            {
                isWalking = true;
                isIdle = false;
                left = true;
                m_Animator.SetBool(idleState, false);
                m_Animator.SetBool(walkState, true);
            }
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!isRunning)
            {
                isWalking = true;
                isIdle = false;
                backward = true;
                m_Animator.SetBool(idleState, false);
                m_Animator.SetBool(walkState, true);
            }
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (!isRunning)
            {
                isWalking = true;
                isIdle = false;
                right = true;
                m_Animator.SetBool(idleState, false);
                m_Animator.SetBool(walkState, true);
            }
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            forward = false;
            if (!left && !right && !backward)
            {
                StopMotion();
            }
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            left = false;
            if (!forward && !right && !backward)
            {
                StopMotion();
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            backward = false;
            if (!left && !right && !forward)
            {
                StopMotion();
            }
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            right = false;
            if (!left && !forward && !backward)
            {
                StopMotion();
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (isWalking)
            {
                isRunning = true;
                m_Animator.SetBool(runState, true);
                m_Animator.SetBool(walkState, false);
            }
        }  
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (isRunning && isWalking)
            {
                isRunning = false;
                m_Animator.SetBool(walkState, true);
                m_Animator.SetBool(runState, false);
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.E)|| Input.GetMouseButtonDown(0))
        {
            Throw();
        }
        if (PlayerManager.livesRemaining == 0)
        {
            m_Animator.Play("CM_Die");
        }
    }

    void StopMotion()
    {
        isWalking = false;
        isRunning = false;
        isIdle = true;
        m_Animator.SetBool(idleState, true);
        m_Animator.SetBool(walkState, false);
        m_Animator.SetBool(runState, false);
    }

    void Jump()
    {
        m_Animator.SetBool(jumpState, true);
        m_Animator.SetBool(idleState, false);
        m_Animator.SetBool(walkState, false);
        m_Animator.SetBool(runState, false);
        StartCoroutine(ConsumeJump());
    }

    void Throw()
    {
        m_Animator.SetBool(throwState, true);
        m_Animator.SetBool(idleState, false);
        m_Animator.SetBool(walkState, false);
        m_Animator.SetBool(runState, false);
        StartCoroutine(ConsumeThrow());
    }

    IEnumerator ConsumeJump()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(jumpClip);
        yield return new WaitForSeconds(0.66f);//Se esperará 66 segundos que es lo que dura la animación antes de hacer cualquier cosa

       

        m_Animator.SetBool(jumpState, false);
        ReturnMoveState();
    }

    IEnumerator ConsumeThrow()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(throwClip);
        yield return new WaitForSeconds(0.66f);
       
        m_Animator.SetBool(throwState, false);
        ReturnMoveState();
    }

    void ReturnMoveState()
    {
        if (isRunning)
        {
            m_Animator.SetBool(runState, true);
        }
        else if (isWalking)
        {
            m_Animator.SetBool(walkState, true);
        }
        else if (isIdle)
        {
            m_Animator.SetBool(idleState, true);
        }
    }
}
