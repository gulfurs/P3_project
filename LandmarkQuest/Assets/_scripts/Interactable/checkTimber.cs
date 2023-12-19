using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkTimber : checkItem
{
    public GameObject choppingAnim;
    public override void CorrectItem()
    {
        base.CorrectItem();
        choppingAnim.SetActive(true);
        PlayerManager.PlayerInstance.player.SetActive(false);
        choppingAnim.GetComponent<Animator>().Play("Chopping");
        enabled = false;
    }
}
