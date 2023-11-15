using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class checkItem : Interactable
{
    public Sprite itemCheck;
    public GameObject disclaimerText;
    public string disclaimer;

    public virtual void CorrectItem()
    {
        //This method will be overwritten (virtual void)
    }

    public virtual void WrongItem()
    {
        //This method will be overwritten (virtual void)
    }

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
                    if (inventory.GetComponent<SpriteRenderer>().sprite == itemCheck)
                    {
                        inventory.GetComponent<SpriteRenderer>().sprite = null;
                        CorrectItem();
                    }
                    else
                    {
                        disclaimerText.GetComponent<TextMeshProUGUI>().text = disclaimer;
                        disclaimerText.GetComponent<Animator>().Play("FadeInOutText");
                        WrongItem();
                    }
                }
            }
        }
    }
}
