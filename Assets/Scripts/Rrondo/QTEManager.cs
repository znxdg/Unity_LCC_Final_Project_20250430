using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Rrondo
{
    public class QTEManager : MonoBehaviour
    {
        [Header("QTE 設定")]
        public Transform lightDot;
        [Header("QTE UI 控制")]
        public GameObject qteCanvas;  // QTE系統的Canvas
        public QTE_LightDot lightDotController;
        public Transform uiParent;
        public GameObject qteUIPrefab;

        private bool qteActive = false;
        private string playerInput = "";

        public string allowedKeys = "QWERTYASDFGH";
        public int minQTEEvents = 2;              // QTE最少出現2次 最多出現4次
        public int maxQTEEvents = 4;
        public int minKeysPerEvent = 6;           // QTE最少出現6個字母 最多出現8個
        public int maxKeysPerEvent = 8;
        public int playerLevel = 0;
        [Header("音效")]
        public AudioSource audioSource;
        public AudioClip keyPressClip;     // 一般按鍵音效
        public AudioClip finalKeyClip;     // 最後一個鍵音效
        public AudioClip errorClip;        // 錯誤音效

        private List<Text> currentQTETexts = new List<Text>();
        private float inputTimeLimit;
        private bool _inputSuccess;
        private bool _qteRunning;


        public System.Action<bool> OnQTEFinished;

        public void StartQTE()
        {
            // 把QTE的畫布給打開
            if (qteCanvas != null)
                    qteCanvas.SetActive(true);
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
                qteIndices.Add(Random.Range(1, lightDotController.pathPoints.Count - 1));

            bool allQTESuccess = true;

            yield return StartCoroutine(lightDotController.MoveAlongPath(
                (index) => qteIndices.Contains(index),
                (index) => TriggerQTEEventCoroutine((success) => {
                    if (!success) allQTESuccess = false;
                })
            ));

            lightDot.gameObject.SetActive(false);
            OnQTEFinished?.Invoke(allQTESuccess);
        }

        IEnumerator TriggerQTEEventCoroutine(System.Action<bool> onFinish)
        {
            _qteRunning = true;
            bool result = false;
            yield return StartCoroutine(TriggerQTEEvent((success) => result = success));
            _qteRunning = false;
            onFinish?.Invoke(result);
        }

        IEnumerator TriggerQTEEvent(System.Action<bool> onFinish)
        {
            _inputSuccess = false;
            float timer = 0f;
            string input = "";

            List<char> expectedKeys = GenerateQTEKeys();
            ShowQTEUI(expectedKeys);

            Log.Text("準備生成QTE字母 : " + string.Join("", expectedKeys));

            while (timer < inputTimeLimit)
            {
                timer += Time.deltaTime;

                foreach (char c in allowedKeys)
                {
                    if (Input.GetKeyDown(c.ToString().ToLower()))
                    {
                        input += c;
                        HighlightInputProgress(input.Length - 1);

                        //  音效播放
                        if (audioSource)
                        {
                            if (input.Length == expectedKeys.Count && finalKeyClip != null)
                                audioSource.PlayOneShot(finalKeyClip);
                            else if (keyPressClip != null)
                                audioSource.PlayOneShot(keyPressClip);
                        }

                        if (input.Length >= expectedKeys.Count)
                        {
                            if (CheckInput(input, expectedKeys))
                            {
                                _inputSuccess = true;
                                break;
                            }
                            else
                            {
                                //  錯誤音效
                                if (audioSource && errorClip != null)
                                    audioSource.PlayOneShot(errorClip);

                                input = "";
                                expectedKeys = GenerateQTEKeys();
                                ShowQTEUI(expectedKeys);

                                Log.Text("輸入錯誤，重新生成: " + string.Join("", expectedKeys));
                            }
                        }
                    }
                }

                if (_inputSuccess) break;
                yield return null;
            }

            onFinish?.Invoke(_inputSuccess);
        }

        List<char> GenerateQTEKeys()
        {
            int count = Random.Range(minKeysPerEvent, maxKeysPerEvent + 1);
            List<char> keys = new List<char>();
            for (int i = 0; i < count; i++)
                keys.Add(allowedKeys[Random.Range(0, allowedKeys.Length)]);
            return keys;
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
                
                if(txt == null)
                {
                    Debug.LogError("qteUIPrefab 上沒有 TextMeshProUGUI 組件");
                    continue;
                }

                txt.text = k.ToString();
                //currentQTETexts.Add(txt); 等一下再添加
            }
        }

        void HighlightInputProgress(int index)
        {
            if (index >= 0 && index < currentQTETexts.Count)
                currentQTETexts[index].color = Color.green;
        }

        // 單次確認
        bool CheckInput(string input, List<char> expected)
        {
            if (input.Length != expected.Count) return false;
            for (int i = 0; i < expected.Count; i++)
                if (input[i] != expected[i]) return false;
            return true;
        }

        // 輸入按鍵音效確認
        IEnumerator CheckPlayerInputCoroutine(List<char> expectedKeys)
        {
            string input = "";
            float timer = 0f;
            float inputLimit = inputTimeLimit; // 可以根據等級改變這值

            while (timer < inputLimit)
            {
                timer += Time.deltaTime;

                foreach (char c in allowedKeys)
                {
                    if (Input.GetKeyDown(c.ToString().ToLower()))
                    {
                        input += c;
                        Log.Text("輸入: " + c);
                        HighlightInputProgress(input.Length - 1);

                        // 播放音效
                        if (audioSource)
                        {
                            if (input.Length == expectedKeys.Count)
                                audioSource.PlayOneShot(finalKeyClip);
                            else
                                audioSource.PlayOneShot(keyPressClip);
                        }

                        // 判斷是否輸入正確
                        if (input.Length >= expectedKeys.Count)
                        {
                            if (CheckInput(input, expectedKeys))
                            {
                                yield break; // 成功
                            }
                            else
                            {
                                // 撥放錯誤音效
                                if (audioSource && errorClip)
                                    audioSource.PlayOneShot(errorClip);

                                // 重新開始
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

            // 超時未完成
            Log.Text("輸入超時！");
        }

        // 結束關掉UI，確保畫面乾淨
        public void EndQTE()
        {
            if (qteCanvas != null)
                qteCanvas.SetActive(false);
        }
    }
}

