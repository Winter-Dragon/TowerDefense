using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Базовый класс всех улучшений.
    /// </summary>
    public class Upgrade : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Список всех активных апгрейдов.
        /// </summary>
        public static List<Upgrade> m_AllActiveUpgrades;

        #endregion


        #region Public API

        /// <summary>
        /// Добавляет улучшение в список активных улучшений.
        /// </summary>
        /// <param name="upgrade">Скрипт апгрейда.</param>
        public void AddUpgrade(Upgrade upgrade)
        {
            // Если списка нет - создать его.
            if (m_AllActiveUpgrades == null) m_AllActiveUpgrades = new();
            // Добавить текущий объект в список.
            m_AllActiveUpgrades.Add(upgrade);
        }

        #endregion

    }
}