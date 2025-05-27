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
                    Log.Text("���ŦX�t��I");
                    gameManager.StartHeatingWithRecipe(recipe);
                    return;
                }
            }

            // �S���t�令�\
            if (droppedIngredients.Count >= 4) // �Ҧp�̦h4�ӧ���
            {
                Log.Text(" �S���o�Ӱt��A���ѡI");
                
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

