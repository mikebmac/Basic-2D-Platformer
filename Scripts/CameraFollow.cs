using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    private float smoothing = 0.3f;

    private void LateUpdate() {
        float blend = 1f - Mathf.Pow(1f - smoothing, Time.deltaTime * 30f);
        Vector3 targetPos = player.position + offset;
        Vector3 lerpPos = Vector3.Lerp(transform.position, targetPos, smoothing);
        transform.position = lerpPos;
    }
}
