using UnityEngine;
using UnityEngine.UI;

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
        /// ��������� ������.
        /// </summary>
        private Timer m_Timer;

        #endregion


        #region Unity Events

        private void OnEnable()
        {
            // �������� �� LevelController.
            if (LevelController.Instance == null) { Debug.Log("LevelController.Instance == null!"); enabled = false; return; }

            // �� ����������, ���� ������� - ���������.
            if (LevelController.Instance.CurrentWave == LevelController.Instance.NumberWaves) { NextWave(); }

            // ������ ������� ����������� �����.
            if (m_InnerSircleImage == null) Debug.Log("InnerSircleImage is null!");
            else m_InnerSircleImage.fillAmount = 1;

            // ������������� �������.
            m_Timer = new Timer(m_NextWaveTime, false);
        }

        private void Start()
        {
            // ������ ������� ����������� �����.
            if (m_InnerSircleImage == null) Debug.Log("InnerSircleImage is null!");
            else m_InnerSircleImage.fillAmount = 1;
        }

        private void FixedUpdate()
        {
            // ���� ������� ����� = 0 - ����� �� ������.
            if (LevelController.Instance.CurrentWave == 0) return;

            // ���������� �������.
            m_Timer.UpdateTimer();
            
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
            // ��������� ������� ������.
            gameObject.SetActive(false);
            // ��������� ����� �����.
            LevelController.Instance.StartNextWave();
        }

        #endregion

    }
}