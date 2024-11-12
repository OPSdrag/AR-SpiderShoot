using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;  // For UI elements

public class ARTouchSpawn : MonoBehaviour
{
    public GameObject objectToSpawn; // Prefab to spawn (spider)
    private ARRaycastManager arRaycastManager;

    private bool hasSpawned = false; // To track if we've already spawned the spiders
    private int remainingSpiders;    // To track the remaining spiders
    public Text scoreText;           // Reference to UI Text to display score
    private int score;               // Score variable
    private bool isGameOver = false;
    void Start()
    {
        // Find and assign the ARRaycastManager component
        arRaycastManager = FindObjectOfType<ARRaycastManager>();

        // Initialize score
        score = 0;
        UpdateScoreText();  // Update UI to show initial score
    }

    void Update()
    {
        // If all spiders have been destroyed, respawn them
        if (remainingSpiders == 0)
        {
            RespawnSpiders();
        }
        if(!isGameOver) {
            if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began && !hasSpawned)
            {
                // Raycast to detect a plane at the touch position
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (arRaycastManager.Raycast(touch.position, hits, TrackableType.Planes))
                {
                    Pose hitPose = hits[0].pose;

                    // Spawn 7 spiders in a circle around the touch position
                    for (int i = 0; i < 7; i++)
                    {
                        float angle = i * Mathf.PI / 3.5f; // Calculate angle for circular distribution
                        Vector3 spawnPosition = hitPose.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)); // Calculate spawn position

                        // Instantiate the spider and freeze its position
                        GameObject spawnedSpider = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

                        // Make sure the spider is not affected by physics or movement
                        Rigidbody rb = spawnedSpider.GetComponent<Rigidbody>();
                        if (rb != null)
                        {
                            rb.isKinematic = true; // This will prevent physics forces from affecting it
                        }

                        // Increase the count of remaining spiders
                        remainingSpiders++;
                    }

                    hasSpawned = true; // Set flag to prevent further spawns on subsequent touches
                }
            }
        }
        }

        
    }

    // Respawn the spiders when all are destroyed
    private void RespawnSpiders()
    {
        // Reset the spawned flag and remaining spiders count
        hasSpawned = false;
        remainingSpiders = 0;
    }

    // Call this method from the Spider script when a spider is destroyed
    public void OnSpiderDestroyed()
    {
        remainingSpiders--; // Decrease the count when a spider is destroyed

        // Increment the score by 5
        score += 5;

        // Update the score display
        UpdateScoreText();

        // If all spiders are destroyed, trigger respawn
        if (remainingSpiders == 0)
        {
            RespawnSpiders();
        }
    }
    
    public void gameover(){
        isGameOver = true;
    }

    // Update the score display
    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score; // Update the score text UI
    }
}
