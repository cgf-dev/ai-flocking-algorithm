using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehaviour : ScriptableObject
{
    // Abstract method that returns a Vector2
    // It will take a FlockAgent called agent, a list of transforms called context and the flock itself
    public abstract Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock);
}
