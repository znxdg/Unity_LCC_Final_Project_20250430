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
        [Header("QTE 配置")]
        public Transform[] pathPoints;
        public Transform lightDot;

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

        public System.Action<bool> OnQTEFinished;

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
            int qteTriggerCount = Random.Range(minQTEEvents, maxQTEEvents + 1);
            HashSet<int> qteIndices = new HashSet<int>();
            while (qteIndices.Count < qteTriggerCount)
                qteIndices.Add(Random.Range(1, pathPoints.Length - 1));

            for (int i = 0; i < pathPoints.Length - 1; i++)
            {
                Vector3 start = pathPoints[i].position;
                Vector3 end = pathPoints[i + 1].position;
                float t = 0f;

                while (t < 1f)
                {
                    t += Time.deltaTime / (totalMoveDuration / (pathPoints.Length - 1));
                    lightDot.position = Vector3.Lerp(start, end, t);
                    yield return null;
                }

                if (qteIndices.Contains(i))
                {
                    yield return StartCoroutine(TriggerQTEEvent()); ;
                    if (!_inputSuccess)
                    {
                        OnQTEFinished?.Invoke(false);
                        lightDot.gameObject.SetActive(false);
                        yield break;
                    }
                }
            }

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
                    }
                }

                if (input.Length >= expectedKeys.Count)
                    break;

                yield return null;
            }

            if (input.Length != expectedKeys.Count)
                yield break;

            for (int i = 0; i < expectedKeys.Count; i++)
            {
                if (input[i] != expectedKeys[i])
                    yield break;
            }

            _inputSuccess = true;
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


