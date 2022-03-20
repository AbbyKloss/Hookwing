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
    public float yBottomLimit = 0.0f;
    public float xLeftLimit = 0.0f;

    void Start() {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate() {
        Vector3 temp = transform.position;

        

        if (FollowX) {
            // if (System.Math.Abs(playerTransform.position.x) > System.Math.Abs(temp.x) + XPadding) {
            // }
            if ((xLeftLimit != 0.0f) && (playerTransform.position.x > xLeftLimit)) {
                temp.x = playerTransform.position.x;
                temp.x += XOffset;
            }
            else if (xLeftLimit == 0.0f) {
                temp.x = playerTransform.position.x;
                temp.x += XOffset;
            }
        }
        if (FollowY) {
            // if (System.Math.Abs(playerTransform.position.y) > System.Math.Abs(temp.y) + YPadding) {
            // }
            if ((yBottomLimit != 0.0f) && (playerTransform.position.y > yBottomLimit)) {
                temp.y = playerTransform.position.y;
                temp.y += YOffset;
            }
            else if (yBottomLimit == 0.0f) {
                temp.y = playerTransform.position.y;
                temp.y += YOffset;
            }
        }

        transform.position = temp;
    }
}
