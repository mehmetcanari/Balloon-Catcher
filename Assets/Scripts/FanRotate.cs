using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotate : MonoBehaviour
{
    private float rotSpeed = 2;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, rotSpeed));
    }
}
