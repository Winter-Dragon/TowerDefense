using UnityEngine;
using UnityEngine.EventSystems;

namespace TowerDefense
{
    /// <summary>
    /// ������, ���������� �� ��������� ������� �� ������� �� ����� �������.
    /// </summary>
    public class UX_LevelPoint : UX_ClickHandler
    {

        #region Public API


        public override void OnPointerDown(PointerEventData eventData)
        {
            InvokeEvent();
        }

        #endregion

    }
}