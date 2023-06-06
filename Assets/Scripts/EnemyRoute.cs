using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Класс, хранящий в себе маршрут движения врагов в виде объектов с классом CircleArea.
    /// </summary>
    public class EnemyRoute : MonoBehaviour
    {

        #region Properties and Compontnts

        /// <summary>
        /// Маршрут движения из Circle Area.
        /// </summary>
        [SerializeField] private List<CircleArea> m_Route;

        #region Links

        /// <summary>
        /// Маршрут движения из Circle Area.
        /// </summary>
        public List<CircleArea> Route => m_Route;

        #endregion

        #endregion

    }
}