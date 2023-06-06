using UnityEngine;
using UnityEngine.EventSystems;


namespace TowerDefense
{
    /// <summary>
    /// �����, ���������� �� ��������� ������� �� Build Site.
    /// </summary>
    public class UX_BuildSite : UX_ClickHandler
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� ����� ��� �������������.
        /// </summary>
        [SerializeField] private ConstructionSite m_BuildSite;

        #endregion


        #region Public API

        /// <summary>
        /// ��� ������� �� Build Site �������� ������� �����.
        /// </summary>
        public override void OnPointerDown(PointerEventData eventData)
        {
            // �������� �� ����� ��������� � ��������� ���������.
            if (m_BuildSite == null) { Debug.Log("Build Site is null!"); return; }
            if (UI_Interface_BuyTower.Instance == null) { Debug.Log("UI_Interface_BuyTower.Instance == null!"); return; }

            // ������ ����� ���������.
            UI_Interface_BuyTower.Instance.SetBuildSiteToAllElements(m_BuildSite);

            // ����� ������� ����� �� ������ ��������.
            InvokeEvent();
        }

        #endregion

    }
}