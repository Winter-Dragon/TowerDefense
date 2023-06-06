using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// Контроллёр интерфейса вызова новой волны.
    /// </summary>
    public class UI_Interface_NextWave : Singleton<UI_Interface_NextWave>
    {

        #region Properties And Components

        /// <summary>
        /// Картинка внутреннего круга.
        /// </summary>
        [SerializeField] private Image m_InnerSircleImage;

        /// <summary>
        /// Время до следующей волны.
        /// </summary>
        [SerializeField] private int m_NextWaveTime;

        /// <summary>
        /// Локальный таймер.
        /// </summary>
        private Timer m_Timer;

        #endregion


        #region Unity Events

        private void OnEnable()
        {
            // Проверка на LevelController.
            if (LevelController.Instance == null) { Debug.Log("LevelController.Instance == null!"); enabled = false; return; }

            // Не отображать, если уровень - последний.
            if (LevelController.Instance.CurrentWave == LevelController.Instance.NumberWaves) { NextWave(); }

            // Полная заливка внутреннего круга.
            if (m_InnerSircleImage == null) Debug.Log("InnerSircleImage is null!");
            else m_InnerSircleImage.fillAmount = 1;

            // Инициализация таймера.
            m_Timer = new Timer(m_NextWaveTime, false);
        }

        private void Start()
        {
            // Полная заливка внутреннего круга.
            if (m_InnerSircleImage == null) Debug.Log("InnerSircleImage is null!");
            else m_InnerSircleImage.fillAmount = 1;
        }

        private void FixedUpdate()
        {
            // Если текущая волна = 0 - выйти из метода.
            if (LevelController.Instance.CurrentWave == 0) return;

            // Обновление таймера.
            m_Timer.UpdateTimer();
            
            // Если таймер завершён.
            if (m_Timer.IsFinished)
            {
                // Выключить текущий объект.
                gameObject.SetActive(false);
                // Запустить новую волну.
                LevelController.Instance.StartNextWave();
            }
            // Если время не прошло - заливать картинку согласно пройденному времени.
            else m_InnerSircleImage.fillAmount = m_Timer.GetCurrentTime() / m_NextWaveTime;
        }

        #endregion


        #region Public API

        /// <summary>
        /// Запуск следующей волны досрочно.
        /// </summary>
        public void NextWave()
        {
            // Выключить текущий объект.
            gameObject.SetActive(false);
            // Запустить новую волну.
            LevelController.Instance.StartNextWave();
        }

        #endregion

    }
}