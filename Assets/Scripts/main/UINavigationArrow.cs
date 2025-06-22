using UnityEngine;
using UnityEngine.U2D;

namespace YuCheng
{
    /// <summary>
    /// 用以控制導航UI
    /// </summary>
    public class UINavigationArrow : MonoBehaviour
    {
        public RectTransform icon;                  // 導航圖示（UI）
        public Transform target;                    // 任務目標
        public Camera mainCamera;                   // 主攝影機
        public Canvas canvas;                       // 畫布

        public float borderPadding = 30f;           // UI 不貼邊太近

        private RectTransform canvasRect;
        private Vector3[] corners;                  // 紀錄導航icon的四角
        private bool isFullyInsideScreen = true;    // 判斷icon是否有在鏡頭內

        public UINavigationArrow navArrow;

        void Start()
        {
            if (canvas != null)
                canvasRect = canvas.GetComponent<RectTransform>();

            // 去取得物件四個角的座標，用以判斷是否已經在畫布外
            corners = new Vector3[4];
            icon.GetWorldCorners(corners);
        }

        void Update()
        {
            if (target == null || icon == null || mainCamera == null || canvas == null) return;

            // 將世界座標轉為螢幕座標
            Vector3 screenPos = mainCamera.WorldToScreenPoint(target.position);

            // 判斷目標是否在螢幕內
            bool isTargetVisible = screenPos.z > 0 &&
                screenPos.x >= 0 && screenPos.x <= Screen.width &&
                screenPos.y >= 0 && screenPos.y <= Screen.height;

            // 將螢幕座標轉為畫布座標
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvasRect,
                screenPos,
                canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera,
                out Vector2 localPoint
            );

            foreach (Vector3 corner in corners)
            {
                Vector3 screenPoint = Camera.main.WorldToScreenPoint(corner);
                if (screenPoint.x < 0 || screenPoint.x > Screen.width || screenPoint.y < 0 || screenPoint.y > Screen.height)
                {
                    isFullyInsideScreen = false;
                    break;
                }
            }

            // 如果在螢幕內 → 直接顯示在畫面上
            if (isTargetVisible & isFullyInsideScreen)
            {
                icon.localPosition = localPoint;
            }
            else
            {
                // 如果目標在螢幕外 → 計算貼邊位置
                Vector2 clampedPos = localPoint;

                float halfWidth = canvasRect.rect.width / 2 - borderPadding;
                float halfHeight = canvasRect.rect.height / 2 - borderPadding;

                clampedPos.x = Mathf.Clamp(clampedPos.x, -halfWidth, halfWidth);
                clampedPos.y = Mathf.Clamp(clampedPos.y, -halfHeight, halfHeight);

                icon.localPosition = clampedPos;
            }
            // 可選：讓 icon 指向目標方向（旋轉）
            // Vector3 dir = screenPos - new Vector3(Screen.width / 2, Screen.height / 2);
            // float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            // icon.rotation = Quaternion.Euler(0, 0, angle - 90);  // -90 是讓箭頭向上
        }

        public void SetNewTarget(Transform newTarget)
        {
            if (navArrow != null)
            {
                navArrow.target = newTarget;
            }
        }
    }
}
