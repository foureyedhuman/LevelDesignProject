using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TeleportBulletController : MonoBehaviour
{
    [SerializeField] float proximityToWall = 1;

    public float LifeTimer { get; set; }
    public PlayerTeleportBullet Player { get; set; }

    private void Awake()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Update()
    {
        LifeTimer -= Time.deltaTime;
        if (LifeTimer <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TeleportPlayer()
    {
        var pos = transform.position;
        pos.z = 0;
        pos = Vector3.MoveTowards(pos, Player.transform.position, proximityToWall);
        Player.Teleport(pos);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Platforms")
            || collision.gameObject.layer == LayerMask.NameToLayer("MovingPlatforms"))
        {
            TeleportPlayer();
        }
    }
}
