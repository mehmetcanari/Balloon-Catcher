using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

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
    public ParticleSystem balloonPop;
    private float moveSpeedX = 10;
    [Range(10, 100)]
    public float moveSmoother = 50;
    public GameObject mainballoon;
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
            rb.AddForce(new Vector3(0, GetComponent<BalloonDestroyer>().seviye / 1.2f, 0));
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

        if (Input.GetMouseButtonUp(0))
        {
            m_deltaPos = Vector3.zero;
            transform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f);
        }
    }

    public void CallSwerve()
    {
        m_deltaPos = (Vector2)Input.mousePosition - m_startPos;
        m_deltaPos.y = 0;
        transform.position = new Vector3(Mathf.Lerp(transform.position.x, transform.position.x + (m_deltaPos.x / Screen.width) * moveSmoother, moveSpeedX), transform.position.y, transform.position.z);
        m_startPos = Input.mousePosition;

        if (m_deltaPos.x > 0)
        {
            transform.DOLocalRotate(new Vector3(0, 45, 0), 0.2f);
        }
        else if (m_deltaPos.x < 0)
        {
            transform.DOLocalRotate(new Vector3(0, -45, 0), 0.2f);
        }

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
