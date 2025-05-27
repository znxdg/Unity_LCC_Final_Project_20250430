using UnityEngine;
using System.Collections.Generic;

namespace Rrondo
{
    /// <summary>
    /// 加熱階段
    /// </summary>
    [System.Serializable]
    public class HeatPhase
    {
        public float idealMin;
        public float idealMax;
        public float duration;
    }

    /// <summary>
    /// 藥品資料
    /// </summary>
    [CreateAssetMenu(menuName = "Rrondo/RecipeConfig")]
    public class RecipeConfig : ScriptableObject
    {
        public string displayName;
        public List<HeatPhase> heatPhases = new List<HeatPhase>();

        public List<IngredientConfig> requiredIngredients;

        public float maxPlayTime = 15f;
        public float overheatFailTime = 3f;
        public float underheatFailTime = 6f;
    }
}

