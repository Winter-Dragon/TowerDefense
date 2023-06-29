using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TowerDefense
{
    /// <summary>
    /// ���������� ����� ������� �����.
    /// </summary>
    public class UI_TowerBuyController : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� ��� �����.
        /// </summary>
        [SerializeField] private SO_TowerProperties m_TowerProperties;

        /// <summary>
        /// ������ �� ������ ������.
        /// </summary>
        [SerializeField] private Image m_GoldImage;

        /// <summary>
        /// ������ �� ����� ������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_GoldText;

        /// <summary>
        /// ������ ������ �����.
        /// </summary>
        [SerializeField] private Button m_Button;

        /// <summary>
        /// ������� ��� ��������, ������������ �������� ���������.
        /// </summary>
        [SerializeField] private Image m_ActiveImage;

        /// <summary>
        /// ����������, ������������, ������� �� ��������� �������.
        /// </summary>
        private bool m_IsActive;

        /// <summary>
        /// ����� ��� ��������� �����.
        /// </summary>
        private ConstructionSite m_ConstructionSite;

        #endregion


        #region Unity Events

        private void Awake()
        {
            if (!m_TowerProperties) Debug.Log("Tower Properties is null!");
            // ���� ���� �������������� �����.
            else 
            {
                
                // ���� ������� ����� = 1, ������������� �������� ������ � ��������� ����.
                if (m_TowerProperties.Tier == 1)
                {
                    m_GoldText.text = m_TowerProperties.GoldCost.ToString();
                }
                else { Debug.Log("Tower level > 1! The gold value is not assigned."); }
                
            }
        }

        private void OnEnable()
        {
            // �������� �� ������.
            if (Player.Instance == null) return;

            // �������� ��������� ������.
            UpdateState(Player.Instance.CurrentGold);
        }

        private void Start()
        {
            // ���� ����� ���� - ����������� �� ������� ��������� ������ � ���������� ���������� � �����.
            if (Player.Instance != null) Player.OnGoldUpdate += UpdateState;
            else { Debug.Log("Player.Instance is null!"); return; };

            // �������� ��������� ������.
            UpdateButtonState(Player.Instance.CurrentGold);

            // ����������� �� ������� ����� �� ������.
            m_Button.onClick.AddListener(ClickToButton);

            // ��������� ������� ������.
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            // ���������� �� ������� ����� �� ������.
            m_Button.onClick.RemoveListener(ClickToButton);

            // ���������� �� ������� ������������ ��������� ������.
            Player.OnGoldUpdate -= UpdateState;
        }

        #endregion


        #region Private API

        /// <summary>
        /// �����, ����������� ��������� ���������� � ����������� �� ����, ������� �� ������ ������.
        /// </summary>
        /// <param name="gold">������� ������ ������.</param>
        private void UpdateButtonState(int gold)
        {
            switch (m_IsActive)
            {
                case false:
                    // ��������� �����.
                    if (ColorPallete.Instance != null)
                    {
                        m_GoldImage.color = ColorPallete.Instance.GoldImageColorUnactive;
                        m_GoldText.color = ColorPallete.Instance.GoldTextColorUnactive;
                    }
                    else Debug.Log("Color Pallete is null!");

                    // �������� �������� �������� ������.
                    m_ActiveImage.gameObject.SetActive(false);
                    break;

                case true:
                    // ��������� �����.
                    if (ColorPallete.Instance != null)
                    {
                        m_GoldImage.color = Color.white;
                        m_GoldText.color = ColorPallete.Instance.GoldTextColor;
                    }
                    else Debug.Log("Color Pallete is null!");

                    // �������� �������� �������� �������.
                    m_ActiveImage.gameObject.SetActive(true);
                    break;
            }

            // ������ ���������� �������� ��� �������������.
            m_Button.interactable = m_IsActive;
        }

        /// <summary>
        /// ��� ����� �� ������ �������� ������ � TowerBuyController.Instance.
        /// </summary>
        private void ClickToButton()
        {
            if (TowerBuyController.Instance == null) { Debug.Log("TowerBuyController.Instance == null!"); return; }

            TowerBuyController.Instance.TryBuildTower(m_ConstructionSite, m_TowerProperties, m_TowerProperties.GoldCost);
        }

        #endregion


        #region Public API

        /// <summary>
        /// �����, �����������, ����� �� ��������� ���������.
        /// </summary>
        /// <param name="gold">������� ���-�� ������ ������.</param>
        public void UpdateState(int gold)
        {
            if (!m_TowerProperties) return;

            // ��������, ������� �� ������ ������. ���� ����� ���������� �������� - ����� (����� ������ ��� �� ��������� ���������).
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
        /// ���������� ����� ��� ������������� �����.
        /// </summary>
        /// <param name="buildSite">������ �� ����� �������������.</param>
        public void SetConstructionSite(ConstructionSite buildSite)
        {
            m_ConstructionSite = buildSite;
        }

        #endregion

    }
}