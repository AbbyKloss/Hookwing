using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grappleZip : MonoBehaviour
{

    private Camera camera;
    private LineRenderer lineRenderer;
    private DistanceJoint2D distanceJoint;
    private Rigidbody2D RigidBody;
    private Vector3 PlayerPos;
    private Vector3 mousePos;
    private Transform closest;
    public float PlayerDistance;
    public float MouseDistance;
    private bool check;
    private Vector3 direction;
    public float force = 2500f;
    private float time = 0f;
    public float totalTime = 10f;
    private bool zip;
    public bool grappled;

    public float xOffset;
    public float yOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;

        lineRenderer = GetComponent<LineRenderer>();
        RigidBody = GetComponent<Rigidbody2D>();
        distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
        lineRenderer.positionCount = 0;

        grappled = false;
        xOffset = 1.15f;
        yOffset = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        closest = GetClosestGrapple(GameObject.FindGameObjectsWithTag("grappleZip"));
        GetMousePos();
        DistanceCheck();
        
       
        if (Input.GetMouseButtonDown(0) && check)
        {
            grappled = true;
            direction = closest.position - PlayerPos;
            direction.Normalize();
            zip = true;
            AddForceOverTime();

            lineRenderer.positionCount = 2;

            DrawLine();

            
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            grappled = false;
            lineRenderer.positionCount = 0;
        }
       

    }
    private void DrawLine()
    {
        if (lineRenderer.positionCount <= 0) return;
        bool facingRight = GetComponent<stolen>().m_FacingRight;
        Vector3 temp = transform.position + new Vector3((facingRight ? 1 : -1) * xOffset, yOffset);
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, closest.position);
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
            float dist = Vector3.Distance(grapplePoint.transform.position, mousePos);
            if (dist < closestDistance)
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
       
    }
    private void AddForceOverTime()
    {
        if(zip)
        {
            lineRenderer.positionCount = 2;
            time += Time.fixedDeltaTime;
            if(time < totalTime)
            {
                RigidBody.AddForce(direction * force);
                
                
            }
                
            else
            {
                time = 0;
                zip = false;
                
            }
        }
        
        
    }

}
