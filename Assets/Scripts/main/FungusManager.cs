using UnityEngine;
using System.Collections.Generic;

namespace YuCheng
{
    /// <summary>
    /// 用來記錄各個對話框狀態
    /// </summary>
    public class FungusManager : MonoBehaviour
    {
        public static FungusManager instance;

        [Header("所有對話框顯示狀態")]
        [SerializeField]
        private List<Fungus_Item> dialogFlags = new List<Fungus_Item>();

        void Awake()
        {
            
        }
    }
}
