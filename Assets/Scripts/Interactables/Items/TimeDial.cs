using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeDial : MonoBehaviour {
    
    Text _Label;
    
    void Start() {
        _Label = GetComponent<Text>();
    }

    public void DialChanged(DialInteractable dial) {
        //_Label.text = (dial.CurrentAngle).ToString("n0");
        _Label.text = (dial.CurrentStep).ToString("n0");
    }
}
