using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// Меню покупки улучшений.
    /// </summary>
    public class UI_UpgradePanel : Singleton<UI_UpgradePanel>
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на кнопку покупки апгрейда.
        /// </summary>
        [SerializeField] private Button m_ButtonBuyUpgrade;

        /// <summary>
        /// Картинка на кнопке покупки апгрейда.
        /// </summary>
        [SerializeField] private Image m_ButtonBuyUpgradeImage;

        /// <summary>
        /// Ссылка на кнопку сброса улучшений.
        /// </summary>
        [SerializeField] private Button m_ButtonClearUpgrade;

        /// <summary>
        /// Картинка на кнопке сброса улучшений.
        /// </summary>
        [SerializeField] private Image m_ButtonClearUpgradeImage;

        /// <summary>
        /// Ссылка на текст заголовка.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_HeaderText;

        /// <summary>
        /// Ссылка на текст описания.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_InfoText;

        /// <summary>
        /// Ссылка на картинку звезды.
        /// </summary>
        [SerializeField] private Image m_Star;

        /// <summary>
        /// Ссылка на стоимость улучшения в звёздах.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_StarCostText;

        /// <summary>
        /// Локально сохранённое текущее выбранное улучшение.
        /// </summary>
        private SO_UpgradeCell m_CurrentUpgrade;

        /// <summary>
        /// Событие клика на кнопку сброса улучшений.
        /// </summary>
        public event EmptyDelegate ClickButtonOnUpgradePanel;

        /// <summary>
        /// Кол-во полученных звёзд.
        /// </summary>
        private static int m_StarsCountReceived;

        /// <summary>
        /// Кол-во использованных звёзд.
        /// </summary>
        private static int m_StarsCountUsed;

        /// <summary>
        /// Кол-во оставшихся звёзд.
        /// </summary>
        private static int m_StarsRemaining;

        #region Links

        /// <summary>
        /// Кол-во оставшихся звёзд.
        /// </summary>
        public static int SrarsRemaining => m_StarsRemaining;

        #endregion

        #endregion


        #region Unity Events

        private void OnEnable()
        {
            // Очистить текстовые поля.
            m_HeaderText.text = "";
            m_InfoText.text = "";
            // Скрыть звёзды.
            m_Star.gameObject.SetActive(false);
            m_StarCostText.gameObject.SetActive(false);
            // Скрыть кнопку покупки апгрейда.
            m_ButtonBuyUpgrade.gameObject.SetActive(false);
            // Если ни одно улучшение не куплено
            if (Upgrades.AllActiveUpgrades == null || Upgrades.AllActiveUpgrades.Count == 0)
            {
                // Скрыть кнопку сброса улучшений.
                m_ButtonClearUpgrade.interactable = false;
                m_ButtonClearUpgradeImage.color = new Color(1, 1, 1, 0.5f);
            }
            // Если есть купленные улучшения
            else
            {
                // Показать кнопку сброса улучшений.
                m_ButtonClearUpgrade.interactable = true;
                m_ButtonClearUpgradeImage.color = new Color(1, 1, 1, 1);
            }
            // Локально сохранить кол-во полученных звёзд.
            m_StarsCountReceived = MapCompletion.StarsCount;
            // Обновить значение звёзд.
            UpdateStars();
        }

        private void Start()
        {
            // Подписаться на события клика на кнопки.
            m_ButtonBuyUpgrade.onClick.AddListener(ClickButtonResearch);
            m_ButtonClearUpgrade.onClick.AddListener(ClickButtonClear);

            // Скрыть интерфейс.
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            // Отписаться от событий клика на кнопки.
            m_ButtonBuyUpgrade.onClick.RemoveListener(ClickButtonResearch);
            m_ButtonClearUpgrade.onClick.RemoveListener(ClickButtonClear);
        }

        #endregion


        #region Private API

        /// <summary>
        /// Метод нажатия на кнопку покупки улучшения.
        /// </summary>
        private void ClickButtonResearch()
        {
            if (m_CurrentUpgrade == null) { Debug.Log("Current Upgrade is null!"); return; }

            // Добавить выбранное улучшение.
            Upgrades.Instance.AddUpgrade(m_CurrentUpgrade.CurrentUpgrade, m_CurrentUpgrade.UpgradeCost);

            // Обновить значение звёзд.
            UpdateStars();

            // Обновление отображения кнопок.
            m_ButtonClearUpgrade.interactable = true;
            m_ButtonClearUpgradeImage.color = new Color(1, 1, 1, 1);
            m_ButtonBuyUpgrade.interactable = false;
            m_ButtonBuyUpgradeImage.color = new Color(1, 1, 1, 0.5f);

            // Вызов события для обновления интерфейса в ячейках.
            ClickButtonOnUpgradePanel?.Invoke();
        }

        /// <summary>
        /// Метод нажатия на кнопку сброса улучшений.
        /// </summary>
        private void ClickButtonClear()
        {
            // Сброс сохранённых улучшений.
            Upgrades.Instance.ResetSavedUpgrades();

            // Обновить значение звёзд.
            UpdateStars();

            // Обновление отображения кнопок.
            m_ButtonClearUpgrade.interactable = false;
            m_ButtonClearUpgradeImage.color = new Color(1, 1, 1, 0.5f);
            m_ButtonBuyUpgrade.interactable = true;
            m_ButtonBuyUpgradeImage.color = new Color(1, 1, 1, 1);

            // Вызов события для обновления интерфейса в ячейках.
            ClickButtonOnUpgradePanel?.Invoke();
        }

        /// <summary>
        /// Метод, обновляющий значение звёзд.
        /// </summary>
        private void UpdateStars()
        {
            // Локально сохранить кол-во потраченных звёзд.
            m_StarsCountUsed = Upgrades.GetCountUsedStars();
            // Посчитать кол-во свободных для покупок звёзд.
            m_StarsRemaining = m_StarsCountReceived - m_StarsCountUsed;
        }

        #endregion


        #region Public API

        /// <summary>
        /// Выбирает конкретное улучшение для отображения в интерфейсе.
        /// </summary>
        /// <param name="upgrade">SO улучшения.</param>
        /// <param name="purnached">Куплено ли улучшение.</param>
        /// <param name="available">Доступно ли улучшение к покупке.</param>
        public void ChoseUpgrade(SO_UpgradeCell upgrade, bool purnached, bool available)
        {
            // Выбранное улучшение сохраняется локально.
            m_CurrentUpgrade = upgrade;
            // Написать инфу об улучшении.
            m_HeaderText.text = upgrade.UpgradeName;
            m_InfoText.text = upgrade.UpgradeInfo;
            // Показать стоимость в звёздах.
            m_Star.gameObject.SetActive(true);
            m_StarCostText.gameObject.SetActive(true);
            m_StarCostText.text = upgrade.UpgradeCost.ToString();
            // Показать кнопку покупки улучшения.
            m_ButtonBuyUpgrade.gameObject.SetActive(true);

            // Если улучшение куплено.
            if (purnached)
            {
                // Кнопку покупки улучшения нельзя нажать.
                m_ButtonBuyUpgrade.interactable = false;
                m_ButtonBuyUpgradeImage.color = new Color(1, 1, 1, 0.5f);
            }
            // Если улучшение не куплено.
            else
            {
                // Кнопку покупки улучшения можно нажать.
                m_ButtonBuyUpgrade.interactable = true;
                m_ButtonBuyUpgradeImage.color = new Color(1, 1, 1, 1);
            }

            // Если улучшение недрступно к покупке.
            if (!available)
            {
                // Кнопка купить недоступна.
                m_ButtonBuyUpgrade.interactable = false;
                m_ButtonBuyUpgradeImage.color = new Color(1, 1, 1, 0.5f);
            }
        }

        /// <summary>
        /// Показать/скрыть интерфейс покупки апгрейдов.
        /// </summary>
        /// <param name="open">true - показать, false - скрыть.</param>
        public void OpenUpgradeInterface(bool open)
        {
            gameObject.SetActive(open);
        }

        #endregion

    }
}