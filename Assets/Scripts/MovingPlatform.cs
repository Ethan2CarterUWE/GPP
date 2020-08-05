using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{

    public Transform[] spots;
    public float moveSpeed;
    public float waitTime;

    private int numberOfSpots;
    private float delay;
    private int nextSpot;


    void Start()
    {
        gameObject.transform.position = spots[0].transform.position;
        numberOfSpots = spots.Length;
        nextSpot = 0;
        delay = waitTime;
    }
    void Update()
    {
        // moves the platform to the next location
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, spots[nextSpot].transform.position, moveSpeed * Time.deltaTime);

        // this calculates the amount of delay left
        if (gameObject.transform.position == spots[nextSpot].transform.position && delay > 0)
        {
            delay -= Time.deltaTime;
        }

        // assigns next spot for platform to move to
        if (delay <= 0 && gameObject.transform.position == spots[nextSpot].transform.position)
        {
            nextSpot += 1;
            delay = waitTime;
        }
        // after platform reaches the last spot, it travels from that location to the first spot and repeats the loop
        if (nextSpot == numberOfSpots)
        {
            nextSpot = 0;
        }
    }

}

