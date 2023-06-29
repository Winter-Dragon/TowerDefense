using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TowerDefense
{
    /// <summary>
    /// Контроллер слота покупки башни.
    /// </summary>
    public class UI_TowerBuyController : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на вид башни.
        /// </summary>
        [SerializeField] private SO_TowerProperties m_TowerProperties;

        /// <summary>
        /// Ссылка на спрайт золота.
        /// </summary>
        [SerializeField] private Image m_GoldImage;

        /// <summary>
        /// Ссылка на текст золота.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_GoldText;

        /// <summary>
        /// Кнопка выбора башни.
        /// </summary>
        [SerializeField] private Button m_Button;

        /// <summary>
        /// Обводка для карточки, отображающая активное состояние.
        /// </summary>
        [SerializeField] private Image m_ActiveImage;

        /// <summary>
        /// Переменная, отображающая, активен ли интерфейс покупки.
        /// </summary>
        private bool m_IsActive;

        /// <summary>
        /// Точка для постройки башни.
        /// </summary>
        private ConstructionSite m_ConstructionSite;

        #endregion


        #region Unity Events

        private void Awake()
        {
            if (!m_TowerProperties) Debug.Log("Tower Properties is null!");
            // Если есть характеристики башни.
            else 
            {
                
                // Если уровень башни = 1, устанавливает значение золота в текстовое поле.
                if (m_TowerProperties.Tier == 1)
                {
                    m_GoldText.text = m_TowerProperties.GoldCost.ToString();
                }
                else { Debug.Log("Tower level > 1! The gold value is not assigned."); }
                
            }
        }

        private void OnEnable()
        {
            // Проверка на игрока.
            if (Player.Instance == null) return;

            // Обновить состояние кнопки.
            UpdateState(Player.Instance.CurrentGold);
        }

        private void Start()
        {
            // Если игрок есть - подписаться на событие изменение золота и передавать информацию в метод.
            if (Player.Instance != null) Player.OnGoldUpdate += UpdateState;
            else { Debug.Log("Player.Instance is null!"); return; };

            // Обновить состояние кнопки.
            UpdateButtonState(Player.Instance.CurrentGold);

            // Подписаться на событие клика на кнопку.
            m_Button.onClick.AddListener(ClickToButton);

            // Отключить игровой объект.
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            // Отписаться от события клика на кнопку.
            m_Button.onClick.RemoveListener(ClickToButton);

            // Отписаться от события отслеживания состояния золота.
            Player.OnGoldUpdate -= UpdateState;
        }

        #endregion


        #region Private API

        /// <summary>
        /// Метод, обновляющий состояние интерфейса в зависимости от того, хватает ли игроку золота.
        /// </summary>
        /// <param name="gold">Текущее золото игрока.</param>
        private void UpdateButtonState(int gold)
        {
            switch (m_IsActive)
            {
                case false:
                    // Изменение цвета.
                    if (ColorPallete.Instance != null)
                    {
                        m_GoldImage.color = ColorPallete.Instance.GoldImageColorUnactive;
                        m_GoldText.color = ColorPallete.Instance.GoldTextColorUnactive;
                    }
                    else Debug.Log("Color Pallete is null!");

                    // Подложка активной карточки скрыта.
                    m_ActiveImage.gameObject.SetActive(false);
                    break;

                case true:
                    // Изменение цвета.
                    if (ColorPallete.Instance != null)
                    {
                        m_GoldImage.color = Color.white;
                        m_GoldText.color = ColorPallete.Instance.GoldTextColor;
                    }
                    else Debug.Log("Color Pallete is null!");

                    // Подложка активной карточки открыта.
                    m_ActiveImage.gameObject.SetActive(true);
                    break;
            }

            // Кнопка становится активной при необходимости.
            m_Button.interactable = m_IsActive;
        }

        /// <summary>
        /// При клике на кнопку передать данные в TowerBuyController.Instance.
        /// </summary>
        private void ClickToButton()
        {
            if (TowerBuyController.Instance == null) { Debug.Log("TowerBuyController.Instance == null!"); return; }

            TowerBuyController.Instance.TryBuildTower(m_ConstructionSite, m_TowerProperties, m_TowerProperties.GoldCost);
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод, проверяющий, стоит ли обновлять интерфейс.
        /// </summary>
        /// <param name="gold">Текущее кол-во золота игрока.</param>
        public void UpdateState(int gold)
        {
            if (!m_TowerProperties) return;

            // Проверка, хватает ли игроку золота. Если метод вызывается повторно - выйти (чтобы лишний раз не обновлять интерфейс).
            switch (m_IsActive)
            {
                case false:
                    if (gold >= m_TowerProperties.GoldCost)
                    {
                        m_IsActive = true;
                    }
                    else return;
                    break;

                case true:
                    if (gold < m_TowerProperties.GoldCost)
                    {
                        m_IsActive = false;
                    }
                    else return;
                    break;
            }

            UpdateButtonState(gold);
        }

        /// <summary>
        /// Установить точку для строительства башни.
        /// </summary>
        /// <param name="buildSite">Ссылка на точку строительства.</param>
        public void SetConstructionSite(ConstructionSite buildSite)
        {
            m_ConstructionSite = buildSite;
        }

        #endregion

    }
}