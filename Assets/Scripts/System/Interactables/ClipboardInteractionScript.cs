using System;
using UnityEngine;
using UnityEngine.UI;

public class ClipboardInteractionScript : MonoBehaviour
{
    [Header("Requiered Components")]
    [SerializeField] private Toggle[] toggles;
    public GameObject timeDial;
       
    public void Awake() {
        // set delegates to look for Toggle value Changes
        foreach (Toggle t in toggles)
            t.onValueChanged.AddListener((a) => { ToggleValueChanged(t); });
        
        GameObject.FindGameObjectWithTag(GameMode.Mode.REGULARMODE.GetDescription()).GetComponent<Toggle>().isOn = true;
        GameManager.Instance.timedRoundTime = timeDial.GetComponentInChildren<TimeDial>().currentTime;
        timeDial.SetActive(false);
    }

    private void OnEnable() {
        if (GameManager.Instance.CurrentMode == GameMode.Mode.TIMEDROUND)
            EnableTimeDial();
    }

    private void OnDisable() {
        if (GameManager.Instance.CurrentMode == GameMode.Mode.TIMEDROUND)
            DisableTimeDial();
    }

    private void ToggleValueChanged(Toggle toggle) {
        GameManager.Instance.CurrentMode = toggle.tag.ToEnum<GameMode.Mode>();

        foreach (Toggle t in toggles) {
            if (!t.Equals(toggle)) {
                t.SetIsOnWithoutNotify(false);
            }
        }
        
        if (toggle.tag.ToEnum<GameMode.Mode>() == GameMode.Mode.TIMEDROUND && toggle.isOn)
            EnableTimeDial();
        else
            DisableTimeDial();
    }
    
    private void EnableTimeDial() {
        if (timeDial != null) {
            timeDial.SetActive(true);
            GameManager.Instance.timedRoundTime = timeDial.GetComponentInChildren<TimeDial>().currentTime;
        }
    }
    
    private void DisableTimeDial() {
        if (timeDial != null) {
            GameManager.Instance.timedRoundTime = timeDial.GetComponentInChildren<TimeDial>().currentTime;
            timeDial.SetActive(false);
        }
    }
}
