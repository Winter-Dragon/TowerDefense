using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{

    /// <summary>
    /// Делегат паузы.
    /// </summary>
    /// <param name="paused">true - пауза, false - возобновление.</param>
    public delegate void OnApplicationPaused(bool paused);

    /// <summary>
    /// 
    /// </summary>
    public class PauseController : Singleton<PauseController>
    {

        #region Properties and Components

        /// <summary>
        /// Событие паузы.
        /// </summary>
        public static event OnApplicationPaused OnPaused;

        #endregion

        #region Public Events


        public void Pause(bool pause)
        {
            OnPaused?.Invoke(pause);
        }

        #endregion
    }
}