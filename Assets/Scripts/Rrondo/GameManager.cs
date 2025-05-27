using Unity.VisualScripting;
using UnityEngine;

namespace Rrondo
{
    /// <summary>
    /// 遊戲管理系統
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("關聯物件")]
        public HeatManager heatManager;
        public RecipeConfig selectedRecipe;

        void Start()
        {
            StartHeatingWithRecipe(selectedRecipe);
        }

        public void StartHeatingWithRecipe(RecipeConfig recipe)
        {
            heatManager.Init(recipe);
            heatManager.StartHeating();
        }

        
    }
}

