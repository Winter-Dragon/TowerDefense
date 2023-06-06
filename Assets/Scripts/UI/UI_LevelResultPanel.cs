using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ������ ����������� ����.
    /// </summary>
    public class UI_LevelResultPanel : Singleton<UI_LevelResultPanel>
    {

        #region Properties and Components

        /// <summary>
        /// ������ � ����������.
        /// </summary>
        [SerializeField] private GameObject m_LosePanel;

        /// <summary>
        /// ������ � �������.
        /// </summary>
        [SerializeField] private GameObject m_WinPanel;

        /// <summary>
        /// ��������� ����������, ������������, �������� �� �������.
        /// </summary>
        private bool isLevelCompleted;

        #endregion


        #region Unity Events

        private void Start()
        {
            // ��������.
            if (m_LosePanel == null || m_WinPanel == null) { Debug.Log("Result Panel is null!"); return; }
            if (LevelController.Instance == null) { Debug.Log("LevelController.Instance == null"); return; }

            // ������ ����������.
            m_LosePanel.gameObject.SetActive(false);
            m_WinPanel.gameObject.SetActive(false);

            // ��������� ������� ������.
            enabled = false;
        }

        private void FixedUpdate()
        {
            // ��������, ��� �� ������ ������ ������.
            if (Destructible.AllDestructibles == null) { Debug.Log("Destructible.AllDestructibles == null! ����� �� ���� �������."); enabled = false; return; }

            // ���� ��� ����� ���������� - ���������� ���������� ������.
            if (Destructible.AllDestructibles.Count == 0) DisplayLevelResult(isLevelCompleted);
        }

        #endregion


        #region Public API

        /// <summary>
        /// ���������� ���� �����������.
        /// </summary>
        /// <param name="isLevelCompleted">��������� ����������� ������.</param>
        public void DisplayLevelResult(bool isLevelCompleted)
        {
            // ���������� ������ ����.
            if (isLevelCompleted) m_WinPanel.gameObject.SetActive(true);
            else m_LosePanel.gameObject.SetActive(true);

            // ��������� Update.
            enabled = false;
        }

        /// <summary>
        /// ������� ���������� ������ ����� ����, ��� ����� ���������� ��� �����.
        /// </summary>
        /// <param name="isCompleted">��������� �����������. true ���� ������� �������.</param>
        public void LevelCompleted(bool isCompleted)
        {
            // ������������ �������� ��������� ����������� � ����������� ������ ������.
            isLevelCompleted = isCompleted;
            enabled = true;
        }

        #endregion

    }
}
