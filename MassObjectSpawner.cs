using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MassObjectSpawner : MonoBehaviour
{
    public bool IsBound = false;
    public GameObject SourceObject;
    public MassObjectSpawner ChildObject;
    public bool SpawnObjectsButton = false;
    public bool DeleteObjectsButton = false;
    public Vector3 Extents;

    void SpawnObjects() {
        Vector3 InExtent = SourceObject.GetComponent<Renderer>().bounds.extents * 2.0f;
        Vector3 MaxIn = Vector3.Scale(ChildObject.transform.localPosition, new Vector3(0.5f / SourceObject.GetComponent<Renderer>().bounds.extents.x, 0.5f / SourceObject.GetComponent<Renderer>().bounds.extents.y, 0.5f / SourceObject.GetComponent<Renderer>().bounds.extents.z));
        for(int i = 0; i < (int)Mathf.Floor(MaxIn.x); i++) {
            for(int i2 = 0; i2 < (int)Mathf.Floor(MaxIn.y); i2++) {
                for(int i3 = 0; i3 < (int)Mathf.Floor(MaxIn.z); i3++) {
                    GameObject newObject = GameObject.Instantiate(SourceObject, ChildObject.transform);
                    newObject.transform.position = InExtent + (this.transform.position + this.transform.TransformVector(Vector3.Scale(new Vector3(i, i2, i3), InExtent)));
                    // newObject.GetComponent<RayTracingObject>().BaseColor[0] = new Vector3(Random.Range(0,1.0f), Random.Range(0,1.0f), Random.Range(0,1.0f));
                    // if(Random.Range(0,1.0f) < 0.1f) {
                    //     newObject.GetComponent<RayTracingObject>().IOR[0] = Random.Range(1.05f,2.0f);
                    //     newObject.GetComponent<RayTracingObject>().SpecTrans[0] = 1;
                    //     newObject.GetComponent<RayTracingObject>().Roughness[0] = 0;
                    // } else if(Random.Range(0,1.0f) < 0.1f) {
                    //     newObject.GetComponent<RayTracingObject>().Sheen[0] = Random.Range(0,10.0f);
                    // } else if(Random.Range(0,1.0f) < 0.1f) {
                    //     newObject.GetComponent<RayTracingObject>().emmission[0] = Random.Range(1,12);
                    // } else if(Random.Range(0,1.0f) < 0.1f) {
                    //     newObject.GetComponent<RayTracingObject>().Metallic[0] = Mathf.Min(Random.Range(0,6.0f), 1.0f);
                    //     newObject.GetComponent<RayTracingObject>().Roughness[0] = Random.Range(0,0.5f);
                    // } else if(Random.Range(0,1.0f) < 0.1f) {
                    //     newObject.GetComponent<RayTracingObject>().IOR[0] = Random.Range(1.05f,2.0f);
                    //     newObject.GetComponent<RayTracingObject>().SpecTrans[0] = 1;
                    //     newObject.GetComponent<RayTracingObject>().Roughness[0] = Random.Range(0,0.5f);
                    // } else if(Random.Range(0,1.0f) < 0.1f) {
                    //     newObject.GetComponent<RayTracingObject>().ClearCoat[0] = Random.Range(0,1.0f);
                    //     newObject.GetComponent<RayTracingObject>().ClearCoatGloss[0] = Random.Range(0,1.0f);
                    // }      
                }                
            }
        }
    }


    void DeleteObjects() {
        int ChildCount = this.transform.GetChild(0).childCount;
        for(int i = ChildCount - 1; i >= 0; i--) {
            DestroyImmediate(this.transform.GetChild(0).GetChild(i).gameObject);
        }
    }


    void OnValidate() {
        if(SpawnObjectsButton) {
            SpawnObjects();
            SpawnObjectsButton = false;
        }
        if(DeleteObjectsButton) {
            DeleteObjects();
            DeleteObjectsButton = false;
        }
    }
    void Start() {
        if((this.transform.parent == null || this.transform.parent.gameObject.GetComponent<MassObjectSpawner>() == null) && !(this.transform.childCount != 0 && this.transform.GetChild(0).GetComponent<MassObjectSpawner>() != null)) {
            GameObject TempObject = GameObject.Instantiate(this.gameObject, this.transform);
            ChildObject = TempObject.GetComponent<MassObjectSpawner>();
            ChildObject.IsBound = true;
        }
    }
    void DrawCube(Vector3 Min, Vector3 Max) {
        Gizmos.DrawLine(Min, Min + this.transform.TransformVector(new Vector3(0, 0, Max.z)));
        Gizmos.DrawLine(Min, Min + this.transform.TransformVector(new Vector3(0, Max.y, 0)));
        Gizmos.DrawLine(Min, Min + this.transform.TransformVector(new Vector3(Max.x, 0, 0)));
        Gizmos.DrawLine(Min + this.transform.TransformVector(new Vector3(0, Max.y, 0)), Min + this.transform.TransformVector(new Vector3(Max.x, Max.y, 0)));
        Gizmos.DrawLine(Min + this.transform.TransformVector(new Vector3(Max.x, 0, 0)), Min + this.transform.TransformVector(new Vector3(Max.x, Max.y, 0)));
        Gizmos.DrawLine(Min + this.transform.TransformVector(new Vector3(Max.x, Max.y, 0)), Min + this.transform.TransformVector(new Vector3(Max.x, Max.y, Max.z)));
        Gizmos.DrawLine(Min + this.transform.TransformVector(new Vector3(Max.x, 0, 0)), Min + this.transform.TransformVector(new Vector3(Max.x, 0, Max.z)));
        Gizmos.DrawLine(Min + this.transform.TransformVector(new Vector3(Max.x, 0, Max.z)), Min + this.transform.TransformVector(new Vector3(Max.x, Max.y, Max.z)));
        Gizmos.DrawLine(Min + this.transform.TransformVector(new Vector3(0, 0, Max.z)), Min + this.transform.TransformVector(new Vector3(0, Max.y, Max.z)));
        Gizmos.DrawLine(Min + this.transform.TransformVector(new Vector3(0, Max.y, 0)), Min + this.transform.TransformVector(new Vector3(0, Max.y, Max.z)));
        Gizmos.DrawLine(Min + this.transform.TransformVector(new Vector3(0, Max.y, Max.z)), Min + this.transform.TransformVector(new Vector3(Max.x, Max.y, Max.z)));
        Gizmos.DrawLine(Min + this.transform.TransformVector(new Vector3(0, 0, Max.z)), Min + this.transform.TransformVector(new Vector3(Max.x,0, Max.z)));
    }

    void OnDrawGizmos() {
        if(!IsBound) {
            DrawCube(this.transform.position, ChildObject.transform.localPosition);
            // Gizmos.DrawWireCube((this.transform.position + ChildObject.transform.position) / 2.0f, (ChildObject.transform.localPosition));
        }
        int ChildCount = this.transform.GetChild(0).childCount;
        for(int i = ChildCount - 1; i >= 0; i--) {
            DrawCube(this.transform.GetChild(0).GetChild(i).GetComponent<Renderer>().bounds.center - this.transform.GetChild(0).GetChild(i).GetComponent<Renderer>().bounds.extents, this.transform.GetChild(0).GetChild(i).GetComponent<Renderer>().bounds.extents * 2.0f);
        }
        // Debug.Log((this.transform.position - ChildObject.transform.position));
    }

}
