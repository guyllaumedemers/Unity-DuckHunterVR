using System;
using UnityEngine;
using UnityEngine.UI;

public class TimeDial : MonoBehaviour {
    
    public float timeIncrement = 60f;
    public float currentTime;
    
    private float _currentAngle;
    private int _currentStep;
    private Text _label;

    private void Awake() {
        _label = GetComponent<Text>();
        _label.text = timeIncrement.ToString("n0");
        currentTime = timeIncrement;
    }

    public void DialChanged(DialInteractable dial) {
        _currentAngle = dial.CurrentAngle;
        _currentStep = dial.CurrentStep;

        currentTime = timeIncrement * (_currentStep + 1);
        _label.text = $"{currentTime:n0}";
    }
}
