using System.Collections.Generic;
using UnityEngine;

namespace YuCheng
{

    public class PlayerCheckFarm : MonoBehaviour
    {
        [SerializeField]
        protected string farmTag;
        [HideInInspector]
        public GameObject farmObject;

        private List<GameObject> currentFarms = new List<GameObject>();     // 存放同時觸碰到的農田

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
            {
                if (!currentFarms.Contains(collision.gameObject)) currentFarms.Add(collision.gameObject);
                farmObject = currentFarms[currentFarms.Count - 1]; // 取最後一個進入的
            }
        }

        /// <summary>
        /// 碰撞箱離開農田做清除
        /// </summary>
        /// <param name="collision"></param>
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(farmTag))
            {
                currentFarms.Remove(collision.gameObject);

                // 若還有重疊中的農田，就改用最後一個
                if (currentFarms.Count > 0)
                    farmObject = currentFarms[currentFarms.Count - 1];
                else
                    farmObject = null;
            }
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
