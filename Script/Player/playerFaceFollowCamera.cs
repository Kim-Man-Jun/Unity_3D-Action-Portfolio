using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerFaceFollowCamera : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 0.1f;

    //카메라와 대상 간 거리
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
