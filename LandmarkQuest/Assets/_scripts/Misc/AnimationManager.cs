using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public Animator animateTree;
    public GameObject toggleObject;

    void AnimateChop() {
        if (animateTree != null)
        {
            animateTree.SetBool("isTimber", true);
        }
    }

    void ChopFinished() {
        PlayerManager.PlayerInstance.player.SetActive(true);
        gameObject.SetActive(false);
    }

    void ToggleObject()
    {
        if (toggleObject != null)
        {
            toggleObject.SetActive(!toggleObject.activeSelf);
        }
    }
}
