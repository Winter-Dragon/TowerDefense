using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Скрипт, хранящий в себе прохождение всех уровней.
    /// </summary>
    public class MapCompletion : Singleton<MapCompletion>
    {

        #region Properties and Components

        /// <summary>
        /// Сохранялка конкретного уровня.
        /// </summary>
        [Serializable]
        public class LevelScore
        {
            /// <summary>
            /// Уровень.
            /// </summary>
            public SO_Level Level;

            /// <summary>
            /// Кол-во звёзд на уровне.
            /// </summary>
            public int LevelStars;
        }

        /// <summary>
        /// Массив с прохождением уровней.
        /// </summary>
        private List <LevelScore> completionData = new List<LevelScore>();

        #endregion


        #region Unity Events

        

        #endregion


        #region Private API

        /// <summary>
        /// Сохраняет/обновляет результаты уровня в массив completionData.
        /// </summary>
        /// <param name="level">Уровень для сохранения.</param>
        /// <param name="levelStars">Кол-во звёзд на уровне.</param>
        private void SaveResult(SO_Level level, int levelStars)
        {
            // Отображает, было ли сохранение результатов.
            bool saveResult = false;

            // Перебираются все сохранённые уровни.
            foreach (LevelScore item in completionData)
            {
                // Если уровень совпадает с текущим уровнем.
                if (item.Level == LevelSequenceController.Instance.CurrentLevel)
                {
                    // Если кол-во звёзд на уровне больше - перезаписывает значение.
                    if (levelStars > item.LevelStars) item.LevelStars = levelStars;

                    // Результаты сохранены.
                    saveResult = true;
                }
            }

            // Если результаты не сохранены.
            if (!saveResult)
            {
                // Создаётся новыя сохранялка.
                LevelScore newLevelScore = new LevelScore();

                // Записываются значения уровня.
                newLevelScore.Level = LevelSequenceController.Instance.CurrentLevel;
                newLevelScore.LevelStars = levelStars;

                // Уровень добавляется в массив.
                completionData.Add(newLevelScore);
            }
        }

        #endregion

        #region Public API

        /// <summary>
        /// Сохраняет результаты прохождения уровня.
        /// </summary>
        /// <param name="isCompleted">Пройден ли уровень.</param>
        /// <param name="levelStars">Кол-во звёзд на уровне.</param>
        public static void SaveLevelResult(bool isCompleted, int levelStars)
        {
            // Если уровень не пройден - не сохранять.
            if (!isCompleted || levelStars == 0) return;

            // Запустить сохранение уровня.
            if (LevelSequenceController.Instance != null)
            {
                if (LevelSequenceController.Instance.CurrentLevel != null) Instance.SaveResult(LevelSequenceController.Instance.CurrentLevel, levelStars);
                else Debug.Log("Level in LevelSequenseController is null!");
            }
            else Debug.Log("LevelSequenseController is null!");
        }

        /// <summary>
        /// Получить сохранённые результаты уровня.
        /// </summary>
        /// <param name="level">Уровень.</param>
        /// <returns>Сохранённые результаты прохождения уровня.</returns>
        public LevelScore GetLevelResult(SO_Level level)
        {
            foreach (LevelScore item in completionData)
            {
                // Если уровень совпадает с текущим уровнем - возвращает уровень.
                if (item.Level == level) return item;
            }

            // Если уровень не сохранён - возвращает null.
            return null;
        }

        /// <summary>
        /// Проверяет, пройден ли уровень с указанным номером.
        /// </summary>
        /// <param name="levelNumber">Номер уровня.</param>
        /// <returns>true - пройдён, false - не пройден.</returns>
        public bool CheckLevelResultByLevelNumber(int levelNumber)
        {
            foreach (LevelScore item in completionData)
            {
                // Если уровень совпадает с текущим уровнем - возвращает тру.
                if (item.Level.LevelNumber == levelNumber) return true;
            }

            // Если уровень не был записан - возвращает false.
            return false;
        }

        #endregion

    }
}