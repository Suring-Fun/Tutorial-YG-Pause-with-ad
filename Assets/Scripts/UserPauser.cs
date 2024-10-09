using System.Collections;
using SuringFun.Library;
using UnityEngine;
using UnityEngine.UI;

namespace SuringFun.Game
{
    /// <summary>
    /// Контролирует пазу, созданную игроком.
    /// </summary>
    public class UserPauser: MonoBehaviour
    {
        [field: SerializeField]
        public Button PauseButton { get; private set; }

        [field: SerializeField]
        public Button ContinueButton { get; private set; }

        private GamePauser.PauseUnit m_pauseUnit;

        void Awake()
        {

            m_pauseUnit = GamePauser.DefinePauseUnit();

            PauseButton.onClick.AddListener(() => m_pauseUnit.Paused = true);
            ContinueButton.onClick.AddListener(() => m_pauseUnit.Paused = false);
        }
    }
}