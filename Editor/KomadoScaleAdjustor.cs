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
    private static void ApplyScale(MenuCommand command, float baseMultiplier)
    {
        // Hierarchy の右クリックメニューから実行した場合、Unity は選択オブジェクトごとに
        // このメソッドを呼び出し、command.context にそのオブジェクトを渡す。
        // ここで Selection 全体を処理すると、複数選択時に倍率が選択数分重ねがけされてしまうため、
        // context がある場合はその1体だけを処理する。
        // 上部メニューバーから実行した場合は context が null なので、選択中のオブジェクトをまとめて処理する。
        GameObject[] targets = command != null && command.context is GameObject contextObject
            ? new[] { contextObject }
            : Selection.gameObjects;

        if (targets == null || targets.Length == 0)
        {
            Debug.LogWarning("スケールを変更するオブジェクトが選択されていません。");
            return;
        }

        KomadoScaleSettings settings = KomadoScaleSettings.GetOrCreateSettings();
        float targetScale = settings != null ? settings.targetAvatarScale : 1.0f;
        
        float finalMultiplier = baseMultiplier * targetScale;

        foreach (GameObject go in targets)
        {
            Undo.RecordObject(go.transform, "Komado Scale Adjust");
            go.transform.localScale = go.transform.localScale * finalMultiplier;
            Debug.Log($"【KomadoScaleAdjustor】{go.name}: 基本倍率({baseMultiplier}) × 設定倍率({targetScale}) = 最終倍率 {finalMultiplier} を適用しました。");
        }
    }

    // ==========================================
    // 変換元：Chiffon（シフォン）・Chocolat（ショコラ）
    // ==========================================
    [MenuItem("GameObject/KomadoScaleAdjustor/From Chiffon & Chocolat/To Plum & Salt (x1.051)", false, 0)]
    public static void ChiffonAndChocolatToPlumAndSalt(MenuCommand command) => ApplyScale(command, 1.051f);

    [MenuItem("GameObject/KomadoScaleAdjustor/From Chiffon & Chocolat/To Lime (x1.115)", false, 1)]
    public static void ChiffonAndChocolatToLime(MenuCommand command) => ApplyScale(command, 1.115f);

    // ==========================================
    // 変換元：Plum（プラム）・Salt（ソルト）
    // ==========================================
    [MenuItem("GameObject/KomadoScaleAdjustor/From Plum & Salt/To Chiffon & Chocolat (x0.9512)", false, 10)]
    public static void PlumAndSaltToChiffonAndChocolat(MenuCommand command) => ApplyScale(command, 0.9512f);

    [MenuItem("GameObject/KomadoScaleAdjustor/From Plum & Salt/To Lime (x1.061)", false, 11)]
    public static void PlumAndSaltToLime(MenuCommand command) => ApplyScale(command, 1.061f);

    // ==========================================
    // 変換元：Lime（ライム）
    // ==========================================
    [MenuItem("GameObject/KomadoScaleAdjustor/From Lime/To Chiffon & Chocolat (x0.8965)", false, 20)]
    public static void LimeToChiffonAndChocolat(MenuCommand command) => ApplyScale(command, 0.8965f);

    [MenuItem("GameObject/KomadoScaleAdjustor/From Lime/To Plum & Salt (x0.9425)", false, 21)]
    public static void LimeToPlumAndSalt(MenuCommand command) => ApplyScale(command, 0.9425f);
}
