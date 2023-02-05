using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableCollider : MonoBehaviour
{

    public Collider2D col;

    public List<GameObject> inRange = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Hair")
        {
            // Add to set of characters that are in range of attack
            inRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Hair")
        {
            // Remove, since it exit the range of the attacking character.
            inRange.Remove(other.gameObject);
        }
    }
}
