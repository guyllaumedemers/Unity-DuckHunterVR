using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScript2 : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    private const string sentence1 = "IN ORDER TO RELEASE A MAGAZINE, PRESS THE\nPRIMARY BUTTON -- (A) OR (X) WHEN YOU ARE HOLDING THE GUN";
    private const string sentence2 = "DEPENDING ON YOUR WEAPON, IT WILL BE RELOADED DIFFERENTLY";
    private const string sentence3 = "IF YOUR WEAPON HAS A MAGAZINE, SIMPLY TAKE THE APPROPRIATE\nMAGAZINE AND INSERT IT IN THE WEAPON'S HANDLE";
    private const string sentence4 = "IF YOUR WEAPON DOES NOT CONTAIN A MAGAZINE (RIFLE, SHOTGUN),\nTAKE AN AMMO BOX AND TOUCH THE AMMO INSERT OF THE WEAPON.";


    private string[] strings = new string[]
    {
        sentence1,
        sentence2,
        sentence3,
        sentence4
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
