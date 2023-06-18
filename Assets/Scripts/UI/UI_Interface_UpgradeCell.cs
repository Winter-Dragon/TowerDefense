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
        /// Кнопка улучшения в ветке.
        /// </summary>
        [SerializeField] private Button m_CurrentButton;

        /// <summary>
        /// Картинка улучшения в ветке.
        /// </summary>
        [SerializeField] private Image m_UpgradeImageInBranch;

        /// <summary>
        /// Картинка со стоимостью звёзд в ветке.
        /// </summary>
        [SerializeField] private Image m_StarImageInBranch;

        /// <summary>
        /// Стоимость улучшения в звёздах в ветке.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_StarCostInBranch;

        /// <summary>
        /// Отображает, куплен ли текущий апгрейд.
        /// </summary>
        private bool m_UpgradeActive;

        /// <summary>
        /// Отображает, доступен ли к покупке апгрейд.
        /// </summary>
        private bool m_UpgradeAvailable;

        #region Links

        /// <summary>
        /// Отображает, куплен ли этот апгрейд.
        /// </summary>
        public bool UpgradeActive => m_UpgradeActive;

        #endregion

        #endregion


        #region Unity Events

        private void OnEnable()
        {
            // Обновление интерфейса отображения.
            UpdateCellInterface();

            // Задаётся спрайт картинке в ветке.
            m_UpgradeImageInBranch.sprite = m_UpgradeCell.UpgradeSprite;
        }

        private void Start()
        {
            // Подписка на событие клика кнопки.
            m_CurrentButton.onClick.AddListener(UpgradeButtonClick);
            UI_UpgradePanel.Instance.ClickButtonOnUpgradePanel += UpdateCellInterface;
        }

        private void OnDestroy()
        {
            // Отписка от события клика кнопки.
            m_CurrentButton.onClick.RemoveListener(UpgradeButtonClick);
            UI_UpgradePanel.Instance.ClickButtonOnUpgradePanel -= UpdateCellInterface;
        }

        #endregion


        #region Private API

        /// <summary>
        /// Нажатие на кнопку текущего улучшения.
        /// </summary>
        private void UpgradeButtonClick()
        {
            if (UI_UpgradePanel.Instance == null) { Debug.Log("UI_UpgradePanel null!"); return; }

            // Передать инфу об улучшении в меню улучшений.
            UI_UpgradePanel.Instance.ChoseUpgrade(m_UpgradeCell, m_UpgradeActive, m_UpgradeAvailable);
        }

        #endregion


        #region Public API

        /// <summary>
        /// Обновляет отображение текущей ячейки.
        /// </summary>
        public void UpdateCellInterface()
        {
            // Если создан список с апгрейдами.
            if (Upgrades.AllActiveUpgrades != null && Upgrades.AllActiveUpgrades.Count > 0)
            {
                // Перебрать каждый апгрейд.
                foreach (Upgrades.Uprgade upgrade in Upgrades.AllActiveUpgrades)
                {
                    // Если он совпадает с текущим (уже куплен) - чекмарка активируется.
                    if (upgrade.upgrade == m_UpgradeCell.CurrentUpgrade) m_UpgradeActive = true;
                }
                // Если апгрейд уже куплен.
                if (m_UpgradeActive)
                {
                    // Деактивируются элементы в ветке.
                    m_StarImageInBranch.gameObject.SetActive(false);
                    m_StarCostInBranch.gameObject.SetActive(false);
                }
                // Если апгрейд не куплен
                else
                {
                    // Задаётся стоимость в звёздах.
                    m_StarCostInBranch.text = m_UpgradeCell.UpgradeCost.ToString();
                }
            }
            // Если массив не создан.
            else
            {
                // Чекмарка деактивируется.
                m_UpgradeActive = false;
                // Активируются элементы в ветке.
                m_StarImageInBranch.gameObject.SetActive(true);
                m_StarCostInBranch.gameObject.SetActive(true);
                // Задаётся стоимость в звёздах.
                m_StarCostInBranch.text = m_UpgradeCell.UpgradeCost.ToString();
            }

            // Если апгрейд разработан, у него есть родительский апгрейд и родительский апгрейд куплен, звёзд хватает // нет рут апгрейда - текущий доступен к покупке.
            if (m_UpgradeCell.CurrentUpgrade != UpgradeList.ComingSoon && m_RootUpgrade && m_RootUpgrade.m_UpgradeActive == true
                && m_UpgradeCell.UpgradeCost <= UI_UpgradePanel.SrarsRemaining) m_UpgradeAvailable = true;
            if (!m_RootUpgrade && m_UpgradeCell.UpgradeCost <= UI_UpgradePanel.SrarsRemaining) m_UpgradeAvailable = true;

            // Затемняет кнопке, если апгрейд недоступен к покупке.
            if (m_UpgradeAvailable) m_CurrentButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            else m_CurrentButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }

        #endregion

    }
}