using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDeleter : MonoBehaviour
{
    public float TimeUntilDeletion;
    private float TimeSinceStart = 0;

    void FixedUpdate() {
        if((TimeSinceStart += (Time.deltaTime)) > TimeUntilDeletion) Destroy(this.gameObject);
    }
}
