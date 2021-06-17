using MoreMountains.CorgiEngine;
using UnityEngine;

public class PlayerTeleportBullet : MonoBehaviour
{
    [SerializeField] public KeyCode inputCode = KeyCode.G;
    [SerializeField] float cooldown = 1;
    [SerializeField] GameObject teleportBulletPrefab;
    Character player;
    float currentCooldown;
    private TeleportBulletController teleportBulletController;

    private void Awake()
    {
        if (teleportBulletPrefab == null)
        {
            Debug.LogError("Missing teleport bullet prefab.");
        }
        player = GetComponent<Character>();
    }

    private void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (Input.GetKeyDown(inputCode))
        {
            if (teleportBulletController == null && currentCooldown <= 0)
            {
                currentCooldown = cooldown;
                var currentBullet = Instantiate(teleportBulletPrefab, transform.position, Quaternion.identity);
                var projectile = currentBullet.GetComponent<Projectile>();
                teleportBulletController = currentBullet.GetComponent<TeleportBulletController>();
                teleportBulletController.Player = this;
                teleportBulletController.LifeTimer = cooldown;
                var direction = Vector3.zero;

                direction.y = Input.GetAxisRaw("Player1_Vertical");

                if (direction.y == 0)
                {
                    direction.x = player.IsFacingRight ? 1 : -1;
                }

                projectile.SetDirection(direction, Quaternion.identity);
            }
            else if (teleportBulletController != null)
            {
                teleportBulletController.TeleportPlayer();
                teleportBulletController = null;
            }
        }
    }

    internal void Teleport(Vector3 pos)
    {
        transform.position = pos;
        teleportBulletController = null;
    }
}
