using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] GameObject pressPlate;
    [SerializeField] GameObject objectToMove;
    [SerializeField] bool isPressed = false;
    [SerializeField] int distanceX;
    [SerializeField] int distanceY;
    [SerializeField] int distanceZ;
    //[SerializeField] float speedOfMove = 5f;
    Vector3 moveDistance;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "presser" || col.tag == "Player") 
        {
            MeshRenderer renderer = GetComponent<MeshRenderer>();
            
            if (!isPressed)
            {
                isPressed = true;
                if (renderer != null){renderer.material.color = Color.blue;}
                moveDistance = new Vector3(distanceX, distanceY ,distanceZ);
                if (objectToMove != null) {objectToMove.transform.Translate(moveDistance, Space.World);}
            }
        }
    }
}
