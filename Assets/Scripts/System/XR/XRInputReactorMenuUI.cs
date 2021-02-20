using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRInputReactorMenuUI : MonoBehaviour
{
    [SerializeField]
    private XRInputWatcher _xRInputWatcherLeft;
    /// <summary>
    /// Testing purposes only => show in inspector
    /// </summary>
    [SerializeField]
    private bool _menuIsPressed = false;

    private void Start()
    {
        AssignWatcher();
    }

    public void onMenuButtonEvent(bool pressed)
    {
        _menuIsPressed = pressed;
        if (pressed)
        {
            if (PlayerMenuUIScript.Instance.IsInGameScene() && PlayerMenuUIScript.Instance.IsSettingsMenuUIActive() == false && PlayerMenuUIScript.Instance.IsStatsMenuUIActive() == false)
            {
                PlayerMenuUIScript.Instance.ActivateInGameMenuUI();
            }
        }
    }

    public void AssignWatcher()
    {
        _xRInputWatcherLeft = GameObject.FindGameObjectWithTag("LeftController").GetComponent<XRInputWatcher>();
        _xRInputWatcherLeft.menuButtonPressEvent.AddListener(onMenuButtonEvent);
    }
}
