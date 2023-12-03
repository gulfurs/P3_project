using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemize : Interactable
{
    public Sprite newItem;
    public bool noDestroy = false;
    public bool RandomItem = false; // Add a boolean for random item selection
    public Sprite[] randomItems; // Array of random items

    public override void Interact()
    {
        base.Interact();

        PlayerController player = FindObjectOfType<PlayerController>();

        if (player != null)
        {
            Transform inventory = player.transform.Find("Inventory");

            if (inventory != null)
            {
                if (inventory.GetComponent<SpriteRenderer>() != null)
                {
                    if (RandomItem && randomItems.Length > 0)
                    {
                        int randomIndex = Random.Range(0, randomItems.Length);
                        inventory.GetComponent<SpriteRenderer>().sprite = randomItems[randomIndex];
                    }
                    else
                    {
                        inventory.GetComponent<SpriteRenderer>().sprite = newItem;
                    }

                    if (!noDestroy)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
