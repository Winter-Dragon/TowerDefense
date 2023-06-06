using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// �����, �����������, ��� �� ������� ��� ���������� ������ ���������.
    /// </summary>
    public class LevelController : Singleton<LevelController>
    {

        #region Properties and Components

        /// <summary>
        /// ������� �������.
        /// </summary>
        [Min(1)]
        [SerializeField] private int m_CurrentLevel;

        /// <summary>
        /// ���-�� ���� �� ������� ������.
        /// </summary>
        [Min(1)]
        [SerializeField] private int m_NumberWaves;

        /// <summary>
        /// ������� �����.
        /// </summary>
        private int m_CurrentWave;

        /// <summary>
        /// ������� ���������� �����.
        /// </summary>
        private float m_LevelTime;

        /// <summary>
        /// ������� ��������� ������ �����.
        /// </summary>
        public static event IntToVoidDelegate OnWaveUpdate;

        #region Links

        /// <summary>
        /// ����� ���-�� ���� �� ������.
        /// </summary>
        public int NumberWaves => m_NumberWaves;

        /// <summary>
        /// ������� �����.
        /// </summary>
        public int CurrentWave => m_CurrentWave;

        /// <summary>
        /// ������� �������.
        /// </summary>
        public int CurrentLevel => m_CurrentLevel;

        /// <summary>
        /// ������� ���������� ����� ������.
        /// </summary>
        public float LevelTime => m_LevelTime;

        #endregion

        #endregion


        #region Unity Events

        private void Start()
        {
            // ������� ����� - 0.
            m_CurrentWave = 0;
        }

        #endregion


        #region Private API

        /// <summary>
        /// ����������� ������� ���������� ������.
        /// </summary>
        /// <param name="isCompleted">true ���� ������� �������.</param>
        private void CheckLevelResult(bool isCompleted)
        {
            // �������� �� ������ �����������.
            if (UI_LevelResultPanel.Instance == null) { Debug.Log("(UI_LevelResultPanel.Instance == null!"); return; }

            // �������� ���������� � ������ �����������.
            UI_LevelResultPanel.Instance.LevelCompleted(isCompleted);
        }

        #endregion


        #region Public API

        /// <summary>
        /// �����, �����������, ��� �� �������� ��������� �����. ���� ��� ��������� - ��������� ��������� ��������� �����.
        /// </summary>
        public void SpawnCompleted()
        {
            // �������� �� ������� �������� � ������ ���������.
            if (EnemySpawner.AllSpawners == null || EnemySpawner.AllSpawners.Count == 0) { Debug.Log("EnemySpawner.AllSpawners is null!"); return; }

            // ����������� ������ �� ���� ���������.
            foreach (EnemySpawner spawner in EnemySpawner.AllSpawners)
            {
                // ���� ����� �� �������� - ����� �� ������.
                if (spawner.Mode != SpawnMode.Completed) return;
            }

            // ��������� ��������� ����� �������.
            if (UI_Interface_NextWave.Instance == null) { Debug.Log("UI_Interface_NextWave.Instance == null!"); }
            else UI_Interface_NextWave.Instance.gameObject.SetActive(true);
        }

        public void StartNextWave()
        {
            // ������� �����++.
            m_CurrentWave++;
            // ���� ������� ����� > ���-�� ����.
            if (m_CurrentWave > m_NumberWaves)
            {
                // ������� ����� - ��������� �����.
                m_CurrentWave = m_NumberWaves;

                // ������� ���������� ������.
                CheckLevelResult(true);
                return;
            }
            // ����� ������� ��������� ������ �����.
            OnWaveUpdate?.Invoke(CurrentWave);

            // �������� �� ������� �������� � ������ ���������.
            if (EnemySpawner.AllSpawners == null || EnemySpawner.AllSpawners.Count == 0) { Debug.Log("EnemySpawner.AllSpawners is null!"); return; }

            // ����������� ������ �� ���� ���������.
            foreach (EnemySpawner spawner in EnemySpawner.AllSpawners)
            {
                // ������ ��������� ����� ������.
                spawner.NextWave();
            }
        }

        #endregion

    }
}