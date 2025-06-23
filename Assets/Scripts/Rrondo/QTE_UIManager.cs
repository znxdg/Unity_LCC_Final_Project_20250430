using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rrondo
{
    /// <summary>
    /// QTE UI 管理系統：顯示按鍵提示、光點、訊息等
    /// </summary>
    public class QTE_UIManager : MonoBehaviour
    {
        [Header(" 字母提示相關")]
        public GameObject keyPrefab;             // 單字母的 TextMeshProUGUI prefab
        public Transform keyParent;              // 字母提示 UI 的容器
        private List<TextMeshProUGUI> keyTexts = new List<TextMeshProUGUI>();

        [Header(" 光點")]
        public RectTransform pointerDot;         // 指向的光點 UI

        [Header(" 成功 / ❌ 失敗提示")]
        public TMP_Text resultText;

        /// <summary>
        /// 產生 QTE 按鍵 UI
        /// </summary>
        public void ShowKeySequence(List<char> keys)
        {
            ClearKeys();

            foreach (char key in keys)
            {
                GameObject go = Instantiate(keyPrefab, keyParent);
                TextMeshProUGUI txt = go.GetComponent<TextMeshProUGUI>();
                txt.text = key.ToString();
                keyTexts.Add(txt);
            }
        }

        /// <summary>
        /// 高亮顯示輸入成功的字母
        /// </summary>
        public void HighlightInput(int index)
        {
            if (index >= 0 && index < keyTexts.Count)
            {
                keyTexts[index].color = Color.green;
            }
        }

        /// <summary>
        /// 清除舊字母
        /// </summary>
        public void ClearKeys()
        {
            foreach (Transform child in keyParent)
                Destroy(child.gameObject);

            keyTexts.Clear();
        }

        /// <summary>
        /// 控制光點移動到特定 UI 座標（用於提示動畫）
        /// </summary>
        public void MovePointerTo(Vector2 uiPosition)
        {
            if (pointerDot != null)
                pointerDot.anchoredPosition = uiPosition;
        }

        /// <summary>
        /// 顯示結果訊息
        /// </summary>
        public void ShowResultText(string message, Color color)
        {
            if (resultText != null)
            {
                resultText.text = message;
                resultText.color = color;
                resultText.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// 隱藏結果訊息
        /// </summary>
        public void HideResultText()
        {
            if (resultText != null)
                resultText.gameObject.SetActive(false);
        }


    }
}

