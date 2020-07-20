using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        // Set up a new filtered List
        List<Transform> filtered = new List<Transform>();

        // Iterate through original List and see if 
        // #1 It is a flock agent?
        // #2 Is it a member of the same flock?
        // If both are true, it can be added to the new filtered List
        foreach (Transform item in original)
        {
            FlockAgent itemAgent = item.GetComponent<FlockAgent>();
            // If this item isn't a flock agent, the item will be null
            if (itemAgent != null && itemAgent.AgentFlock == agent.AgentFlock)
            {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}
