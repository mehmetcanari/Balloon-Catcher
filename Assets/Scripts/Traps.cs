using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Traps : MonoBehaviour
{
    Vector3 boyut;
    private int pop;
    public ParticleSystem balloonPop;

    private void Start()
    {
        boyut = transform.localScale;
    }
    private void Update()
    {
        if (pop == 2)
        {
            Instantiate(balloonPop, transform.position, Quaternion.identity);
            transform.localScale = boyut;
            pop = 0;
            gameObject.SetActive(false);
        }
        if (transform.localScale.x < 90)
        {
            transform.localScale = boyut;
            Instantiate(balloonPop, transform.position, Quaternion.identity);
            gameObject.SetActive(false);
        }
    }

    #region Triggers
    private void OnTriggerEnter(Collider collision)
    {
            Debug.Log(collision.gameObject);
        if (collision.gameObject.tag == "trap")
        {
            BaloonPop();
        }
        if (collision.gameObject.tag == "trapdetect")
        {
            collision.gameObject.transform.Find("trap").GetComponent<TrapMovement>().Forth();
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

    public void BaloonPop()
    {
        gameObject.SetActive(false);
        transform.localScale = boyut;
        Instantiate(balloonPop, transform.position, Quaternion.identity);
    }
    #endregion
}
