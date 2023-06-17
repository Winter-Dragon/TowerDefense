using UnityEngine;
using UnityEngine.UI;

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
        /// ����� �� ������ � �������.
        /// </summary>
        [SerializeField] private Image[] m_ActiveStars;

        #endregion


        #region Unity Events

        private void Start()
        {
            if (m_LosePanel == null || m_WinPanel == null) { Debug.Log("Result Panel is null!"); return; }
            if (LevelController.Instance == null) { Debug.Log("LevelController.Instance == null"); return; }

            // ������ ��� �������� ����� �� ������ ������.
            for (int i = 0; i < m_ActiveStars.Length; i++) m_ActiveStars[i].gameObject.SetActive(false);

            // ������ ����������.
            m_LosePanel.gameObject.SetActive(false);
            m_WinPanel.gameObject.SetActive(false);

            // ����������� �� ������� ���������� ������.
            LevelController.LevelCompleted += DisplayLevelResult;
        }

        private void OnDestroy()
        {
            // ���������� �� ������� ���������� ������.
            LevelController.LevelCompleted -= DisplayLevelResult;
        }

        #endregion


        #region Public API

        /// <summary>
        /// ���������� ���� �����������.
        /// </summary>
        /// <param name="isLevelCompleted">��������� ����������� ������.</param>
        public void DisplayLevelResult(bool isLevelCompleted)
        {
            if (PauseController.Instance != null) PauseController.Instance.Pause(true);
            else Debug.Log("Pause Controller is null!");

            // ���������� ������ ����.
            if (isLevelCompleted)
            {
                m_WinPanel.gameObject.SetActive(true);

                // ���������� ������ ���-�� ����.
                int levelStars = LevelController.Instance.LevelStars;
                if (levelStars >= 1) m_ActiveStars[0].gameObject.SetActive(true);
                if (levelStars >= 2) m_ActiveStars[1].gameObject.SetActive(true);
                if (levelStars > 2) m_ActiveStars[2].gameObject.SetActive(true);
            }
            else m_LosePanel.gameObject.SetActive(true);
        }

        /// <summary>
        /// ����� ������� �� ������ ������ �� ����� �������.
        /// </summary>
        public void ClickButtonLevelMap()
        {
            if (LevelSequenceController.Instance != null) LevelSequenceController.Instance.LoadLevelMapScene();
            else Debug.Log("LevelSequenceController == null!");
        }

        /// <summary>
        /// ����� ������� �� ������ ����������� ������.
        /// </summary>
        public void ClickButtonRestartLevel()
        {
            if (LevelSequenceController.Instance != null) LevelSequenceController.Instance.RestartCurrentLevel();
            else Debug.Log("LevelSequenceController == null!");
        }

        /// <summary>
        /// ����� ������� �� ������ ������ � ������� ����.
        /// </summary>
        public void ClickButtonMainMenu()
        {
            if (LevelSequenceController.Instance != null) LevelSequenceController.Instance.LoadMainMenuScene();
            else Debug.Log("LevelSequenceController == null!");
        }

        #endregion

    }
}
