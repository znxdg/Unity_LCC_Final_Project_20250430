using UnityEngine;

namespace YuCheng
{
    [CreateAssetMenu(menuName = "YC/Fungus_State", order = 0)]
    public class Fungus_Item : ScriptableObject
    {
        [Header("使用區塊名稱")]
        public string fungus_name;
        [Header("對話是否已經執行過")]
        public bool fungusShowState;
    }
}
