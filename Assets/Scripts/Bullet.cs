using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet collides with a spider
        if (collision.gameObject.CompareTag("Spider"))
        {
            // Destroy the spider on collision
            Destroy(collision.gameObject);
        }

        // Destroy the bullet after collision
        Destroy(gameObject);
    }
}