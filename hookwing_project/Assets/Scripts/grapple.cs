using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapple : MonoBehaviour
{

    private Vector3 mousePos;
    private Camera camera;

    private bool grappleCheck;

    private Rigidbody2D RigidBody;
    private LineRenderer lineRenderer;
    private DistanceJoint2D distanceJoint;
    private Vector3 tempPos;
    private float CharDistance;

    private Vector3 PlayerPos;
    
    public float PlayerDistance;
    public float MouseDistance;
    private bool check;
    public Transform closest;
    private bool drawCheck;
    

    // Start is called before the first frame update
    void Start()
    {
        
        


        camera = Camera.main;
        
       
        lineRenderer = GetComponent<LineRenderer>();
        RigidBody = GetComponent<Rigidbody2D>();
        distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
        lineRenderer.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

        closest = GetClosestGrapple(GameObject.FindGameObjectsWithTag("grappleSwing"));
        GetMousePos();
        DistanceCheck();
       
        if (Input.GetMouseButtonDown(0) && check)
        {


             distanceJoint.enabled = true;
             distanceJoint.connectedAnchor = closest.position;
             lineRenderer.positionCount = 2;

                
        }
            
        if (Input.GetMouseButtonUp(0))
        {
             distanceJoint.enabled = false;

             lineRenderer.positionCount = 0;

        }
        DrawCheck();
        if(drawCheck)
            DrawLine();
        

    }
    private void DrawLine()
    {
        if (lineRenderer.positionCount <= 0) return;
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, distanceJoint.connectedAnchor);
    }
    private void GetMousePos()
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
    }
    Transform GetClosestGrapple(GameObject[] grapplePoints)
    {
        Transform closest = null;
        float closestDistance = Mathf.Infinity;
        PlayerPos = transform.position; 
        foreach (GameObject grapplePoint in grapplePoints)
        {
            float dist = Vector3.Distance(grapplePoint.transform.position, PlayerPos);
            if(dist < closestDistance)
            {
                closest = grapplePoint.transform;
                closestDistance = dist;
            }
        }

        return closest;
    }
    private void DistanceCheck()
    {
        check = false;
        PlayerDistance = Vector3.Distance(PlayerPos, closest.position);
        MouseDistance = Vector3.Distance(mousePos, closest.position);
        if (PlayerDistance < 10 && MouseDistance < 10)
            check = true;
        else check = false;
    }
    private void DrawCheck()
    {
        drawCheck = false;
        if (PlayerDistance < 10)
            drawCheck = true;
    }
}
