using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomEditor(typeof(GridGeneration))]
public class GridGenerationInspector : Editor
{
    
    public GridGeneration mainGeneration;
//
//    public GunType haha;

    public void OnEnable()
    {
        mainGeneration = (GridGeneration)target;
    }

    public override void OnInspectorGUI()
    {
//        test.fireTemplate = (GunType)EditorGUILayout.EnumPopup("Gun Type", test.fireTemplate);
//
//        if (test.fireTemplate == GunType.AssaultRifle)
//        {
//            EditorGUILayout.PropertyField(serializedObject.FindProperty("fireDelay"));
//            EditorGUILayout.PropertyField(serializedObject.FindProperty("recoil"));
//            EditorGUILayout.PropertyField(serializedObject.FindProperty("spread"));
//            EditorGUILayout.PropertyField(serializedObject.FindProperty("force"));
//            EditorGUILayout.PropertyField(serializedObject.FindProperty("bulletPrefab"));
//            serializedObject.ApplyModifiedProperties();
//        }

        if (GUILayout.Button("Build Grid"))
        {
            mainGeneration.CreateGrid();
        }

        if (GUILayout.Button("Delete Grid"))
        {
            mainGeneration.DestroyGrid();
        }

        if (GUILayout.Button("Reset Grid"))
        {
            mainGeneration.DestroyGrid();
            mainGeneration.CreateGrid();
        }

		if (GUILayout.Button ("Show Tiles")) 
		{ 
			mainGeneration.ShowTiles (); 
		}

		EditorGUILayout.PropertyField(serializedObject.FindProperty("tile_Tex"), true);
            
        EditorGUILayout.PropertyField(serializedObject.FindProperty("tileSize"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("tileHeight"));
        serializedObject.ApplyModifiedProperties();
    }

}