using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
 using UnityEngine.UIElements;


public class TriSelectorEditor : EditorWindow {
   [MenuItem("Editor/Tri SElector")]
   public static void ShowWindow() {
       GetWindow<TriSelectorEditor>("Triangle Selector");
   }


//    public ComputeBuffer BestHitBuffer;
//    public ComputeShader Shader;
//    public AssetManager Assets;
//    public struct BestHitData {
//     public float t;
//     public int MatIndex;
// }
// BestHitData[] BestHit;
// Vector3 RayDir;
// Vector3 RayOrig;
// void Start() {
//     BestHit = new BestHitData[1];
//     BestHitBuffer = new ComputeBuffer(1, 8);
//     Shader = Resources.Load<ComputeShader>("Utility/SingleTrace"); 
//     Assets = GameObject.Find("Scene").GetComponent<AssetManager>();
// }
// void DoTheTHing()
// {
//     Start();
//     if(Assets.TLASBuilt) {
//         RayOrig = Camera.main.transform.position;
//         RayDir = Camera.main.transform.forward;
//         Shader.SetVector("RayOrig", RayOrig);
//         Shader.SetVector("RayDir", RayDir);
//         Shader.SetBuffer(0, "cwbvh_nodes", Assets.BVH8AggregatedBuffer);
//         ComputeBuffer MeshDataBuffer = new ComputeBuffer(Assets.MyMeshesCompacted.Count, 168);
//         MeshDataBuffer.SetData(Assets.MyMeshesCompacted);
//         Shader.SetBuffer(0, "_MeshData", MeshDataBuffer);
//         Shader.SetBuffer(0, "Output", BestHitBuffer);
//         Shader.SetBuffer(0, "AggTris", Assets.AggTriBuffer);
//         Shader.Dispatch(0, 1, 1, 1);
//         BestHitBuffer.GetData(BestHit);
//         Debug.Log(BestHit[0].t);
//         Selection.objects = new Object[] {Assets.MaterialToRayObject[BestHit[0].MatIndex].RayObject.gameObject};
//         Debug.Log(Assets.MaterialToRayObject[BestHit[0].MatIndex].MaterialOffset);
//         Selection.activeGameObject.GetComponent<RayTracingObject>().Selected = Assets.MaterialToRayObject[BestHit[0].MatIndex].MaterialOffset;
//     }       
// }

// public void CreateGUI() {
//     Button BVHBuild = new Button(() => DoTheTHing()) {text = "Do the THing"};
//     rootVisualElement.Add(BVHBuild);
// }

// void OnDrawGizmos() {
//     Gizmos.DrawSphere(RayOrig + RayDir * BestHit[0].t, 0.5f);
//     Gizmos.DrawSphere(RayOrig + RayDir * 1, 0.5f);
// }




}
