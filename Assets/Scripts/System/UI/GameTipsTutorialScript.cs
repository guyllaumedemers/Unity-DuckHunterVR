using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameTipsTutorialScript : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    private const string sentence1 = "SELECT YOUR GAME MODE WITH THE CLIPBOARD";
    private const string sentence2 = "GUN AMMO ARE AVAILABLE IN THE CRATES BEHIND YOU";
    private const string sentence3 = "BATHROOM ARE ON THE LEFT... BUT I WOULDN'T SUGGEST USING IT";
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
        while (!GameManagerScript.Instance.GetGameState)
        {
            yield return new WaitForSeconds(5.0f);
            UpdateSentenceDisplayed();
            DisplayTips();
        }
    }
}
