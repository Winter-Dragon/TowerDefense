using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ������� ����� ������.
    /// </summary>
    public enum SpawnMode
    {
        Spawn,
        Pause,
        Completed
    }

    /// <summary>
    /// �����, ��������� ������� ��������.
    /// </summary>
    [RequireComponent(typeof(CircleArea))]
    public class EnemySpawner : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� ��������� �����.
        /// </summary>
        [SerializeField] private SO_WaveProperties m_WaveProperties;

        /// <summary>
        /// ������������ ������ �������� �������.
        /// </summary>
        [SerializeField] private Enemy m_EnemyPrefab;

        /// <summary>
        /// ���� ������ ���������.
        /// </summary>
        private CircleArea m_Area;

        /// <summary>
        /// ���-�� ��������, ������� ��������� �� ���.
        /// </summary>
        [SerializeField] private int m_NumberSpawns;

        /// <summary>
        /// ��� ����� ����������� ������ ������.
        /// </summary>
        private float m_RespawnTime;

        /// <summary>
        /// ���������� ������.
        /// </summary>
        private Timer m_Timer;

        /// <summary>
        /// ������ �� ������� ��������.
        /// </summary>
        [SerializeField] private EnemyRoute m_EnemyRoute;

        /// <summary>
        /// ������ ������ ��� ������ � ������� �����.
        /// </summary>
        private List<SO_Enemy> m_CurrentEnemyList;

        /// <summary>
        /// ������ � ���-��� ������ ��� ������.
        /// </summary>
        private List<int> m_CurrentEnemyCountList;

        /// <summary>
        /// ������ �� ��������� ������ ������ � �����.
        /// </summary>
        private List<float> m_RespawnTimeList;

        /// <summary>
        /// ������� ����� ��������.
        /// </summary>
        private SpawnMode m_Mode;

        /// <summary>
        /// ������� ������ ������� ������.
        /// </summary>
        private int m_CurrentEnemyIndex;

        /// <summary>
        /// ��������� ������ ���� ��������� �� �����.
        /// </summary>
        private static HashSet<EnemySpawner> m_AllSpawners;

        /// <summary>
        /// ���������� ����� ������ �� �����.
        /// </summary>
        private SpawnMode m_SpawnModeUntilPause;

        #region Links

        /// <summary>
        /// ������� ����� ��������.
        /// </summary>
        public SpawnMode Mode => m_Mode;

        /// <summary>
        /// ������ ������ ��� ��������� ��� ������ ���� ��������� �� �����.
        /// </summary>
        public static IReadOnlyCollection<EnemySpawner> AllSpawners => m_AllSpawners;

        #endregion

        #endregion


        #region UnityEvents

        private void Start()
        {
            // ���� ������ ��� - ������� ����� ������.
            if (m_AllSpawners == null) m_AllSpawners = new HashSet<EnemySpawner>();
            // ��������� ������� ������ � ������.
            m_AllSpawners.Add(this);

            // �� ������� ������� ��������� ���� ������.
            m_Area = GetComponent<CircleArea>();

            // �������� ������ �� ������� ������.
            m_Timer = new Timer(m_RespawnTime, true);

            // �������� � �����.
            m_Mode = SpawnMode.Pause;

            // ��������� ��������.
            TuneSpawner();

            // ����������� �� ������� �����.
            PauseController.OnPaused += Paused;
        }

        private void OnDestroy()
        {
            // ���������� �� ������� �����.
            PauseController.OnPaused -= Paused;

            // ������ ������� ������� �� ������.
            m_AllSpawners.Remove(this);
        }

        private void FixedUpdate()
        {
            // ��������� ������.
            m_Timer.UpdateTimer();

            // ���� ����� ������ ��� �� �������, �������� ���������� ������ � ����� �� ������.
            if (m_Timer.IsFinished)
            {
                UpdateSpawner();
            }
        }

        #endregion


        #region Private API

        /// <summary>
        /// �����, ��������� ��������� �������� � ��������� �������.
        /// </summary>
        private void UpdateSpawner()
        {
            // �������� �� ��� �������� "�����".
            if (m_Mode != SpawnMode.Spawn) return;
            // �������� �� LevelController.
            if (LevelController.Instance == null) { Debug.Log("LevelController is null!"); return; }
            // �������� �� ������� ������� ������� ������.
            if (m_EnemyPrefab == null) { Debug.Log("m_EnemyPrefab is null!"); return; }

            // ���� ������ ������ �� ��������� �� ��������� ������ �����.
            if (m_RespawnTimeList[m_CurrentEnemyIndex] != m_RespawnTime)
            {
                // ������� ������ ������.
                m_RespawnTime = m_RespawnTimeList[m_CurrentEnemyIndex];
                // �������� ������ �� ������� ������.
                m_Timer = new Timer(m_RespawnTime, true);

                // �� ������� � ������� ��������.
                return;
            }

            for(int i = 0; i < m_NumberSpawns; i++)
            {
                // ��������, ����� ������ ������ ����.
                if (m_CurrentEnemyList.Count == 1)
                {
                    // ���� ������� ��� �� ������ ���� ������ �� �����.
                    if (m_CurrentEnemyCountList[m_CurrentEnemyIndex] > 0)
                    {
                        // ����� �����.
                        SpawnEnemy();
                    }
                    // ���� ������� ������ ���� ������.
                    else
                    {
                        // ����� ��� "��������".
                        m_Mode = SpawnMode.Completed;

                        // ��������� �������.
                        m_CurrentEnemyIndex = 0;

                        // ���������� ��������� � ���������� ������.
                        LevelController.Instance.SpawnCompleted();

                        return;
                    }
                }
                // ��������, ���� �������� ��� ������ ���������.
                else
                {
                    // ���� ������� ��� �� ������ ���� ������ �� ���������� ������� �� �����.
                    if (m_CurrentEnemyCountList[m_CurrentEnemyIndex] > 0)
                    {
                        // ����� �����.
                        SpawnEnemy();
                    }
                    // ���� ������� ������ ���� ������.
                    else
                    {
                        // ������ ����� �� �����++.
                        m_CurrentEnemyIndex++;

                        // ���� ������ ������, ��� ���-�� ������ ������ � �����.
                        if (m_CurrentEnemyIndex > m_CurrentEnemyCountList.Count - 1)
                        {
                            // ����� ��� "��������".
                            m_Mode = SpawnMode.Completed;

                            // ��������� �������.
                            m_CurrentEnemyIndex = 0;

                            // ���������� ��������� � ���������� ������.
                            LevelController.Instance.SpawnCompleted();

                            return;
                        }
                        // ���� ��� ���� ������� ��� ������.
                        else
                        {
                            // ����� �����.
                            SpawnEnemy();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// �����, ��������� � ������������� �����. ���������� ������ �� ������ UpdateSpawner.
        /// </summary>
        private void SpawnEnemy()
        {
            // ������� � �������� ���������� �����.
            Enemy enemy = Instantiate(m_EnemyPrefab);

            // ����� ������� ��������� �������������� � ������� �������� �� �����.
            enemy.SetEnemy�haracteristics(m_CurrentEnemyList[m_CurrentEnemyIndex]);
            m_CurrentEnemyCountList[m_CurrentEnemyIndex]--;

            // ����������� ����� � ��������� ���� � ������� ������.
            enemy.transform.position = m_Area.GetRandomInsideZone();

            // ������ ����� ������� ��������.
            enemy.SetRoute(m_EnemyRoute.Route);
        }

        /// <summary>
        /// �����, ����������� �������� ��������� ������� ������ � �� ���-��.
        /// </summary>
        private void TuneSpawner()
        {
            // �������� �� LevelController.
            if (LevelController.Instance == null) { Debug.Log("LevelController is null!"); return; }
            // �������� �� LevelController.
            if (m_WaveProperties == null) { Debug.Log("WaveProperties is null!"); return; }

            // �� �������� ����� ������ ������ ������.
            m_CurrentEnemyList = m_WaveProperties.GetEnemyInWave(LevelController.Instance.CurrentWave);

            // ���� � ������� ����� �������� �� ����� - ����� ��������.
            if (m_CurrentEnemyList == null || m_CurrentEnemyList.Count == 0)
            {
                m_Mode = SpawnMode.Completed;
                return;
            }

            // �� �������� ����� ������� ���-�� ������, ������� ���������� ���������� � ������� ����� � �������� ������.
            m_CurrentEnemyCountList = m_WaveProperties.GetEnemyAmountInWave(LevelController.Instance.CurrentWave);
            m_RespawnTimeList = m_WaveProperties.GetEnemySpawnSpeed(LevelController.Instance.CurrentWave);

            // ����� ����� ������.
            m_RespawnTime = m_RespawnTimeList[0];
            // �������� ������ �� ������� ������.
            m_Timer = new Timer(m_RespawnTime, true);
        }

        /// <summary>
        /// �������������/����������� �����.
        /// </summary>
        /// <param name="pause">true - �����, false - �������������.</param>
        private void Paused(bool pause)
        {
            // ���� ����� - ����� � �����, ������� �������� �����������.
            if (pause)
            {
                m_SpawnModeUntilPause = m_Mode;
                m_Mode = SpawnMode.Pause;
            }
            else m_Mode = m_SpawnModeUntilPause;
        }

        #endregion


        #region Public API

        /// <summary>
        /// �������� ��������� �����.
        /// </summary>
        public void NextWave()
        {
            // ��� �������� - �����.
            m_Mode = SpawnMode.Spawn;

            // ��������� �������.
            TuneSpawner();
        }

        #endregion

    }
}