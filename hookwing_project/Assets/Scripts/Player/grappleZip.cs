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
    private Transform tempClosest;
    public float PlayerDistance;
    public float MouseDistance;
    private bool check;
    private Vector3 direction;
    public float force = 20f;
    private float time = 0f;
    public float totalTime = 2f;
    private bool zip;
    public bool grappled;

    public float xOffset;
    public float yOffset;
    public GameObject canvas;
    private bool paused;

    public float messing = 1f;
    public bool canGrapple = true;
    private GameObject[] grapplePoints;


    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        grapplePoints = GameObject.FindGameObjectsWithTag("grappleZip");

        lineRenderer = GetComponent<LineRenderer>();
        RigidBody = GetComponent<Rigidbody2D>();
        distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
        lineRenderer.positionCount = 0;

        grappled = false;
        xOffset = 2f;
        yOffset = 0.55f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canGrapple) return;
        
        paused = (canvas.GetComponent<PauseMenu>().pubPaused || canvas.GetComponent<DeathMenu>().pubPaused);
        closest = GetClosestGrapple(grapplePoints);

        GetMousePos();
        DistanceCheck();
        float step = force * Time.deltaTime;
       
        if (Input.GetMouseButtonDown(0) && (!paused) && check)
        {
            tempClosest = closest;
            grappled = true;
            direction = closest.position - PlayerPos;
            direction.Normalize();
            zip = true;

            RigidBody.velocity = Vector3.zero;
            RigidBody.angularVelocity = 0;

            lineRenderer.positionCount = 2;
        }
        
        if(Input.GetMouseButtonUp(0) || (Vector3.Distance(RigidBody.position, closest.position) < messing))
        {
            lineRenderer.positionCount = 0;
            grappled = false;
            zip = false;
            time = 0;            
        }
        
        AddForceOverTime();
        
        
    }

    private void DrawLine()
    {
        
        if (lineRenderer.positionCount <= 0) return;
        bool facingRight = GetComponent<stolen>().m_FacingRight;
        Vector3 temp = transform.position + new Vector3((facingRight ? 1 : -1) * xOffset, yOffset);
        lineRenderer.SetPosition(0, temp);
        lineRenderer.SetPosition(1, tempClosest.position);
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
            float dist = Vector3.Distance(grapplePoint.transform.position,mousePos);
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
                // RigidBody.velocity = Vector3.zero;
                direction = closest.position - PlayerPos;
                direction.Normalize();
                RigidBody.AddForce(direction * force);
            }
            else
            {
                time = 0;
                grappled = false;
                zip = false;
                lineRenderer.positionCount = 0;
            }

            DrawLine();

        }
        
        
    }

    void OnDrawGizmosSelected() {
        if (closest == null) return;
        Gizmos.DrawWireSphere(closest.position, 10);
        Gizmos.DrawWireSphere(closest.position, messing/2);
        Gizmos.DrawWireSphere(RigidBody.position, messing/2);
    }

}
