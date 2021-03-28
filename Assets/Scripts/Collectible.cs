using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private float rotSpeed = 1;
   // public ParticleSystem diamondParticle;
    

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotSpeed));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "baloncu")
        {
            Destroy(transform.gameObject);
            //Instantiate(diamondParticle, gameObject.transform.position, Quaternion.identity);
        }
    }
}
