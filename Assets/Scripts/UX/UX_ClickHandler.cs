using UnityEngine;
using UnityEngine.EventSystems;


namespace TowerDefense
{
    /// <summary>
    /// �����, ���������� �� ��������� ������� �� Build Site.
    /// </summary>
    public class UX_ClickHandler : MonoBehaviour, IPointerDownHandler
    {

        #region Properties and Components

        /// <summary>
        /// ������� �����.
        /// </summary>
        public event EmptyDelegate OnClicked;

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