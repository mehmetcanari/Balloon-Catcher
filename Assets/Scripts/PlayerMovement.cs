using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Range(-7.5f, 7.5f)]
    [SerializeField]
    private float xClamp = 0;
    public Vector2 m_startPos;
    public Vector2 m_deltaPos;
    public State currentState;
    public int kat;
    public float moveSmoother = 50;
    private float moveSpeedX = 10;
    public float moveSpeedZ = 10;
    public Rigidbody rb;
    public Animator anim;
    private GameObject mainballoon;
    public GameObject player;
    private bool finish = false;
    private bool start = false;
    private bool fly;
    private bool isStarted = false;


    public enum State
    {
        Start,
        Win,
        Lose,
        Idle,
    }

    private void Start()
    {
        mainballoon = GetComponent<BalloonDestroyer>().mainballoon;
        currentState = State.Idle;
    }

    private void Update()
    {
        #region State Behaviors
        if (currentState == State.Lose)
        {
            anim.SetBool("Run", false);
            isStarted = false;
        }

        if (currentState == State.Start)
        {
            anim.SetBool("Run", true);
            transform.Translate(Vector3.forward * moveSpeedZ * Time.deltaTime, Space.World);

            SwerveControl();
        }

        if (currentState == State.Idle)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isStarted = true;
            }
        }
        #endregion

        if (fly)
        {
            rb.AddForce(new Vector3(0, GetComponent<BalloonDestroyer>().seviye * 1.6f, 0));
        }

        #region Calling Functions
        SwerveControl();
        #endregion
    }

    
    #region Collision / Trigger
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "finish")
        {
            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            anim.SetBool("Fly", false);
            anim.SetBool("Bump", true);
            finish = true;
        }

        if (collision.gameObject.tag == "poptrapStick")
        {
            currentState = State.Lose;
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "fly")
        {
            if (mainballoon.gameObject.activeSelf)
            {
                rb.useGravity = false;
                fly = true;
            }

            anim.SetBool("Run", false);
            anim.SetBool("Fly", true);
        }
        if (collision.gameObject.tag == "kat")
        {
            kat++;
        }
    }

    #endregion


    #region Methods
    public void SwerveControl()
    {
        if (isStarted == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                m_startPos = Input.mousePosition;
                currentState = State.Start;
            }

            if (Input.GetMouseButton(0))
            {
                m_deltaPos = (Vector2)Input.mousePosition - m_startPos;
                m_deltaPos.y = 0;
                transform.position = new Vector3(Mathf.Lerp(transform.position.x, transform.position.x + (m_deltaPos.x / Screen.width) * moveSmoother, moveSpeedX), transform.position.y, transform.position.z);
                m_startPos = Input.mousePosition;

                xClamp = Mathf.Clamp(transform.position.x, -3f, 3f);
                transform.position = new Vector3(xClamp, transform.position.y, transform.position.z);
            }
        }
    }
    #endregion
}
