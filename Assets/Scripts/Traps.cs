using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Traps : MonoBehaviour
{
    public PlayerMovement pb;
    public BalloonDestroyer bd;
    Vector3 boyut;
    private int pop;

    private void Start()
    {
        boyut = transform.localScale;
    }
    private void Update()
    {
        if (pop == 2)
        {
            gameObject.SetActive(false);
            pop = 0;
        }
    }

    #region Triggers
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "trap")
        {
            gameObject.transform.DOScale(Vector3.zero, 0.1f);
            Instantiate(pb.balloonPop, pb.mainballoon.transform.position, Quaternion.identity);

            if (pop == 0)
            {
                Debug.Log("Balon patladı");
                bd.seviye = 0; // patladığında seviye sıfırlanır
            }
        }
        if (collision.gameObject.tag == "trapdetect")
        {
            collision.gameObject.transform.Find("trap").GetComponent<TrapMovement>().Fourth();
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "trap2")
        {
            pop++;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "trap2")
        {
            pop--;
        }
    }
    #endregion
}
