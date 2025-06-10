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
        private GameObject crop;            // 作物本身
        private PlantTile cropScript;       // 作物的腳本
        public bool isWatered = false;      // 該農田是否濕潤
        public bool isPlanting = false;     // 該農田上是否有植物
        public bool canHarvest = false;     // 是否可以採收

        private void Awake()
        {
            // 獲取農田本身物件
            sr = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (isPlanting)
            {
                if (cropScript.currentState == PlantState.Mature || 
                    cropScript.currentState == PlantState.Withered) canHarvest = true;
            }
        }

        /// <summary>
        /// 對農田澆水
        /// 角色澆水前會判斷農田是否為濕潤
        /// </summary>
        public void Water()
        {
            FarmColorState();
        }

        /// <summary>
        /// 於農田上種植植物
        /// </summary>
        public void PlantCrop()
        {
            crop = Instantiate(cropPrefab, plantPoint.position, Quaternion.identity, this.transform);
            cropScript = crop.GetComponent<PlantTile>();
            isPlanting = true;
        }

        public void PlantHarvest()
        {
            canHarvest = false;
            isPlanting = false;
            Destroy(crop);
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
                isWatered =false;
                sr.color = new Color(1, 1, 1);
            }
        }
    }
}
