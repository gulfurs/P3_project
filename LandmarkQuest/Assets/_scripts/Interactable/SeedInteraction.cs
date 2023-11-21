using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedInteraction : Itemize
{
    public GameObject seedMayhem;
    public GameObject timer;

    public override void Interact()
    {
        base.Interact();

        seedMayhem.SetActive(true);
        timer.SetActive(true);
    }
}
