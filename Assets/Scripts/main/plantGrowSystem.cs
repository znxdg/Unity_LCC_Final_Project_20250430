using System.Collections;
using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// 用來控制作物成長
    /// </summary>
    public class plantGrowSystem : MonoBehaviour
    {
        private static plantGrowSystem _instance;
        public static plantGrowSystem instance
        {
            get
            {
                if (_instance == null) _instance = FindAnyObjectByType<plantGrowSystem>();
                return _instance;
            }
        }
        private FarmTile tile;

        [SerializeField, Header("作物成長階段圖")]
        private Sprite[] growthSprites;
        [SerializeField]
        private float growSpendTime = 1f;      // 成長至下一階段所需時間
        [SerializeField]
        private float waterIntervalTime = 5f;   // 至枯萎的時間

        private int currentStage = 0;           // 當前植物成長階段
        private bool plantHealth = true;        // 是否健康
        private float lastWaterTime;            // 上次澆水時間
        private float WaterInterval = 3;        // 澆水間隔(土地變乾的時間)
        private SpriteRenderer sr;
        private float timer;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
            timer = 0;
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (plantHealth)
            {
                if (timer - lastWaterTime >= WaterInterval)
                {
                    if (tile.isWatered) tile.FarmColorState();
                }

                int stage = Mathf.FloorToInt(timer / growSpendTime);
                if (stage != currentStage && stage < (growthSprites.Length - 1))
                {
                    currentStage = stage;
                    sr.sprite = growthSprites[currentStage]; 
                }
            }
        }

        public void LogWaterTime()
        {
            lastWaterTime = timer;
        }

        public void AssignFarmTile(FarmTile _tile)
        {
            tile = _tile;
        }
    }
}
