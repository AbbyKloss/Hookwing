using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;

    public bool FollowX = true;
    public bool FollowY = true;
    public float XOffset;
    public float YOffset;

    void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate() {
        Vector3 temp = transform.position;

        

        if (FollowX) {
            temp.x = playerTransform.position.x;
            temp.x += XOffset;
        }
        if (FollowY) {
            temp.y = playerTransform.position.y;
            temp.y += YOffset;
        }

        transform.position = temp;
    }
}
