using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// Контроллёр главного меню игры.
    /// </summary>
    public class UI_Controller_MainMenu : Singleton<UI_Controller_MainMenu>
    {

        #region Properties and Components

        /// <summary>
        /// Кнопка продолжения игры.
        /// </summary>
        [SerializeField] private Button m_ButtonContinue;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Кнопка продолжить активна только если есть файл с сохранением.
            m_ButtonContinue.interactable = Saver<UI_Controller_MainMenu>.HasFile(MapCompletion.filename);
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод начала новой игры.
        /// </summary>
        public void StartNewGame()
        {
            // Удаление сохранённого файла.
            if (MapCompletion.Instance != null) MapCompletion.ResetSavedData();
            else Debug.Log("MapCompletion.Instance == null!");

            // Запуск карты уровней.
            if (LevelSequenceController.Instance != null) LevelSequenceController.Instance.LoadLevelMapScene();
            else Debug.Log("LevelSequenceController.Instance == null!");
        }

        /// <summary>
        /// Метод, загружающий прошлую игру.
        /// </summary>
        public void ContinueGame()
        {
            // Запуск карты уровней.
            if (LevelSequenceController.Instance != null) LevelSequenceController.Instance.LoadLevelMapScene();
            else Debug.Log("LevelSequenceController.Instance == null!");
        }

        /// <summary>
        /// Метод, выходящий из игры.
        /// </summary>
        public void ExitGame()
        {
            Application.Quit();
        }

        #endregion

    }
}