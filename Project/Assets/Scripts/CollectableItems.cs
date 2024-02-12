using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableItems : MonoBehaviour
{
    private int fruit = 3;
    [SerializeField] private Text Health;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable")) 
        {
            Destroy(collision.gameObject);
            fruit++;
            Health.text = "Health: " + fruit;
        }
        else if (collision.gameObject.CompareTag("BadCollectable")) 
        {
            Destroy(collision.gameObject);
            fruit--;
            Health.text = "Health: " + fruit;
        }
    }
}
