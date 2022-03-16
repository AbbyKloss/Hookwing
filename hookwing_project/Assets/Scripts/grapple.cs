using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapple : MonoBehaviour
{

    private Vector3 mousePos;
    private Camera camera;

    private bool grappleCheck;

    private DistanceJoint2D distanceJoint;
    private LineRenderer lineRenderer;
    private Vector3 tempPos;
    private float CharDistance;
    private bool check;
    private Vector3 location;
    public float mouseDistance;

    // Start is called before the first frame update
    void Start()
    {

        GameObject grapplePoint = GameObject.FindWithTag("grappleable");
        location = grapplePoint.transform.position;


        camera = Camera.main;
        distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
        lineRenderer = GetComponent<LineRenderer>();
        grappleCheck = true;
        lineRenderer.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePos();
        GetCharDistance();
        CheckMouse();
        if (Input.GetMouseButtonDown(0) && grappleCheck && check && CharDistance < 10)
        {
            distanceJoint.enabled = true;
            distanceJoint.connectedAnchor = location;
            lineRenderer.positionCount = 2;
            tempPos = mousePos;
            grappleCheck = false;
        }
        else if (Input.GetMouseButtonDown(0))
        {
            distanceJoint.enabled = false;
            grappleCheck = true;
            lineRenderer.positionCount = 0;
        }
        DrawLine();
    }
    private void DrawLine()
    {
        if (lineRenderer.positionCount <= 0) return;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, location);
    }
    private void GetMousePos()
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
    }
    private void GetCharDistance()
    {
        CharDistance = Vector3.Distance(transform.position, location);
    }
    private void CheckMouse()
    {
        // it looks like this broke it?
        // i reverted it back to where it was before

        check = false; 
        mouseDistance = Vector3.Distance(mousePos, location);
        if(mouseDistance < 10.6)
        // if(Vector3.Distance(mousePos,location) < 10.6)
        {
            check = true;
        }
    }
}