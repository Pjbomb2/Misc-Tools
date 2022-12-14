using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FountainCreator : MonoBehaviour
{
    public GameObject SourceObject;
    private Vector3 InitialPosition;
    public float SpawnSpeed = 0.7f;
    public float TimeSinceLastSpawn = 0;
    public int MaxObjects = 50;
    public Vector3 ForceMagnitudes = new Vector3(1,1,1);
    public bool DeleteAfterTime = false;
    void Start() {
        InitialPosition = this.transform.position;
    }
    void FixedUpdate() {
        if(this.transform.childCount < MaxObjects && (TimeSinceLastSpawn += Time.deltaTime) > SpawnSpeed) {
            TimeSinceLastSpawn = 0;
            GameObject newObject = GameObject.Instantiate(SourceObject, this.transform);
            newObject.transform.localPosition = new Vector3(0,0,0);
            if(DeleteAfterTime) {
                newObject.AddComponent<TimedDeleter>();
                newObject.GetComponent<TimedDeleter>().TimeUntilDeletion = 4.0f;
            }
            Vector2 RandomizedUnitCircle = Random.insideUnitCircle;
            newObject.GetComponent<Rigidbody>().velocity = Vector3.Scale(new Vector3(RandomizedUnitCircle.x, 1, RandomizedUnitCircle.y), ForceMagnitudes);
            newObject.GetComponent<RayTracingObject>().BaseColor[0] = new Vector3(Random.Range(0,1.0f), Random.Range(0,1.0f), Random.Range(0,1.0f));
            if(Random.Range(0,1.0f) < 0.1f) {
                newObject.GetComponent<RayTracingObject>().IOR[0] = Random.Range(1.05f,2.0f);
                newObject.GetComponent<RayTracingObject>().SpecTrans[0] = 1;
                newObject.GetComponent<RayTracingObject>().Roughness[0] = 0;
            } else if(Random.Range(0,1.0f) < 0.1f) {
                newObject.GetComponent<RayTracingObject>().Sheen[0] = Random.Range(0,10.0f);
            } else if(Random.Range(0,1.0f) < 0.1f) {
                newObject.GetComponent<RayTracingObject>().emmission[0] = Random.Range(1,12);
            } else if(Random.Range(0,1.0f) < 0.1f) {
                newObject.GetComponent<RayTracingObject>().Metallic[0] = Mathf.Min(Random.Range(0,6.0f), 1.0f);
                newObject.GetComponent<RayTracingObject>().Roughness[0] = Random.Range(0,0.5f);
            } else if(Random.Range(0,1.0f) < 0.1f) {
                newObject.GetComponent<RayTracingObject>().IOR[0] = Random.Range(1.05f,2.0f);
                newObject.GetComponent<RayTracingObject>().SpecTrans[0] = 1;
                newObject.GetComponent<RayTracingObject>().Roughness[0] = Random.Range(0,0.5f);
            } else if(Random.Range(0,1.0f) < 0.1f) {
                newObject.GetComponent<RayTracingObject>().ClearCoat[0] = Random.Range(0,1.0f);
                newObject.GetComponent<RayTracingObject>().ClearCoatGloss[0] = Random.Range(0,1.0f);
            }


        }
    }
}
