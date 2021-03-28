using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PinTrapMovement : MonoBehaviour
{
    void Start()
    {
        Fourth();
    }

    public void Fourth()
    {
        transform.DOLocalMoveZ(1, 1);
        Invoke("Back", 1f);
    }
    void Back()
    {
        transform.DOLocalMoveZ(0.2f, 1);
        Invoke("Fourth", 1f);
    }
}
