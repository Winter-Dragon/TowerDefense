using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ��������� �������������� ���� �����.
    /// </summary>
    public class UI_Controller_PauseMenu : Singleton<UI_Controller_PauseMenu>
    {

        #region Unity Events

        private void Start()
        {
            gameObject.SetActive(false);
        }

        #endregion

        #region Buttons

        /// <summary>
        /// ��������� ������� �� ������ ������������� ����.
        /// </summary>
        public void ClickContinueButton()
        {
            // ����� � �����.
            if (PauseController.Instance != null) PauseController.Instance.Pause(false);
            else Debug.Log("PauseController.Instance == null!");

            // ��������� ����.
            gameObject.SetActive(false);
        }

        /// <summary>
        /// ��������� ������� �� ������ ����������� ������.
        /// </summary>
        public void ClickRestartButton()
        {
            // ����� � �����.
            if (PauseController.Instance != null) PauseController.Instance.Pause(false);
            else Debug.Log("PauseController.Instance == null!");

            // ���������� ������.
            if (LevelSequenceController.Instance != null) LevelSequenceController.Instance.RestartCurrentLevel();
            else Debug.Log("LevelSequenceController == null!");
        }

        /// <summary>
        /// ��������� ������� �� ������ ������ �� ����� �������.
        /// </summary>
        public void ClickLevelMapButton()
        {
            // ����� � �����.
            if (PauseController.Instance != null) PauseController.Instance.Pause(false);
            else Debug.Log("PauseController.Instance == null!");

            // ����� �� ����� �������.
            if (LevelSequenceController.Instance != null) LevelSequenceController.Instance.LoadLevelMapScene();
            else Debug.Log("LevelSequenceController == null!");
        }

        /// <summary>
        /// ��������� ������� �� ������ ������ � ����.
        /// </summary>
        public void ClickMainMenuButton()
        {
            // ����� � �����.
            if (PauseController.Instance != null) PauseController.Instance.Pause(false);
            else Debug.Log("PauseController.Instance == null!");

            // ����� � ������� ����.
            if (LevelSequenceController.Instance != null) LevelSequenceController.Instance.LoadMainMenuScene();
            else Debug.Log("LevelSequenceController == null!");
        }

        #endregion

    }
}