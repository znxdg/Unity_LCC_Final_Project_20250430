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
        private float growInterval = 5f;                                // 生長間隔
        private float wetDuration = 10f;                                // 濕潤時間
        private float dryTolerance = 5f;                                // 可維持乾燥多久
        private float growTimer;                                        // 成長計時器
        private float wetTimer;                                         // 濕潤計時器
        private float dryTimer;                                         // 乾燥計時器
        private int currentStateID;                                     // 成長階段數值

        [Header("作物(子物件)狀態")]
        private bool isDead = false;                                    // 作物是否死亡

        [Header("農田(父物件)狀態")]
        [SerializeField]
        FarmTile farmTile;                                              // 取得掛載於農田上的腳本
        [SerializeField]
        private bool isWater;                                           // 取得農田的濕潤狀態

        private SpriteRenderer spriteRenderer;
        private Coroutine growCoroutine;

        PlantState[] growStages = new PlantState[]
        {
            PlantState.Seed,
            PlantState.Growing_1,
            PlantState.Growing_2,
            PlantState.Growing_3,
            PlantState.Mature
        };

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();            // 取得當前作物存放顯示圖片的參數欄位
            farmTile = transform.parent.GetComponent<FarmTile>();       // 取得父物件(農田)掛載的腳本
            isWater = farmTile.isWatered;                               // 確認當前農田濕潤狀況
            wetTimer = wetDuration;                                     // 初始化濕潤計時器
            growTimer = growInterval;                                   // 初始化成長計時器
            dryTimer = dryTolerance;                                    // 初始化乾燥計時器
        }

        private void Start()
        {
            SetState(PlantState.Seed);                                  // 設定作物初始狀態為種子
            //growCoroutine = StartCoroutine(GrowRoutine());
        }

        private void Update()
        {
            if (isDead) return;
            if (currentStateID == growStages.Length)
            {
                farmTile.canHarvest = true;
                return;
            }
            isWater = farmTile.isWatered;                               // 持續更新當前農田濕潤狀況

            #region 成長倒數
            growTimer -= Time.deltaTime;
            if (growTimer <= 0f && currentStateID < growStages.Length)
            {
                SetState(growStages[currentStateID]);
                growTimer = growInterval;
                currentStateID += 1;
            }
            #endregion

            #region 濕潤狀態倒數
            if (isWater)
            {
                wetTimer -= Time.deltaTime;
                if (wetTimer <= 0f)
                {
                    farmTile.FarmColorState();  // 變乾
                    wetTimer = wetDuration;
                }
                dryTimer = dryTolerance;
            }
            else
            {
                dryTimer -= Time.deltaTime;
                if (dryTimer <= 0f)
                {
                    Die(); // 作物死亡
                }
            } 
            #endregion
        }

        /// <summary>
        /// 設置狀態與圖片序列化
        /// </summary>
        /// <param name="newState"></param>
        private void SetState(PlantState newState)
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
        /// 作物死亡
        /// </summary>
        private void Die()
        {
            SetState(PlantState.Withered);
            isDead = true;
        }
    }
}
