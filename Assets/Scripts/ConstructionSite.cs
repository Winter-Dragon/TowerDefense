using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ����� ����� ��� ��������� �����.
    /// </summary>
    public class ConstructionSite : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� ���������� ������.
        /// </summary>
        [SerializeField] private UX_BuildSite m_UX;

        #endregion


        #region Unity Events

        private void Start()
        {
            // �������� �� ������� �����.
            m_UX.OnClicked += ClickEvent;
        }

        private void OnDestroy()
        {
            // ���������� �� ������� �����.
            m_UX.OnClicked -= ClickEvent;
        }

        #endregion


        #region Private API


        private void ClickEvent()
        {
            // �������� �� ������� ���������� ������� �� �����.
            if (UI_Interface_BuyTower.Instance == null) { Debug.Log("UI_Interface_BuyTower.Instance == null!"); return; }

            // ���������� ��������� � �������� �������.
            UI_Interface_BuyTower.Instance.DisplayInterface(transform.position);
        }

        #endregion


        #region Public API

        /// <summary>
        /// ����� ��������� ����� �� �����, ��� ���������� construstion site.
        /// </summary>
        /// <param name="tower">������ �����.</param>
        public void BuildTower(GameObject tower)
        {
            // ������� �����, ������ �������.
            tower = Instantiate(tower);
            tower.transform.position = transform.position;

            // ���������� ������� ������.
            Destroy(gameObject);
        }

        #endregion

    }
}
