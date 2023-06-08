using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ������, ���������� �� �������� � ������ ����������� ������ �� ����� �������.
    /// </summary>
    public class MapLevel : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� �������.
        /// </summary>
        [SerializeField] private SO_Level m_Level;

        /// <summary>
        /// ������ �� ���������� �������.
        /// </summary>
        [SerializeField] private UX_LevelPoint m_UX;

        #endregion


        #region Unity Events

        private void Start()
        {
            if (m_UX == null) { Debug.Log("UX_LevelPoint == null!"); return; }

            // �������� �� ������� �����.
            m_UX.OnClicked += StartLevel;
        }

        #endregion


        #region Public API

        /// <summary>
        /// ��������� ��������� �������.
        /// </summary>
        public void StartLevel()
        {
            // ��������.
            if (m_Level == null) { Debug.Log("m_Level == null!"); return; }
            if (m_Level.LevelName == null || m_Level.LevelName == "") { Debug.Log($"LevelName in Level {m_Level} == null!"); return; }

            // ��������� ��������� �������.
            LevelSequenceController.Instance.LoadLevel(m_Level.LevelName);
        }

        #endregion

    }
}