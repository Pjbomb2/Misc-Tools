using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    public float TimeOffset = 1;
    public float Speed = 1;
    public float MaxIntensity = 1;
    public float ChangeSpeed = 1;
    private Light light;
    private float TimeSinceStart = 0;
    void Start() {
        light = this.GetComponent<Light>();
    }

    void FixedUpdate() {
        light.intensity = Mathf.Clamp(Mathf.Sin((TimeOffset * 3.14159f) + (TimeSinceStart += Speed * Time.deltaTime)) * ChangeSpeed, 0, 1) * MaxIntensity;
    }
}
