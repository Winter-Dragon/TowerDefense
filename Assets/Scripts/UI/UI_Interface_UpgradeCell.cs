using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TowerDefense
{
    /// <summary>
    /// ������ ��� ������ ��������� � ���� ������� ���������.
    /// </summary>
    public class UI_Interface_UpgradeCell : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// SO � ����� �� ���������.
        /// </summary>
        [SerializeField] private SO_UpgradeCell m_UpgradeCell;

        /// <summary>
        /// �������, ����������� ��� ������������� ��������.
        /// </summary>
        [SerializeField] private UI_Interface_UpgradeCell m_RootUpgrade;

        /// <summary>
        /// �������� �� ���������� ������ � �����.
        /// </summary>
        [SerializeField] private Image m_StarImageInBranch;

        /// <summary>
        /// ��������� ��������� � ������ � �����.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_StarCostInBranch;

        #endregion

    }
}