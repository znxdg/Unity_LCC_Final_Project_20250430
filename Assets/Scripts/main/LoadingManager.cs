using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace YuCheng
{

    public class LoadingManager : MonoBehaviour
    {
        private static LoadingManager _instance;
        public static LoadingManager instance
        {
            get
            {
                if (_instance == null) _instance = FindAnyObjectByType<LoadingManager>();
                return _instance;
            }
        }

        [Header("載入介面資料")]
        [SerializeField]
        private CanvasGroup group;
        [SerializeField]
        private Image imgLoadingBar;

        public string prevSceneName {  get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.activeSceneChanged += SceneChanged;
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= SceneChanged;
        }

        private void SceneChanged(Scene arg0, Scene arg1)
        {
            StartCoroutine(FadeSystem.Fade(group, false));
        }

        private void Update()
        {
#if UNITY_EDITOR
            TestLoadingScene();
#endif
        }

#if UNITY_EDITOR
        private void TestLoadingScene()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                StartCoroutine(Loading("製藥場景"));
            }
        } 
#endif

        public IEnumerator Loading(string sceneName)
        {
            prevSceneName = SceneManager.GetActiveScene().name;
            yield return StartCoroutine(FadeSystem.Fade(group));
            AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
            ao.allowSceneActivation = false;

            while (!ao.isDone)
            {
                imgLoadingBar.fillAmount = ao.progress / 0.9f;
                yield return null;

                if(ao.progress == 0.9f)
                {
                    yield return new WaitForSeconds(1);
                    ao.allowSceneActivation = true;
                }
            }
        }
    }
}
