using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TowerDefense
{
    /// <summary>
    /// Скрипт для ячейки улучшений в меню покупки улучшений.
    /// </summary>
    public class UI_Interface_UpgradeCell : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// SO с инфой об улучшении.
        /// </summary>
        [SerializeField] private SO_UpgradeCell m_UpgradeCell;

        /// <summary>
        /// Апгрейд, необходимый для разблокировки текущего.
        /// </summary>
        [SerializeField] private UI_Interface_UpgradeCell m_RootUpgrade;

        /// <summary>
        /// Картинка со стоимостью золота в ветке.
        /// </summary>
        [SerializeField] private Image m_StarImageInBranch;

        /// <summary>
        /// Стоимость улучшения в золоте в ветке.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_StarCostInBranch;

        #endregion

    }
}