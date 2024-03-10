using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableItems : MonoBehaviour
{
    private int health = 1;
    [SerializeField] private Text healthText;
    [SerializeField] private AudioSource goodItemSound;
    [SerializeField] private AudioSource badItemSound;
    [SerializeField] private AudioSource PowerUpSound;
    [SerializeField] private PlayerMovement playerMovement; // Reference to PlayerMovement script

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            goodItemSound.Play();
            Destroy(collision.gameObject);
            health++;
            Debug.Log("Health increased: " + health); // Add debug log
            healthText.text = "Health : " + health;
            Debug.Log("Health text updated."); // Add debug log
        }
        else if (collision.CompareTag("BadCollectable"))
        {
            badItemSound.Play();
            Destroy(collision.gameObject);
            health--;
            healthText.text = "Health : " + health;
        }
        else if (collision.CompareTag("PowerUpCollectable"))
        {
            PowerUpSound.Play();
            Destroy(collision.gameObject);
            StartCoroutine(PowerUpEffect());
        }
    }

    IEnumerator PowerUpEffect()
    {
        playerMovement.SetPowerUp(true); // Activate power-up in PlayerMovement script
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        playerMovement.SetPowerUp(false); // Deactivate power-up after 5 seconds
    }

    public int GetHealth()
    {
        return health;
    }
}
