using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFaceFollowCamera : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.1f;

    //ī�޶�� ��� �� �Ÿ�
    public Vector3 offset;

    private void Update()
    {
        if (target == null)
        {
            return;
        }

        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
