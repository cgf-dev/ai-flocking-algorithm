using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// Add this namespace to give access to the custom editor attribute
using UnityEditor;

// This attribute only works with the namespace 'UnityEditor'
[CustomEditor(typeof(CompositeBehaviour))]
// Inherits from Editor instead of Monobehaviour
public class CompositeBehaviourEditor : Editor
{

    public override void OnInspectorGUI()
    {
        #region Setup
        // Here we cast target, this is an object (in the inspector), directly to composite behaviour so we can access it's variables etc.
        CompositeBehaviour cb = (CompositeBehaviour)target;




        #endregion

        EditorGUILayout.BeginHorizontal();

        // Check for behaviours
        // If there are no behaviours or the array is empty
        if (cb.behaviours == null || cb.behaviours.Length == 0)
        {
            // Display error message on it's own line
            EditorGUILayout.HelpBox("No behaviours in array.", MessageType.Warning);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.BeginHorizontal();
        }

        

        else
        {
            // If there are behaviours (whole reason of this script!)
            // Each one of these simple creates a label field with a min width and max width
            // These two widths mean that even if the inspector window is resized, the text will remain where we it want
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Number", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
            EditorGUILayout.LabelField("Behaviours", GUILayout.MinWidth(60f));
            EditorGUILayout.LabelField("Weights", GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
            EditorGUILayout.EndHorizontal();
           


            // Set save checker
            // This checks for any inputs
            EditorGUI.BeginChangeCheck();


            // Display each behaviour:
            for (int i = 0; i < cb.behaviours.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                // Make each item a string
                EditorGUILayout.LabelField(i.ToString(), GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                //EditorGUILayout.BeginHorizontal();
                // Show a field for the behaviours
                // False means we wont take any objects that come from the scene - we want only scriptable objects
                cb.behaviours[i] = (FlockBehaviour)EditorGUILayout.ObjectField(cb.behaviours[i], typeof(FlockBehaviour), false, GUILayout.MinWidth(60f));
                // Do the same for the weights:
                cb.weights[i] = EditorGUILayout.FloatField(cb.weights[i], GUILayout.MinWidth(60f), GUILayout.MaxWidth(60f));
                EditorGUILayout.EndHorizontal();
            }


            // End the change check
            // This just means that any altered behaviours/weights will be updated as appropriate
            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(cb);
            }

        }

        EditorGUILayout.EndHorizontal();

        
        
        if (GUILayout.Button("Add Behaviour"))
        {

            // Add behaviour
            AddBehaviour(cb);
            // Let Unity know this scriptable object has been changed and needs to be saved
            EditorUtility.SetDirty(cb);

        }

        

        
        // We only want the add button to appear if there are behaviours to remove
        if (cb.behaviours != null && cb.behaviours.Length > 0)
        {

            if (GUILayout.Button("Remove Behaviour"))
            {
                // Remove behaviour
                RemoveBehaviour(cb);
                // Let unity know this scriptable object has been changed and needs to be saved
                EditorUtility.SetDirty(cb);

            }
        }
        
        
    }

    // This method is called when the user clicks the add behaviour button
    void AddBehaviour(CompositeBehaviour cb)
    {
        // Get original size of the array
        int oldCount = (cb.behaviours != null) ? cb.behaviours.Length : 0;
        // If array is empty we will start with 1 item
        FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount + 1];
        // Create new array
        float[] newWeights = new float[oldCount + 1];
        // Iterate through and add the original values
        for (int i = 0; i < oldCount; i++)
        {
            newBehaviours[i] = cb.behaviours[i];
            newWeights[i] = cb.weights[i];

        }
        // newWeights can't be zero otherwise new behaviours can't take effect
        // The user would think something has gone wrong if we don't set it to 1f:
        newWeights[oldCount] = 1f;
        cb.behaviours = newBehaviours;
        cb.weights = newWeights;
    }

    // This method is called when the user clicks the remove behaviour button
    void RemoveBehaviour(CompositeBehaviour cb)
    {
        // We know the array wont be null, as the button would not appear
        int oldCount = cb.behaviours.Length;

        // If there is only one behaviour, we can just clear it out:
        if (oldCount == 1)
        {
            cb.behaviours = null;
            cb.weights = null;
            return;

        }

        // If array is empty we will start with 1 item
        FlockBehaviour[] newBehaviours = new FlockBehaviour[oldCount - 1];
        // Create new array
        float[] newWeights = new float[oldCount - 1];
        // Iterate through and add the original values
        for (int i = 0; i < oldCount - 1; i++)
        {
            newBehaviours[i] = cb.behaviours[i];
            newWeights[i] = cb.weights[i];

        }
        // We do not need to assign anything to newWeights here because it has already been assigned
        cb.behaviours = newBehaviours;
        cb.weights = newWeights;
    }

}

