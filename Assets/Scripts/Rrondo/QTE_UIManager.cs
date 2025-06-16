using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using UnityEngine.UI;

namespace Rrondo
{
    /// <summary>
    /// 我是QTE的UI管理系統
    /// 負責操控畫面
    /// </summary>
    public class QTE_UIManager : MonoBehaviour
    {
        public RectTransform pointerDot;
        public GameObject qteUIPrefab;  // 一個單字母 Text 的 prefab
        public Transform uiParent;      // 生成字母的 UI 容器
        public TMP_Text successText;    // 成功失敗訊息
       

        /// <summary>
        /// 生成按鍵提示
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        public List<TextMeshProUGUI> CreateKeyUI(List<char> keys)
        {
            foreach (Transform child in uiParent)Destroy(child.gameObject);

            var texts = new List<TextMeshProUGUI>();
            foreach(var key in keys)
            {
                GameObject obj = Instantiate(qteUIPrefab, uiParent);
                var text = obj.GetComponent<TextMeshProUGUI>();
                text.text = key.ToString();
                texts.Add(text);
            }
            return texts;
        }

        public void MovePointerTo(Vector3 position)
        {
            pointerDot.anchoredPosition = position;
        }

        public void ShowResult(bool success)
        {
            successText.gameObject.SetActive(true);
            successText.text = success ? "成功" : "失敗";
            successText.color = success ? Color.green : Color.red;
        }
        

        public void HideAllHints()
        {
            foreach (Transform child in uiParent) Destroy(child.gameObject);
        }

        public void ResetUI()
        {
            HideAllHints();
            successText.gameObject.SetActive(false);
        }
        
    }
}

