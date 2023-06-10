using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Скрипт, хранящий в себе информацию об уровне.
    /// </summary>
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/CreateNewLevel")]
    public class SO_Level : ScriptableObject
    {

        #region Properties and Components

        /// <summary>
        /// Название сцены уровня.
        /// </summary>
        [SerializeField] private string m_LevelName;

        /// <summary>
        /// Номер уровня.
        /// </summary>
        [SerializeField] private int m_LevelNumber;

        #region Links

        /// <summary>
        /// Название сцены уровня.
        /// </summary>
        public string LevelName => m_LevelName;

        /// <summary>
        /// Номер уровня.
        /// </summary>
        public int LevelNumber => m_LevelNumber;

        #endregion

        #endregion

    }
}