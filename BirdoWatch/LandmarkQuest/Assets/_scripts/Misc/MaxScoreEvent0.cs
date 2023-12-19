using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxScoreEvent0 : MonoBehaviour
{
    public Animator birdAnim;
    public GameObject timey;
    // Start is called before the first frame update
    void Start() {
        birdAnim.Play("PeakABoo");
        timey.SetActive(false);
    }
}
