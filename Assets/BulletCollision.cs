using UnityEngine;

[AddComponentMenu("Nokobot/Modern Guns/Bullet Collision")]
public class BulletCollision : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("Damage dealt by the bullet")]
    public float damage = 10f;

    [Tooltip("Force applied to hit objects")]
    public float impactForce = 100f;

    void OnTriggerEnter(Collider other)
    {
        // Print what we're colliding with
        Debug.Log($"Bullet hit: {other.gameObject.name} (Tag: {other.tag})");

        // Optional: Handle different collision types
        HandleCollision(other);

        // Destroy the bullet after collision
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Alternative method if using non-trigger colliders
        Debug.Log($"Bullet collided with: {collision.gameObject.name} (Tag: {collision.gameObject.tag})");

        // Optional: Handle different collision types
        HandleCollision(collision.collider);

        // Destroy the bullet after collision
        Destroy(gameObject);
    }

    void HandleCollision(Collider hitCollider)
    {
        // Example of handling different object types
        switch (hitCollider.tag)
        {
            case "Enemy":
               EnemyScript enemyScript = hitCollider.GetComponent<EnemyScript>();

                if (enemyScript != null)
                {
                    enemyScript.TakeDamage();
                    Debug.Log($"Hit enemy for {damage} damage!");
                    Destroy(gameObject);
                }
                else
                {
                Debug.LogWarning("EnemyScript component not found on the hit enemy!");
                }
                break;

            case "Environment":
                Debug.Log("Hit environment object");
                Destroy(gameObject);
                break;

            default:
                Debug.Log($"Hit generic object: {hitCollider.name}");
                break;
        }

        // Optional: Add impact force to rigidbody objects
        // Rigidbody hitRigidbody = hitCollider.GetComponent<Rigidbody>();
        // if (hitRigidbody != null)
        // {
        //     Vector3 forceDirection = transform.forward;
        //     hitRigidbody.AddForce(forceDirection * impactForce, ForceMode.Impulse);
        // }
    }
}