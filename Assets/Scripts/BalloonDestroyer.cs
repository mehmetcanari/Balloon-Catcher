using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BalloonDestroyer : MonoBehaviour
{
    public GameObject mainballoon;
    public float balonscale;
    public int seviye;
    bool blue = false;
    bool purple = true;
    bool green = false;
    public Material blueMat;
    public Material greenMat;
    public Material purpleMat;
    public GameObject plusOne;
    public GameObject minusOne;
    public ParticleSystem particle;
    public ParticleSystem greenPuff;
    public ParticleSystem bluePuff;
    public ParticleSystem purplePuff;
    public Traps tr;
    Vector3 numberSpawn;

    private void Update()
    {
        numberSpawn = new Vector3(transform.position.x + 1, transform.position.y + 2, transform.position.z);
        if (!mainballoon.gameObject.activeSelf)
        {
            seviye = 0;
        }

        if (seviye < 0)
        {
            seviye = 0; // Seviye eksiye düşemez
        }
    }


    private void OnTriggerEnter(Collider collision)
    {
        #region triggers
        if (collision.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "blueTrigger")
        {
            blue = true;
            green = false;
            purple = false;
            mainballoon.GetComponent<MeshRenderer>().material = blueMat;
            if (mainballoon.gameObject.activeSelf)
            Instantiate(bluePuff, mainballoon.transform.position, Quaternion.identity);
        }

        if (collision.gameObject.tag == "greenTrigger")
        {
            green = true;
            blue = false;
            purple = false;
            mainballoon.GetComponent<MeshRenderer>().material = greenMat;
            if (mainballoon.gameObject.activeSelf)
            Instantiate(greenPuff, mainballoon.transform.position, Quaternion.identity);
        }
        if (collision.gameObject.tag == "purpleTrigger")
        {
            green = false;
            blue = false;
            purple = true;
            mainballoon.GetComponent<MeshRenderer>().material = purpleMat;
            if (mainballoon.gameObject.activeSelf)
            Instantiate(purplePuff, mainballoon.transform.position, Quaternion.identity);
        }
        #endregion

        #region Green Index
        if (green)
        {
            if (collision.gameObject.tag == "green")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye++;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x + balonscale, mainballoon.transform.localScale.y + balonscale, mainballoon.transform.localScale.z + balonscale), 0.1f);
                    mainballoon.transform.position += new Vector3(0, 0.05f, 0);
                    Instantiate(plusOne, numberSpawn, Quaternion.identity);
                }
                Instantiate(particle, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                mainballoon.SetActive(true);
            }

            if (collision.gameObject.tag == "purple")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye--;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                    mainballoon.transform.position -= new Vector3(0, 0.05f, 0);
                    Instantiate(minusOne, numberSpawn, Quaternion.identity);
                }
                Instantiate(particle, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);

            }

            if (collision.gameObject.tag == "blue")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye--;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                    mainballoon.transform.position -= new Vector3(0, 0.05f, 0);
                    Instantiate(minusOne, numberSpawn, Quaternion.identity);
                }
                Instantiate(particle, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }
        }
        #endregion

        #region Blue Index
        if (blue)
        {
            if (collision.gameObject.tag == "green")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye--;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                    mainballoon.transform.position -= new Vector3(0, 0.05f, 0);
                    Instantiate(minusOne, numberSpawn, Quaternion.identity);
                }
                Instantiate(particle, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag == "purple")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye--;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                    mainballoon.transform.position -= new Vector3(0, 0.05f, 0);
                    Instantiate(minusOne, numberSpawn, Quaternion.identity);
                }
                Instantiate(particle, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag == "blue")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye++;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x + balonscale, mainballoon.transform.localScale.y + balonscale, mainballoon.transform.localScale.z + balonscale), 0.1f);
                    mainballoon.transform.position += new Vector3(0, 0.05f, 0);
                    Instantiate(plusOne, numberSpawn, Quaternion.identity);
                }
                Instantiate(particle, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                mainballoon.SetActive(true);
            }
            if (collision.gameObject.tag == "trap")
            {
                Debug.Log("TRAP");
                tr.BalloonPop();
            }
        }
        #endregion

        #region Purple Index
        if (purple)
        {
            if (collision.gameObject.tag == "green")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye--;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                    mainballoon.transform.position -= new Vector3(0, 0.05f, 0);
                    Instantiate(minusOne, numberSpawn, Quaternion.identity);
                }
                Instantiate(particle, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.tag == "purple")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye++;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x + balonscale, mainballoon.transform.localScale.y + balonscale, mainballoon.transform.localScale.z + balonscale), 0.1f);
                    mainballoon.transform.position += new Vector3(0, 0.05f, 0);
                    Instantiate(plusOne, numberSpawn, Quaternion.identity);
                }
                Instantiate(particle, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
                mainballoon.SetActive(true);
            }

            if (collision.gameObject.tag == "blue")
            {
                if (mainballoon.gameObject.activeSelf)
                {

                    seviye--;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                    mainballoon.transform.position -= new Vector3(0, 0.05f, 0);
                    Instantiate(minusOne, numberSpawn, Quaternion.identity);
                }
                Instantiate(particle, collision.gameObject.transform.position, Quaternion.identity);
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.tag == "trap")
            {
                Debug.Log("TRAP");
                tr.BalloonPop();
            }
        }
        #endregion
    }
}
