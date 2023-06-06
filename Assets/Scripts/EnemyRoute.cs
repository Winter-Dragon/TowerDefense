using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// �����, �������� � ���� ������� �������� ������ � ���� �������� � ������� CircleArea.
    /// </summary>
    public class EnemyRoute : MonoBehaviour
    {

        #region Properties and Compontnts

        /// <summary>
        /// ������� �������� �� Circle Area.
        /// </summary>
        [SerializeField] private List<CircleArea> m_Route;

        #region Links

        /// <summary>
        /// ������� �������� �� Circle Area.
        /// </summary>
        public List<CircleArea> Route => m_Route;

        #endregion

        #endregion

    }
}