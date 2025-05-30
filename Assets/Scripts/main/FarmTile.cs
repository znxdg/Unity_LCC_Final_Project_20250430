using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// 農地狀態
    /// </summary>
    public class FarmTile : MonoBehaviour
    {
        [SerializeField]
        private Transform plantPoint;
        [SerializeField]
        private GameObject cropPrefab;

        private SpriteRenderer sr;
        public bool isWatered = false;
        public bool isPlanting = false;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        public void Water()
        {
            FarmColorState();
            plantGrowSystem.instance.LogWaterTime();
        }

        public void PlantCrop()
        {
            GameObject crop = Instantiate(cropPrefab, plantPoint.position, Quaternion.identity);
            crop.GetComponent<plantGrowSystem>().AssignFarmTile(this);
            isPlanting =true;
        }

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
