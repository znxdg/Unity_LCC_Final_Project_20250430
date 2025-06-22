using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// 世界座標轉為介面座標
    /// </summary>
    public class WorldToUI : MonoBehaviour
    {
        private RectTransform rect;

        private void Awake()
        {
            rect = GetComponent<RectTransform>();
        }

        /// <summary>
        /// 更新介面座標
        /// </summary>
        /// <param name="target">目標物件</param>
        /// <param name="offsetX">X 軸位移</param>
        /// <param name="offsetY">Y 軸位移</param>
        public void UpdateUIPoint(Transform target, float offsetX, float offsetY)
        {
            Vector3 targetPoint = target.position;
            targetPoint.x += offsetX;
            targetPoint.y += offsetY;
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(targetPoint);
            rect.position = screenPoint;
        }
    }
}
