using UnityEngine;

namespace YuCheng
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        public GameObject targetObject;
        [SerializeField]
        public GameObject scopeObject;
        [SerializeField]
        Fungus_Item triggerMedicine;

        private void Awake()
        {
            callNPC();
        }

        // 召喚NPC
        public void callNPC()
        {
            if (triggerMedicine.fungusShowState)
            { 
                targetObject.SetActive(true);
                scopeObject.SetActive(true);
            }
        }
    }
}
