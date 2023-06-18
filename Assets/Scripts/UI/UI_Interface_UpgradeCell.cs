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
        /// ������ ��������� � �����.
        /// </summary>
        [SerializeField] private Button m_CurrentButton;

        /// <summary>
        /// �������� ��������� � �����.
        /// </summary>
        [SerializeField] private Image m_UpgradeImageInBranch;

        /// <summary>
        /// �������� �� ���������� ���� � �����.
        /// </summary>
        [SerializeField] private Image m_StarImageInBranch;

        /// <summary>
        /// ��������� ��������� � ������ � �����.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_StarCostInBranch;

        /// <summary>
        /// ����������, ������ �� ������� �������.
        /// </summary>
        private bool m_UpgradeActive;

        /// <summary>
        /// ����������, �������� �� � ������� �������.
        /// </summary>
        private bool m_UpgradeAvailable;

        #region Links

        /// <summary>
        /// ����������, ������ �� ���� �������.
        /// </summary>
        public bool UpgradeActive => m_UpgradeActive;

        #endregion

        #endregion


        #region Unity Events

        private void OnEnable()
        {
            // ���������� ���������� �����������.
            UpdateCellInterface();

            // ������� ������ �������� � �����.
            m_UpgradeImageInBranch.sprite = m_UpgradeCell.UpgradeSprite;
        }

        private void Start()
        {
            // �������� �� ������� ����� ������.
            m_CurrentButton.onClick.AddListener(UpgradeButtonClick);
            UI_UpgradePanel.Instance.ClickButtonOnUpgradePanel += UpdateCellInterface;
        }

        private void OnDestroy()
        {
            // ������� �� ������� ����� ������.
            m_CurrentButton.onClick.RemoveListener(UpgradeButtonClick);
            UI_UpgradePanel.Instance.ClickButtonOnUpgradePanel -= UpdateCellInterface;
        }

        #endregion


        #region Private API

        /// <summary>
        /// ������� �� ������ �������� ���������.
        /// </summary>
        private void UpgradeButtonClick()
        {
            if (UI_UpgradePanel.Instance == null) { Debug.Log("UI_UpgradePanel null!"); return; }

            // �������� ���� �� ��������� � ���� ���������.
            UI_UpgradePanel.Instance.ChoseUpgrade(m_UpgradeCell, m_UpgradeActive, m_UpgradeAvailable);
        }

        #endregion


        #region Public API

        /// <summary>
        /// ��������� ����������� ������� ������.
        /// </summary>
        public void UpdateCellInterface()
        {
            // ���� ������ ������ � ����������.
            if (Upgrades.AllActiveUpgrades != null && Upgrades.AllActiveUpgrades.Count > 0)
            {
                // ��������� ������ �������.
                foreach (Upgrades.Uprgade upgrade in Upgrades.AllActiveUpgrades)
                {
                    // ���� �� ��������� � ������� (��� ������) - �������� ������������.
                    if (upgrade.upgrade == m_UpgradeCell.CurrentUpgrade) m_UpgradeActive = true;
                }
                // ���� ������� ��� ������.
                if (m_UpgradeActive)
                {
                    // �������������� �������� � �����.
                    m_StarImageInBranch.gameObject.SetActive(false);
                    m_StarCostInBranch.gameObject.SetActive(false);
                }
                // ���� ������� �� ������
                else
                {
                    // ������� ��������� � ������.
                    m_StarCostInBranch.text = m_UpgradeCell.UpgradeCost.ToString();
                }
            }
            // ���� ������ �� ������.
            else
            {
                // �������� ��������������.
                m_UpgradeActive = false;
                // ������������ �������� � �����.
                m_StarImageInBranch.gameObject.SetActive(true);
                m_StarCostInBranch.gameObject.SetActive(true);
                // ������� ��������� � ������.
                m_StarCostInBranch.text = m_UpgradeCell.UpgradeCost.ToString();
            }

            // ���� ������� ����������, � ���� ���� ������������ ������� � ������������ ������� ������, ���� ������� // ��� ��� �������� - ������� �������� � �������.
            if (m_UpgradeCell.CurrentUpgrade != UpgradeList.ComingSoon && m_RootUpgrade && m_RootUpgrade.m_UpgradeActive == true
                && m_UpgradeCell.UpgradeCost <= UI_UpgradePanel.SrarsRemaining) m_UpgradeAvailable = true;
            if (!m_RootUpgrade && m_UpgradeCell.UpgradeCost <= UI_UpgradePanel.SrarsRemaining) m_UpgradeAvailable = true;

            // ��������� ������, ���� ������� ���������� � �������.
            if (m_UpgradeAvailable) m_CurrentButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            else m_CurrentButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }

        #endregion

    }
}