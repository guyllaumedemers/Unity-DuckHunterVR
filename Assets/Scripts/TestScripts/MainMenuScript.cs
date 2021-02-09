using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;

    public void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene("gdemersTestScene", LoadSceneMode.Single);
    }

    public void AccessSettings()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        settingsMenu.SetActive(!settingsMenu.activeSelf);
    }

    public void ExitGame()
    {
        Debug.Log("Application.Quit");
        Application.Quit();
    }
}
