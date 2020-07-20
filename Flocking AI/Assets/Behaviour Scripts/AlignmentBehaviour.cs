using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Because it is a scriptable object we need a way to create it, so we use an attribute:
[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FilteredFlockBehaviour
{
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // If no neighbours, maintain current alignment
        if (context.Count == 0)
            return agent.transform.up;

        // Add all points together and find the average point
        Vector2 alignmentMove = Vector2.zero;

        // We go through each items transform in our list of neighbours
        // If using filter, we choose the filtered list of transforms, otherwise ignore this
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
        foreach (Transform item in filteredContext)
        {
            alignmentMove += (Vector2)item.transform.up;
        }
        // We now average the Vector out again so it is not a huuuge number
        alignmentMove /= context.Count;

        return alignmentMove;
    }


}
