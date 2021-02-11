using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class PlayerMenuUIScript : MonoBehaviour
{
    #region Singleton
    /// <summary>
    /// Instance
    /// </summary>
    private static PlayerMenuUIScript instance;
    /// <summary>
    /// Default Constructor
    /// </summary>
    private PlayerMenuUIScript() { }
    /// <summary>
    /// Property to retrieve instance
    /// </summary>
    public static PlayerMenuUIScript Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerMenuUIScript();
            }
            return instance;
        }
    }
    #endregion
    [SerializeField]
    private GameObject inGameMenuUI;
    private const string gameSceneName = "gdemersTestScene";

    public bool IsInGameScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.IsValid() && currentScene.name.Equals(gameSceneName))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ActivateInGameMenuUI()
    {
        bool isLeftMenuButtonPressed = false;
        if (XRInputManager.Instance.leftHandController.TryGetFeatureValue(CommonUsages.menuButton, out isLeftMenuButtonPressed) && isLeftMenuButtonPressed)
        {
            inGameMenuUI.SetActive(!inGameMenuUI.activeSelf);
        }
    }
}
