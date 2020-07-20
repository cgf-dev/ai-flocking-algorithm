using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Stay In Radius")]
public class StayInRadiusBehaviour : FlockBehaviour
{
    // This vector will default to 0,0 which is fine
    public Vector2 center;
    public float radius = 15f;

    // Override this vector
    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // Find out how far away from the center the agent is, may have to call them back!
        // This line gives us the distance from the center of the screen
        Vector2 centerOffset = center - (Vector2)agent.transform.position;

        // If t is 0 we are at the center
        // Costly, however if I used SqrMagnitude the ratios would not be entirely correct
        float t = centerOffset.magnitude / radius;

        // Check distance from the radius:
        // If within 90% of the radius, don't bother to act upon it
        if (t < 0.9f)
        {
            return Vector2.zero;
        }

        
        return centerOffset * t * t;
    }
}
