using UnityEngine.EventSystems;


namespace TowerDefense
{
    /// <summary>
    /// Класс, отвечающий за обработку нажатий на Build Site.
    /// </summary>
    public class UX_NullBuildSite : UX_ClickHandler
    {

        #region Public API

        /// <summary>
        /// При нажатии на Build Site вызывает событие клика.
        /// </summary>
        public override void OnPointerDown(PointerEventData eventData)
        {
            InvokeEvent();
        }

        #endregion

    }
}