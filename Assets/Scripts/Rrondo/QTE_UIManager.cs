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
        public List<TextMeshProUGUI> keyHints;

        public void ShowKeyHint(int index, char key)
        {
            keyHints[index].gameObject.SetActive(true);
            keyHints[index].text = key.ToString();
        }

        public void HideAllHints()
        {
            foreach (var hint in keyHints)
                hint.gameObject.SetActive(false);
        }

        public void MovePointerTo(Vector3 position)
        {
            pointerDot.anchoredPosition = position;
        }
    }
}

