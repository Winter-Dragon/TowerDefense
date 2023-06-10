using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// ��������� �������� ���� ����.
    /// </summary>
    public class UI_Controller_MainMenu : Singleton<UI_Controller_MainMenu>
    {

        #region Properties and Components

        /// <summary>
        /// ������ ����������� ����.
        /// </summary>
        [SerializeField] private Button m_ButtonContinue;

        #endregion


        #region Unity Events

        private void Start()
        {
            /*
            if (Saver<UI_Controller_MainMenu>.HasFile())
            {

            }
            */
        }

        #endregion


        #region Public API

        /// <summary>
        /// ����� ������ ����� ����.
        /// </summary>
        public void StartNewGame()
        {
            // ������ ����� �������.
            if (LevelSequenceController.Instance != null) LevelSequenceController.Instance.LoadLevelMapScene();
            else Debug.Log("LevelSequenceController.Instance == null!");
    }

        /// <summary>
        /// �����, ����������� ������� ����.
        /// </summary>
        public void ContinueGame()
        {

        }

        /// <summary>
        /// �����, ��������� �� ����.
        /// </summary>
        public void ExitGame()
        {
            Application.Quit();
        }

        #endregion

    }
}