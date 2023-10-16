using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] GameObject pressPlate;
    [SerializeField] bool isPressed = false;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "presser") 
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            if (!isPressed)
            {
                isPressed = true;
                if (renderer != null){renderer.material.color = Color.blue;}
            }
        }
    }
}
