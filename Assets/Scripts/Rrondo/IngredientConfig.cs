using UnityEngine;
namespace Rrondo
{
    [CreateAssetMenu(menuName = "Rrondo/Ingredient")]
    public class IngredientConfig : ScriptableObject
    {
        public string id;
        public string displayName;
        public Sprite icon;
    }

}
