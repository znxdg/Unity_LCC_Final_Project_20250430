using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace YuCheng
{
    [System.Serializable]
    public class PlantStateSprite
    {
        public PlantState state;
        public Sprite sprite;
    }

    /// <summary>
    /// 作物預置物
    /// </summary>
    public class PlantTile : MonoBehaviour
    {
        public PlantState currentState;

        [Header("不同狀態對應的圖片")]
        public List<PlantStateSprite> stateSprites;

        [Header("狀態切換設定")]
        [SerializeField] 
        private float timePerStage = 5f;   // 每階段持續秒數，可在 Inspector 調整

        private SpriteRenderer spriteRenderer;
        private Coroutine growCoroutine;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            SetState(PlantState.Seed);
            growCoroutine = StartCoroutine(GrowRoutine());
        }

        /// <summary>
        /// 設置狀態與圖片序列化
        /// </summary>
        /// <param name="newState"></param>
        public void SetState(PlantState newState)
        {
            currentState = newState;

            foreach (var pair in stateSprites)
            {
                if (pair.state == newState)
                {
                    spriteRenderer.sprite = pair.sprite;
                    break;
                }
            }
        }

        /// <summary>
        /// 設定每 timePerStage 秒，更新作物圖片
        /// </summary>
        /// <returns></returns>
        private IEnumerator GrowRoutine()
        {
            // 我們定義的成長順序，只走到 Mature 為止
            PlantState[] growStages = new PlantState[] 
            {
                PlantState.Seed,
                PlantState.Growing_1,
                PlantState.Growing_2,
                PlantState.Growing_3,
                PlantState.Mature
            };

            int index = 0;
            while (index < growStages.Length)
            {
                SetState(growStages[index]);
                index++;
                yield return new WaitForSeconds(timePerStage);
            }
        }
    }
}
