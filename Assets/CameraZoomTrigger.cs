using Cinemachine;
using UnityEngine;

public class CameraZoomTrigger : MonoBehaviour
{
    [SerializeField] float lensSize = 10f;

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
            Camera.main.orthographicSize = lensSize;
        }
    }
}
