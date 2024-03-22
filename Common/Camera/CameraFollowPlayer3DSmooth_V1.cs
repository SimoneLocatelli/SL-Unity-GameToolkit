using System;
using UnityEngine;

public class CameraFollowPlayer3DSmooth_V1 : BaseBehaviour
{
    [SerializeField]
    [ReadOnlyProperty]
    private Vector3 currentVelocity;

    [SerializeField]
    [Range(0.1f, 10f)]
    private float distance = 3;

    [SerializeField]
    private Transform player;

    [SerializeField]
    [Range(0, 4f)]
    private float smoothTime = 0.25f;
    private void LateUpdate()
    {
        Vector3 target = player.position + (transform.position - player.position).normalized * distance;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref currentVelocity, smoothTime);
        //transform.LookAt(player);
    }
}
