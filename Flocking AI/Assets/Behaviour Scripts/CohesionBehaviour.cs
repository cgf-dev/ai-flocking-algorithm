using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Because it is a scriptable object we need a way to create it, so we use an attribute:
[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FilteredFlockBehaviour
{
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
        // Then we can return the vector
        return cohesionMove;
    }


}
