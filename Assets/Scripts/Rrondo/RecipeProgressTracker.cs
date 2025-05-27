using System.Collections.Generic;
using UnityEngine;


namespace Rrondo
{
    /// <summary>
    /// 我是製作成功紀錄系統 用於藥譜的解鎖確認
    /// </summary>
    public class RecipeProgressTracker : MonoBehaviour
    {
        public List<RecipeConfig> unlockedRecipes = new List<RecipeConfig>();

        public void MarkRecipeAsUnlocked(RecipeConfig recipe)
        {
            if (!unlockedRecipes.Contains(recipe))
            {
                unlockedRecipes.Add(recipe);
                Log.Text($"已解鎖藥品：{recipe.displayName}");
            }
        }
    }
}

