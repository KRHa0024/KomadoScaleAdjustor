using UnityEngine;
using UnityEditor;

public class KomadoScaleAdjustor : Editor
{
    // ==========================================
    // 設定ファイルを開くショートカット
    // ==========================================
    [MenuItem("GameObject/KomadoScaleAdjustor/Settings", false, 100)]
    public static void SelectSettings()
    {
        KomadoScaleSettings settings = KomadoScaleSettings.GetOrCreateSettings();
        Selection.activeObject = settings;
        EditorGUIUtility.PingObject(settings);
    }

    // ==========================================
    // スケールを適用する共通メソッド
    // ==========================================
    private static void ApplyScale(float baseMultiplier)
    {
        if (Selection.gameObjects == null || Selection.gameObjects.Length == 0)
        {
            Debug.LogWarning("スケールを変更するオブジェクトが選択されていません。");
            return;
        }

        KomadoScaleSettings settings = KomadoScaleSettings.GetOrCreateSettings();
        float targetScale = settings != null ? settings.targetAvatarScale : 1.0f;
        
        float finalMultiplier = baseMultiplier * targetScale;

        foreach (GameObject go in Selection.gameObjects)
        {
            Undo.RecordObject(go.transform, "Komado Scale Adjust");
            go.transform.localScale = go.transform.localScale * finalMultiplier;
        }
        
        Debug.Log($"【KomadoScaleAdjustor】基本倍率({baseMultiplier}) × 設定倍率({targetScale}) = 最終倍率 {finalMultiplier} を適用しました。");
    }

    // ==========================================
    // 変換元：Chocolat（ショコラ）
    // ==========================================
    [MenuItem("GameObject/KomadoScaleAdjustor/From Chocolat/To Chiffon (x1.000)", false, 0)]
    public static void ChocolatToChiffon() => ApplyScale(1.000f);

    [MenuItem("GameObject/KomadoScaleAdjustor/From Chocolat/To Plum (x1.051)", false, 1)]
    public static void ChocolatToPlum() => ApplyScale(1.051f);

    [MenuItem("GameObject/KomadoScaleAdjustor/From Chocolat/To Lime (x1.115)", false, 2)]
    public static void ChocolatToLime() => ApplyScale(1.115f);

    // ==========================================
    // 変換元：Chiffon（シフォン）
    // ==========================================
    [MenuItem("GameObject/KomadoScaleAdjustor/From Chiffon/To Chocolat (x1.000)", false, 10)]
    public static void ChiffonToChocolat() => ApplyScale(1.000f);

    [MenuItem("GameObject/KomadoScaleAdjustor/From Chiffon/To Plum (x1.051)", false, 11)]
    public static void ChiffonToPlum() => ApplyScale(1.051f);

    [MenuItem("GameObject/KomadoScaleAdjustor/From Chiffon/To Lime (x1.115)", false, 12)]
    public static void ChiffonToLime() => ApplyScale(1.115f);

    // ==========================================
    // 変換元：Plum（プラム）
    // ==========================================
    [MenuItem("GameObject/KomadoScaleAdjustor/From Plum/To Chocolat (x0.9512)", false, 20)]
    public static void PlumToChocolat() => ApplyScale(0.9512f);

    [MenuItem("GameObject/KomadoScaleAdjustor/From Plum/To Chiffon (x0.9512)", false, 21)]
    public static void PlumToChiffon() => ApplyScale(0.9512f);

    [MenuItem("GameObject/KomadoScaleAdjustor/From Plum/To Lime (x1.061)", false, 22)]
    public static void PlumToLime() => ApplyScale(1.061f);

    // ==========================================
    // 変換元：Lime（ライム）
    // ==========================================
    [MenuItem("GameObject/KomadoScaleAdjustor/From Lime/To Chocolat (x0.8965)", false, 30)]
    public static void LimeToChocolat() => ApplyScale(0.8965f);

    [MenuItem("GameObject/KomadoScaleAdjustor/From Lime/To Chiffon (x0.8965)", false, 31)]
    public static void LimeToChiffon() => ApplyScale(0.8965f);

    [MenuItem("GameObject/KomadoScaleAdjustor/From Lime/To Plum (x0.9425)", false, 32)]
    public static void LimeToPlum() => ApplyScale(0.9425f);
}
