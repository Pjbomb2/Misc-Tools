using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
 using UnityEngine.UIElements;
 using UnityEditor.UIElements;

public class MaterialTexturePairer : EditorWindow
{
     [MenuItem("Utility/MaterialFixer")]
     public static void ShowWindow() {
         GetWindow<MaterialTexturePairer>("Material Texture Pairer");
     }



     public Object TextureFolder;
     public Object MaterialFolder;



 public static int Compute(string s, string t)
    {
        if (string.IsNullOrEmpty(s))
        {
            if (string.IsNullOrEmpty(t))
                return 0;
            return t.Length;
        }

        if (string.IsNullOrEmpty(t))
        {
            return s.Length;
        }

        int n = s.Length;
        int m = t.Length;
        int[,] d = new int[n + 1, m + 1];

        // initialize the top and right of the table to 0, 1, 2, ...
        for (int i = 0; i <= n; d[i, 0] = i++);
        for (int j = 1; j <= m; d[0, j] = j++);

        for (int i = 1; i <= n; i++)
        {
            for (int j = 1; j <= m; j++)
            {
                int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                int min1 = d[i - 1, j] + 1;
                int min2 = d[i, j - 1] + 1;
                int min3 = d[i - 1, j - 1] + cost;
                d[i, j] = Mathf.Min(Mathf.Min(min1, min2), min3);
            }
        }
        return d[n, m];
    }

     private void AssignTextures() {
        string MaterialPath = AssetDatabase.GetAssetPath(MaterialFolder);
        string TexturePath = AssetDatabase.GetAssetPath(TextureFolder);
        Object[] Materials = Resources.LoadAll(MaterialPath.Replace("Assets/Resources/", ""));
        Object[] Textures = Resources.LoadAll(TexturePath.Replace("Assets/Resources/", ""));

        AssetDatabase.StartAssetEditing();
        foreach(var ThisMaterial in Materials) {
            int MinSimilarity = 99999;
            Texture2D ClosestTexture = null;
            string ClosestString = " ";
            Texture2D NormalTex = null;
            foreach(var ThisTexture in Textures) {
                int NewDistance = Compute(ThisMaterial.name, ThisTexture.name);
                if(NewDistance < MinSimilarity) {
                    if(ThisTexture.name.Contains("norm")) {
                        NormalTex = ThisTexture as Texture2D;
                    } else {
                        MinSimilarity = NewDistance;
                        ClosestTexture = ThisTexture as Texture2D;
                        ClosestString = ThisMaterial.name;
                    }
                }
            }
            Material CurrentMat = (ThisMaterial as Material);
            CurrentMat.SetTexture("_MainTex", ClosestTexture);
            CurrentMat.SetTexture("_BumpMap", NormalTex);
            Debug.Log(MaterialPath + ThisMaterial.name + ".mat");
            try {
                EditorUtility.CopySerialized(new Material(CurrentMat), AssetDatabase.LoadAssetAtPath<Material>(MaterialPath + "/" + ThisMaterial.name + ".mat"));
            } catch(System.Exception E) {

            }
        }
        AssetDatabase.StopAssetEditing();
        AssetDatabase.SaveAssets();


     }
     public void CreateGUI() {
        ObjectField MaterialField = new ObjectField("Material Folder");
        MaterialField.objectType = typeof(Object);
        ObjectField TextureField = new ObjectField("Texture Folder");
        TextureField.objectType = typeof(Object);
        Button FixMaterials = new Button(() => AssignTextures()) {text = "Assign Textures To Materials"};
        rootVisualElement.Add(MaterialField);
        rootVisualElement.Add(TextureField);
        rootVisualElement.Add(FixMaterials);

        MaterialField.RegisterValueChangedCallback(evt => {MaterialFolder = evt.newValue;});
        TextureField.RegisterValueChangedCallback(evt => {TextureFolder = evt.newValue;});


    }



}
