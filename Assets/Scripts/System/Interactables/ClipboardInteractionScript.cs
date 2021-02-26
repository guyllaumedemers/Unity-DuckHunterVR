using System;
using UnityEngine;
using UnityEngine.UI;

public class ClipboardInteractionScript : MonoBehaviour
{
    [Header("Requiered Components")]
    [SerializeField] private Toggle[] toggles;
    public GameObject timeDial;
    
    public void Awake()
    {
        // set delegates to look for Toggle value Changes
        foreach (Toggle t in toggles)
            t.onValueChanged.AddListener((a) => { ToggleValueChanged(t); });
        
        GameObject.FindGameObjectWithTag(GameMode.Mode.REGULARMODE.GetDescription()).GetComponent<Toggle>().isOn = true;
    }

    private void ToggleValueChanged(Toggle toggle) {
        GameManager.Instance.CurrentMode = toggle.tag.ToEnum<GameMode.Mode>();

        if (GameManager.Instance.CurrentMode == GameMode.Mode.TIMEDROUND)
            timeDial.SetActive(true);
        else
            timeDial.SetActive(false);

        foreach (Toggle t in toggles) {
            if (!t.Equals(toggle))
                t.SetIsOnWithoutNotify(false);
        }
    }
}
