using MoreMountains.CorgiEngine;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    [SerializeField] int healValue = 10;

    private void Awake()
    {
        var collider = GetComponent<Collider2D>();
        if (collider == null)
        {
            Debug.LogError("Missing a 2D collider.");
        }
        else
        {
            collider.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            var health = collision.gameObject.GetComponent<Health>();
            if (health != null && health.CurrentHealth < health.MaximumHealth)
            {
                health.GetHealth(healValue, gameObject);
                Destroy(gameObject);
            }
        }
    }
}
