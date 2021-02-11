using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClipboardInteractionScript : MonoBehaviour
{
    [SerializeField]
    private Toggle[] toggles;

    public void Awake()
    {
        ActivateRegularModeOnAwake();
    }

    public void SetGameMode(GameManagerScript.GameMode mode)
    {
        GameManagerScript.Instance.GetCurrentMode = mode;
    }

    public void ActivateRegularModeOnAwake()
    {
        Toggle regularModeToggle = GameObject.FindGameObjectWithTag("RegularMode").GetComponent<Toggle>();
        foreach (Toggle t in toggles)
        {
            t.isOn = false;
            if (t.Equals(regularModeToggle))
            {
                t.isOn = true;
            }
        }
    }
}
