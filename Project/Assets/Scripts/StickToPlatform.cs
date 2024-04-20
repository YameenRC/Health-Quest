using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickToPlatform : MonoBehaviour // This class allows a player object to stick to a moving platform when in contact.
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // OnTriggerEnter2D is called when a object enters a trigger collider attached to this object.
        // It specifically checks for collision with the player.
        if (collision.gameObject.name == "Player") // Check if the object entering the collider is named "Player"
        {
            // If it is the player, change the player's parent to this object (the platform).
            // This makes the player move together with the platform.
            collision.gameObject.transform.SetParent(transform);
        }
    }

    // OnTriggerExit2D is called when another object leaves a trigger collider attached to this object.
    // It handles the event when the player exits the platform's area.
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player") // Check if the object exiting the collider is named "Player"
        {
            // If it is the player, remove the parenting relationship.
            // This stops the player from moving together with the platform.
            collision.gameObject.transform.SetParent(null);
        }
    }
}
