using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject[] buttons;
    private int currentIndexButton;
    private const int maxIndex = 2;
    private bool isAllowedToSwitchButton;

    public void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        currentIndexButton = 0;
        isAllowedToSwitchButton = true;
        buttons[currentIndexButton].GetComponent<Button>().Select();
    }

    public void Update()
    {
        SwitchBetweenMenuButton();
    }

    public void SwitchBetweenMenuButton()
    {
        float yInput = Input.GetAxisRaw("Vertical");
        yInput = Mathf.Clamp(yInput, -1, 1);
        // positive value registered by the joystick
        //
        if (yInput > 0.5f && isAllowedToSwitchButton)
        {
            currentIndexButton++;
            ChangeIndexButtonSelected(); // need to stay inside to prevent to switch between button insanly fast
        }
        // negative value registered by the joystick
        //
        else if (yInput < -0.5f && isAllowedToSwitchButton)
        {
            currentIndexButton--;
            ChangeIndexButtonSelected();
        }
    }
    /// <summary>
    /// Check if the value registered by the joystick is positive or negative. Depending on the result we check to see if we increment the index of the button select or not
    /// </summary>
    /// <param name="isPositive"></param>
    public void ChangeIndexButtonSelected()
    {
        if (currentIndexButton > maxIndex)
        {
            currentIndexButton = 0;
        }
        else if (currentIndexButton < 0)
        {
            currentIndexButton = maxIndex;
        }
        buttons[currentIndexButton].GetComponent<Button>().Select();
        StartCoroutine(DelayJoystickToggleBetweenButtons());
    }

    private IEnumerator DelayJoystickToggleBetweenButtons()
    {
        isAllowedToSwitchButton = false;
        yield return new WaitForSeconds(0.5f);
        isAllowedToSwitchButton = true;
    }

    #region OnClick Event interaction
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
    #endregion
}
