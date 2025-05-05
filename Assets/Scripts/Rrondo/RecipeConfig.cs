using UnityEngine;

namespace Rrondo
{
    /// <summary>
    /// 我是藥品資料
    /// </summary>
    [System.Serializable]
    public class RecipeConfig
    {
        public float idealMin = 80f;
        public float idealMax = 120f;
        public float requiredStableTime = 5f;
    }
}

