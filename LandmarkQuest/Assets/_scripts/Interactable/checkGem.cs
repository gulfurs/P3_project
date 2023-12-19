using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkGem : checkItem
{
    public GameObject gemSpot;
    public GameObject Moth;
    public GameObject lantern;
    public GameObject lightSource;

    public override void CorrectItem()
    {
        base.CorrectItem();

        gemSpot.SetActive(true);
        lantern.SetActive(true);

        if (Moth != null)
        {
            EnemyController enemyController = Moth.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                enemyController.target = lightSource.transform;
            }
        }

        if (lightSource != null)
        {
            Renderer renderer = lightSource.GetComponent<Renderer>();
            if (renderer != null)
            {
                Material material = renderer.material;
                if (material != null)
                {
                    material.EnableKeyword("_EMISSION");
                }
            }
        }

        enabled = false;
    }
}
