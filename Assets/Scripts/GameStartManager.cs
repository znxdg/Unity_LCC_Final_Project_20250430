using UnityEngine;
using UnityEngine.UI;
using YuCheng;

namespace Rrondo
{
    /// <summary>
    /// 我是遊戲開始的管理器
    /// </summary>
    public class GameStartManager : MonoBehaviour
    {
        private Button btnContinue, btnStart, btnSetting, btnQuit, btnSettingClose;
        private CanvasGroup groupSettings;

        private void Awake()
        {
            // groupSettings = GameObject.Find("群組_設定介面").GetComponent<CanvasGroup>();
            btnContinue = GameObject.Find("按鈕_繼續遊戲").GetComponent<Button>();
            btnStart = GameObject.Find("按鈕_開始遊戲").GetComponent <Button>();
            btnSetting = GameObject.Find("按鈕_選項").GetComponent <Button>();
            btnQuit = GameObject.Find("按鈕_結束遊戲").GetComponent <Button>();
            // btnSettingClose = GameObject.Find("按鈕_關閉").GetComponent <Button>();

            btnQuit.onClick.AddListener(() => Application.Quit());
            btnSetting.onClick.AddListener(() =>
            {
                StopAllCoroutines();
                StartCoroutine(FadeSystem.Fade(groupSettings));
            });

            // 還沒實作按鈕關閉
            // btnSettingClose.onClick.AddListener(() =>
            // {
               // StopAllCoroutines();
                //StartCoroutine(FadeSystem.Fade(groupSettings, false));
            // });
            btnStart.onClick.AddListener(() => StartCoroutine(LoadingManager.instance.Loading("主場景")));
        }
    }
}

