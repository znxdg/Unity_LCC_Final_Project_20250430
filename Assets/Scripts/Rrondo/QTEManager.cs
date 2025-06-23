using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Rrondo
{
    public class QTEManager : MonoBehaviour
    {
        [Header("QTE 配置")]
        public Transform[] pathPoints;
        public Transform lightDot;
        public QTE_LightDot lightDotController;
        public QTE_UIManager uiManager;

        [Header("鍵位設定")]
        public string allowedKeys = "QWERTYASDFGH";       // QTE的文字按鍵
        public int minQTEEvents = 2;                      // QTE最少觸發2次
        public int maxQTEEvents = 4;                      // QTE最多觸發4次
        public int minKeysPerEvent = 6;                   // QTE最少刷出6個字母
        public int maxKeysPerEvent = 8;                   // QTE最多刷出8個字母
        public float totalMoveDuration = 6f;
        public int playerLevel = 1;                

        public GameObject qteUIPrefab;
        public Transform uiParent;

        private List<TextMeshProUGUI> currentQTETexts = new();
        private float inputTimeLimit = 3f;
        private bool _inputSuccess = false;

        public System.Action<bool> OnQTEFinished;

        public void StartQTE()
        {
            lightDot.gameObject.SetActive(true);
            inputTimeLimit = Mathf.Clamp(3f + playerLevel, 3f, 6f);
            StartCoroutine(MoveAlongPathWithQTE());
        }

        IEnumerator MoveAlongPathWithQTE()
        {
            int qteTriggerCount = Random.Range(minQTEEvents, maxQTEEvents + 1);
            HashSet<int> qteIndices = new();
            while (qteIndices.Count < qteTriggerCount)
                qteIndices.Add(Random.Range(1, pathPoints.Length - 1));

            bool allQTESuccess = true;

            yield return StartCoroutine(lightDotController.MoveAlongPath(
                (i) => qteIndices.Contains(i),
                (i) => TriggerQTEEvent((success) => {
                    if (!success) allQTESuccess = false;
                })
            ));

            lightDot.gameObject.SetActive(false);
            OnQTEFinished?.Invoke(allQTESuccess);
        }

        IEnumerator TriggerQTEEvent(System.Action<bool> callback)
        {
            _inputSuccess = false;
            int retryLimit = 3;
            int retryCount = 0;

            List<char> expectedKeys = GenerateQTEKeys();
            ShowQTEUI(expectedKeys);

            float timer = 0f;
            string input = "";

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

                        if (input.Length >= expectedKeys.Count)
                        {
                            if (IsInputCorrect(input, expectedKeys))
                            {
                                _inputSuccess = true;
                                callback(true);
                                yield break;
                            }
                            else
                            {
                                retryCount++;
                                if (retryCount >= retryLimit)
                                {
                                    Log.Text("重試次數過多，判定失敗");
                                    callback(false);
                                    yield break;
                                }

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

            callback(false); // 超時
        }

        List<char> GenerateQTEKeys()
        {
            int count = Random.Range(minKeysPerEvent, maxKeysPerEvent + 1);
            List<char> result = new();
            for (int i = 0; i < count; i++)
                result.Add(allowedKeys[Random.Range(0, allowedKeys.Length)]);
            return result;
        }

        void ShowQTEUI(List<char> keys)
        {
            foreach (Transform child in uiParent)
                Destroy(child.gameObject);
            currentQTETexts.Clear();

            foreach (char k in keys)
            {
                GameObject go = Instantiate(qteUIPrefab, uiParent);
                TextMeshProUGUI txt = go.GetComponent<TextMeshProUGUI>();
                txt.text = k.ToString();
                currentQTETexts.Add(txt);
            }
        }

        void HighlightInputProgress(int index)
        {
            if (index >= 0 && index < currentQTETexts.Count)
                currentQTETexts[index].color = Color.green;
        }

        bool IsInputCorrect(string input, List<char> expected)
        {
            if (input.Length != expected.Count) return false;
            for (int i = 0; i < expected.Count; i++)
                if (input[i] != expected[i]) return false;
            return true;
        }
    }
}


