using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Tutorial : MonoBehaviour
{
    [SerializeField] float waitTime;
    public bool hasMoved = false;
    public bool hasJumped = false;
    public bool hasAttacked = false;
    int I = 0;
    // [SerializeField] Slider healthSlider;
    // [SerializeField] Health playerHealth;

    [Header("Tutorial")]
    [SerializeField] TextMeshProUGUI tutorialText;
    [SerializeField] String[] TutorialTexts;
    void Start()
    {
        tutorialText.text = TutorialTexts[0];
        I++;
        StartCoroutine(WaitForNext());
    }

    IEnumerator WaitForNext()
    {
        yield return new WaitForSeconds(waitTime);
        tutorialText.text = TutorialTexts[1];
    }

    void Update()
    {
        if (hasMoved)
        {
            tutorialText.text = TutorialTexts[2];
        }
        if (hasJumped && hasMoved)
        {
            tutorialText.text = TutorialTexts[3];
        }
        if (hasAttacked && hasJumped && hasMoved)
        {
            tutorialText.text = TutorialTexts[4];
        }
    }
}
