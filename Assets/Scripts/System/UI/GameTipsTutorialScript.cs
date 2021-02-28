using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTipsTutorialScript : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    private const string sentence1 = "THIS WILL PROVIDE YOU WITH A GENERAL OVERVIEW\nOF THE GAME AND CONTROLS";
    private const string sentence2 = "WELCOME TO DUCK HUNT VR!";
    private const string sentence3 = "IN ORDER TO KNOW HOW THE GAME WORKS, PRESS THE\n-MENU BUTTON- ON THE LEFT CONTROLLER AND SELECT README";
    private string[] strings = new string[]
    {
        sentence1,
        sentence2,
        sentence3
    };
    private int currentIndex;
    public void Awake()
    {
        currentIndex = 0;
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Start()
    {
        StartCoroutine(UpdateIndex());
    }

    public void DisplayTips()
    {
        textMeshProUGUI.text = strings[currentIndex];
    }

    public void UpdateSentenceDisplayed()
    {
        currentIndex++;
        if (currentIndex == strings.Length)
        {
            currentIndex = 0;
        }
    }

    private IEnumerator UpdateIndex()
    {
        while (!GameManager.Instance.GetGameState)
        {
            yield return new WaitForSeconds(5.0f);
            UpdateSentenceDisplayed();
            DisplayTips();
        }
    }
}
