using UnityEngine;

public class TeleportAbilityGiver : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerTeleportBullet.isAbilityUnlocked)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerTeleportBullet = collision.gameObject.GetComponent<PlayerTeleportBullet>();
        if (playerTeleportBullet != null)
        {
            playerTeleportBullet.GiveAbility();
            Destroy(gameObject);
        }
    }
}
