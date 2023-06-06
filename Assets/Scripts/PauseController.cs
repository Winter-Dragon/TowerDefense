namespace TowerDefense
{
    /// <summary>
    /// Класс, вызывающий и обрабатывающий событие паузы.
    /// </summary>
    public class PauseController : Singleton<PauseController>
    {

        #region Properties and Components

        /// <summary>
        /// Событие паузы.
        /// </summary>
        public static event BoolDelegate OnPaused;

        #endregion


        #region Public Events

        /// <summary>
        /// Вызывает событие паузы.
        /// </summary>
        /// <param name="pause">True - пауза, false - возобновление.</param>
        public void Pause(bool pause)
        {
            OnPaused?.Invoke(pause);
        }

        #endregion

    }
}