using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.SceneManagement;

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
    public GameObject tapTo;
    public GameObject nextButton;
    public GameObject retryButton;
    public int kat;
    public Collider[] ragDollCol;
    public Rigidbody[] ragdollRb;
    public Collider playerCol;
    public bool ragdollCheck = false;
    public CinemachineVirtualCamera cmCam;
    public Transform ragdollTransform;
    public BalloonDestroyer bd;
    Rigidbody rb;
    bool fly;
    bool finish = false;
    bool start = false;


    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        mainballoon = GetComponent<BalloonDestroyer>().mainballoon;


        DisableRagdoll();
    }

    private void Update()
    {
        SwerveControl();
        

        if (fly && !finish)
        {
            rb.AddForce(new Vector3(0, GetComponent<BalloonDestroyer>().seviye / 1.1f, 0));
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
            
            if(tapTo != null)
            {
                Destroy(tapTo);
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (!finish)
            {
                CallSwerve();
            }
            
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
            transform.DOLocalRotate(new Vector3(0, 20, 0), 0.2f);
        }
        else if (m_deltaPos.x < 0)
        {
            transform.DOLocalRotate(new Vector3(0, -20, 0), 0.2f);
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "finish")
        {
            Debug.Log("finish");

            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            finish = true;

            anim.SetBool("Fly", false);
            anim.SetBool("Bump", true);
            nextButton.SetActive(true);
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
                mainballoon.GetComponent<Collider>().isTrigger = true;
            }

            anim.SetBool("Run", false);
            anim.SetBool("Fly", true);
        }
        if (collision.gameObject.tag == "kat")
        {
            kat++;
        }
        if (collision.gameObject.tag == "fail")
        {
            cmCam.Follow = ragdollTransform;
            cmCam.LookAt = ragdollTransform;
            retryButton.SetActive(true);
            ActivateRagdoll();
        }
    }

    public void DisableRagdoll()
    {
        ragdollRb = GetComponentsInChildren<Rigidbody>();
        ragDollCol = GetComponentsInChildren<Collider>();
        for (var i = ragDollCol.Length - 1; i > 1; i--)
        {
            ragDollCol[i].enabled = false;
            ragdollRb[i].isKinematic = true;
        }
    }
    public void ActivateRagdoll()
    {
        Debug.Log("Ragdoll");
        for (var i = ragDollCol.Length - 1; i > 1; i--)
        {
            ragDollCol[i].enabled = true;
            ragdollRb[i].isKinematic = false;
            ragdollRb[i].mass = 1;
            ragdollRb[i].useGravity = true;
            playerCol.isTrigger = true;
            anim.enabled = false;
        }
        if (finish)
        {
            enabled = false;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
