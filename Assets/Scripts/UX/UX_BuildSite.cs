using UnityEngine;
using UnityEngine.EventSystems;


namespace TowerDefense
{
    /// <summary>
    /// Класс, отвечающий за обработку нажатий на Build Site.
    /// </summary>
    public class UX_BuildSite : UX_ClickHandler
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на точку для строительства.
        /// </summary>
        [SerializeField] private ConstructionSite m_BuildSite;

        #endregion


        #region Public API

        /// <summary>
        /// При нажатии на Build Site вызывает событие клика.
        /// </summary>
        public override void OnPointerDown(PointerEventData eventData)
        {
            // Проверка на точку постройки и интерфейс постройки.
            if (m_BuildSite == null) { Debug.Log("Build Site is null!"); return; }
            if (UI_Interface_BuyTower.Instance == null) { Debug.Log("UI_Interface_BuyTower.Instance == null!"); return; }

            // Задать точку постройки.
            UI_Interface_BuyTower.Instance.SetBuildSiteToAllElements(m_BuildSite);

            // Вызов события клика из метода родителя.
            InvokeEvent();
        }

        #endregion

    }
}