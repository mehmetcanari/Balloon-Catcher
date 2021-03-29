using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberMovement : MonoBehaviour
{
    public GameObject player;
    SpriteRenderer rend;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("baloncu");
        transform.parent = player.transform;

        rend = GetComponent<SpriteRenderer>();
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        for (float i = 1f; i >= -0.05f; i-= 0.05f)
        {
            Color c = rend.material.color;
            c.a = i;
            rend.material.color = c;
            yield return new WaitForSeconds(0.05f);
        }
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, transform.position.y + 3, transform.position.z), 1 * Time.deltaTime);
        Destroy(gameObject, 2);
    }
}
