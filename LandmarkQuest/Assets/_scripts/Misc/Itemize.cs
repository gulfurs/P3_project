using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemize : Interactable
{
    public Sprite newItem;
    public bool noDestroy = false;

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
                    inventory.GetComponent<SpriteRenderer>().sprite = newItem;
                    if (!noDestroy)
                    {
                        Destroy(gameObject);
                    }
                }
            }
        }
    }
}
