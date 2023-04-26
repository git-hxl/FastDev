using UnityEditor;
using UnityEngine;
namespace Framework.Editor
{
    public class MiniExtension
    {
        [MenuItem("FastDev/Other/Clear PlayerPrefs")]
        static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
        [MenuItem("FastDev/Other/打开StreamingAssets")]
        static void OpenStreamingAssetsFinder()
        {
            EditorUtility.RevealInFinder(Application.streamingAssetsPath);
        }
        [MenuItem("FastDev/Other/打开PersisentData")]
        static void OpenPersistentDataFinder()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }

        [MenuItem("Assets/File 获取AssetPath")]
        static void GetResAssetPath()
        {
            if (Selection.activeObject == null) return;
            TextEditor textEditor = new TextEditor();
            textEditor.text = AssetDatabase.GetAssetPath(Selection.activeObject);
            textEditor.OnFocus();
            textEditor.Copy();
            Debug.Log("已复制：" + textEditor.text);
        }
        [MenuItem("Assets/File 获取文件Hash")]
        static void DebugFileHash()
        {
            if (Selection.activeObject == null) return;
            string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            string hash = FileUtil.GetFileMD5(assetPath);
            TextEditor textEditor = new TextEditor();
            textEditor.text = hash;
            textEditor.OnFocus();
            textEditor.Copy();
            Debug.Log("已复制：" + hash);
        }

        [MenuItem("GameObject/FastDev/Add BoxCollider")]
        static void AddBoxCollider()
        {
            if (Selection.activeObject == null) return;
            var prefab = Selection.activeObject as GameObject;
            Vector3 center = Vector3.zero;
            var renders = prefab.GetComponentsInChildren<Renderer>();

            for (int i = 0; i < renders.Length; i++)
            {
                center += renders[i].bounds.center;
            }

            center /= renders.Length;

            Bounds bounds = new Bounds(center, Vector3.zero);

            foreach (var item in renders)
            {
                bounds.Encapsulate(item.bounds);
            }

            var boxCollider = prefab.GetComponent<BoxCollider>();
            if (boxCollider == null)
                boxCollider = prefab.AddComponent<BoxCollider>();

            boxCollider.center = bounds.center - prefab.transform.position;

            float x = bounds.size.x;
            float y = bounds.size.y;
            float z = bounds.size.z;

            boxCollider.size = new Vector3(x, y, z);

        }
    }
}