using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ������ ���������� ������� �����.
    /// </summary>
    public class UI_Interface_BuyTower : Singleton<UI_Interface_BuyTower>
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� ����������.
        /// </summary>
        [SerializeField] private UI_TowerBuyController[] m_TowerBuyControllers;

        /// <summary>
        /// ������ �� Rect Transform.
        /// </summary>
        private RectTransform m_RectTransform;

        #endregion


        #region Unity Events

        private void Start()
        {
            // ����� ��������� Rect Transform.
            m_RectTransform = GetComponent<RectTransform>();
        }

        #endregion


        #region Public API

        /// <summary>
        /// ���������� ��������� � �������� �������.
        /// </summary>
        /// <param name="globalPosition">������� Vector2.</param>
        public void DisplayInterface(Vector2 globalPosition)
        {
            // ������� ���������� ������� � Screen Point �������.
            Vector2 screenPointPosition = Camera.main.WorldToScreenPoint(globalPosition);
            m_RectTransform.anchoredPosition = screenPointPosition;

            // ���������� ���������.
            SetStateInterface(true);
        }

        /// <summary>
        /// ��������/��������� ��������� ���������� ������� �����.
        /// </summary>
        /// <param name="state">true - ��������, false - ���������.</param>
        public void SetStateInterface(bool state)
        {
            for (int i = 0; i < m_TowerBuyControllers.Length; i++)
            {
                m_TowerBuyControllers[i].gameObject.SetActive(state);
            }
        }

        /// <summary>
        /// ��������� ����� ��������� ���� ��������� ����������.
        /// </summary>
        /// <param name="buildSite">����� ���������.</param>
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