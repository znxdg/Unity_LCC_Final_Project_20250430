using UnityEngine;
using UnityEngine.SceneManagement;

namespace YuCheng
{
    /// <summary>
    /// 傳送陣
    /// </summary>
    public class Teleport_for_newScene : MonoBehaviour
    {
        [SerializeField, Header("要傳送的場景名稱")]
        private string sceneName;
        [Header("玩家座標與角度")]
        [SerializeField]
        private Vector3 playerPoint;
        [SerializeField]
        private float playerAngle;
        [SerializeField, Header("從哪一個場景傳送過來")]
        private string prevSceneName;

        private Transform player;

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0.6f, 1, 0.6f, 0.7f);
            Gizmos.DrawSphere(playerPoint, 0.2f);
        }

        private void Awake()
        {
            player = GameObject.Find("玩家_藥師").transform;
            SceneManager.activeSceneChanged += UpdatePlayer;
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= UpdatePlayer;
        }

        private void UpdatePlayer(Scene arg0, Scene arg1)
        {
            // 如果場景變更前的場景名稱 等於 從哪一個場景傳送過來
            if (LoadingManager.instance.prevSceneName == prevSceneName)
            {
                // 更新 玩家 的座標與角度
                player.position = playerPoint;
                player.eulerAngles = Vector3.up * playerAngle;
            }
        }

        // OTE 觸發事件：勾選 Is Trigger 後可以使用事件
        // 2D 觸發事件：碰撞器進入後會執行一次
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                StartCoroutine(LoadingManager.instance.Loading(sceneName));
            }
        }
    }
}
