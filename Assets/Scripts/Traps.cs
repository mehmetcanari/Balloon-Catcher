using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traps : MonoBehaviour
{
    Vector3 boyut;
    int pop;
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
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "trap")
        {
            gameObject.SetActive(false);
            transform.localScale = boyut;
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
}
