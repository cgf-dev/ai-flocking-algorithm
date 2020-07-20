using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    // Variables (lots of)
    // This line lets us manually add in our prefabs
    public FlockAgent agentPrefab;
    // Create a new list called agents that will add each prefab to the list
    List<FlockAgent> agents = new List<FlockAgent>();
    // This is where we will put in the scriptable object we made
    public FlockBehaviour behaviour;

    // We will use these ranges to create a slider for the amount of agents within a flock (some other sliders too)
    [Range(1, 500)]
    public int startingCount = 250;
    // The size of our flock circle will depend on how many agents are in the flock
    // This float will let me work out the overall flock radius:
    const float AgentDensity = 0.08f;

    // I have created another variable which we will use to remove any counteracting movements within the flock
    // Without this variable the flock will move very slowly
    [Range(1f, 100f)]
    public float driveFactor = 10f;

    // This float will cap the speed of the flock
    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    // Now I will create two radiuses
    // This one will determine the radius for each agent's neighbours
    [Range(1f, 10f)]
    public float neighbourRadius = 1.5f;

    // This one will be a multiplier, used to work out our avoidance (see plan)
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;


    // The next variables will not be set, but will be used to calculate things
    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }



    void Start()
    {
        // The next section is explained in the blogging section of my documentation
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        // Instanciate our flock:
        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                // This is the object we will be instanciating:
                agentPrefab,
                // Set the spawn point for our agents:
                // We use the AgentDensity constant so that they are always similarly placed - no huge gaps
                Random.insideUnitCircle * startingCount * AgentDensity,
                // Now we set the rotation (on the z axis) of each agent
                // It requires a quaternion, so I am going to create a random Vector3 between 0 and 360 and then convert it to a quaternion
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                // Set the parent of the agent - the flock itself's transform
                transform
                );
            // Give the agents a name so we can keep track of them more easily
            newAgent.name = "Agent " + i;
            // When the agent is created it gets added to it's particular flock
            newAgent.Initialise(this);
            // Now add each agent to our list of agents
            agents.Add(newAgent);
        }
    }



    void Update()
    {
        foreach(FlockAgent agent in agents)
        {
            // Create a list called context, this will deal with things in context to our neighbour radius
            // This list looks at nearby objects using the agent we are currently looking at
            List<Transform> context = GetNearbyObjects(agent);

            // For demo, checks how many neighbours the agent has, the more neighbours, the greener the agent will appear (from white)
            agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.green, context.Count / 6f);

            // Use the nearby objects - takes in our agent, the list of neighbours and the flock (this) as parameters
            // This returns back the way in which the agent should move
            Vector2 move = behaviour.CalculateMove(agent, context, this);
            // Speed the agent up
            move *= driveFactor;
            // Then check we have not exceeded our maxSpeed limit
            // If it has, bring speed back to the maximum
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                // This resets magnitude to 1 then multiplies it's current speed to our maxSpeed 
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        // We create a new list of transforms called context
        List<Transform> context = new List<Transform>();
        // We create a circle in space that gets ahold of all colliders within it, hence why we are using our neighbourRadius
        // These colliders will be placed within an array called contextColliders
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighbourRadius);
        // For each collider we run into, as long as it's not the agent in question, we want to add them to our list
        foreach(Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        // Update our context list
        return context;
    }




}
