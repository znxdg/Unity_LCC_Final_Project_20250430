using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Rrondo
{
    /// <summary>
    /// 我是QTE系統
    /// </summary>
    public class QTEManager : MonoBehaviour
    {
        public Transform[] pathPoints;     //光點
        public Transform lightDot;

        public GameObject qteUIPrefab;
        public Transform uiParent;

        public string allowedKeys = "QWERTYASDFGH";      //QTE指定按鍵
        public int minQTEEvents = 2;
        public int maxQTEEvents = 4;
        public int minKeysPerEvent = 6;
        public int maxKeysPerEvent = 8;

        public float totalMoveDuration = 6f;

        private List<List<char>> qteEvents = new List<List<char>>();
        private int currentEventIndex = 0;
        private string playerInput = "";
        private bool qteActive = false;
        private float inputTimer = 0f;
        private float inputTimeLimit = 3f;

        public int playerLevel = 0;

        public System.Action<bool> OnQTEFinished;

        public void StartQTE()
        {
            qteActive = false;
            lightDot.gameObject.SetActive(true);
            inputTimeLimit = Mathf.Clamp(3f + playerLevel, 3f, 6f);
            qteEvents.Clear();
            currentEventIndex = 0;
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
                    yield return StartCoroutine(TriggerQTEEvent());
                    if (!qteActive)
                    {
                        OnQTEFinished?.Invoke(false);
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
            int keyCount = Random.Range(minKeysPerEvent, maxKeysPerEvent + 1);

            List<char> keys = new List<char>();
            for (int i = 0; i < keyCount; i++)
                keys.Add(allowedKeys[Random.Range(0, allowedKeys.Length)]);

            qteEvents.Add(keys);

            // 顯示UI提示
            foreach (Transform child in uiParent)
                Destroy(child.gameObject);

            foreach (char k in keys)
            {
                GameObject go = Instantiate(qteUIPrefab, uiParent);
                go.GetComponentInChildren<Text>().text = k.ToString();
            }

            Log.Text("請輸入: " + string.Join("", keys));

            while (inputTimer < inputTimeLimit)
            {
                inputTimer += Time.deltaTime;

                foreach (char c in allowedKeys)
                {
                    if (Input.GetKeyDown(c.ToString().ToLower()))
                    {
                        playerInput += c;
                        Log.Text("輸入：" + c);
                    }
                }

                if (playerInput.Length >= keys.Count)
                    break;

                yield return null;
            }

            if (playerInput.Length != keys.Count)
            {
                qteActive = false;
                yield break;
            }

            for (int i = 0; i < keys.Count; i++)
            {
                if (playerInput[i] != keys[i])
                {
                    qteActive = false;
                    yield break;
                }
            }

            qteActive = true;
        }
    }
    }


