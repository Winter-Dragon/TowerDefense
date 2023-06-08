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

        #endregion


        #region Public API

        /// <summary>
        /// Загружает уровень с указанным названием.
        /// </summary>
        /// <param name="level">Название сцены.</param>
        public void LoadLevel(string level)
        {
            SceneManager.LoadScene(level);
        }

        #endregion

    }
}