using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense
{
    /// <summary>
    /// Контроллер последовательности уровней (глобальный контроллёр уровней).
    /// </summary>
    public class LevelSequenceController : Singleton<LevelSequenceController>
    {

        #region Properties and Components

        /// <summary>
        /// Статичная строка, хранящая в себе имя сцены главного меню.
        /// </summary>
        public static string MainMenuSceneNickName = "scene_MainMenu";

        /// <summary>
        /// Статичная строка, хранящая в себе имя сцены меню уровней.
        /// </summary>
        public static string LevelMapSceneNickName = "scene_LevelMap";

        /// <summary>
        /// Текущий уровень.
        /// </summary>
        private SO_Level m_CurrentLevel;

        #region Links

        /// <summary>
        /// Текущий уровень.
        /// </summary>
        public SO_Level CurrentLevel => m_CurrentLevel;

        #endregion

        #endregion


        #region Public API

        /// <summary>
        /// Загружает уровень.
        /// </summary>
        /// <param name="level">Уровень для загрузки.</param>
        public void LoadLevel(SO_Level level)
        {
            // Локально запоминает текущий уровень.
            m_CurrentLevel = level;

            // Загрузка уровня.
            SceneManager.LoadScene(level.LevelName);
        }

        /// <summary>
        /// Загрузить сцену главного меню.
        /// </summary>
        public void LoadMainMenuScene()
        {
            SceneManager.LoadScene(MainMenuSceneNickName);
        }

        /// <summary>
        /// Загрузить сцену с картой уровней.
        /// </summary>
        public void LoadLevelMapScene()
        {
            SceneManager.LoadScene(LevelMapSceneNickName);
        }

        #endregion

    }
}