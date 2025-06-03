using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// 農地狀態
    /// </summary>
    public class FarmTile : MonoBehaviour
    {
        [SerializeField]
        private Transform plantPoint;       // 農作物生成的座標
        [SerializeField]
        private GameObject cropPrefab;      // 作物預置物

        private SpriteRenderer sr;          // 農田本身
        public bool isWatered = false;      // 該農田是否濕潤
        public bool isPlanting = false;     // 該農田上是否有植物

        private void Awake()
        {
            // 獲取農田本身物件
            sr = GetComponent<SpriteRenderer>();
        }

        /// <summary>
        /// 於農田上澆水
        /// </summary>
        public void Water()
        {
            FarmColorState();               // 改變農田顏色
            //if (plantGrowSystem.instance != null) plantGrowSystem.instance.LogWaterTime();
        }

        /// <summary>
        /// 於農田上種植植物
        /// </summary>
        public void PlantCrop()
        {
            GameObject crop = Instantiate(cropPrefab, plantPoint.position, Quaternion.identity, this.transform);
            isPlanting = true;
        }

        /// <summary>
        /// 改變顏色農田
        /// </summary>
        public void FarmColorState()
        {
            if (!isWatered)
            {
                isWatered = true;
                sr.color = new Color(0.7f, 0.7f, 0.7f);
            } 
            else
            {
                isWatered=false;
                sr.color = new Color(1, 1, 1);
            }
        }
    }
}
