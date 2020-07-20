using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Because it is a scriptable object we need a way to create it, so we use an attribute:
[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{

    // Variables
    // This array will be our behaviours to composite together
    public FlockBehaviour[] behaviours;
    // This array will correlate with the behaviours and is used for weighting
    public float[] weights;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // Our 2 arrays need to contain the same number of items, so check for that here and debug it
        if (weights.Length != behaviours.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector2.zero;
        }

        // Set up move
        Vector2 move = Vector2.zero;

        // Iterate through behaviours
        // I used a for loop instead of a foreach loop here because behaviours and weights need to be using the same indexes
        for (int i = 0; i < behaviours.Length; i++)
        {
            Vector2 partialMove = behaviours[i].CalculateMove(agent, context, flock) * weights[i];

            // Make sure partialMove is being limited to the extent of the weight
            // If there is some movement being returned:
            if (partialMove != Vector2.zero)
            {
                // Check does the overall movement exceed the weight?
                if (partialMove.sqrMagnitude > weights[i] * weights[i])
                {
                    // Normalise it back to a magnitude of 1, then multiply it by the weight
                    // This means it will be set at the maximum of the weight
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }

        return move;
    }

















}
