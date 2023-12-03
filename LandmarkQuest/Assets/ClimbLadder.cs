using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbLadder : Interactable
{
    public bool goingUp = true;
    public GameObject climbingAnim;
    public override void Interact()
    {
        base.Interact();
        climbingAnim.SetActive(true);
        PlayerManager.PlayerInstance.player.SetActive(false);
        if (goingUp)
        {
            climbingAnim.GetComponent<Animator>().Play("ClimbingUp");
        }
        else {
            climbingAnim.GetComponent<Animator>().Play("ClimbingDown");
        }
        //enabled = false;
    }
}
