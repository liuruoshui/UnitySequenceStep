using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering;


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
namespace PigeonKingGames.Steps
{
    [ExecuteAlways]
    public class SpriteMesh : MonoBehaviour
    {
        public Sprite sprite;
        public Color color = Color.white;

        private Mesh mesh;
        private Material material;

        public void Initialize()
        {
            UpdateMesh();
        }


        void UpdateMesh()
        {
            // Early exit if sprite or its required properties are null
            if (sprite == null || sprite.vertices == null || sprite.uv == null || sprite.triangles == null)
            {
                return;
            }

            // Create or update material
            if (material == null)
            {
                material = CreateMaterialBasedOnRenderPipeline();
            }

            // Create or update mesh
            if (mesh == null)
            {
                mesh = new Mesh { name = "Generated Mesh" };
            }
            else
            {
                mesh.Clear();
            }

            // Initialize arrays
            Vector3[] vertices = new Vector3[sprite.vertices.Length];
            Vector2[] uvs = new Vector2[sprite.uv.Length];
            int[] triangles = new int[sprite.triangles.Length];

            // Convert sprite data to mesh data
            for (int i = 0; i < sprite.vertices.Length; i++)
            {
                vertices[i] = sprite.vertices[i];  // Assuming sprite.vertices are Vector3
            }

            for (int i = 0; i < sprite.uv.Length; i++)
            {
                uvs[i] = sprite.uv[i];  // Copy UVs
            }

            for (int i = 0; i < sprite.triangles.Length; i++)
            {
                triangles[i] = sprite.triangles[i];  // Copy triangles
            }

            // Assign vertices, UVs, and triangles to the Mesh
            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = triangles;

            // Recalculate normals for proper lighting
            mesh.RecalculateNormals();

            // Ensure MeshFilter and MeshRenderer components exist
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            if (meshFilter == null)
            {
                meshFilter = gameObject.AddComponent<MeshFilter>();
            }
            meshFilter.mesh = mesh;

            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer == null)
            {
                meshRenderer = gameObject.AddComponent<MeshRenderer>();
            }

            meshRenderer.sharedMaterial = material; // Assign the material to MeshRenderer

            // Assign texture and color to material, only if sprite.texture is not null
            if (sprite.texture != null)
            {
                material.mainTexture = sprite.texture;
            }
            else
            {
                material.mainTexture = null;
            }
            material.color = color;
            material.SetFloat("_Smoothness", 0f);
            SaveMeshAndMaterial();
            RemoveThis();
        }

        void SaveMeshAndMaterial()
        {
            //获取当前mesh和material
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            //获取sprite的文件路径
            string spritePath = AssetDatabase.GetAssetPath(sprite);
            //获取sprite的文件名
            string spriteName = System.IO.Path.GetFileNameWithoutExtension(spritePath);
            //创建文件夹
            string folderPath = System.IO.Path.GetDirectoryName(spritePath) + "/" + spriteName;
            if (!System.IO.Directory.Exists(folderPath))
            {
                System.IO.Directory.CreateDirectory(folderPath);
            }
            //保存mesh
            string meshPath = folderPath + "/" + spriteName + ".asset";
            AssetDatabase.CreateAsset(meshFilter.sharedMesh, meshPath);
            //保存material
            string materialPath = folderPath + "/" + spriteName + ".mat";
            AssetDatabase.CreateAsset(meshRenderer.sharedMaterial, materialPath);

            //设置mesh和material的引用
            meshFilter.sharedMesh = AssetDatabase.LoadAssetAtPath<Mesh>(meshPath);
            meshRenderer.sharedMaterial = AssetDatabase.LoadAssetAtPath<Material>(materialPath);

            //刷新资源
            AssetDatabase.Refresh();
        }

        //删除当前脚本
        void RemoveThis()
        {
            DestroyImmediate(this);
        }

        void SetMaterialToTransparent(Material mat)
        {
            // Set material to transparent
            mat.SetFloat("_Surface", 1); // SurfaceType.Transparent
            mat.SetFloat("_Blend", 0); // BlendMode.Alpha
            mat.SetFloat("_DstBlend", (float)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetFloat("_SrcBlend", (float)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetFloat("_ZWrite", 0);
            mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
            mat.EnableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.EnableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.DisableKeyword("_SURFACE_TYPE_OPAQUE");
            mat.EnableKeyword("_SURFACE_TYPE_TRANSPARENT");
        }

        void SetMaterialToOpaque(Material mat)
        {
            // set material to opaque, alpha clipping on, threshol to 0.5
            mat.SetFloat("_Surface", 0); // SurfaceType.Opaque
            mat.SetFloat("_AlphaClip", 1); // Enable Alpha Clipping
            mat.SetFloat("_AlphaCutoff", 0.5f); // Alpha Clipping Threshold
            mat.SetFloat("_ZWrite", 1);
            mat.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.DisableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.EnableKeyword("_SURFACE_TYPE_OPAQUE");
            mat.DisableKeyword("_SURFACE_TYPE_TRANSPARENT");

        }

        Material CreateMaterialBasedOnRenderPipeline()
        {
            Material mat;
            var renderPipelineAsset = GraphicsSettings.currentRenderPipeline;

            if (renderPipelineAsset == null)
            {
                mat = new Material(Shader.Find("Standard"))
                {
                    name = "Generated Material"
                };
                //SetMaterialToTransparent(mat);
            }
            else if (renderPipelineAsset.GetType().Name.Contains("HDRenderPipelineAsset"))
            {
                mat = new Material(Shader.Find("HDRP/Lit"))
                {
                    name = "Generated Material"
                };
                //SetMaterialToTransparent(mat);
            }
            else if (renderPipelineAsset.GetType().Name.Contains("UniversalRenderPipelineAsset"))
            {
                mat = new Material(Shader.Find("Universal Render Pipeline/Lit"))
                {
                    name = "Generated Material"
                };
                //SetMaterialToTransparent(mat);
            }
            else
            {
                Debug.Log("Current render pipeline: Unknown");
                mat = new Material(Shader.Find("Standard"))
                {
                    name = "Generated Material"
                };
                //SetMaterialToTransparent(mat);
            }
            SetMaterialToOpaque(mat);

            return mat;
        }
    }
}
