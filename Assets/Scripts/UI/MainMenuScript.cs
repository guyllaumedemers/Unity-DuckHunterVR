using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class MainMenuScript : MonoBehaviour
{
    #region Singleton
    private static MainMenuScript instance;
    private MainMenuScript() { }
    public static MainMenuScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new MainMenuScript();
            }
            return instance;
        }
    }
    #endregion
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject[] buttonObjects;
    private Button[] buttons;
    private int currentIndexButton;
    private const int maxIndex = 2;
    private bool isAllowedToSwitchButton;

    public void Awake()
    {
        instance = this;
        buttons = new Button[3];
        for (int i = 0; i < buttonObjects.Length; i++)
        {
            buttons[i] = buttonObjects[i].GetComponent<Button>();
        }
    }

    public void Start()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        currentIndexButton = 0;
        isAllowedToSwitchButton = true;
    }

    public void Update()
    {
        //bool isRightPrimaryButtonTriggered = false;
        //if (XRInputManager.Instance.rightHandController.TryGetFeatureValue(CommonUsages.primaryButton, out isRightPrimaryButtonTriggered) && isRightPrimaryButtonTriggered)
        //{
        //    switch (currentIndexButton)
        //    {
        //        case 0:
        //            LaunchGame();
        //            break;
        //        case 1:
        //            AccessSettings();
        //            break;
        //        case 2:
        //            ExitGame();
        //            break;
        //    }
        //}
        //SwitchBetweenMenuButton();
    }

    public void SwitchBetweenMenuButton()
    {
        Vector2 joystickRetrieve = new Vector2();
        XRInputManager.Instance.leftHandController.TryGetFeatureValue(CommonUsages.primary2DAxis, out joystickRetrieve);
        joystickRetrieve.y = Mathf.Clamp(joystickRetrieve.y, -1, 1);
        // positive value registered by the joystick
        //
        if (joystickRetrieve.y > 0.5f && isAllowedToSwitchButton)
        {
            currentIndexButton++;
            ChangeIndexButtonSelected(); // need to stay inside to prevent to switch between button insanly fast
        }
        // negative value registered by the joystick
        //
        else if (joystickRetrieve.y < -0.5f && isAllowedToSwitchButton)
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
        buttons[currentIndexButton].Select();
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

    public void GoBack()
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

    public GameObject GetMainMenuUI { get => mainMenu; }
    public GameObject GetSettingsMenuUI { get => settingsMenu; }
}
