using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public float totalTime = 10f;
    private float currentTime;

    public TextMeshProUGUI textUI;
    public TextMeshPro textGame;

    public Animator retryButton;
    public GameObject SEEDS;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = totalTime;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateUI();
        }
        else
        {
            TimesUp();
        }
    }

    void UpdateUI()
    {
        if (textUI != null)
        {
            textUI.text = currentTime.ToString("F0");
        }
        else if (textGame != null)
        {
            textGame.text = currentTime.ToString("F0");
        }
    }

    public virtual void TimesUp() {
        //This method will be overwritten (virtual void)
        if (SEEDS != null)
        SEEDS.SetActive(false);

        if (retryButton != null)
            retryButton.Play("BlinkingLights");

    }
}
