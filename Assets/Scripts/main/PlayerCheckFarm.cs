using UnityEngine;

namespace YuCheng
{

    public class PlayerCheckFarm : MonoBehaviour
    {
        [SerializeField]
        protected string farmTag;
        [HideInInspector]
        public GameObject farmObject;

        private static PlayerCheckFarm _instance;
        public static PlayerCheckFarm instance
        {
            get
            {
                if (_instance == null) _instance = FindAnyObjectByType<PlayerCheckFarm>();
                return _instance;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(farmTag)) farmObject = collision.gameObject;
        }

        /// <summary>
        /// 是否站在農田旁
        /// </summary>
        /// <returns></returns>
        public GameObject IsFramToHit()
        {
            if (farmObject != null)
                return farmObject;
            return null;
        }
    }
}
