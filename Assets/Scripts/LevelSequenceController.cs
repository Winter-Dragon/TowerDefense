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

        #endregion


        #region Public API

        /// <summary>
        /// ��������� ������� � ��������� ���������.
        /// </summary>
        /// <param name="level">�������� �����.</param>
        public void LoadLevel(string level)
        {
            SceneManager.LoadScene(level);
        }

        #endregion

    }
}