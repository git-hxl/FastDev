using UnityEditor;
using UnityEngine;
namespace Bigger
{
    public class MiniExtension
    {
        [MenuItem("Bigger/Tools/Clear PlayerPrefs")]
        static void ClearPlayerPrefs()
        {
            PlayerPrefs.DeleteAll();
        }
        [MenuItem("Bigger/Tools/打开StreamingAssets")]
        static void OpenStreamingAssetsFinder()
        {
            EditorUtility.RevealInFinder(Application.streamingAssetsPath);
        }
        [MenuItem("Bigger/Tools/打开PersisentData")]
        static void OpenPersistentDataFinder()
        {
            EditorUtility.RevealInFinder(Application.persistentDataPath);
        }
        [MenuItem("Assets/Tools/获取AssetPath")]
        static void GetResAssetPath()
        {
            if (Selection.activeObject == null) return;
            TextEditor textEditor = new TextEditor();
            textEditor.text = AssetDatabase.GetAssetPath(Selection.activeObject);
            textEditor.OnFocus();
            textEditor.Copy();
            Debug.Log("已复制：" + textEditor.text);
        }
        [MenuItem("Assets/Tools/打印文件Hash")]
        static void DebugFileHash()
        {
            if (Selection.activeObject == null) return;
            string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
            string hash = Bigger.FileUtil.GetFileMD5(assetPath);
            Debug.Log(hash);
        }
    }
}