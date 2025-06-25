using UnityEngine;

namespace YuCheng
{
    /// <summary>
    /// 音效管理器
    /// </summary>
    // RC 要求元件(元件類型) 套用腳本時會自行加入元件
    // AudioSource 播放音樂音效元件
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager _instance;
        public static SoundManager instance
        {
            get
            {
                if (_instance == null) _instance = FindAnyObjectByType<SoundManager>();
                return _instance;
            }
        }

        private AudioSource aud;

        private void Awake()
        {
            aud = GetComponent<AudioSource>();
        }

        /// <summary>
        /// 播放音效：固定音量
        /// </summary>
        /// <param name="sound">音效</param>
        public void PlaySound(AudioClip sound)
        {
            aud.PlayOneShot(sound);
        }

        /// <summary>
        /// 播放音效：隨機音量
        /// </summary>
        /// <param name="sound">音效</param>
        /// <param name="min">最小音量</param>
        /// <param name="max">最大音量</param>
        public void PlaySound(AudioClip sound, float min = 0.7f, float max = 1.2f)
        {
            float volume = Random.Range(min, max);
            aud.PlayOneShot(sound, volume);
        }
    }
}
