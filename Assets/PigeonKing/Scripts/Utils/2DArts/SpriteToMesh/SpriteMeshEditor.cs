using UnityEngine;
using UnityEditor;


//SpriteMesh README
//=================

//Created by: MadCUP

//Overview
//--------
//The SpriteMesh script is a Unity component designed to generate a 3D mesh from a 2D sprite. This allows for enhanced control and flexibility in rendering sprites within your Unity projects.

//Usage
//-----
//1. Place the SpriteMesh.cs script in your Unity project's Assets folder.
//2. Use the Unity Editor menu to create a new SpriteMesh GameObject.
//3. Assign a sprite to the SpriteMesh component in the Inspector, and customize its color.

//License
//-------
//This script is provided "as is" without any warranty. You are free to modify and use it in your projects.


// original source is created by MadCUP, I Edit it to generate mesh and material file, and I moved it to the PigeonKingGames namespace for not to conflict with the original source.
namespace PigeonKingGames.Utils.SpriteToMesh
{
    [CustomEditor(typeof(SpriteMesh))]
    public class SpriteMeshEditor : Editor
    {

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUIUtility.labelWidth = 120f;

            if (targets.Length > 1)
            {
                EditorGUILayout.HelpBox("Multi-object editing is supported. Properties may not be editable.", MessageType.Info);
                return;
            }

            SpriteMesh SpriteMesh = (SpriteMesh)target;
            SpriteRenderer spriteRenderer = SpriteMesh.GetComponent<SpriteRenderer>();

            if (spriteRenderer != null)
            {
                EditorGUILayout.HelpBox("SpriteRenderer component detected. Consider converting to mesh.", MessageType.Warning);

                EditorGUILayout.Space();

                if (GUILayout.Button("Convert to Mesh", GUILayout.Height(30f)))
                {
                    SpriteMesh.sprite = spriteRenderer.sprite;

                    Undo.RecordObject(SpriteMesh.gameObject, "Convert to Mesh");
                    DestroyImmediate(spriteRenderer, true);

                    SpriteMesh.Initialize();

                    Debug.Log("Converted Done!!");
                }
            }


        }

        protected override void OnHeaderGUI()
        {
            if (targets.Length > 1)
            {
                GUILayout.Label("SpriteMesh (Multi-edit)", EditorStyles.boldLabel);
            }
            else
            {
                base.OnHeaderGUI();
            }
        }
    }
}
