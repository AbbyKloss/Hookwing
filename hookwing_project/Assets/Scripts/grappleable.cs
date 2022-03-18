using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grappleable : MonoBehaviour
{

    private Vector3 mousePos;
    private Camera camera;
    public bool Check;
    public Vector3 location;
    public float distance;
    public float grappleDistance;

    // Start is called before the first frame update
    void Start()
    {
        grappleDistance = 10.6F;
        camera = Camera.main;
        location = transform.position;
        Check = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePos();
        distance = Vector3.Distance(mousePos, location);
        if (distance < grappleDistance)
            Check = true;
        else
            Check = false;
    }

    private void GetMousePos()
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
    }
}