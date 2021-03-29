using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TrapMovement : MonoBehaviour
{
    public void Forth()
    {
        transform.DOLocalMoveY(1, 1);
        //Invoke("Back", 1f);
    }
    void Back()
    {
        transform.DOLocalMoveY(0.5f, 1);
        Invoke("Fourth", 1f);
    }
}
