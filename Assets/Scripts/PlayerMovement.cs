using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [Header("Movement")]
    public Animator anim;
    public float moveSpeedZ = 10;
    [Range(-7.5f, 7.5f)]
    [SerializeField]
    public Vector2 m_startPos;
    public Vector2 m_deltaPos;
    public ParticleSystem balloonPop;
    [Range(10, 100)]
    public GameObject mainballoon;
    public GameObject tapTo;
    public GameObject nextButton;
    public GameObject retryButton;
    public Collider[] ragDollCol;
    private Rigidbody rb;
    public Rigidbody[] ragdollRb;
    public Collider playerCol;
    public CinemachineVirtualCamera cmCam;
    public Transform ragdollTransform;
    public BalloonDestroyer bd;
    public TextMeshProUGUI tmpro;
    public GameObject scoreImage;
    public int kat;
    private float moveSpeedX = 10;
    public float moveSmoother = 50;
    private float xClamp = 0;
    private float scoreCount = 0;
    bool fly;
    bool finish = false;
    bool start = false;
    public bool ragdollCheck = false;
    #endregion

    private void Start()
    {
        #region Start Parameters
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        mainballoon = GetComponent<BalloonDestroyer>().mainballoon;
        DisableRagdoll();
        #endregion
    }

    private void Update()
    {
        #region Finish Arguments
        if (!fly)
        {
            SwerveControl();
        }
        
        if (start && !finish)
        {
            transform.Translate(Vector3.forward * moveSpeedZ * Time.deltaTime, Space.World);
        }

        //Debug.Log(bd.seviye);

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        #endregion
    }

    private void FixedUpdate()
    {
        if (fly && !finish)
        {
            rb.AddForce(new Vector3(0, bd.seviye * 1.5f, 0), ForceMode.Force);
        }
    }

    #region Swerve
    public void SwerveControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            m_startPos = Input.mousePosition;
            anim.SetBool("Run", true);
            start = true;

            if (start)
            {
                tapTo.gameObject.transform.DOScale(new Vector3(0, 0, tapTo.gameObject.transform.localScale.z), 0.2f);
            }

        }

        if (Input.GetMouseButton(0))
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

            xClamp = Mathf.Clamp(transform.position.x, -2f, 2f);
            transform.position = new Vector3(xClamp, transform.position.y, transform.position.z);
        }

        if (Input.GetMouseButtonUp(0))
        {
            m_deltaPos = Vector3.zero;
            transform.DOLocalRotate(new Vector3(0, 0, 0), 0.2f);
        }
    }
    #endregion

    #region Colliders / Triggers
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "finish")
        {
            //Debug.Log("finish");

            rb.isKinematic = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            finish = true;

            anim.SetBool("Fly", false);
            anim.SetBool("Bump", true);
            nextButton.SetActive(true);
            nextButton.gameObject.transform.DOScale(new Vector3(3.5f, nextButton.transform.localScale.y, nextButton.transform.localScale.z), 0.4f);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "fly")
        {
            if (bd.seviye < 5)
            {
                bd.seviye = -5;
            }

            if (mainballoon.gameObject.activeSelf)
            {
                rb.useGravity = false;
                fly = true;
                mainballoon.GetComponent<Collider>().isTrigger = true;
            }

            else if (mainballoon.gameObject.activeSelf == false)
            {
                //Debug.Log("Elinde balon yok");
                fly = true;
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
            retryButton.gameObject.transform.DOScale(new Vector3(3.5f, retryButton.transform.localScale.y, retryButton.transform.localScale.z), 0.4f);
            ActivateRagdoll();
        }

        if (collision.gameObject.tag == "collectible")
        {
            scoreCount++;
            //Debug.Log(scoreCount);
            tmpro.text = "" + scoreCount;
            ScoreScale();
            ScoreImageScale();
        }
    }
    #endregion

    #region Methods
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
        //Debug.Log("Ragdoll");
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

    public void ScoreScale()
    {
        tmpro.gameObject.transform.DOScale(new Vector3(x: 0.4f, y: 0.4f, z: 0.4f), 0.2f);
        tmpro.gameObject.transform.DOScale(new Vector3(x: 0.2f, y: 0.2f, z: 0.2f), 0.4f);
    }

    public void ScoreImageScale()
    {
        scoreImage.gameObject.transform.DOScale(new Vector3(x: 10f, y: 10f, z: 10f), 0.2f);
        scoreImage.gameObject.transform.DOScale(new Vector3(x: 5.5f, y: 5.5f, z: 5.5f), 0.4f);
    }
    #endregion 
}
