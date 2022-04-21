using UnityEditor;
using UnityEngine;
namespace FastDev
{
    public class MiniExtension
    {
        [MenuItem("FastDev/Clear PlayerPrefs")]
        static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
        [MenuItem("FastDev/打开StreamingAssets")]
        static void OpenStreamingAssetsFinder()
        {
            EditorUtility.RevealInFinder(Application.streamingAssetsPath);
        }
        [MenuItem("FastDev/打开PersisentData")]
        static void OpenPersistentDataFinder()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }
        [MenuItem("Assets/获取AssetPath")]
        static void GetResAssetPath()
        {
            if (Selection.activeObject == null) return;
            TextEditor textEditor = new TextEditor();
            textEditor.text = AssetDatabase.GetAssetPath(Selection.activeObject);
            textEditor.OnFocus();
            textEditor.Copy();
            Debug.Log("已复制：" + textEditor.text);
        }
        [MenuItem("Assets/打印文件Hash")]
        static void DebugFileHash()
        {
            if (Selection.activeObject == null) return;
            string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            string hash = FastDev.FileUtil.GetFileMD5(assetPath);
            Debug.Log(hash);
        }
    }
}