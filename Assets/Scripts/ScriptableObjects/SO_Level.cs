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

        /// <summary>
        /// ����� ������.
        /// </summary>
        [SerializeField] private int m_LevelNumber;

        #region Links

        /// <summary>
        /// �������� ����� ������.
        /// </summary>
        public string LevelName => m_LevelName;

        /// <summary>
        /// ����� ������.
        /// </summary>
        public int LevelNumber => m_LevelNumber;

        #endregion

        #endregion

    }
}