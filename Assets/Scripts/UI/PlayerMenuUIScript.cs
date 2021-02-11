using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class PlayerMenuUIScript : MonoBehaviour
{
    [SerializeField]
    private GameObject inGameMenuUI;
    private bool isGameSceneActive;
    public void Awake()
    {
        isGameSceneActive = false;
        isGameSceneActive = IsInGameScene();
    }

    public void Update()
    {
        if (!isGameSceneActive)
        {
            isGameSceneActive = IsInGameScene();
        }
        // this cannot be triggered at every update => it need to be triggered during an event
        ActivateInGameMenuUI();
    }

    public bool IsInGameScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.IsValid() && currentScene.name.Equals("GameScene"))
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
