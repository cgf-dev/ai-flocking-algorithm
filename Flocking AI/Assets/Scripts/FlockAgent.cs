using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Each agent will have a circle collider as their larger radius (explained in the plan)
[RequireComponent(typeof(Collider2D))]

public class FlockAgent : MonoBehaviour
{
    // Variable 
    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }


    // Set up my COllider2D:
    Collider2D agentCollider;
    // We want to be able to access this collider without assigning to it:
    public Collider2D AgentCollider { get { return agentCollider; } }



    void Start()
    {
        // Find the instance of the Collider on our object
        agentCollider = GetComponent<Collider2D>();
    }

    // This method takes the flock as a parameter and assigns the agent to it
    public void Initialise(Flock flock)
    {
        agentFlock = flock;
    } 

    public void Move(Vector2 velocity)
    {
        // Turn agent to position it's moving to
        // In Unity2D, the 'up' basically means forwards for the arrows
        transform.up = velocity;

        // Move agent to position it's moving to
        // This line ensures constant movement regardless of framerate
        // We cast velocity as a Vector3 so we aren't adding a Vector2 and Vector3 together; this gives errors
        transform.position += (Vector3)velocity * Time.deltaTime;
    }



}
