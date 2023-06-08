using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Скрипт, отвечающий за хранение и запуск конкретного уровня на карте уровней.
    /// </summary>
    public class MapLevel : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на уровень.
        /// </summary>
        [SerializeField] private SO_Level m_Level;

        /// <summary>
        /// Ссылка на обработчик нажатий.
        /// </summary>
        [SerializeField] private UX_LevelPoint m_UX;

        #endregion


        #region Unity Events

        private void Start()
        {
            if (m_UX == null) { Debug.Log("UX_LevelPoint == null!"); return; }

            // Подписка на событие клика.
            m_UX.OnClicked += StartLevel;
        }

        #endregion


        #region Public API

        /// <summary>
        /// Загружает указанный уровень.
        /// </summary>
        public void StartLevel()
        {
            // Проверки.
            if (m_Level == null) { Debug.Log("m_Level == null!"); return; }
            if (m_Level.LevelName == null || m_Level.LevelName == "") { Debug.Log($"LevelName in Level {m_Level} == null!"); return; }

            // Загрузить указанный уровень.
            LevelSequenceController.Instance.LoadLevel(m_Level.LevelName);
        }

        #endregion

    }
}