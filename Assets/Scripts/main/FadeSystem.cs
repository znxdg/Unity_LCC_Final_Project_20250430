using UnityEngine;
using System.Collections;

namespace YuCheng
{
    /// <summary>
    /// 淡入淡出系統
    /// </summary>
    public class FadeSystem
    {
        /// <summary>
        /// 淡入淡出
        /// </summary>
        /// <param name="group">畫布群組</param>
        /// <param name="fadeIn">是否淡入</param>
        public static IEnumerator Fade(CanvasGroup group, bool fadeIn = true)
        {
            float increase = fadeIn ? +0.1f : -0.1f;

            for (int i = 0;i<10;i++)
            {
                group.alpha += increase;
                yield return new WaitForSeconds(0.03f);
            }
        }
    }
}
