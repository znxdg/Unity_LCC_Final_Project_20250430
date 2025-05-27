using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// 農地狀態
    /// </summary>
    public class FarmTile : MonoBehaviour
    {
        private SpriteRenderer sr;
        public bool isWatered = false;

        private void Awake()
        {
            sr = GetComponent<SpriteRenderer>();
        }

        public void Water()
        {
            if (!isWatered)
            {
                isWatered=true;
                sr.color = new Color(0.7f, 0.7f, 0.7f);
            }
        }
    }
}
