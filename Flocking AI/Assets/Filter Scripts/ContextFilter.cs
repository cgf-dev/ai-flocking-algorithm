using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract class inheritting from ScriptableObject
public abstract class ContextFilter : ScriptableObject
{
    // New method returns a list of transforms, called Filter and takes in the flock agent (for comparisons) and the original List of
    // Neighbour's transforms
    public abstract List<Transform> Filter(FlockAgent agent, List<Transform> original);
}
