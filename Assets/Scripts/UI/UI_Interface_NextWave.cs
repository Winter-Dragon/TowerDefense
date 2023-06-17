using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TowerDefense
{
    /// <summary>
    /// ��������� ���������� ������ ����� �����.
    /// </summary>
    public class UI_Interface_NextWave : Singleton<UI_Interface_NextWave>
    {

        #region Properties And Components

        /// <summary>
        /// �������� ����������� �����.
        /// </summary>
        [SerializeField] private Image m_InnerSircleImage;

        /// <summary>
        /// ����� �� ��������� �����.
        /// </summary>
        [SerializeField] private int m_NextWaveTime;

        /// <summary>
        /// ������ � �������� �������.
        /// </summary>
        [SerializeField] private GameObject m_GoldPanel;

        /// <summary>
        /// �����, ������������ ���������� ������ ��� �������� �����.
        /// </summary>
        [SerializeField] private TextMeshProUGUI m_GoldText;

        /// <summary>
        /// ��������� ������.
        /// </summary>
        private Timer m_Timer;

        /// <summary>
        /// �������� ������ �� ������ ��������� �����.
        /// </summary>
        private int m_BonusGold;

        #endregion


        #region Unity Events

        private void OnEnable()
        {
            // �������� �� LevelController.
            if (LevelController.Instance == null) { Debug.Log("LevelController.Instance == null!"); enabled = false; return; }
            // �� ����������, ���� ������� - ���������.
            if (LevelController.Instance.CurrentWave == LevelController.Instance.NumberWaves) { m_BonusGold = 0; NextWave(); }

            // ������ ������� ����������� �����.
            if (m_InnerSircleImage == null) Debug.Log("InnerSircleImage is null!");
            else m_InnerSircleImage.fillAmount = 1;

            // ������������� �������.
            m_Timer = new Timer(m_NextWaveTime, false);

            // ���������� ������ � �������.
            m_GoldPanel.SetActive(true);
            // ���������� ������ � �������� �������.
            m_BonusGold = (int)m_Timer.GetCurrentTime();
            m_GoldText.text = m_BonusGold.ToString();
        }

        private void Start()
        {
            // ������ ������� ����������� �����.
            if (m_InnerSircleImage == null) Debug.Log("InnerSircleImage is null!");
            else m_InnerSircleImage.fillAmount = 1;
        }

        private void FixedUpdate()
        {
            // ���� ������� ����� = 0 - ����� �� ������ � ������ ����� � �������.
            if (LevelController.Instance.CurrentWave == 0)
            {
                m_BonusGold = 0;
                m_GoldPanel.SetActive(false);
                return;
            }

            // ���������� �������.
            m_Timer.UpdateTimer();

            // ���������� ������ � �������� �������.
            m_BonusGold = (int)m_Timer.GetCurrentTime();
            m_GoldText.text = m_BonusGold.ToString();

            // ���� ������ ��������.
            if (m_Timer.IsFinished)
            {
                // ��������� ������� ������.
                gameObject.SetActive(false);
                // ��������� ����� �����.
                LevelController.Instance.StartNextWave();
            }
            // ���� ����� �� ������ - �������� �������� �������� ����������� �������.
            else m_InnerSircleImage.fillAmount = m_Timer.GetCurrentTime() / m_NextWaveTime;
        }

        #endregion


        #region Public API

        /// <summary>
        /// ������ ��������� ����� ��������.
        /// </summary>
        public void NextWave()
        {
            // �������� ������ ������.
            if (Player.Instance != null) Player.Instance.ChangeGold(m_BonusGold);
            else Debug.Log("Player.Instance == null!");

            // ��������� ������� ������.
            gameObject.SetActive(false);
            // ��������� ����� �����.
            LevelController.Instance.StartNextWave();
        }

        #endregion

    }
}