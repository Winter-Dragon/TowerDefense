using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ������� ����� ���� ���������.
    /// </summary>
    public class Upgrade : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������ ���� �������� ���������.
        /// </summary>
        public static List<Upgrade> m_AllActiveUpgrades;

        #endregion


        #region Public API

        /// <summary>
        /// ��������� ��������� � ������ �������� ���������.
        /// </summary>
        /// <param name="upgrade">������ ��������.</param>
        public void AddUpgrade(Upgrade upgrade)
        {
            // ���� ������ ��� - ������� ���.
            if (m_AllActiveUpgrades == null) m_AllActiveUpgrades = new();
            // �������� ������� ������ � ������.
            m_AllActiveUpgrades.Add(upgrade);
        }

        #endregion

    }
}