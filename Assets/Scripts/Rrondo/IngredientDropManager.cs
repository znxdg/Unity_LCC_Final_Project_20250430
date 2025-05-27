using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Rrondo
{
    public class IngredientDropManager : MonoBehaviour
    {
        public List<IngredientConfig> droppedIngredients = new List<IngredientConfig>();
        public List<RecipeConfig> knownRecipes;
        public GameManager gameManager;

        public void AddIngredient(IngredientConfig ingredient)
        {
            droppedIngredients.Add(ingredient);
            CheckForMatch();
        }

        void CheckForMatch()
        {
            foreach (var recipe in knownRecipes)
            {
                if (IsSameIngredients(recipe.requiredIngredients, droppedIngredients))
                {
                    Log.Text("找到符合配方！");
                    gameManager.StartHeatingWithRecipe(recipe);
                    return;
                }
            }

            // 沒有配對成功
            if (droppedIngredients.Count >= 4) // 例如最多4個材料
            {
                Log.Text(" 沒有這個配方，失敗！");
                
            }
        }

        bool IsSameIngredients(List<IngredientConfig> a, List<IngredientConfig> b)
        {
            if (a.Count != b.Count) return false;

            var sortedA = a.OrderBy(i => i.id).ToList();
            var sortedB = b.OrderBy(i => i.id).ToList();

            for (int i = 0; i < sortedA.Count; i++)
            {
                if (sortedA[i].id != sortedB[i].id) return false;
            }
            return true;
        }

        public void ResetPot()
        {
            droppedIngredients.Clear();
        }
    }
}

