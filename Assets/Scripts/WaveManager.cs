using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ��������� ������� ����� � ������ ������ ���������� �������� � ����������� �� �����.
    /// </summary>
    public class WaveManager : Singleton<WaveManager>
    {

        #region Properties and Components

        /// <summary>
        /// ����� ���-�� ���� �� ������.
        /// </summary>
        private int m_MaxWave;

        #endregion


        #region Unity Events

        private void Start()
        {
            if (LevelController.Instance == null) { Debug.Log("LevelController null."); return; }

            // ����������� �� ������� ��������� ������ �����.
            LevelController.OnWaveUpdate += WaveUpdate;

            // �������� ��������� ����� ��������� �����.
            m_MaxWave = LevelController.Instance.NumberWaves;
        }

        private void OnDestroy()
        {
            // ���������� �� �������.
            LevelController.OnWaveUpdate -= WaveUpdate;
            Enemy.EnemyDestroy -= LastWaveUpdate;
        }

        #endregion


        #region Private API

        /// <summary>
        /// �����, �������� ����������� ����� �����.
        /// </summary>
        /// <param name="currentWave">����� ������� �����.</param>
        private void WaveUpdate(int currentWave)
        {
            // ���� ����� ��������� - ���������� ������ ������.
            if (currentWave == m_MaxWave) Enemy.EnemyDestroy += LastWaveUpdate;
        }

        /// <summary>
        /// �����, ����������� ��� �� ����� �� ������ ����������.
        /// </summary>
        private void LastWaveUpdate()
        {
            // ��������, ��� �� ������ ������ ������.
            if (Destructible.AllDestructibles == null) { Debug.Log("Destructible.AllDestructibles == null! ����� �� ���� �������."); enabled = false; return; }

            // ���� ��� ����� ���������� - ������ ��������� ��������� ������� � ���������� �� �������.
            if (Destructible.AllDestructibles.Count == 0)
            {
                LevelController.Instance.CompleteLevel(true);
                Enemy.EnemyDestroy -= LastWaveUpdate;
            }
        }

        #endregion

    }
}