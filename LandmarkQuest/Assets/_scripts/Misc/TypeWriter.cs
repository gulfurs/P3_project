using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    public float typingSpeed = 0.05f;
    private GameObject textField;
    public GameObject doneTypingEvent;

    private string fullText;
    private string currentText = "";
    private TextMeshProUGUI tmpUI;

    void Start()
    {
        if (transform.parent != null)
        {
            textField = transform.parent.gameObject;
        }
        tmpUI = GetComponent<TextMeshProUGUI>();
        fullText = tmpUI.text; 
        tmpUI.text = ""; 
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        foreach (char c in fullText)
        {
            currentText += c;
            tmpUI.text = currentText;   
            yield return new WaitForSeconds(typingSpeed);
        }

        if (doneTypingEvent != null)
        {
            doneTypingEvent.SetActive(true);
            textField.SetActive(false);
        }
    }
}
