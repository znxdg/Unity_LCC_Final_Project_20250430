using UnityEngine;
namespace Rrondo
{
    [CreateAssetMenu(menuName = "Rrondo/Ingredient")]
    public class IngredientConfig : ScriptableObject
    {
        public string id;
        public string displayName;
        [TextArea(2,5)]
        public string description;
        public Sprite icon;
        public int count;
    }

}
