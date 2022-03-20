using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.name == "Player") {
            collision.GetComponent<grapple>().canGrapple = true;
            collision.GetComponent<grappleZip>().canGrapple = true;
            Destroy(gameObject);
        }
    }
}
