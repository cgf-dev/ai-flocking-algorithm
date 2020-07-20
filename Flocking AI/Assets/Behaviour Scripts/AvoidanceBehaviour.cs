using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Because it is a scriptable object we need a way to create it, so we use an attribute:
[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // If no neighbours, return no adjustment
        if (context.Count == 0)
            return Vector2.zero;

        // Add all points together and find the average point
        Vector2 avoidanceMove = Vector2.zero;
        // We also need to keep a count of neighbours within the new smaller radius
        // nAvoid being the number of things to avoid
        int nAvoid = 0;

        // We go through each items transform in our list of neighbours
        // If using filter, we choose the filtered list of transforms, otherwise ignore this
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            if (Vector2.SqrMagnitude(item.position - agent.transform.position) < flock.SquareAvoidanceRadius)
            {
                // We add to the count of neighbours the agent is currently avoiding:
                nAvoid++;
                // Here we want to move away from the neighbour instead of towards it
                // This line will also automatically give us our offset
                avoidanceMove += (Vector2)(agent.transform.position - item.position);
            }
        }
        // If we have neighbours to avoid:
        if (nAvoid > 0)
            // Average out
            avoidanceMove /= nAvoid;

        // We can return this value even if nAvoid is 0, then avoidanceMove == Vector2.zero anyway
        return avoidanceMove; 
    }


}
