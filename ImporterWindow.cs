 using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
 using UnityEngine;
 using UnityEditor;
 using System.Xml;
 using System.IO;
         using System.Threading;


public class ImporterWindow : EditorWindow 
{
    [MenuItem("Importer/Importer")]
     public static void ShowWindow() {
         GetWindow<ImporterWindow>("Importer");
     }
     public Object OBJFile;
     private Material BaseMat;


     private Material FindMaterial(Material[] MaterialList, string Name) {
        for(int i = 0; i < MaterialList.Length; i++) {
            if(MaterialList[i].name.Equals(Name)) return MaterialList[i];
        }
        return null;
     }
     private void LoadOBJ() {
        BaseMat = new Material(AssetDatabase.LoadAssetAtPath<Material>("Assets/Editor/DefaultMat.mat"));
  
        Object MTLFile = AssetDatabase.LoadAssetAtPath<Object>(AssetDatabase.GetAssetPath(OBJFile).Replace(".obj", ".mtl"));
        Debug.Log("Assigning Materials For OBJ: " + MTLFile);
        string MaterialPath = AssetDatabase.GetAssetPath(MTLFile).Replace(MTLFile.name + ".mtl", "") + "Materials/";
        Debug.Log("Asset Path: " + MaterialPath);
        string TexturePath = AssetDatabase.GetAssetPath(MTLFile).Replace(MTLFile.name + ".mtl", "") + "Textures/";
        Debug.Log("Asset Path: " + TexturePath);

        List<string> OrigionalName = new List<string>();
        List<string> NewName = new List<string>();
        bool IsNewMaterial = true;
        List<Material> Materials = new List<Material>();
        using(var stream = File.Open(AssetDatabase.GetAssetPath(MTLFile), FileMode.Open)) {
            StreamReader Reader = new StreamReader(stream);
            string CurrentLine = "";
            Material TargetMaterial = BaseMat;
            while((CurrentLine = Reader.ReadLine()) != null) {
                if(CurrentLine.Contains("newmtl")) {
                    Materials.Add(TargetMaterial);
                    TargetMaterial = new Material(BaseMat);
                    TargetMaterial.name = CurrentLine.Replace("newmtl ", "");
                } else if(CurrentLine.Contains("map")) {
                    if(CurrentLine.Contains("map_Kd")) {
                        if(AssetDatabase.LoadAssetAtPath<Texture2D>(TexturePath + CurrentLine.Replace("\tmap_Kd ", "")) != null) TargetMaterial.SetTexture("_MainTex", AssetDatabase.LoadAssetAtPath<Texture2D>(TexturePath + CurrentLine.Replace("\tmap_Kd ", "")) as Texture2D);
                    } else if(CurrentLine.Contains("map_Ks")) {

                    }
                } else if(CurrentLine.Contains("bump")) {
                        if(AssetDatabase.LoadAssetAtPath<Texture2D>(TexturePath + CurrentLine.Replace("\tbump ", "")) != null) TargetMaterial.SetTexture("_BumpMap", AssetDatabase.LoadAssetAtPath<Texture2D>(TexturePath + CurrentLine.Replace("\tbump ", "")) as Texture2D);
                }

            }
        }

        List<Material> NewMaterials = new List<Material>();

        using(var stream = File.Open(AssetDatabase.GetAssetPath(OBJFile), FileMode.Open)) {
            StreamReader Reader = new StreamReader(stream);
            string CurrentLine = "";
            string CurrentGroup = "";
            while((CurrentLine = Reader.ReadLine()) != null) {
                if(CurrentLine.Contains("g ")) CurrentGroup = CurrentLine.Replace("g ", "");
                if(CurrentLine.Contains("usemtl ")) {
                    Material TempMat = FindMaterial(Materials.ToArray(), CurrentLine.Replace("usemtl ", ""));
                    if(TempMat != null) {
                        TempMat = new Material(TempMat);
                        TempMat.name = CurrentGroup + "Mat";
                        NewMaterials.Add(TempMat);
                    }
                } 
            }
        }
        AssetDatabase.StartAssetEditing();
        for(int i = 0; i < NewMaterials.Count; i++) {
            if(AssetDatabase.LoadAssetAtPath<Material>(MaterialPath + NewMaterials[i].name + ".mat") != null) {
                EditorUtility.CopySerialized(new Material(NewMaterials[i]), AssetDatabase.LoadAssetAtPath<Material>(MaterialPath + NewMaterials[i].name + ".mat"));
            } else {
                AssetDatabase.CreateAsset(new Material(NewMaterials[i]), MaterialPath + NewMaterials[i].name + ".mat");
            }
        }
        AssetDatabase.StopAssetEditing();
        AssetDatabase.SaveAssets();
     }

      private void OnGUI() {
        Rect ObjectLabel = new Rect(10,10,(position.width - 10) / 2, 20);
        Rect ObjectField = new Rect(10 + (position.width - 10) / 4 + ((position.width - 10) / 2 - 10),10,(position.width - 10) / 4 - 10, 20);
        EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
                GUILayout.Label("OBJLoader");
                if(GUILayout.Button("Load")) LoadOBJ();
            EditorGUILayout.EndHorizontal();
            OBJFile = EditorGUILayout.ObjectField(OBJFile, typeof(Object), false);
        EditorGUILayout.EndVertical();
      }
void OnInspectorUpdate() {
   Repaint();
}

}
