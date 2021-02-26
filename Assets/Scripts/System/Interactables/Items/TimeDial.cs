using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeDial : MonoBehaviour {
    
    public float currentAngle;
    public int currentStep;

    private float timeIncrement = 60f;
    private Text _label;
    
    void Start() {
        _label = GetComponent<Text>();
        _label.text = timeIncrement.ToString("n0");
    }

    public void DialChanged(DialInteractable dial) {
        currentAngle = dial.CurrentAngle;
        currentStep = dial.CurrentStep;

        _label.text = $"{timeIncrement * (currentStep + 1)}";
    }
}
