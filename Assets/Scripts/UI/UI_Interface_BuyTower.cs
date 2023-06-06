using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Объект интерфейса покупки башен.
    /// </summary>
    public class UI_Interface_BuyTower : Singleton<UI_Interface_BuyTower>
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на контроллёры.
        /// </summary>
        [SerializeField] private UI_TowerBuyController[] m_TowerBuyControllers;

        /// <summary>
        /// Ссылка на Rect Transform.
        /// </summary>
        private RectTransform m_RectTransform;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Задаём компонент Rect Transform.
            m_RectTransform = GetComponent<RectTransform>();
        }

        #endregion


        #region Public API

        /// <summary>
        /// Отобразить интерфейс в заданной позиции.
        /// </summary>
        /// <param name="globalPosition">Позиция Vector2.</param>
        public void DisplayInterface(Vector2 globalPosition)
        {
            // Перевод глобальной позиции в Screen Point позицию.
            Vector2 screenPointPosition = Camera.main.WorldToScreenPoint(globalPosition);
            m_RectTransform.anchoredPosition = screenPointPosition;

            // Отобразить интерфейс.
            SetStateInterface(true);
        }

        /// <summary>
        /// Включает/отключает видимость интерфейса покупки башен.
        /// </summary>
        /// <param name="state">true - включить, false - отключить.</param>
        public void SetStateInterface(bool state)
        {
            for (int i = 0; i < m_TowerBuyControllers.Length; i++)
            {
                m_TowerBuyControllers[i].gameObject.SetActive(state);
            }
        }

        /// <summary>
        /// Запомнить точку постройки всем элементам интерфейса.
        /// </summary>
        /// <param name="buildSite">Точка постройки.</param>
        public void SetBuildSiteToAllElements(ConstructionSite buildSite)
        {
            for (int i = 0; i < m_TowerBuyControllers.Length; i++)
            {
                m_TowerBuyControllers[i].SetConstructionSite(buildSite);
            }
        }

        #endregion

    }
}