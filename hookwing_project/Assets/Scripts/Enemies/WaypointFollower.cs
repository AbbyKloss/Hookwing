using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex = 0;
    [SerializeField] private float speed = 2f;
    [SerializeField] private bool loop = true;
    [SerializeField] private bool squirrel = false;
    private bool backtracking = false;
    void Update()
    {
        if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position)  < 0.1) {
            if (!loop && backtracking) currentWaypointIndex--;
            else currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Length) {
                if (loop) currentWaypointIndex = 0;
                else {
                    backtracking = true;
                    currentWaypointIndex = waypoints.Length - 1;
                }
            }
            else if ((!loop & backtracking) && currentWaypointIndex < 0) {
                backtracking = false;
                currentWaypointIndex = 1;
            }
            if (squirrel) Flip();
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
    }

    private void Flip() {
        // only useable for squirrels
        // squirrels only have two waypoints
        // this is the only way i can get this to work easily
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
