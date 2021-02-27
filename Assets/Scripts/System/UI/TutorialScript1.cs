using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScript1 : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    private const string sentence1 = "TO START OFF, IN ORDER TO MOVE, POINT YOUR CONTROLLER\nON THE TELEPORTER AND PRESS (JOYSTICK FORWARD)";
    private const string sentence2 = "WELCOME TO DUCK HUNTER!";
    private const string sentence3 = "THESE LITTLE TIPS WILL HELP YOU GET AROUND THE GAME";

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
