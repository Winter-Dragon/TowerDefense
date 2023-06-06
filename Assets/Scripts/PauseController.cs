namespace TowerDefense
{
    /// <summary>
    /// �����, ���������� � �������������� ������� �����.
    /// </summary>
    public class PauseController : Singleton<PauseController>
    {

        #region Properties and Components

        /// <summary>
        /// ������� �����.
        /// </summary>
        public static event BoolDelegate OnPaused;

        #endregion


        #region Public Events

        /// <summary>
        /// �������� ������� �����.
        /// </summary>
        /// <param name="pause">True - �����, false - �������������.</param>
        public void Pause(bool pause)
        {
            OnPaused?.Invoke(pause);
        }

        #endregion

    }
}