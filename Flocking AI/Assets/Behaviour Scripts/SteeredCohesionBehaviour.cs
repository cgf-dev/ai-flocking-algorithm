using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Because it is a scriptable object we need a way to create it, so we use an attribute:
[CreateAssetMenu(menuName = "Flock/Behaviour/SteeredCohesion")]
public class SteeredCohesionBehaviour : FilteredFlockBehaviour
{
    // Variables
    // Smoothdamp needs this variable:
    Vector2 currentVelocity;
    // How long it should take for the agent to get from it's current state to the calculated state
    // The higher the float, the less direction changes will be apparent
    public float agentSmoothTime = 0.5f;


    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // If no neighbours, return no adjustment
        if (context.Count == 0)
            return Vector2.zero;

        // Add all points together and find the average point
        Vector2 cohesionMove = Vector2.zero;

        // We go through each items transform in our list of neighbours
        // If using filter, we choose the filtered list of transforms, otherwise ignore this
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            cohesionMove += (Vector2)item.position;
        }
        // We now average the Vector out again so it is not a huuuge number
        cohesionMove /= context.Count;

        // Change from global position to offset of the agent itself
        // Create offset from agent position:
        cohesionMove -= (Vector2)agent.transform.position;

        cohesionMove = Vector2.SmoothDamp(agent.transform.up, cohesionMove, ref currentVelocity, agentSmoothTime);
        // Then we can return the vector
        return cohesionMove;
    }






}
