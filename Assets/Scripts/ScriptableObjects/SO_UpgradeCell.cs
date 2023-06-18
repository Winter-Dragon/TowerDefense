using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TowerDefense
{
    /// <summary>
    /// Класс, содержащий в себе информацию об конкретном улучшении.
    /// </summary>
    [CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/CreateNewUpgradeCell")]
    public class SO_UpgradeCell : ScriptableObject
    {

        #region Propetries and Components

        /// <summary>
        /// Текущее улучшение.
        /// </summary>
        [SerializeField] private UpgradeList m_CurrentUpgrade;

        /// <summary>
        /// Картинка улучшения.
        /// </summary>
        [SerializeField] private Sprite m_UpgradeSprite;

        /// <summary>
        /// Стоимость улучшения.
        /// </summary>
        [Range(0, 5)]
        [SerializeField] private int m_UpgradeCost;

        /// <summary>
        /// Название улучшения.
        /// </summary>
        [SerializeField] private string m_UpgradeName;

        /// <summary>
        /// Описание улучшения.
        /// </summary>
        [SerializeField] private string m_UpgradeInfo;

        #region Links

        /// <summary>
        /// Текущее улучшение.
        /// </summary>
        public UpgradeList CurrentUpgrade => m_CurrentUpgrade;

        /// <summary>
        /// Картинка улучшения.
        /// </summary>
        public Sprite UpgradeSprite => m_UpgradeSprite;

        /// <summary>
        /// Стоимость улучшения.
        /// </summary>
        public int UpgradeCost => m_UpgradeCost;

        /// <summary>
        /// Название улучшения.
        /// </summary>
        public string UpgradeName => m_UpgradeName;

        /// <summary>
        /// Описание улучшения.
        /// </summary>
        public string UpgradeInfo => m_UpgradeInfo;

        #endregion

        #endregion

    }
}