using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    public ChairPath currentPath;
    int waypointIndex = 0;

    public float moveSpeed = 1.0f;

    bool isOnPath;
    [HideInInspector]
    public bool hasReachedChair;

    public bool isLeaving;
    bool hasReachedExit;

    Vector2 movement;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!isOnPath)
        {
            Move(GameManager.instance.entryPoint);

            if (transform.position == GameManager.instance.entryPoint.position)
                isOnPath = true;
        }

        if (isOnPath && !hasReachedChair)
        {
            Move(currentPath.path[waypointIndex]);

            if (transform.position == currentPath.path[waypointIndex].position)
            {
                waypointIndex++;
                if (waypointIndex == currentPath.path.Count)
                {
                    waypointIndex = currentPath.path.Count - 1;
                    hasReachedChair = true;
                }
            }
        }

        if (isLeaving)
        {
            Move(currentPath.path[waypointIndex]);
            if (transform.position == currentPath.path[waypointIndex].position)
            {
                waypointIndex--;
                if (waypointIndex == 0)
                {
                    hasReachedExit = true;
                }
            }
        }

        if (hasReachedExit)
        {
            Move(GameManager.instance.entryPoint);

            if (transform.position == GameManager.instance.entryPoint.position)
            {
                currentPath.isOccupied = false;
                Destroy(gameObject);
            }
        }
    }

    void Move(Transform destination)
    {
        movement = Vector2.MoveTowards(transform.position, destination.position, Time.deltaTime * moveSpeed);
        rb.MovePosition(movement);
    }
}