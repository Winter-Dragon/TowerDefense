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
        /// ������� �������������� ����� ��� ���������.
        /// </summary>
        private SO_TurretProperties m_CurrentTurret;

        /// <summary>
        /// ����� ��� ��������� �����.
        /// </summary>
        private ConstructionSite m_ConstructionSite;

        #endregion


        #region Unity Events

        private void Awake()
        {
            // �������� �� ������� ������������� �������.
            if (m_TowerProperties == null) Debug.Log("Tower Properties is null!");
            else
            {
                // �������� �� ������� ������� � ������ �����.
                if (m_TowerProperties.Turrets == null || m_TowerProperties.Turrets.Length == 0) Debug.Log("Turret Properties in TowerProps is null!");
                else
                {
                    // ����������, ������������ ���������� ����� � ������� � ��� �� �������.
                    int count = 0;

                    // ���� �� ���� �������� � �����.
                    for (int i = 0; i < m_TowerProperties.Turrets.Length; i++)
                    {
                        // ���� ������� ����� = 1, ��������� � �������� ��� ���������.
                        if (m_TowerProperties.Turrets[i].Tier == 1)
                        {
                            m_CurrentTurret = m_TowerProperties.Turrets[i];
                            count++;
                        }
                    }

                    // ���� ���-�� ����� � Tier 1: 0 ���� ������ ����� - ��������� �� ������.
                    if (count == 0 || count < 1) { Debug.Log("Turrets in Tier 1 < 1 or 0!"); m_CurrentTurret = null; }
                    // ���� �� �� - ���������� ��������� �������� ������.
                    else
                    {
                        m_GoldText.text = m_CurrentTurret.GoldCost.ToString();
                    }
                }
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

            TowerBuyController.Instance.TryBuildTower(m_ConstructionSite, m_TowerProperties, m_CurrentTurret.GoldCost);
        }

        #endregion


        #region Public API

        /// <summary>
        /// �����, �����������, ����� �� ��������� ���������.
        /// </summary>
        /// <param name="gold">������� ���-�� ������ ������.</param>
        public void UpdateState(int gold)
        {
            // ���� �������� ������� ��� - ����� �� ������.
            if (m_CurrentTurret == null) return;

            // ��������, ������� �� ������ ������. ���� ����� ���������� �������� - ����� (����� ������ ��� �� ��������� ���������).
            switch (m_IsActive)
            {
                case false:
                    if (gold >= m_CurrentTurret.GoldCost)
                    {
                        m_IsActive = true;
                    }
                    else return;
                    break;

                case true:
                    if (gold < m_CurrentTurret.GoldCost)
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