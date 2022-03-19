using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using System.Math;

public class CameraFollow : MonoBehaviour
{
    private Transform playerTransform;

    public bool FollowX = true;
    public bool FollowY = true;
    public float XOffset;
    public float YOffset;
    public float XPadding = 5.0f;
    public float YPadding = 5.0f;

    void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate() {
        Vector3 temp = transform.position;

        

        if (FollowX) {
            // if (System.Math.Abs(playerTransform.position.x) > System.Math.Abs(temp.x) + XPadding) {
            temp.x = playerTransform.position.x;
            // }
            temp.x += XOffset;
        }
        if (FollowY) {
            // if (System.Math.Abs(playerTransform.position.y) > System.Math.Abs(temp.y) + YPadding) {
            temp.y = playerTransform.position.y;
            // }
            temp.y += YOffset;
        }

        transform.position = temp;
    }
}
