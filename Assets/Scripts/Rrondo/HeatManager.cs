using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace Rrondo
{
    /// <summary>
    /// 我是加熱系統
    /// 還有加熱的UI
    /// </summary>
    public class HeatManager : MonoBehaviour
    {
        [Header("溫度控制")]
        public float temperature = 0f;
        public float maxTemperature = 200f;
        public float increasePerClick = 15f;
        public float decayPerSecond = 20f;

        [Header("時間控制")]
        public float maxPlayTime = 15f;
        public float currentPlayTime = 0f;

        [Header("失敗條件")]
        public float overheatFailTime = 3f;
        public float underheatFailTime = 6f;

        [Header("狀態")]
        public float stableTimer = 0f;
        public float overheatTimer = 0f;
        public float underheatTimer = 0f;
        private bool isHeating = false;
        private bool isFinished = false;

        [Header("階段管理")]
        private List<HeatPhase> heatPhases = new List<HeatPhase>();
        private int currentPhaseIndex = 0;
        private float phaseTimer = 0f;

        [Header("UI")]
        public Slider temperatureSlider;
        public Image fillBar;
        public Image perfectZoneImage;

        public System.Action<bool, bool> OnHeatingResult; // success, isFake

        public event System.Action<bool> OnHeatingFinished; // 結束流程 換到下一個流程

        private bool isFakeRecipe = false; // 是否是假的廢丹流程
        public TextMeshProUGUI temperatureText;

        #region 小游新增
        private bool startHeat = false;
        public HeatPhase currentPhase;
        #endregion

        /// <summary>
        /// 來自 RecipeConfig 的初始化
        /// </summary>
        public void Init(RecipeConfig config, bool fake = false)
        {
            isFakeRecipe = fake;

            if (fake)
            {
                // 建立一個隨機的 HeatPhase
                HeatPhase randomPhase = new HeatPhase();
                int choice = Random.Range(0, 2); // 0 或 1

                if (choice == 0)
                {
                    randomPhase.idealMin = 80f;
                    randomPhase.idealMax = 100f;
                }
                else
                {
                    randomPhase.idealMin = 100f;
                    randomPhase.idealMax = 120f;
                }
                randomPhase.duration = 3f;

                heatPhases = new List<HeatPhase> { randomPhase };
                maxPlayTime = 10f;
                overheatFailTime = 2f;
                underheatFailTime = 2f;
            }
            else
            {
                heatPhases = config.heatPhases;
                maxPlayTime = config.maxPlayTime;
                overheatFailTime = config.overheatFailTime;
                underheatFailTime = config.underheatFailTime;
            }

            currentPhaseIndex = 0;
            phaseTimer = 0f;

            ResetState();
        }

        public void StartHeating()
        {
            isHeating = true;
            ResetState();
        }

        void ResetState()
        {
            temperature = 0;
            currentPlayTime = 0;
            stableTimer = 0;
            overheatTimer = 0;
            underheatTimer = 0;
            isFinished = false;
            phaseTimer = 0f;
            currentPhaseIndex = 0;

            UpdatePerfectZone();
        }

        void Update()
        {
            #region 小游新增
            if (!startHeat) return;
            #endregion
            if (!isHeating || isFinished) return;
            if (temperatureText)
                temperatureText.text = Mathf.RoundToInt(temperature) + "°";

            currentPlayTime += Time.deltaTime;
            temperature -= decayPerSecond * Time.deltaTime;
            temperature = Mathf.Clamp(temperature, 0f, maxTemperature);

            if (heatPhases == null || heatPhases.Count == 0)
            {
                Debug.LogError("heatPhases 未正確設定");
                Finish(false);
                return;
            }

            currentPhase = heatPhases[currentPhaseIndex];

            bool isInPerfectRange = (temperature >= currentPhase.idealMin && temperature <= currentPhase.idealMax);

            if (isInPerfectRange)
            {
                stableTimer += Time.deltaTime;
                overheatTimer = 0;
                underheatTimer = 0;
            }
            else if (temperature > currentPhase.idealMax)
            {
                overheatTimer += Time.deltaTime;
                underheatTimer = 0;
                stableTimer = 0;
            }
            else
            {
                underheatTimer += Time.deltaTime;
                overheatTimer = 0;
                stableTimer = 0;
            }

            if (overheatTimer > overheatFailTime || underheatTimer > underheatFailTime)
            {
                Finish(false);
                return;
            }

            if (stableTimer >= currentPhase.duration)
            {
                currentPhaseIndex++;
                if (currentPhaseIndex >= heatPhases.Count)
                {
                    Finish(true);
                    return;
                }

                stableTimer = 0f;
                overheatTimer = 0f;
                underheatTimer = 0f;
                phaseTimer = 0f;

                UpdatePerfectZone();
            }

            if (currentPlayTime >= maxPlayTime)
            {
                Finish(false);
                return;
            }
            #region 小游新增
            float remainingTime = maxPlayTime - currentPlayTime;
            remainingTime = Mathf.Max(remainingTime, 0f);
            //Log.Text($"加熱系統：{remainingTime.ToString()}/{maxPlayTime}");
            #endregion
            UpdateUI();
        }

        public void OnHeatButtonClick()
        {
            if (!isHeating || isFinished) return;
            temperature += increasePerClick;
            temperature = Mathf.Clamp(temperature, 0, maxTemperature);
            #region 小游新增
            startHeat = true;
            #endregion
        }

        void UpdateUI()
        {
            if (temperatureSlider)
                temperatureSlider.value = temperature / maxTemperature;

            var currentPhase = heatPhases[Mathf.Clamp(currentPhaseIndex, 0, heatPhases.Count - 1)];

            if (fillBar)
                fillBar.color = (temperature >= currentPhase.idealMin && temperature <= currentPhase.idealMax) ? Color.green :
                                (temperature > currentPhase.idealMax) ? Color.red : Color.blue;
        }

        void UpdatePerfectZone()
        {
            if (perfectZoneImage == null || temperatureSlider == null || heatPhases == null || heatPhases.Count == 0)
                return;

            var currentPhase = heatPhases[Mathf.Clamp(currentPhaseIndex, 0, heatPhases.Count - 1)];

            RectTransform fillArea = temperatureSlider.fillRect.parent.GetComponent<RectTransform>();
            RectTransform perfectRect = perfectZoneImage.GetComponent<RectTransform>();

            float totalWidth = fillArea.rect.width;
            float range = maxTemperature;

            float startPercent = currentPhase.idealMin / range;
            float endPercent = currentPhase.idealMax / range;

            float startX = totalWidth * startPercent;
            float width = totalWidth * (endPercent - startPercent);

            perfectRect.anchoredPosition = new Vector2(startX, perfectRect.anchoredPosition.y);
            perfectRect.sizeDelta = new Vector2(width, perfectRect.sizeDelta.y);
        }

        void Finish(bool success)
        {
            isFinished = true;
            isHeating = false;

            if (isFakeRecipe)
                Log.Text(" 這是廢丹流程：" + (success ? "加熱成功但無效" : "加熱失敗，還是廢丹"));
            else
                Log.Text(success ? "加熱成功！" : " 加熱失敗！");

            OnHeatingResult?.Invoke(success, isFakeRecipe);

            #region 小游新增
            return;
            #endregion

            OnHeatingFinished?.Invoke(success); // 呼叫下一個流程
        }
    }
}
