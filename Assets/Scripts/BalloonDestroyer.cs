using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BalloonDestroyer : MonoBehaviour
{
    public PlayerMovement pb;
    public BaloonTypes baloonTypes;
    public float balonscale;
    public int seviye;
    bool blue = false;
    bool purple = true;
    bool green = false;
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
            mainballoon.GetComponent<MeshRenderer>().material = redMat;
        }

        else if (collision.gameObject.tag == "greenTrigger")
        {
            baloonTypes = BaloonTypes.green;
            green = true;
            blue = false;
            purple = false;
            mainballoon.GetComponent<MeshRenderer>().material = greenMat;
        }

        else if (collision.gameObject.tag == "purpleTrigger")
        {
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
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x + balonscale, mainballoon.transform.localScale.y + balonscale, mainballoon.transform.localScale.z + balonscale), 0.1f);
                }
                mainballoon.SetActive(true);
                Destroy(collision.gameObject);
            }

            if (collision.gameObject.tag == "purple")
            {
                if (mainballoon.gameObject.activeSelf)
                {

                    seviye--;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                }

                Destroy(collision.gameObject);

            }
            if (collision.gameObject.tag == "blue")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye--;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
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
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                }
                Destroy(collision.gameObject);
            }
            
            else if (collision.gameObject.tag == "purple")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye--;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                }
                Destroy(collision.gameObject);
            }
            
            else if (collision.gameObject.tag == "blue")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye++;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x + balonscale, mainballoon.transform.localScale.y + balonscale, mainballoon.transform.localScale.z + balonscale), 0.1f);
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
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                }
                Destroy(collision.gameObject);
            }

            else if (collision.gameObject.tag == "purple")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye++;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x + balonscale, mainballoon.transform.localScale.y + balonscale, mainballoon.transform.localScale.z + balonscale), 0.1f);
                }
                Destroy(collision.gameObject);
            }

            else if (collision.gameObject.tag == "blue")
            {
                if (mainballoon.gameObject.activeSelf)
                {

                    seviye--;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                }
                Destroy(collision.gameObject);
            }

            else if (collision.gameObject.tag == "trap" && baloonTypes == BaloonTypes.purple)
            {
                pb.currentState = PlayerMovement.State.Lose;
            }
        }
        #endregion
    }
}
