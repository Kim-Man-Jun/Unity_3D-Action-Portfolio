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

    //�е忡�� ���� ���� ���
    public void PadDown()
    {
        downPos = Input.mousePosition;
    }

    //�е带 �巡�� �� ���
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

        //�÷��̾� �̵�
        player.GetComponent<playerController>().xInput = axis.x;
        player.GetComponent<playerController>().zInput = axis.y;
    }

    //�е忡�� ���� �� ���
    public void PadUp()
    {
        GetComponent<RectTransform>().localPosition = defPos;

        player.GetComponent<playerController>().xInput = 0;
        player.GetComponent<playerController>().zInput = 0;

        //�÷��̾� ����
        player.GetComponent<playerController>().zeroVelocity();
    }
}
