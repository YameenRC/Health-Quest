using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableItems : MonoBehaviour
{
    private int health = 1;
    [SerializeField] private Text Health;
    [SerializeField] private AudioSource GoodItemSound;
    [SerializeField] private AudioSource BadItemSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable")) 
        {
            GoodItemSound.Play();
            Destroy(collision.gameObject);
            health++;
            Health.text = "Health: " + health;
        }
        else if (collision.gameObject.CompareTag("BadCollectable")) 
        {
            BadItemSound.Play();
            Destroy(collision.gameObject);
            health--;
            Health.text = "Health : " + health;
        }
    }
}
