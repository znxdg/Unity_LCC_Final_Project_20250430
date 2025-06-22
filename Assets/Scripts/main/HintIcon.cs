using UnityEngine;
using UnityEngine.UI;

namespace YuCheng
{
    /// <summary>
    /// Icon 閃爍
    /// </summary>

    public class HintIcon:MonoBehaviour
    {
        private Image image;
        private Color originalColor;

        private void Awake()
        {
            image = GetComponent<Image>();
            originalColor = image.color;
        }

        private void Update()
        {
            float alpha = Mathf.PingPong(Time.time * 0.5f, 0.27f) + 0.55f; // 在 0.5 ~ 1 之間閃爍
            image.color = new Color(alpha, originalColor.g, originalColor.b);
        }

    }
}
