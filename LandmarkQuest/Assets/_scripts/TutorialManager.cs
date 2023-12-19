using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public float belowYaw, aboveYaw;
    private CameraControl cameraCTRL;
    private GameObject player;
    public int tutorialStep = 0; // Add type declaration 'int' here

    public GameObject[] tutorialText;
    public GameObject tutorialParticle1, tutorialParticle2, tutorialParticle3, tutorialParticle4;
    public GameObject objectif;

    // Start is called before the first frame update
    void Start()
    {
        cameraCTRL = GetComponent<CameraControl>();
        player = PlayerManager.PlayerInstance.player;

        player.GetComponent<PlayerController>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (tutorialStep)
        {
            case 0:
                if (cameraCTRL != null && cameraCTRL.currentYaw > belowYaw && cameraCTRL.currentYaw < aboveYaw)
                {
                    player.GetComponent<PlayerController>().enabled = false;
                }
                else
                {
                    player.GetComponent<PlayerController>().enabled = true;
                    if (tutorialText[0] != null)
                    {
                        tutorialText[0].SetActive(false);
                        tutorialText[1].SetActive(true);
                        tutorialStep++;
                    }
                }
                break;
            case 1:
                if (tutorialText[2].activeSelf)
                    if (tutorialParticle1 != null)
                    tutorialParticle1.SetActive(true);

                    if (tutorialParticle1 == null) {
                        tutorialText[2].SetActive(false);
                        tutorialText[3].SetActive(true);
                        tutorialStep = 2;
                    }
                    break;
            case 2:
                if (tutorialText[4].activeSelf)
                    if (tutorialParticle2 != null)
                        tutorialParticle2.SetActive(true);

                if (tutorialParticle2 == null)
                {
                    tutorialText[4].SetActive(false);
                    tutorialText[5].SetActive(true);
                    tutorialStep = 3;
                }
                break;
            case 3:
                if (tutorialText[6].activeSelf)
                    if (tutorialParticle3 != null)
                        tutorialParticle3.SetActive(true);

                if (tutorialParticle3 == null)
                {
                    tutorialText[6].SetActive(false);
                    tutorialText[7].SetActive(true);
                    tutorialStep = 4;
                }
                break;
            case 4:
                if (tutorialText[8].activeSelf)
                {
                    if (tutorialParticle4 != null)
                    {
                        tutorialParticle4.SetActive(true);
                        if (objectif.GetComponent<ObjectiveInteraction>().isFlying)
                        {
                            tutorialParticle4.SetActive(false);
                            tutorialText[9].SetActive(true);
                        }
                    }
                }
                break;
        }
    }
}
