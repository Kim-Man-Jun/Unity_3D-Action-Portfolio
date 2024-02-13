using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualPad : MonoBehaviour
{
    public float MaxLength = 100f;
    GameObject player;
    Vector2 defPos;
    Vector2 downPos;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        defPos = GetComponent<RectTransform>().localPosition;
    }

    //패드에서 손이 닿을 경우
    public void PadDown()
    {
        downPos = Input.mousePosition;
    }

    //패드를 드래그 할 경우
    public void PadDrag()
    {
        Vector2 mousePosition = Input.mousePosition;

        Vector2 newTabPos = mousePosition - downPos;

        Vector2 axis = newTabPos.normalized;

        float len = Vector2.Distance(defPos, newTabPos);

        if (len > MaxLength)
        {
            newTabPos.x = axis.x * MaxLength;
            newTabPos.y = axis.y * MaxLength;
        }

        GetComponent<RectTransform>().localPosition = newTabPos;

        //플레이어 이동
        player.GetComponent<playerController>().xInput = axis.x;
        player.GetComponent<playerController>().zInput = axis.y;
    }

    //패드에서 손을 뗄 경우
    public void PadUp()
    {
        GetComponent<RectTransform>().localPosition = defPos;

        player.GetComponent<playerController>().xInput = 0;
        player.GetComponent<playerController>().zInput = 0;

        //플레이어 정지
        player.GetComponent<playerController>().zeroVelocity();
    }
}
