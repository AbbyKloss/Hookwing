using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapple_ : MonoBehaviour
{

    private Vector3 mousePos;
    private Camera camera;

    private bool grappleCheck;

    private DistanceJoint2D distanceJoint;
    private LineRenderer lineRenderer;
    private Vector3 tempPos;
    public float CharDistance;
    
    public Vector3 location;
    public float mouseDistance;
    private Vector3 PlayerPos;
    private GameObject Player;
   

    
    // Start is called before the first frame update
    void Start()
    {
        
        Player = GameObject.FindWithTag("Player");
        
        location = transform.position;


        camera = Camera.main;

        distanceJoint = Player.GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
        lineRenderer = Player.GetComponent<LineRenderer>();
        grappleCheck = true;
        lineRenderer.positionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
       
       
        PlayerPos = Player.transform.position;
        GetMousePos();
        GetCharDistance();
        CheckMouse();
        if (Input.GetMouseButtonDown(0) && grappleCheck && mouseDistance < 10 && CharDistance < 10)
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
        lineRenderer.SetPosition(0, PlayerPos);
        lineRenderer.SetPosition(1, location);
    }
    private void GetMousePos()
    {
        mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
    }
    private void GetCharDistance()
    {
        CharDistance = Vector3.Distance(PlayerPos, location);
    }
    private void CheckMouse()
    {
        
        mouseDistance = Vector3.Distance(mousePos, location);
        
    }
}
