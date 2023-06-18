using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// ���� ������� ���������.
    /// </summary>
    public class UI_UpgradePanel : Singleton<UI_UpgradePanel>
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� ������ ������� ��������.
        /// </summary>
        [SerializeField] private Button m_ButtonBuyUpgrade;

        /// <summary>
        /// �������� �� ������ ������� ��������.
        /// </summary>
        [SerializeField] private Image m_ButtonBuyUpgradeImage;

        /// <summary>
        /// ������ �� ������ ������ ���������.
        /// </summary>
        [SerializeField] private Button m_ButtonClearUpgrade;

        /// <summary>
        /// �������� �� ������ ������ ���������.
        /// </summary>
        [SerializeField] private Image m_ButtonClearUpgradeImage;

        /// <summary>
        /// ������ �� ����� ���������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_HeaderText;

        /// <summary>
        /// ������ �� ����� ��������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_InfoText;

        /// <summary>
        /// ������ �� �������� ������.
        /// </summary>
        [SerializeField] private Image m_Star;

        /// <summary>
        /// ������ �� ��������� ��������� � ������.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_StarCostText;

        /// <summary>
        /// �������� ���������� ������� ��������� ���������.
        /// </summary>
        private SO_UpgradeCell m_CurrentUpgrade;

        /// <summary>
        /// ������� ����� �� ������ ������ ���������.
        /// </summary>
        public event EmptyDelegate ClickButtonOnUpgradePanel;

        /// <summary>
        /// ���-�� ���������� ����.
        /// </summary>
        private static int m_StarsCountReceived;

        /// <summary>
        /// ���-�� �������������� ����.
        /// </summary>
        private static int m_StarsCountUsed;

        /// <summary>
        /// ���-�� ���������� ����.
        /// </summary>
        private static int m_StarsRemaining;

        #region Links

        /// <summary>
        /// ���-�� ���������� ����.
        /// </summary>
        public static int SrarsRemaining => m_StarsRemaining;

        #endregion

        #endregion


        #region Unity Events

        private void OnEnable()
        {
            // �������� ��������� ����.
            m_HeaderText.text = "";
            m_InfoText.text = "";
            // ������ �����.
            m_Star.gameObject.SetActive(false);
            m_StarCostText.gameObject.SetActive(false);
            // ������ ������ ������� ��������.
            m_ButtonBuyUpgrade.gameObject.SetActive(false);
            // ���� �� ���� ��������� �� �������
            if (Upgrades.AllActiveUpgrades == null || Upgrades.AllActiveUpgrades.Count == 0)
            {
                // ������ ������ ������ ���������.
                m_ButtonClearUpgrade.interactable = false;
                m_ButtonClearUpgradeImage.color = new Color(1, 1, 1, 0.5f);
            }
            // ���� ���� ��������� ���������
            else
            {
                // �������� ������ ������ ���������.
                m_ButtonClearUpgrade.interactable = true;
                m_ButtonClearUpgradeImage.color = new Color(1, 1, 1, 1);
            }
            // �������� ��������� ���-�� ���������� ����.
            m_StarsCountReceived = MapCompletion.StarsCount;
            // �������� �������� ����.
            UpdateStars();
        }

        private void Start()
        {
            // ����������� �� ������� ����� �� ������.
            m_ButtonBuyUpgrade.onClick.AddListener(ClickButtonResearch);
            m_ButtonClearUpgrade.onClick.AddListener(ClickButtonClear);

            // ������ ���������.
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            // ���������� �� ������� ����� �� ������.
            m_ButtonBuyUpgrade.onClick.RemoveListener(ClickButtonResearch);
            m_ButtonClearUpgrade.onClick.RemoveListener(ClickButtonClear);
        }

        #endregion


        #region Private API

        /// <summary>
        /// ����� ������� �� ������ ������� ���������.
        /// </summary>
        private void ClickButtonResearch()
        {
            if (m_CurrentUpgrade == null) { Debug.Log("Current Upgrade is null!"); return; }

            // �������� ��������� ���������.
            Upgrades.Instance.AddUpgrade(m_CurrentUpgrade.CurrentUpgrade, m_CurrentUpgrade.UpgradeCost);

            // �������� �������� ����.
            UpdateStars();

            // ���������� ����������� ������.
            m_ButtonClearUpgrade.interactable = true;
            m_ButtonClearUpgradeImage.color = new Color(1, 1, 1, 1);
            m_ButtonBuyUpgrade.interactable = false;
            m_ButtonBuyUpgradeImage.color = new Color(1, 1, 1, 0.5f);

            // ����� ������� ��� ���������� ���������� � �������.
            ClickButtonOnUpgradePanel?.Invoke();
        }

        /// <summary>
        /// ����� ������� �� ������ ������ ���������.
        /// </summary>
        private void ClickButtonClear()
        {
            // ����� ���������� ���������.
            Upgrades.Instance.ResetSavedUpgrades();

            // �������� �������� ����.
            UpdateStars();

            // ���������� ����������� ������.
            m_ButtonClearUpgrade.interactable = false;
            m_ButtonClearUpgradeImage.color = new Color(1, 1, 1, 0.5f);
            m_ButtonBuyUpgrade.interactable = true;
            m_ButtonBuyUpgradeImage.color = new Color(1, 1, 1, 1);

            // ����� ������� ��� ���������� ���������� � �������.
            ClickButtonOnUpgradePanel?.Invoke();
        }

        /// <summary>
        /// �����, ����������� �������� ����.
        /// </summary>
        private void UpdateStars()
        {
            // �������� ��������� ���-�� ����������� ����.
            m_StarsCountUsed = Upgrades.GetCountUsedStars();
            // ��������� ���-�� ��������� ��� ������� ����.
            m_StarsRemaining = m_StarsCountReceived - m_StarsCountUsed;
        }

        #endregion


        #region Public API

        /// <summary>
        /// �������� ���������� ��������� ��� ����������� � ����������.
        /// </summary>
        /// <param name="upgrade">SO ���������.</param>
        /// <param name="purnached">������� �� ���������.</param>
        /// <param name="available">�������� �� ��������� � �������.</param>
        public void ChoseUpgrade(SO_UpgradeCell upgrade, bool purnached, bool available)
        {
            // ��������� ��������� ����������� ��������.
            m_CurrentUpgrade = upgrade;
            // �������� ���� �� ���������.
            m_HeaderText.text = upgrade.UpgradeName;
            m_InfoText.text = upgrade.UpgradeInfo;
            // �������� ��������� � ������.
            m_Star.gameObject.SetActive(true);
            m_StarCostText.gameObject.SetActive(true);
            m_StarCostText.text = upgrade.UpgradeCost.ToString();
            // �������� ������ ������� ���������.
            m_ButtonBuyUpgrade.gameObject.SetActive(true);

            // ���� ��������� �������.
            if (purnached)
            {
                // ������ ������� ��������� ������ ������.
                m_ButtonBuyUpgrade.interactable = false;
                m_ButtonBuyUpgradeImage.color = new Color(1, 1, 1, 0.5f);
            }
            // ���� ��������� �� �������.
            else
            {
                // ������ ������� ��������� ����� ������.
                m_ButtonBuyUpgrade.interactable = true;
                m_ButtonBuyUpgradeImage.color = new Color(1, 1, 1, 1);
            }

            // ���� ��������� ���������� � �������.
            if (!available)
            {
                // ������ ������ ����������.
                m_ButtonBuyUpgrade.interactable = false;
                m_ButtonBuyUpgradeImage.color = new Color(1, 1, 1, 0.5f);
            }
        }

        /// <summary>
        /// ��������/������ ��������� ������� ���������.
        /// </summary>
        /// <param name="open">true - ��������, false - ������.</param>
        public void OpenUpgradeInterface(bool open)
        {
            gameObject.SetActive(open);
        }

        #endregion

    }
}