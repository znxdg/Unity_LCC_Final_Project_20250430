using UnityEngine;
#if UNITY_EDITOR
using UnityEditor; 
#endif

/// <summary>
/// 建立藥品資料系統
/// </summary>
public class RecipeAssetCreator
{
#if UNITY_EDITOR
    [MenuItem("Assets/Create/Recipe Config")]
    public static void CreateAsset()
    {
        var asset = ScriptableObject.CreateInstance<Rrondo.RecipeConfig>();
        AssetDatabase.CreateAsset(asset, "Assets/Recipes/NewRecipeConfig.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    } 
#endif
}
