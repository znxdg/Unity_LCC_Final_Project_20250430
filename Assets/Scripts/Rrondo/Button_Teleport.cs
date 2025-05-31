using UnityEngine;
//using UnityEngine.SceneManagement;

namespace YuCheng
{

    public class Button_Teleport:MonoBehaviour
    {
        public void Teleport(string sceneName)
        {
            StartCoroutine(LoadingManager.instance.Loading(sceneName));
        }
    }
}
