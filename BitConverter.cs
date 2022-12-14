using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BitConverter : MonoBehaviour
{
    public class EditModeFunctions : EditorWindow {
     [MenuItem("BitConverter/BitConverter Settings")]
     public static void ShowWindow() {
         GetWindow<EditModeFunctions>("BitConverter Settings");
     }
     public Mesh AssetObject;

        private void OnGUI() {
            Rect ConvertButton = new Rect(10,30,(position.width - 10) / 2 - 10, 20);
            AssetObject = (Mesh) EditorGUILayout.ObjectField("Asset", AssetObject, typeof (Mesh), false);
            if(GUI.Button(ConvertButton, "Convert Index")) {
                int SubMeshCount = AssetObject.subMeshCount;
                List<int>[] Indexes = new List<int>[SubMeshCount];
                for(int i = 0; i < SubMeshCount; i++) {
                    int[] SubMeshIndexes = AssetObject.GetIndices(i);
                    int IndexCount = SubMeshIndexes.Length;
                    Indexes[i] = new List<int>(SubMeshIndexes);
                }
                AssetObject.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
                AssetObject.subMeshCount = SubMeshCount;
                for(int i = 0; i < SubMeshCount; i++) {
                    AssetObject.SetIndices(Indexes[i], MeshTopology.Triangles, i);
                }

                Debug.Log(AssetObject);
            }
        }

}

}
