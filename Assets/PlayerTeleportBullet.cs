using MoreMountains.CorgiEngine;
using UnityEngine;

public class PlayerTeleportBullet : MonoBehaviour
{
    [SerializeField] public KeyCode inputCode = KeyCode.G;
    [SerializeField] float cooldown = 1;
    [SerializeField] GameObject teleportBulletPrefab;
    Character player;
    private CorgiController corgiController;
    float currentCooldown;
    private TeleportBulletController teleportBulletController;
    public static bool isAbilityUnlocked;

    internal void GiveAbility()
    {
        isAbilityUnlocked = true;
    }

    private void Awake()
    {
        if (teleportBulletPrefab == null)
        {
            Debug.LogError("Missing teleport bullet prefab.");
        }
        player = GetComponent<Character>();
        corgiController = GetComponent<CorgiController>();
    }

    private void Update()
    {
        if (currentCooldown > 0)
        {
            currentCooldown -= Time.deltaTime;
        }

        if (Input.GetKeyDown(inputCode) && isAbilityUnlocked)
        {
            if (teleportBulletController == null && currentCooldown <= 0)
            {
                currentCooldown = cooldown;
                var currentBullet = Instantiate(teleportBulletPrefab, transform.position, Quaternion.identity);
                var projectile = currentBullet.GetComponent<Projectile>();
                teleportBulletController = currentBullet.GetComponent<TeleportBulletController>();
                teleportBulletController.Player = this;
                teleportBulletController.LifeTimer = cooldown;

                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = transform.position.z;
                var direction = mousePos - transform.position;

                projectile.SetDirection(direction.normalized, Quaternion.identity);
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
        corgiController.SetForce(Vector2.zero);
    }
}
