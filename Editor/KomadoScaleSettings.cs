using UnityEngine;
using UnityEditor;

public class KomadoScaleSettings : ScriptableObject
{
    [Tooltip("変換先こまどアバターのスケール値を入力")]
    public float targetAvatarScale = 1.0f;

    public static KomadoScaleSettings GetOrCreateSettings()
    {
        string[] guids = AssetDatabase.FindAssets("t:KomadoScaleSettings");
        if (guids.Length > 0)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[0]);
            return AssetDatabase.LoadAssetAtPath<KomadoScaleSettings>(path);
        }

        KomadoScaleSettings newSettings = CreateInstance<KomadoScaleSettings>();

        // このスクリプト自身の GUID から配置フォルダを特定し、その隣に .asset を作成する。
        // これによりフォルダを移動・リネームしてもパスがずれてエラーになることがない。
        MonoScript script = MonoScript.FromScriptableObject(newSettings);
        string scriptPath = AssetDatabase.GetAssetPath(script);
        string directory = scriptPath.Substring(0, scriptPath.LastIndexOf('/'));
        string assetPath = directory + "/KomadoScaleSettings.asset";

        AssetDatabase.CreateAsset(newSettings, assetPath);
        AssetDatabase.SaveAssets();
        
        return newSettings;
    }
}
