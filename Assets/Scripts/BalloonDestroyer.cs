using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BalloonDestroyer : MonoBehaviour
{
    public PlayerMovement pb;
    private Vector3 mainbaloonNewPos;
    public BaloonTypes baloonTypes;
    public float balonscale;
    public float balonscalePlus;
    public int seviye;
    bool blue = false;
    bool purple = true;
    bool green = false;
    private bool stopCountNegative = false;
    public GameObject mainballoon;
    public Material redMat;
    public Material greenMat;
    public Material purpleMat;

    public enum BaloonTypes
    {
        blue,
        green,
        purple
    }

    private void Start()
    {
        baloonTypes = BaloonTypes.purple;
    }

    private void Update()
    {
        if (!mainballoon.gameObject.activeSelf)
        {
            seviye = 0;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        #region Color Triggers
        if (collision.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }

        else if (collision.gameObject.tag == "blueTrigger")
        {
            baloonTypes = BaloonTypes.blue;
            blue = true;
            green = false;
            purple = false;
            stopCountNegative = false;
            mainballoon.GetComponent<MeshRenderer>().material = redMat;
        }

        else if (collision.gameObject.tag == "greenTrigger")
        {
            stopCountNegative = false;
            baloonTypes = BaloonTypes.green;
            green = true;
            blue = false;
            purple = false;
            mainballoon.GetComponent<MeshRenderer>().material = greenMat;
        }

        else if (collision.gameObject.tag == "purpleTrigger")
        {
            stopCountNegative = false;
            baloonTypes = BaloonTypes.purple;
            green = false;
            blue = false;
            purple = true;
            mainballoon.GetComponent<MeshRenderer>().material = purpleMat;
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
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x + (balonscale + balonscalePlus), mainballoon.transform.localScale.y + (balonscale + balonscalePlus), mainballoon.transform.localScale.z + (balonscale + balonscalePlus)), 0.1f);
                    mainballoon.transform.position += new Vector3(0, 0.05f, 0); // Balonlar toplandığında hem büyür hemde Y değeri artar
                }
                mainballoon.SetActive(true);
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.tag == "purple")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye--;

                    if (mainballoon.transform.localScale == Vector3.zero)
                    {
                        Debug.Log("0 oldu");
                        stopCountNegative = true;
                    }

                    if (!stopCountNegative)
                    {
                        mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                        mainballoon.transform.position -= new Vector3(0, 0.05f, 0); //Farklı balonu toplarsa hem küçülür hemde Y değeri azalır.
                    }
                }

                Destroy(collision.gameObject);

            }
            if (collision.gameObject.tag == "blue")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye--;
                    
                    if (mainballoon.transform.localScale == Vector3.zero)
                    {
                        Debug.Log("0 oldu");
                        stopCountNegative = true;
                    }

                    if (!stopCountNegative)
                    {
                        mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                        mainballoon.transform.position -= new Vector3(0, 0.05f, 0);
                    }
                }
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
                    if (mainballoon.transform.localScale == Vector3.zero)
                    {
                        Debug.Log("0 oldu");
                        stopCountNegative = true;
                    }
                    if (!stopCountNegative)
                    {
                        mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                        mainballoon.transform.position -= new Vector3(0, 0.05f, 0);
                    }
                }
                Destroy(collision.gameObject);
            }
            
            else if (collision.gameObject.tag == "purple")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye--;

                    if (mainballoon.transform.localScale == Vector3.zero)
                    {
                        Debug.Log("0 oldu");
                        stopCountNegative = true;
                    }

                    if (!stopCountNegative)
                    {
                        mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                        mainballoon.transform.position -= new Vector3(0, 0.05f, 0);
                    }
                }
                Destroy(collision.gameObject);
            }
            
            else if (collision.gameObject.tag == "blue")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye++;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x + (balonscale + balonscalePlus), mainballoon.transform.localScale.y + (balonscale + balonscalePlus), mainballoon.transform.localScale.z + (balonscale + balonscalePlus)), 0.1f);
                    mainballoon.transform.position += new Vector3(0, 0.05f, 0);
                }
                Destroy(collision.gameObject);
                mainballoon.SetActive(true);

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

                    if (mainballoon.transform.localScale == Vector3.zero)
                    {
                        Debug.Log("0 oldu");
                        stopCountNegative = true;
                    }
                    
                    if (!stopCountNegative)
                    {
                        mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                        mainballoon.transform.position -= new Vector3(0, 0.05f, 0);
                    }
                }
                Destroy(collision.gameObject);
            }

            else if (collision.gameObject.tag == "purple")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye++;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x + (balonscale + balonscalePlus), mainballoon.transform.localScale.y + (balonscale + balonscalePlus), mainballoon.transform.localScale.z + (balonscale + balonscalePlus)), 0.1f);
                    mainballoon.transform.position += new Vector3(0, 0.05f, 0);
                }
                Destroy(collision.gameObject);
            }

            else if (collision.gameObject.tag == "blue")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye--;

                    if (mainballoon.transform.localScale == Vector3.zero)
                    {
                        Debug.Log("0 oldu");
                        stopCountNegative = true;
                    }

                    if (!stopCountNegative)
                    {
                        mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                        mainballoon.transform.position -= new Vector3(0, 0.05f, 0);
                    }
                }
                Destroy(collision.gameObject);
            }

            else if (collision.gameObject.tag == "trap")
            {
                pb.mainballoon.transform.DOScale(Vector3.zero, 0.1f);
                Instantiate(pb.balloonPop, pb.mainballoon.transform.position, Quaternion.identity);
            }
        }
        #endregion
    }
}
