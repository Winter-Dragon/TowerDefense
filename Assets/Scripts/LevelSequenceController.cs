using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense
{
    /// <summary>
    /// ���������� ������������������ ������� (���������� ��������� �������).
    /// </summary>
    public class LevelSequenceController : Singleton<LevelSequenceController>
    {

        #region Properties and Components

        /// <summary>
        /// ��������� ������, �������� � ���� ��� ����� �������� ����.
        /// </summary>
        public static string MainMenuSceneNickName = "scene_MainMenu";

        /// <summary>
        /// ��������� ������, �������� � ���� ��� ����� ���� �������.
        /// </summary>
        public static string LevelMapSceneNickName = "scene_LevelMap";

        /// <summary>
        /// ������� �������.
        /// </summary>
        private SO_Level m_CurrentLevel;

        #region Links

        /// <summary>
        /// ������� �������.
        /// </summary>
        public SO_Level CurrentLevel => m_CurrentLevel;

        #endregion

        #endregion


        #region Public API

        /// <summary>
        /// ��������� �������.
        /// </summary>
        /// <param name="level">������� ��� ��������.</param>
        public void LoadLevel(SO_Level level)
        {
            // �������� ���������� ������� �������.
            m_CurrentLevel = level;

            // �������� ������.
            SceneManager.LoadScene(level.LevelName);
        }

        /// <summary>
        /// ��������� ����� �������� ����.
        /// </summary>
        public void LoadMainMenuScene()
        {
            SceneManager.LoadScene(MainMenuSceneNickName);
        }

        /// <summary>
        /// ��������� ����� � ������ �������.
        /// </summary>
        public void LoadLevelMapScene()
        {
            SceneManager.LoadScene(LevelMapSceneNickName);
        }

        #endregion

    }
}