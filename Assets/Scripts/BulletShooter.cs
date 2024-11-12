using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;
using UnityEngine.XR.ARSubsystems;

public class BulletShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab for the bullet (sphere)
    public float bulletSpeed = 10f; // Speed of the bullet
    private ARRaycastManager arRaycastManager;

    void Start()
    {
        // Find and assign the ARRaycastManager component
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Raycast to detect a plane at the touch position
                List<ARRaycastHit> hits = new List<ARRaycastHit>();
                if (arRaycastManager.Raycast(touch.position, hits, TrackableType.Planes))
                {
                    Pose hitPose = hits[0].pose;

                    // Shoot a bullet from the hit position
                    ShootBullet(hitPose.position);
                }
            }
        }
    }

    void ShootBullet(Vector3 position)
    {
        // Instantiate the bullet
        GameObject bullet = Instantiate(bulletPrefab, position, Quaternion.identity);

        // Get the bullet's Rigidbody and set its velocity
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        bulletRigidbody.velocity = Camera.main.transform.forward * bulletSpeed; // Shoot in the direction the camera is facing

        // Optionally destroy the bullet after some time
        Destroy(bullet, 3f); // Destroy after 3 seconds to clean up
    }
}