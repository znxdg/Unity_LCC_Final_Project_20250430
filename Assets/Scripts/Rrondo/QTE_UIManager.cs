using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Rrondo
{
    /// <summary>
    /// QTE UI 管理員：控制提示文字、光點 UI 等相關顯示。
    /// </summary>
    public class QTE_UIManager : MonoBehaviour
    {
        [Header("光點顯示")]
        public RectTransform pointerDot;

        [Header("字母提示 UI 設定")]
        public GameObject qteLetterPrefab;       // 單個 QTE 字母的 預置物（需含 TextMeshProUGUI）
        public Transform letterParent;           // 字母的 UI 生成父物件

        [Header("提示文字")]
        public TMP_Text resultText;              // 顯示 QTE 成功 / 失敗的文字

        private List<TMP_Text> currentLetters = new List<TMP_Text>();

        /// <summary>
        /// 建立一組 QTE 字母 UI（6~8 個）
        /// </summary>
        public List<TMP_Text> CreateKeyUI(List<char> keys)
        {
            ClearKeyUI();
            currentLetters.Clear();

            foreach (char key in keys)
            {
                GameObject go = Instantiate(qteLetterPrefab, letterParent);
                TMP_Text text = go.GetComponent<TMP_Text>();
                if (text != null)
                {
                    text.text = key.ToString();
                    currentLetters.Add(text);
                }
            }

            return currentLetters;
        }

        /// <summary>
        /// 更新光點位置（如果 UI 上有顯示）
        /// </summary>
        public void MovePointerTo(Vector3 worldPosition)
        {
            if (pointerDot != null)
            {
                pointerDot.position = worldPosition;
            }
        }

        /// <summary>
        /// 清除所有字母 UI
        /// </summary>
        public void ClearKeyUI()
        {
            foreach (Transform child in letterParent)
            {
                Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// 根據輸入進度上色字母
        /// </summary>
        public void HighlightProgress(int index, Color color)
        {
            if (index >= 0 && index < currentLetters.Count)
            {
                currentLetters[index].color = color;
            }
        }

        /// <summary>
        /// 顯示結果字樣（成功 / 失敗）
        /// </summary>
        public void ShowResultText(string text, Color color)
        {
            if (resultText != null)
            {
                resultText.text = text;
                resultText.color = color;
                resultText.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// 隱藏結果文字
        /// </summary>
        public void HideResultText()
        {
            if (resultText != null)
            {
                resultText.gameObject.SetActive(false);
            }
        }
    }
}

