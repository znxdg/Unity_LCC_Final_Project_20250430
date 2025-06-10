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

        /// <summary>
        /// 碰撞箱進入農田做儲存
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(farmTag))
                farmObject = collision.gameObject;
        }

        /// <summary>
        /// 碰撞箱離開農田做清除
        /// </summary>
        /// <param name="collision"></param>
        //private void OnTriggerExit2D(Collider2D collision)
        //{
        //    if (collision.CompareTag(farmTag))
        //        farmObject = null;
        //}

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
