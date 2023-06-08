using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ������, �������� � ���� ���������� �� ������.
    /// </summary>
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/CreateNewLevel")]
    public class SO_Level : ScriptableObject
    {

        #region Properties and Components

        /// <summary>
        /// �������� ����� ������.
        /// </summary>
        [SerializeField] private string m_LevelName;

        #region Links

        /// <summary>
        /// �������� ����� ������.
        /// </summary>
        public string LevelName => m_LevelName;

        #endregion

        #endregion
    }
}