using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject player; // Reference to the player
    private Vector3 constant = new Vector3(-15.9f, 13.7f, -16.15f); // Constant offset for the camera

    public LayerMask obstacleMask; // Layer mask to detect obstacles

    // Dictionary to store the original alpha values of objects
    private Dictionary<Renderer, float> originalAlphas = new Dictionary<Renderer, float>();

    // Update is called once per frame
    void Update()
    {
        // Update camera position to follow the player with the constant offset
        gameObject.transform.position = player.transform.position + constant;

        // Check for obstacles between the camera and player
        HandleObstacles();
    }

    void HandleObstacles()
    {
        // Clear previous obstacles
        ResetObstaclesTransparency();

        // Raycast to detect obstacles between the camera and player
        RaycastHit hit;
        Vector3 direction = (player.transform.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (Physics.Raycast(transform.position, direction, out hit, distance, obstacleMask))
        {
            // Handle making the obstacle transparent
            Renderer obstacleRenderer = hit.collider.GetComponent<Renderer>();
            if (obstacleRenderer != null)
            {
                // Store the original alpha value if not already stored
                if (!originalAlphas.ContainsKey(obstacleRenderer))
                {
                    originalAlphas[obstacleRenderer] = obstacleRenderer.material.color.a;
                }

                // Make the obstacle transparent
                Color color = obstacleRenderer.material.color;
                color.a = 0.3f; // Adjust alpha for transparency
                obstacleRenderer.material.color = color;
            }
        }
    }

    void ResetObstaclesTransparency()
    {
        // Restore the original transparency for all objects that were modified
        foreach (KeyValuePair<Renderer, float> entry in originalAlphas)
        {
            Renderer renderer = entry.Key;
            if (renderer != null)
            {
                Color color = renderer.material.color;
                color.a = entry.Value; // Reset to original alpha
                renderer.material.color = color;
            }
        }

        // Clear the dictionary after restoring transparency
        originalAlphas.Clear();
    }
}
