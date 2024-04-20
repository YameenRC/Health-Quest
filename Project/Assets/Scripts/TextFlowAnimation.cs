using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Importing the TMPro namespace to use the TextMeshPro components.

public class TextFlowAnimation : MonoBehaviour // This class animates text to create a flowing effect similar to the motion of water.
{
    public TMP_Text textComponent;
    void Update() 
    {
        textComponent.ForceMeshUpdate(); // Force the text mesh to update. This is necessary to get the latest character positions.
        var textInfo = textComponent.textInfo; // Retrieve information about the text currently displayed by TextMeshPro.
        for (int i = 0; i < textInfo.characterCount; i++) // Iterate through each character in the text.
        {
            var charInfo = textInfo.characterInfo[i]; // Get the information of the current character.

            if (!charInfo.isVisible) // Skip characters that are not visible (e.g., spaces, tabs, line breaks).
            {
                continue;

            }
            var verts = textInfo.meshInfo[charInfo.materialReferenceIndex].vertices; // Access the vertices of the mesh used for this character.


            for (int j = 0; j < 4; ++j) // Modify each vertex to create a wave effect. Each character has 4 vertices (since each character is a quad).
            {
                var orig = verts[charInfo.vertexIndex + j]; // Original vertex position.
                verts[charInfo.vertexIndex + j] = orig + new Vector3(0, Mathf.Sin(Time.time * 2f + orig.x * 0.01f) * 10f, 0); // Modify the vertex position to create a wave effect using a sine wave calculation.

            }
        }
        for (int i = 0; i < textInfo.meshInfo.Length; ++i) // Update the meshes with the new vertices to apply the wave effect.
        {

            var meshInfo = textInfo.meshInfo[i];
            meshInfo.mesh.vertices = meshInfo.vertices; // Set the modified vertices back to the mesh.
            textComponent.UpdateGeometry(meshInfo.mesh, i); // Update the geometry to reflect the changes.

        }
    }
}
