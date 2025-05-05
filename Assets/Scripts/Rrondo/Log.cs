using UnityEngine;

namespace Rrondo
{
    public class Log
    {

        /// <summary>
        /// 輸出文字訊息
        /// </summary>
        /// <param name="text">想要輸出的文字</param>
        /// <param name="color">想要設定的文字顏色</param>
        public static void Text(object text, string color = "#fff")
        {
            string result = $"<color= {color} > {text} ></color>";
            Debug.Log(result);
        }
    }
}

