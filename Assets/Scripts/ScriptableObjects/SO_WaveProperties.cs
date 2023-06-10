using System.Collections.Generic;
using UnityEngine;
using System;

namespace TowerDefense
{
    /// <summary>
    /// �����, ������������� ��������� ����� �� ������: �������� ��� �����, �����, �� ������� �� ��������, ��� ���-�� � �������� ������.
    /// </summary>
    [Serializable]
    public class WaveList
    {
        [SerializeField] private int m_Wave;
        [SerializeField] private SO_Enemy m_Enemy;
        [SerializeField] private int m_Amount;
        [SerializeField] private float m_SpawnSpeed;

        public int Wave => m_Wave;
        public SO_Enemy Enemy => m_Enemy;
        public int Amount => m_Amount;
        public float SpawnSpeed => m_SpawnSpeed;
    }

    /// <summary>
    /// ����� �������� ���� ������ �� ������.
    /// </summary>
    [CreateAssetMenu(fileName = "WaveProperties", menuName = "ScriptableObjects/CreateNewWaveProperties")]
    public class SO_WaveProperties : ScriptableObject
    {

        #region Properties and Components

        /// <summary>
        /// ����� ������.
        /// </summary>
        [SerializeField] private int m_level;

        /// <summary>
        /// ���-�� ���� �� ������.
        /// </summary>
        [SerializeField] private int m_NumberWaves;

        /// <summary>
        /// ������, ������������� ����� �� ������.
        /// </summary>
        [SerializeField] private List<WaveList> m_Waves = new List<WaveList>();

        #region Links

        /// <summary>
        /// ����� ������.
        /// </summary>
        public int LevelNumber => m_level;

        /// <summary>
        /// ���-�� ���� �� ������.
        /// </summary>
        public int NumberWaves => m_NumberWaves;

        /// <summary>
        /// ������ � ����������� ����.
        /// </summary>
        public List<WaveList> Waves => m_Waves;

        #endregion

        #endregion


        #region Public API

        /// <summary>
        /// �����, ������������ ������ ������ � ��������� ����� �� ������.
        /// </summary>
        /// <param name="wave">����� �����.</param>
        /// <returns>������ ������ � �����.</returns>
        public List<SO_Enemy> GetEnemyInWave(int wave)
        {
            // ������� ������ �������� ������.
            List<SO_Enemy> enemyList = new List<SO_Enemy>();

            // ����������� �� ������� ����.
            for(int i = 0; i < m_Waves.Count; i++)
            {
                // ���� ����� ����� ������, ��� �������� ����� - ���������� ���� �� ���� ��������.
                if (wave > m_Waves[i].Wave) continue;

                // ���� ����� ��������� � ������ � �������.
                if (wave == m_Waves[i].Wave)
                {
                    // ��������� � ������ ����� �� ���� �����.
                    enemyList.Add(m_Waves[i].Enemy);
                    continue;
                }

                // ���� ����� ����� ������, ��� �������� ����� - ����� �� �����.
                if (wave < m_Waves[i].Wave) break;
            }

            // ���������� ������ ������.
            return enemyList;
        }

        /// <summary>
        /// �����, ������������ ������ � ���-��� ������ � �����.
        /// ����� ����������� ����� ������ GetEnemyInWave, ���� ����� ������ � ����� > 1.
        /// </summary>
        /// <param name="wave">����� �����.</param>
        /// <returns>���������� ������ � �����.</returns>
        public List<int> GetEnemyAmountInWave(int wave)
        {
            // �������� ������� � ���-���.
            List<int> enemyAmount = new List<int>();

            // ����������� �� ������� ����.
            for (int i = 0; i < m_Waves.Count; i++)
            {
                // ���� ����� ����� ������, ��� �������� ����� - ���������� ���� �� ���� ��������.
                if (wave > m_Waves[i].Wave) continue;

                // ���� ����� ��������� � ������ � �������.
                if (wave == m_Waves[i].Wave)
                {
                    // ��������� � ������ ����� �� ���� �����.
                    enemyAmount.Add(m_Waves[i].Amount);
                    continue;
                }

                // ���� ����� ����� ������, ��� �������� ����� - ����� �� �����.
                if (wave < m_Waves[i].Wave) break;
            }

            // ���������� ������ � ���-��� ������.
            return enemyAmount;
        }

        /// <summary>
        /// �����, ������������ ������ �� ��������� ������ ������ � �����.
        /// ����� ����������� ����� ������ GetEnemyInWave, ���� ����� ������ � ����� > 1.
        /// </summary>
        /// <param name="wave">����� �����.</param>
        /// <returns>�������� ������ ������ � �����.</returns>
        public List<float> GetEnemySpawnSpeed(int wave)
        {
            // �������� ������� �� ��������� ������ ������� �����.
            List<float> enemySpawnTime = new List<float>();

            // ����������� �� ������� ����.
            for (int i = 0; i < m_Waves.Count; i++)
            {
                // ���� ����� ����� ������, ��� �������� ����� - ���������� ���� �� ���� ��������.
                if (wave > m_Waves[i].Wave) continue;

                // ���� ����� ��������� � ������ � �������.
                if (wave == m_Waves[i].Wave)
                {
                    // ��������� � ������ �������� ������ ����� �� ���� �����.
                    enemySpawnTime.Add(m_Waves[i].SpawnSpeed);
                    continue;
                }

                // ���� ����� ����� ������, ��� �������� ����� - ����� �� �����.
                if (wave < m_Waves[i].Wave) break;
            }

            // ���������� ������ �� ��������� ������ ������ � �����.
            return enemySpawnTime;
        }

        #endregion

    }
}