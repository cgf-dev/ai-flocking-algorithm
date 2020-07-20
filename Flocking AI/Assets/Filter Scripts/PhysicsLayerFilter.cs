using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Physics Layer")]
public class PhysicsLayerFilter : ContextFilter
{
    // Variables
    public LayerMask mask;

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
            // << represents bitshifting
            if (mask == (mask | (1 << item.gameObject.layer))){
                // Item is on a layer where mask is checked off
                filtered.Add(item);
            }
        }
        return filtered;
    }




}
