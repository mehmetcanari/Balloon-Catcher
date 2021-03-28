using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class BalloonDestroyer : MonoBehaviour
{
    public GameObject mainballoon;
    public float balonscale;
    public int seviye;
    bool red = false;
    bool purple = true;
    bool green = false;
    public Material redMat;
    public Material greenMat;
    public Material purpleMat;
    private void Start()
    {

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

        if (collision.gameObject.tag == "Destroyer")
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "redTrigger")
        {
            red = true;
            green = false;
            purple = false;
            mainballoon.GetComponent<MeshRenderer>().material = redMat;
        }
        if (collision.gameObject.tag == "greenTrigger")
        {
            green = true;
            red = false;
            purple = false;
            mainballoon.GetComponent<MeshRenderer>().material = greenMat;
        }
        if (collision.gameObject.tag == "purpleTrigger")
        {
            green = false;
            red = false;
            purple = true;
            mainballoon.GetComponent<MeshRenderer>().material = purpleMat;
        }

        #region balloons
        if (green)
        {
            if (collision.gameObject.tag == "green")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye++;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x + balonscale, mainballoon.transform.localScale.y + balonscale, mainballoon.transform.localScale.z + balonscale), 0.1f);
                }
                Destroy(collision.gameObject);
                mainballoon.SetActive(true);


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
            if (collision.gameObject.tag == "red")
            {
                if (mainballoon.gameObject.activeSelf)
                {

                    seviye--;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                }
                Destroy(collision.gameObject);


            }

        }
        if (red)
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
            if (collision.gameObject.tag == "purple")
            {
                if (mainballoon.gameObject.activeSelf)
                {

                    seviye--;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x - balonscale, mainballoon.transform.localScale.y - balonscale, mainballoon.transform.localScale.z - balonscale), 0.1f);
                }
                Destroy(collision.gameObject);
            }
            if (collision.gameObject.tag == "red")
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
            if (collision.gameObject.tag == "purple")
            {
                if (mainballoon.gameObject.activeSelf)
                {
                    seviye++;
                    mainballoon.transform.DOScale(new Vector3(mainballoon.transform.localScale.x + balonscale, mainballoon.transform.localScale.y + balonscale, mainballoon.transform.localScale.z + balonscale), 0.1f);
                }
                Destroy(collision.gameObject);
                mainballoon.SetActive(true);
            }
            if (collision.gameObject.tag == "red")
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
    }
}
