using UnityEngine;

public class Spider : MonoBehaviour
{
    public GameObject explosionEffect;  // Reference to the explosion prefab
    private ARTouchSpawn spawner;       // Reference to the spawner script

    void Start()
    {
        // Find the ARTouchSpawn script
        spawner = FindObjectOfType<ARTouchSpawn>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object colliding with the spider is tagged as "Bullet"
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Instantiate the explosion effect at the spider's position and rotation
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            // Destroy the spider on collision
            Destroy(gameObject);

            // Optionally, destroy the bullet
            Destroy(collision.gameObject);

            // Notify the spawner that a spider was destroyed
            spawner.OnSpiderDestroyed();
        }
    }
}
