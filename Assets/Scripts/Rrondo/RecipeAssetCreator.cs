using UnityEngine;
using UnityEditor;

/// <summary>
/// 建立藥品資料系統
/// </summary>
public class RecipeAssetCreator
{
    [MenuItem("Assets/Create/Recipe Config")]
    public static void CreateAsset()
    {
        var asset = ScriptableObject.CreateInstance<Rrondo.RecipeConfig>();
        AssetDatabase.CreateAsset(asset, "Assets/Recipes/NewRecipeConfig.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
