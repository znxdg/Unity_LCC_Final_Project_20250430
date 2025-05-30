using UnityEngine;
using UnityEngine.UI;

namespace Rrondo
{
    /// <summary>
    /// 控制剩餘時間 進度條
    /// </summary>
    public class TimerManager : MonoBehaviour
    {
        public HeatManager heatManager;
        [SerializeField]
        private Image TimeBar;
        [SerializeField]
        private Image hotBar;
        [SerializeField]
        private Image OKBar;
        [SerializeField]
        private Image coldBar;

        private void Awake()
        {
            heatManager = FindAnyObjectByType<HeatManager>();
        }

        private void Update()
        {
            float remainingTime = heatManager.maxPlayTime - heatManager.currentPlayTime;
            remainingTime = Mathf.Max(remainingTime, 0f);

            TimeBar.fillAmount = Mathf.Clamp01(remainingTime / heatManager.maxPlayTime);
            OKBar.fillAmount = Mathf.Clamp01(heatManager.stableTimer / heatManager.currentPhase.duration);
            hotBar.fillAmount = Mathf.Clamp01(heatManager.overheatTimer / heatManager.overheatFailTime);
            coldBar.fillAmount = Mathf.Clamp01(heatManager.underheatTimer / heatManager.underheatFailTime);
        }
    }
}
