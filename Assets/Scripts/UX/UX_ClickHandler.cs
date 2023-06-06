using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


namespace TowerDefense
{
    /// <summary>
    /// ������� ��������� ������.
    /// </summary>
    public delegate void ClickToBuildSite();

    /// <summary>
    /// �����, ���������� �� ��������� ������� �� Build Site.
    /// </summary>
    public class UX_ClickHandler : MonoBehaviour, IPointerDownHandler
    {

        #region Properties and Components

        /// <summary>
        /// ������� �����.
        /// </summary>
        public event ClickToBuildSite OnClicked;

        #endregion


        #region Protected API

        /// <summary>
        /// ����� ������� �� �������� ���������.
        /// </summary>
        protected void InvokeEvent()
        {
            OnClicked?.Invoke();
        }

        #endregion


        #region Public API

        /// <summary>
        /// ���������������� ����� ���������� ���������� �� �������.
        /// </summary>
        public virtual void OnPointerDown(PointerEventData eventData) { }

        #endregion

    }
}