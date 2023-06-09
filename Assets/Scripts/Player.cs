using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ������� ����� ������.
    /// </summary>
    public class Player : Singleton<Player>
    {

        #region Properties and Components

        /// <summary>
        /// ���������� �������������� ������.
        /// </summary>
        [SerializeField] private int m_NumberLives;

        /// <summary>
        /// ��������� ���-�� ������.
        /// </summary>
        [SerializeField] private int m_StartGold;

        /// <summary>
        /// ������� ��������� ������ � ������.
        /// </summary>
        public static event IntToVoidDelegate OnGoldUpdate;

        /// <summary>
        /// ������� ��������� ������ � ������.
        /// </summary>
        public static event IntToVoidDelegate OnLiveUpdate;

        /// <summary>
        /// ������� ���������� ������.
        /// </summary>
        private int m_CurrentLives;

        /// <summary>
        /// ���-�� ������ � ������.
        /// </summary>
        private int m_Gold;

        #region Links

        /// <summary>
        /// ������ �� ������� �������� ������.
        /// </summary>
        public int CurrentLives => m_CurrentLives;

        /// <summary>
        /// ������� �������� ������ ������.
        /// </summary>
        public int CurrentGold => m_Gold;

        #endregion

        #endregion


        #region UnityEvents

        private void Start()
        {
            // ������� ��������� ���-�� ������ � ������.
            m_CurrentLives = m_NumberLives;
            m_Gold = m_StartGold;

            // ����� ������� ���������� ������ � ������.
            OnGoldUpdate?.Invoke(m_Gold);
            OnLiveUpdate?.Invoke(m_CurrentLives);
        }

        #endregion


        #region Public API

        /// <summary>
        /// �������� ���������� � ������ ������.
        /// </summary>
        public void Restart()
        {
            // ���������� �������� �������� HP.
            m_CurrentLives = m_NumberLives;
        }

        /// <summary>
        /// �����, �����������/���������� HP.
        /// </summary>
        /// <param name="lives">���-�� ������, ������� ���������� � �������� ��������. ��� ������������� �������� ����� ���������.</param>
        public void ChangeLives(int lives)
        {
            // ��������� ���-�� ������.
            m_CurrentLives += lives;

            // ���� ����� �����������.
            if (m_CurrentLives <= 0)
            {
                // ��������� ������.
                m_CurrentLives = 0;

                LevelController.Instance.CompleteLevel(false);
            }

            // ����� ������� ��������� HP.
            OnLiveUpdate?.Invoke(m_CurrentLives);
        }

        /// <summary>
        /// �������� ������ ������.
        /// </summary>
        /// <param name="gold">���-�� ������.</param>
        public void ChangeGold(int gold)
        {
            m_Gold += gold;

            // ����� ������� ��������� ������.
            OnGoldUpdate?.Invoke(m_Gold);
        }

        #endregion

    }
}