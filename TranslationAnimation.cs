using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationAnimation : MonoBehaviour
{
    public enum Options {X, Y, Z};
    public Options RotationAxis;
    public float Speed;
    public float Distance;
    private float TimeSinceStart = 0;
    private Vector3 InitialPosition;
    void Start() {
        InitialPosition = this.transform.position;
    }
    void FixedUpdate()
    {
        this.transform.position = InitialPosition + ((RotationAxis == Options.Z ? Vector3.forward : (RotationAxis == Options.Y ? Vector3.up : Vector3.right)) * Distance * Mathf.Cos((TimeSinceStart += (Speed * Time.deltaTime))));
    }
}
