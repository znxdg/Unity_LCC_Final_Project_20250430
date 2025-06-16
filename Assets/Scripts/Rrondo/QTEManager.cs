using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rrondo
{
    /// <summary>
    /// 我是QTE系統
    /// 負責整個QTE的邏輯
    /// </summary>
    public class QTEManager : MonoBehaviour
    {
        #region QTE 基本設置
        [Header("QTE 配置")]
        public Transform[] pathPoints;               // 星星的路徑點
        public Transform lightDot;                   // 光點 transform
        public QTE_UIManager uiManager;              // UI 管理員
        public QTE_LightDot lightDotController;      // 跟光點連動


        public GameObject qteUIPrefab;
        public Transform uiParent;

        public string allowedKeys = "QWERTYASDFGH";   // QTE 指定的按鍵
        public int minQTEEvents = 2;                  // QTE 出現的次數 2-4次
        public int maxQTEEvents = 4;                  // QTE 出現的次數 2-4次
        public int minKeysPerEvent = 6;               // QTE 出現的案件數量6-8個
        public int maxKeysPerEvent = 8;               // QTE 出現的案件數量6-8個
        public float totalMoveDuration = 6f;
        public int playerLevel = 0;

        private List<Text> currentQTETexts = new List<Text>();
        private string playerInput = "";
        private float inputTimer = 0f;
        private float inputTimeLimit = 3f;
        private bool qteActive = false;

        #endregion
        public System.Action<bool> OnQTEFinished;

        // 跟UI連動
        //currentQTETexts = uiManager.CreateKeyUI(keys);
        public void StartQTE()
        {
            qteActive = false;
            lightDot.gameObject.SetActive(true);
            inputTimeLimit = Mathf.Clamp(3f + playerLevel, 3f, 6f);
            playerInput = "";
            currentQTETexts.Clear();

            StartCoroutine(MoveAlongPathWithQTE());
        }

        IEnumerator MoveAlongPathWithQTE()
        {
            // 隨機決定哪幾個點要觸發QTE
            int qteTriggerCount = Random.Range(minQTEEvents, maxQTEEvents + 1);
            HashSet<int> qteIndices = new HashSet<int>();
            while (qteIndices.Count < qteTriggerCount)
                qteIndices.Add(Random.Range(1, pathPoints.Length - 1));

            // 透過 LightDot 控制器移動
            yield return StartCoroutine(lightDotController.MoveAlongPath((i) => qteIndices.Contains(i),(i) => TriggerQTEEvent()));

            lightDot.gameObject.SetActive(false);
            OnQTEFinished?.Invoke(true);
        }

            
            
        

        IEnumerator TriggerQTEEvent()
        {
            qteActive = true;
            inputTimer = 0f;
            playerInput = "";
            
            List<char> keys = GenerateQTEKeys();

            ShowQTEUI(keys);

            Log.Text("請輸入: " + string.Join("", keys));

            yield return StartCoroutine(CheckPlayerInput(keys));

            qteActive = false;
        }

        /// <summary>
        /// 產生隨機 QTE 鍵的序列
        /// </summary>
        List<char> GenerateQTEKeys()
        {
            int keyCount = Random.Range(minKeysPerEvent, maxKeysPerEvent + 1);
            List<char> keys = new List<char>();
            for(int i = 0; i < keyCount; i++)
            {
                keys.Add(allowedKeys[Random.Range(0, allowedKeys.Length)]);
            }
            return keys;
        }
        /// <summary>
        /// 顯示 UI 元素
        /// </summary>
        void ShowQTEUI(List<char> keys)
        {
            foreach (Transform child in uiParent)
                Destroy(child.gameObject);

            currentQTETexts.Clear();  // 清空文字列表

            foreach (char k in keys)
            {
                GameObject go = Instantiate(qteUIPrefab, uiParent);
                Text txt = go.GetComponent<Text>();
                txt.text = k.ToString();
                currentQTETexts.Add(txt);
            }
        }

        private bool _inputSuccess = false;
        /// <summary>
        /// 玩家輸入監聽與判定
        /// </summary>
        /// <param name="expectedkeys">輸入的QTE按鍵</param>
        /// <returns></returns>
        IEnumerator CheckPlayerInput(List<char> expectedKeys)
        {
            float timer = 0f;
            string input = "";
            _inputSuccess = false;

            while (timer < inputTimeLimit)
            {
                timer += Time.deltaTime;

                foreach (char c in allowedKeys)
                {
                    if (Input.GetKeyDown(c.ToString().ToLower()))
                    {
                        input += c;
                        Log.Text("輸入: " + c);
                        HighlightInputProgress(input.Length - 1);

                        // 若輸入長度與答案長度相等就開始判斷
                        if (input.Length >= expectedKeys.Count)
                        {
                            bool isCorrect = true;
                            for (int i = 0; i < expectedKeys.Count; i++)
                            {
                                if (input[i] != expectedKeys[i])
                                {
                                    isCorrect = false;
                                    break;
                                }
                            }

                            if (isCorrect)
                            {
                                _inputSuccess = true;
                                yield break; // 成功，跳出協程
                            }
                            else
                            {
                                //  錯誤，重置輸入並重新產生一組鍵
                                input = "";
                                expectedKeys = GenerateQTEKeys();
                                ShowQTEUI(expectedKeys);
                                Log.Text("輸入錯誤！新組合: " + string.Join("", expectedKeys));
                            }
                        }
                    }
                }

                yield return null;
            }

            // 時間到了仍未成功
            _inputSuccess = false;
        }

        private void HighlightInputProgress(int index)
        {
            if (index >= 0 && index < currentQTETexts.Count)
            {
                currentQTETexts[index].color = Color.green;
            }
        }
    }
}


