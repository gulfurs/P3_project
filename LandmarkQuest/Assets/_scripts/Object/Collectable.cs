using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public string collectSound = "Collect";

    public virtual void Collect()
    {
        //For custom behaviour
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController playerController = other.GetComponent<PlayerController>();

        if (playerController != null)
        {
            Collect();
            AudioManager.instance.Playing(collectSound);
            // Destroy the coin object
            ScoreManager.ScoreInstance.IncreaseScore(1);
            Destroy(gameObject);

            
        }
    }
}
