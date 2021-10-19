using Bigger;
using System.IO;
using UnityEditor;
using UnityEngine;
public class MiniExtension
{
    [MenuItem("Assets/获取AssetPath")]
    static void GetResAssetPath()
    {
        if (Selection.activeObject == null) return;
        TextEditor textEditor = new TextEditor();
        textEditor.text = AssetDatabase.GetAssetPath(Selection.activeObject);
        textEditor.OnFocus();
        textEditor.Copy();
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

    [MenuItem("Bigger/Tools/Clear PlayerPrefs")]
    static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("Assets/打印文件Hash")]
    static void DebugFileHash()
    {
        if (Selection.activeObject == null) return;
        string assetPath = AssetDatabase.GetAssetPath(Selection.activeObject);
        string hash = Bigger.FileUtil.GetFileMD5(assetPath);
        Debug.Log(hash);
    }

    [MenuItem("GameObject/更新当前对象的多语言", false, 0)]
    static void ExcuteLanguageUpdate()
    {
        if (Selection.activeGameObject == null) return;

        System.Collections.Generic.Dictionary<string, LanguageStruct> languageDict = LanguageManager.Instance.ReadEditorLanguageJson();

        LanguageText[] languageTexts = Selection.activeGameObject.GetComponentsInChildren<LanguageText>(true);
        foreach (var item in languageTexts)
        {
            item.AddLanguageText(languageDict);
        }
        LanguageManager.Instance.SaveEditorLanguageJson(languageDict);
    }
}