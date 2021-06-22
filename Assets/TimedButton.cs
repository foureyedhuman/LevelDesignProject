using MoreMountains.CorgiEngine;
using UnityEngine;

public class TimedButton : MonoBehaviour
{
    [SerializeField] KeyCode buttonKey = KeyCode.G;
    [SerializeField] GameObject doorObject;
    [SerializeField] float openDuration = 3f;
    float timerDuration;
    private bool isPlayerNear;

    private void Awake()
    {
        if (doorObject == null)
        {
            Debug.LogError("Missing door object!");
        }

        var collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
        else
        {
            Debug.LogError("Missing collider!");
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(buttonKey))
        {
            doorObject.SetActive(false);
            timerDuration = openDuration;
        }

        if (timerDuration > 0)
        {
            timerDuration -= Time.deltaTime;
            if (timerDuration <= 0)
            {
                doorObject.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var character = collision.gameObject.GetComponent<Character>();
        if (character != null && character.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        var character = collision.gameObject.GetComponent<Character>();
        if (character != null && character.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
