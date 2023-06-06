using UnityEngine.EventSystems;


namespace TowerDefense
{
    /// <summary>
    /// �����, ���������� �� ��������� ������� �� Build Site.
    /// </summary>
    public class UX_NullBuildSite : UX_ClickHandler
    {

        #region Public API

        /// <summary>
        /// ��� ������� �� Build Site �������� ������� �����.
        /// </summary>
        public override void OnPointerDown(PointerEventData eventData)
        {
            InvokeEvent();
        }

        #endregion

    }
}