using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TowerDefense
{
    /// <summary>
    /// �����, ���������� � ���� ���������� �� ���������� ���������.
    /// </summary>
    [CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/CreateNewUpgradeCell")]
    public class SO_UpgradeCell : ScriptableObject
    {

        #region Propetries and Components

        /// <summary>
        /// ������� ���������.
        /// </summary>
        [SerializeField] private UpgradeList m_CurrentUpgrade;

        /// <summary>
        /// �������� ���������.
        /// </summary>
        [SerializeField] private Sprite m_UpgradeSprite;

        /// <summary>
        /// ��������� ���������.
        /// </summary>
        [Range(0, 5)]
        [SerializeField] private int m_UpgradeCost;

        /// <summary>
        /// �������� ���������.
        /// </summary>
        [SerializeField] private string m_UpgradeName;

        /// <summary>
        /// �������� ���������.
        /// </summary>
        [SerializeField] private string m_UpgradeInfo;

        #region Links

        /// <summary>
        /// ������� ���������.
        /// </summary>
        public UpgradeList CurrentUpgrade => m_CurrentUpgrade;

        /// <summary>
        /// �������� ���������.
        /// </summary>
        public Sprite UpgradeSprite => m_UpgradeSprite;

        /// <summary>
        /// ��������� ���������.
        /// </summary>
        public int UpgradeCost => m_UpgradeCost;

        /// <summary>
        /// �������� ���������.
        /// </summary>
        public string UpgradeName => m_UpgradeName;

        /// <summary>
        /// �������� ���������.
        /// </summary>
        public string UpgradeInfo => m_UpgradeInfo;

        #endregion

        #endregion

    }
}