using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace TowerDefense
{
    /// <summary>
    /// Делегат обработки кликов.
    /// </summary>
    public delegate void ClickToBuildSite();

    /// <summary>
    /// Класс, отвечающий за обработку нажатий на Build Site.
    /// </summary>
    public class UX_ClickHandler : MonoBehaviour, IPointerDownHandler
    {

        #region Properties and Components

        /// <summary>
        /// Событие клика.
        /// </summary>
        public event ClickToBuildSite OnClicked;

        #endregion


        #region Protected API

        /// <summary>
        /// Вызов события от дочерних элементов.
        /// </summary>
        protected void InvokeEvent()
        {
            OnClicked?.Invoke();
        }

        #endregion


        #region Public API

        /// <summary>
        /// Переопределяемый метод реализации интерфейса на нажатия.
        /// </summary>
        public virtual void OnPointerDown(PointerEventData eventData) { }

        #endregion

    }
}