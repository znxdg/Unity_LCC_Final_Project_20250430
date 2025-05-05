using UnityEngine;
using UnityEngine.UI;

namespace Rrondo
{
    /// <summary>
    /// 我是加熱系統
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
        private float currentPlayTime = 0f;

        [Header("藥品設定（可替換）")]
        public float idealMin = 80f;
        public float idealMax = 120f;
        public float requiredStableTime = 5f;

        public float overheatFailTime = 3f;
        public float underheatFailTime = 6f;

        [Header("狀態")]
        private float stableTimer = 0f;
        private float overheatTimer = 0f;
        private float underheatTimer = 0f;
        private bool isHeating = false;
        private bool isFinished = false;

        [Header("UI")]
        public Slider temperatureSlider;
        public Image fillBar;

        public System.Action<bool> OnHeatingResult;

        public void Init(RecipeConfig config)
        {
            idealMin = config.idealMin;
            idealMax = config.idealMax;
            requiredStableTime = config.requiredStableTime;
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
        }

        void Update()
        {
            if (!isHeating || isFinished) return;

            currentPlayTime += Time.deltaTime;
            temperature -= decayPerSecond * Time.deltaTime;
            temperature = Mathf.Clamp(temperature, 0f, maxTemperature);

            if (temperature >= idealMin && temperature <= idealMax)
            {
                stableTimer += Time.deltaTime;
                overheatTimer = 0;
                underheatTimer = 0;
            }
            else if (temperature > idealMax)
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
            }

            if (stableTimer >= requiredStableTime)
            {
                Finish(true);
            }

            if (currentPlayTime >= maxPlayTime)
            {
                Finish(false);
            }

            UpdateUI();
        }

        public void OnHeatButtonClick()
        {
            if (!isHeating || isFinished) return;
            temperature += increasePerClick;
            temperature = Mathf.Clamp(temperature, 0, maxTemperature);
        }

        void UpdateUI()
        {
            if (temperatureSlider)
                temperatureSlider.value = temperature / maxTemperature;

            if (fillBar)
                fillBar.color = (temperature >= idealMin && temperature <= idealMax) ? Color.green :
                                (temperature > idealMax) ? Color.red : Color.blue;
        }

        void Finish(bool success)
        {
            isFinished = true;
            isHeating = false;
            Debug.Log(success ? "🔥 加熱成功！" : "💥 加熱失敗！");
            OnHeatingResult?.Invoke(success);
        }
    }
}

