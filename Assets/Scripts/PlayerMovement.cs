using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public Animator anim;
    public float moveSpeedZ = 10;
    [Range(-7.5f, 7.5f)]
    [SerializeField]
    private float xClamp = 0;
    public Vector2 m_startPos;
    public Vector2 m_deltaPos;
    private float moveSpeedX = 10;
    [Range(50, 100)]
    public float moveSmoother = 50;
    GameObject mainballoon;
    public int kat;

    Rigidbody rb;
    bool fly;
    bool finish = false;
    bool start = false;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        mainballoon = GetComponent<BalloonDestroyer>().mainballoon;
    }

    private void Update()
    {
        SwerveControl();

        if (fly)
        {
            rb.AddForce(new Vector3(0, GetComponent<BalloonDestroyer>().seviye * 1.6f, 0));
        }
        if (start && !finish)
        {
            transform.Translate(Vector3.forward * moveSpeedZ * Time.deltaTime, Space.World);
        }
    }

    public void SwerveControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_startPos = Input.mousePosition;
            anim.SetBool("Run", true);
            start = true;
        }

        if (Input.GetMouseButton(0))
        {
            CallSwerve();

            xClamp = Mathf.Clamp(transform.position.x, -2f, 2f);
            transform.position = new Vector3(xClamp, transform.position.y, transform.position.z);
        }
    }

    public void CallSwerve()
    {
        m_deltaPos = (Vector2)Input.mousePosition - m_startPos;
        m_deltaPos.y = 0;
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, transform.position.x + (m_deltaPos.x / Screen.width) * moveSmoother, moveSpeedX), transform.position.y, transform.position.z);
        m_startPos = Input.mousePosition;
    }
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
}
