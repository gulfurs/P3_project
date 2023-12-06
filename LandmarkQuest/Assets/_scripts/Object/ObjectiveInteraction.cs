using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectiveInteraction : Interactable
{
    public Animator animator;
    public string checkRadius = "WithinRadius";
    public bool isCondition = false;

    public float flySpeed = 10.0f;
    public bool isFlying = false;

    public Sprite itemCheck;
    public GameObject disclaimerText;
    public string disclaimer;

    public override void Interact()
    {
        base.Interact();

        if (!isCondition)
        {
            animator.SetBool(checkRadius, true);
            isFlying = true;
        }
        else
        {
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
                            animator.SetBool(checkRadius, true);
                            isFlying = true;
                        }
                        else
                        {
                            disclaimerText.GetComponent<TextMeshProUGUI>().text = disclaimer;
                            disclaimerText.GetComponent<Animator>().Play("FadeInOutText");
                        }
                    }
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (isFlying)
        {
            if (transform.childCount > 0)
            {
               transform.GetChild(0).Translate(Vector3.up * flySpeed * Time.deltaTime);
            }
        }
    }
}
