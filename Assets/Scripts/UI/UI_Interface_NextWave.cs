using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
        /// Панель с бонусным золотом.
        /// </summary>
        [SerializeField] private GameObject m_GoldPanel;

        /// <summary>
        /// Текст, отображающий получаемое золото при пропуске волны.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_GoldText;

        /// <summary>
        /// Локальный таймер.
        /// </summary>
        private Timer m_Timer;

        /// <summary>
        /// Бонусное золото за запуск следующей волны.
        /// </summary>
        private int m_BonusGold;

        #endregion


        #region Unity Events

        private void OnEnable()
        {
            // Проверка на LevelController.
            if (LevelController.Instance == null) { Debug.Log("LevelController.Instance == null!"); enabled = false; return; }
            // Не отображать, если уровень - последний.
            if (LevelController.Instance.CurrentWave == LevelController.Instance.NumberWaves) { m_BonusGold = 0; NextWave(); }

            // Полная заливка внутреннего круга.
            if (m_InnerSircleImage == null) Debug.Log("InnerSircleImage is null!");
            else m_InnerSircleImage.fillAmount = 1;

            // Инициализация таймера.
            m_Timer = new Timer(m_NextWaveTime, false);

            // Отобразить панель с золотом.
            m_GoldPanel.SetActive(true);
            // Обновление текста с бонусным золотом.
            m_BonusGold = (int)m_Timer.GetCurrentTime();
            m_GoldText.text = m_BonusGold.ToString();
        }

        private void Start()
        {
            // Полная заливка внутреннего круга.
            if (m_InnerSircleImage == null) Debug.Log("InnerSircleImage is null!");
            else m_InnerSircleImage.fillAmount = 1;
        }

        private void FixedUpdate()
        {
            // Если текущая волна = 0 - выйти из метода и убрать текст с золотом.
            if (LevelController.Instance.CurrentWave == 0)
            {
                m_BonusGold = 0;
                m_GoldPanel.SetActive(false);
                return;
            }

            // Обновление таймера.
            m_Timer.UpdateTimer();

            // Обновление текста с бонусным золотом.
            m_BonusGold = (int)m_Timer.GetCurrentTime();
            m_GoldText.text = m_BonusGold.ToString();

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
            // Добавить игроку золото.
            if (Player.Instance != null) Player.Instance.ChangeGold(m_BonusGold);
            else Debug.Log("Player.Instance == null!");

            // Выключить текущий объект.
            gameObject.SetActive(false);
            // Запустить новую волну.
            LevelController.Instance.StartNextWave();
        }

        #endregion

    }
}