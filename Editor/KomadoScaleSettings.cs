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

        AssetDatabase.CreateAsset(newSettings, "Assets/KRHa's Assets/KomadoScaleAdjustor/Editor/KomadoScaleSettings.asset");
        AssetDatabase.SaveAssets();
        
        return newSettings;
    }
}
