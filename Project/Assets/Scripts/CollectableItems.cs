using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableItems : MonoBehaviour
{
    private int health = 1;
    [SerializeField] private GameObject heartPrefab; // heart prefab in the inspector
    [SerializeField] private Transform heartsContainer; // parent object for hearts in the inspector
    [SerializeField] private AudioSource goodItemSound;
    [SerializeField] private AudioSource badItemSound;
    [SerializeField] private AudioSource PowerUpSound;
    [SerializeField] private PlayerMovement playerMovement;

    private List<GameObject> heartIcons = new List<GameObject>();

    private void Start()
    {
        UpdateHeartsDisplay(); // show the heart display based on starting health
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            goodItemSound.Play();
            Destroy(collision.gameObject);
            health = Mathf.Min(health + 1, 6); // health does not exceed 6
            UpdateHeartsDisplay();
        }
        else if (collision.CompareTag("BadCollectable"))
        {
            badItemSound.Play();
            Destroy(collision.gameObject);
            health = Mathf.Max(health - 1, 0); // health does not go below 0
            UpdateHeartsDisplay();
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
        playerMovement.SetPowerUp(true);
        yield return new WaitForSeconds(5f);
        playerMovement.SetPowerUp(false);
    }

    private void UpdateHeartsDisplay()
    {
        // Adjust the list to match the current health
        while (heartIcons.Count < health)
        {
            AddHeartIcon();
        }
        while (heartIcons.Count > health)
        {
            // Remove excess hearts
            var heartToRemove = heartIcons[heartIcons.Count - 1];
            heartIcons.RemoveAt(heartIcons.Count - 1);
            Destroy(heartToRemove);
        }
    }

    private void AddHeartIcon()
    {
        GameObject newHeart = Instantiate(heartPrefab, heartsContainer);
        // small space between the hearts
        float offsetX = 30; // Adjust this value to move the heart rightward to position
        float offsetY = -20; // Adjust this value to move the heart downward to position
        float spacing = 10; // Adjust the spacing between hearts 

        // Calculate the position for the new heart
        float heartWidth = newHeart.GetComponent<RectTransform>().sizeDelta.x;
        float newXPosition = offsetX + heartIcons.Count * (heartWidth + spacing);
        float newYPosition = offsetY; // Use offsetY to adjust the vertical position as needed

        newHeart.transform.localPosition = new Vector3(newXPosition, newYPosition, 0);
        heartIcons.Add(newHeart);
    }

    public int GetHealth() // Returns Health
    {
        return health;
    }
}
