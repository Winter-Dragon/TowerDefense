using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Контроллёр внутриигрового меню паузы.
    /// </summary>
    public class UI_Controller_PauseMenu : Singleton<UI_Controller_PauseMenu>
    {

        #region Unity Events

        private void Start()
        {
            gameObject.SetActive(false);
        }

        #endregion

        #region Buttons

        /// <summary>
        /// Обработка нажатия на кнопку возобновления игры.
        /// </summary>
        public void ClickContinueButton()
        {
            // Снять с паузы.
            if (PauseController.Instance != null) PauseController.Instance.Pause(false);
            else Debug.Log("PauseController.Instance == null!");

            // Отключить меню.
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Обработка нажатия на кнопку перезапуска уровня.
        /// </summary>
        public void ClickRestartButton()
        {
            // Снять с паузы.
            if (PauseController.Instance != null) PauseController.Instance.Pause(false);
            else Debug.Log("PauseController.Instance == null!");

            // Перезапуск уровня.
            if (LevelSequenceController.Instance != null) LevelSequenceController.Instance.RestartCurrentLevel();
            else Debug.Log("LevelSequenceController == null!");
        }

        /// <summary>
        /// Обработка нажатия на кнопку выхода на карту уровней.
        /// </summary>
        public void ClickLevelMapButton()
        {
            // Снять с паузы.
            if (PauseController.Instance != null) PauseController.Instance.Pause(false);
            else Debug.Log("PauseController.Instance == null!");

            // Выход на карту уровней.
            if (LevelSequenceController.Instance != null) LevelSequenceController.Instance.LoadLevelMapScene();
            else Debug.Log("LevelSequenceController == null!");
        }

        /// <summary>
        /// Обработка нажатия на кнопку выхода в меню.
        /// </summary>
        public void ClickMainMenuButton()
        {
            // Снять с паузы.
            if (PauseController.Instance != null) PauseController.Instance.Pause(false);
            else Debug.Log("PauseController.Instance == null!");

            // Выход в главное меню.
            if (LevelSequenceController.Instance != null) LevelSequenceController.Instance.LoadMainMenuScene();
            else Debug.Log("LevelSequenceController == null!");
        }

        #endregion

    }
}