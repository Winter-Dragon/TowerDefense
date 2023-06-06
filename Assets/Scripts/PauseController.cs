using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{

    /// <summary>
    /// ������� �����.
    /// </summary>
    /// <param name="paused">true - �����, false - �������������.</param>
    public delegate void OnApplicationPaused(bool paused);

    /// <summary>
    /// 
    /// </summary>
    public class PauseController : Singleton<PauseController>
    {

        #region Properties and Components

        /// <summary>
        /// ������� �����.
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