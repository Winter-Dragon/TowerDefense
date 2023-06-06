using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// �����, ���������� �� ��������� ������ �� ������ ������.
    /// </summary>
    public class WorldSpace : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// UX �������, ������������� �������.
        /// </summary>
        [SerializeField] private UX_NullBuildSite m_UX;

        #endregion


        #region Unity Events

        private void Start()
        {
            m_UX.OnClicked += ClickToWorldSpace;
        }

        #endregion


        #region Private API

        /// <summary>
        /// �����, ������������� ����� ����� ������� �� ������� �����.
        /// </summary>
        private void ClickToWorldSpace()
        {
            // �������� �� ������� ���������� ������� �����.
            if (UI_Interface_BuyTower.Instance == null) { Debug.Log("UI_Interface_BuyTower.Instance == null!"); return; }
            // ���� ��������� ���� - ������ ���.
            else UI_Interface_BuyTower.Instance.SetStateInterface(false);
        }

        #endregion

    }
}