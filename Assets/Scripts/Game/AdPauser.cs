using SuringFun.Library;
using UnityEngine;
using YG;

namespace SuringFun.Game
{
    /// <summary>
    /// Ответственен за паузу во время рекламы. Также покажет её.
    /// </summary>
    public class AdPauser : MonoBehaviour
    {
        GamePauser.PauseUnit m_pauseUnit;

        void Awake()
        {
            m_pauseUnit = GamePauser.DefinePauseUnit();

            YandexGame.OpenFullAdEvent += () => m_pauseUnit.Paused = true;
            YandexGame.CloseFullAdEvent += () => m_pauseUnit.Paused = false;
        }

        [ContextMenu("Run Ad")]
        public void RunAd()
        {
            if (Application.isPlaying)
                YandexGame.FullscreenShow();
            else
                Debug.LogError("Run game first!");
        }
    }
}