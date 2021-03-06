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
    public bool grappled;

    public float xOffset;
    public float yOffset;
    public GameObject canvas;
    private bool paused;

    public bool canGrapple = true;
    private GameObject[] grapplePoints;

    public GameObject SoundGuy;
    public audio AC;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        grapplePoints= GameObject.FindGameObjectsWithTag("grappleSwing");
       
        lineRenderer = GetComponent<LineRenderer>();
        RigidBody = GetComponent<Rigidbody2D>();
        distanceJoint = GetComponent<DistanceJoint2D>();
        distanceJoint.enabled = false;
        lineRenderer.positionCount = 0;

        grappled = false;
        xOffset = 2f;
        yOffset = 0.55f;

        SoundGuy = GameObject.Find("SoundGuy");
        AC = SoundGuy.GetComponent<audio>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canGrapple) return;
        paused = (canvas.GetComponent<PauseMenu>().pubPaused || canvas.GetComponent<DeathMenu>().pubPaused);
        closest = GetClosestGrapple(grapplePoints);
        GetMousePos();
        DistanceCheck();
        
        if (Input.GetMouseButtonDown(0) && (!paused) && check)
        {
            AC.PlaySound("spidertech");
            grappled = true;

            distanceJoint.enabled = true;
            distanceJoint.connectedAnchor = closest.position;
            lineRenderer.positionCount = 2;
        }
            
        if (Input.GetMouseButtonUp(0))
        {
            grappled = false;
            distanceJoint.enabled = false;

            lineRenderer.positionCount = 0;
        }
        DrawLine();
    }

    private void DrawLine()
    {
        if (lineRenderer.positionCount <= 0) return;
        bool facingRight = GetComponent<stolen>().m_FacingRight;
        Vector3 temp = transform.position + new Vector3((facingRight ? 1 : -1) * xOffset, yOffset);
        lineRenderer.SetPosition(0, temp);
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
    }
    private void DrawCheck()
    {
        drawCheck = false;
        if (PlayerDistance < 10)
            drawCheck = true;
    }

    void OnDrawGizmosSelected() {
        if (closest == null) return;
        Gizmos.DrawWireSphere(closest.position, 10);
        // Gizmos.DrawWireSphere(closest.position, messing/2);
        // Gizmos.DrawWireSphere(RigidBody.position, messing/2);
    }
}
