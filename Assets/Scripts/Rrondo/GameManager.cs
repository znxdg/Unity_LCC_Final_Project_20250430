//using Unity.VisualScripting;
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
        public QTEManager qteManager;
        public RecipeConfig selectedRecipe;
        public IngredientConfig item;

        void Start()
        {
            StartHeatingWithRecipe(selectedRecipe);

            // QTEManager 這邊可以提前訂閱，因為不需要等初始化
            qteManager.OnQTEFinished += OnQTEFinished;
        }

        public void StartHeatingWithRecipe(RecipeConfig recipe)
        {
            heatManager.Init(recipe);

            // 確保每次都有註冊（避免重複註冊，可以先取消一次）
            heatManager.OnHeatingFinished -= OnHeatingFinished;
            heatManager.OnHeatingFinished += OnHeatingFinished;

            heatManager.StartHeating();
        }

        // HeatManager 通知加熱結束
        private void OnHeatingFinished(bool success)
        {
            if (success)
            {
                Log.Text("加熱成功，開始 燒香QTE");
                qteManager.StartQTE();
            }
            else
            {
                Log.Text("加熱失敗，結束流程");
                // 這邊可以加重試或失敗處理
            }
        }

        // QTEManager 通知 QTE 結束
        private void OnQTEFinished(bool success)
        {
            if (success)
            {
                Log.Text("QTE成功，繼續後續流程");
                // 往下流程邏輯
                qteManager.EndQTE();
                item.count++;
            }
            else
            {
                Log.Text("QTE失敗，結束流程或重試");
                // 失敗處理
            }
        }


    }
}

