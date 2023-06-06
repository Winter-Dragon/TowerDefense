using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Панель результатов игры.
    /// </summary>
    public class UI_LevelResultPanel : Singleton<UI_LevelResultPanel>
    {

        #region Properties and Components

        /// <summary>
        /// Панель с поражением.
        /// </summary>
        [SerializeField] private GameObject m_LosePanel;

        /// <summary>
        /// Панель с победой.
        /// </summary>
        [SerializeField] private GameObject m_WinPanel;

        /// <summary>
        /// Локальная переменная, отображающая, завершён ли уровень.
        /// </summary>
        private bool isLevelCompleted;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Проверки.
            if (m_LosePanel == null || m_WinPanel == null) { Debug.Log("Result Panel is null!"); return; }
            if (LevelController.Instance == null) { Debug.Log("LevelController.Instance == null"); return; }

            // Скрыть интерфейсы.
            m_LosePanel.gameObject.SetActive(false);
            m_WinPanel.gameObject.SetActive(false);

            // Выключить текущий объект.
            enabled = false;
        }

        private void FixedUpdate()
        {
            // Проверка, был ли создан список врагов.
            if (Destructible.AllDestructibles == null) { Debug.Log("Destructible.AllDestructibles == null! Враги не были созданы."); enabled = false; return; }

            // Если все враги уничтожены - отобразить результаты уровня.
            if (Destructible.AllDestructibles.Count == 0) DisplayLevelResult(isLevelCompleted);
        }

        #endregion


        #region Public API

        /// <summary>
        /// Отобразить меню результатов.
        /// </summary>
        /// <param name="isLevelCompleted">Результат прохождения уровня.</param>
        public void DisplayLevelResult(bool isLevelCompleted)
        {
            // Отобразить нужное окно.
            if (isLevelCompleted) m_WinPanel.gameObject.SetActive(true);
            else m_LosePanel.gameObject.SetActive(true);

            // Выключить Update.
            enabled = false;
        }

        /// <summary>
        /// Вывести результаты уровня после того, как будут уничтожены все враги.
        /// </summary>
        /// <param name="isCompleted">Результат прохождения. true если уровень пройден.</param>
        public void LevelCompleted(bool isCompleted)
        {
            // Записывается локально результат прохождения и мониторится список врагов.
            isLevelCompleted = isCompleted;
            enabled = true;
        }

        #endregion

    }
}
