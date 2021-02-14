using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TipsScript : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    private const string sentence1 = "SELECT YOUR GAME MODE WITH THE CLIPBOARD";
    private const string sentence2 = "GUN AMMO ARE AVAILABLE IN THE CRATES BEHIND YOU";
    private const string sentence3 = "BATHROOM ARE ON THE LEFT... BUT I WOULDN'T SUGGEST USING IT";
    private string[] strings = new string[3];
    private int currentIndex;
    private const int maxIndex = 2;
    private bool yield;
    public void Awake()
    {
        currentIndex = 0;
        yield = false;
        strings[0] = sentence1;
        strings[1] = sentence2;
        strings[2] = sentence3;
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public void Start()
    {
        StartCoroutine(UpdateIndex());
    }

    public void Update()
    {
        if (yield)
        {
            UpdateSentenceDisplayed();
            StartCoroutine(UpdateIndex());
        }
        DisplayTips();
    }

    public void DisplayTips()
    {
        textMeshProUGUI.text = strings[currentIndex];
    }

    public void UpdateSentenceDisplayed()
    {
        currentIndex++;
        if (currentIndex > maxIndex)
        {
            currentIndex = 0;
        }
    }

    private IEnumerator UpdateIndex()
    {
        yield = false;
        yield return new WaitForSeconds(5.0f);
        yield = true;
    }
}
