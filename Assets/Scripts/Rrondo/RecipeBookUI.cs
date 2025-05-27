using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rrondo
{
    /// <summary>
    /// 我是藥譜系統
    /// </summary>
    public class RecipeBookUI : MonoBehaviour
    {
        public GameManager gameManager;
        public Transform recipeListParent;
        public GameObject recipeButtonPrefab;

        public List<RecipeConfig> unlockedRecipes;

        void Start()
        {
            foreach (var recipe in unlockedRecipes)
            {
                var go = Instantiate(recipeButtonPrefab, recipeListParent);
                go.GetComponentInChildren<Text>().text = recipe.displayName;

                var btn = go.GetComponent<Button>();
                var localRecipe = recipe; // 匿名函數閉包問題
                btn.onClick.AddListener(() => gameManager.StartHeatingWithRecipe(localRecipe));
            }
        }

    }
}

