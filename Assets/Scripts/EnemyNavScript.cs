using UnityEngine;
using System.Collections;

public class EnemyNavScript : MonoBehaviour
{

    //private GameObject Goal;
    private GameDataScript gameData;
    public GameObject currentWayPoint;
    private Vector3 targetToMoveTo;
    public float initialSpeed;
    private float speed;
    public float runawaySpeed;
    public bool isFlying = false;

    public float distanceToUni = 0;
    private float slowdownTimer = 0;

    public WaypointScript.direction direction = WaypointScript.direction.right;

    // When the enemy is spawned, the spawner tells them their first waypoint
    void Start()
    {
        gameData = GameObject.FindGameObjectWithTag("GameData").GetComponent<GameDataScript>();
        speed = initialSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        slowdownTimer -= Time.deltaTime;
        if (slowdownTimer <= 0)
        {
            speed = initialSpeed;
        }

        // the enemy moves towards the target at a constant speed
        transform.position = Vector3.MoveTowards(transform.position, targetToMoveTo, speed * Time.deltaTime);
        //if it reaches the target location
        if (transform.position == targetToMoveTo)
        {
            //ask the current waypoint for the next waypoint
            if (GetComponent<EnemyHealthScript>().runningAway)
            {
                Destroy(gameObject);
            }
            else
            {
                ChangeAnimationDirection(currentWayPoint.GetComponent<WaypointScript>().dir);

                ChangeTargetWaypoint(currentWayPoint.GetComponent<WaypointScript>().nextWayPoint);
            }


        }
        //Calculate remaining distance to university
        if (GetComponent<EnemyHealthScript>().runningAway == false)
        {
            CalculateDistanceToUni();
        }

        
    }

    //this is cript is called when we want to change the target to move towards
    public void ChangeTargetWaypoint(GameObject newWaypoint)
    {
        //first, we do the change
        currentWayPoint = newWaypoint;
        //second, we determine the randomized target, so not all enemies line up, and walk to the exact same target. it adds a slight variation
        targetToMoveTo = new Vector3(currentWayPoint.transform.position.x + Random.Range(-0.35f, 0.35f), 0, currentWayPoint.transform.position.z + Random.Range(-0.35f, 0.35f));
        //lastly, we recalculate our distance to the uni
        CalculateDistanceToUni();
    }

    //this is how we calculat the distance
    public void CalculateDistanceToUni()
    {
        //first set the distance to 0. we will add to it step by step
        distanceToUni = 0;
        //The first thing to add, is our distance from the current waypoint
        distanceToUni += Vector3.Distance(transform.position, currentWayPoint.transform.position);
        //then we prep for adding all the other waypoints. For this we need a variable to use to cycle through all the waypoints. We set its value to our current waypoint.
        //we want to add its distance from the waypoint after it.
        GameObject waypointToAddToCalculation;
        waypointToAddToCalculation = currentWayPoint;

        //So while there is waypoint to move on to, we keep adding their distance to our number. when they are done, we have the accurate distance to the uni. :)
        while (waypointToAddToCalculation.GetComponent<WaypointScript>().nextWayPoint != null)
        {
            distanceToUni += Vector3.Distance(waypointToAddToCalculation.transform.position, waypointToAddToCalculation.GetComponent<WaypointScript>().nextWayPoint.transform.position);
            waypointToAddToCalculation = waypointToAddToCalculation.GetComponent<WaypointScript>().nextWayPoint;
        }


    }

    public void Runaway()
    {
        speed = runawaySpeed;
        gameData.ChangeUsac(GetComponent<EnemyHealthScript>().usacValue);
        GameObject[] exitPoints = GameObject.FindGameObjectsWithTag("ExitPoint");
        GameObject closestExitPoint = exitPoints[0];
        for (int i = 1; i < exitPoints.Length; i++)
        {
            if (Vector3.Distance(transform.position, closestExitPoint.transform.position) > Vector3.Distance(transform.position, exitPoints[i].transform.position))
            {
                closestExitPoint = exitPoints[i];
            }
        }
        targetToMoveTo = closestExitPoint.transform.position;
        if (targetToMoveTo.x < transform.position.x)
        {
            ChangeAnimationDirection(WaypointScript.direction.left);
        }
        else {
            ChangeAnimationDirection(WaypointScript.direction.right);
        }
    }

    public void SlowMeDown(float slowBy, float slowDuration)
    {
        if (speed == initialSpeed)
        {
            speed -= speed * slowBy;
            slowdownTimer = slowDuration;
        }

    }

    public void ChangeAnimationDirection(WaypointScript.direction dir) {
        direction = dir;
        if (direction == WaypointScript.direction.down)
        {
            GetComponentInChildren<Animator>().SetTrigger("WaypointDown");
        }
        else
        if (direction == WaypointScript.direction.up)
        {
            GetComponentInChildren<Animator>().SetTrigger("WaypointUp");
        }
        else
        if (direction == WaypointScript.direction.left)
        {
            GetComponentInChildren<Animator>().SetTrigger("WaypointLeft");
        }
        else
        if (direction == WaypointScript.direction.right)
        {
            GetComponentInChildren<Animator>().SetTrigger("WaypointRight");
        }
    }
}
