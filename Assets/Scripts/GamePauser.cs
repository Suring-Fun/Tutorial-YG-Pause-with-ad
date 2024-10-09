using JetBrains.Annotations;
using Unity.Mathematics;
using UnityEngine;

namespace SuringFun.Library
{
    /// <summary>
    /// Управляет паузой в игре.
    /// </summary>
    public class GamePauser : MonoBehaviour
    {
        private static GamePauser m_main;

        /// <summary>
        /// Глобальный доступ к паузе.
        /// </summary>
        public static GamePauser Main
        {
            get
            {
                if (!m_main)
                {
                    // Создаём управляющего паузой по запросу.
                    GameObject pauser = new GameObject("<Dynamic> GamePauser");
                    DontDestroyOnLoad(pauser);

                    m_main = pauser.AddComponent<GamePauser>();
                }
                return m_main;
            }
        }

        /// <summary>
        /// Единица паузы. Именно через неё компоненты пытаются остановить игру.
        /// </summary>
        public struct PauseUnit
        {

            #region  Интерфейс использования

            /// <summary>
            /// Устанавливает, желает ли компонент, владеющий единицой паузы, остановить игру.
            /// </summary>
            public bool Paused
            {
                get => m_paused;
                set
                {
                    if (m_paused != value)
                    {
                        if (value)
                            m_owner.PauseRequests++;
                        else
                            m_owner.PauseRequests--;

                        m_paused = value;
                    }
                }
            }

            #endregion

            #region Реализация

            private GamePauser m_owner;

            private bool m_paused;


            public PauseUnit(GamePauser owner)
            {
                m_owner = owner;
                m_paused = false;
            }

            #endregion
        }

        #region  Интерфейс использования

        /// <summary>
        /// Создаёт единицу паузы.
        /// </summary>
        /// <remarks>Упрощает создание единиц паузы. </remarks>
        /// <returns> Единица паузы. Через неё компонент меняет состояние паузы игры. </returns>
        public static PauseUnit DefinePauseUnit()
            => new PauseUnit(GamePauser.Main);

        #endregion

        #region Реализация

        private int m_pauseRequests = 0;

        private int PauseRequests
        {
            get => m_pauseRequests;
            set
            {
                bool itShouldBePausedNow = value > 0;
                bool itWasPausedBefore = m_pauseRequests > 0;

                m_pauseRequests = value;

                if (itShouldBePausedNow != itWasPausedBefore)
                {
                    if (itShouldBePausedNow)
                        ApplyPause();
                    else
                        RevertPause();
                }

            }
        }

        private void ApplyPause()
            => Time.timeScale = 0; // Код меняется от игры к игре.

        private void RevertPause()
            => Time.timeScale = 1; // Код меняется от игры к игре.


        #endregion
    }
}