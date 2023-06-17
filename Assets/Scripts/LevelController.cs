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
        /// ���-�� ���� �� ������� ������.
        /// </summary>
        [Min(1)]
        [SerializeField] private int m_NumberWaves;

        /// <summary>
        /// ������� �������.
        /// </summary>
        private int m_CurrentLevel;

        /// <summary>
        /// ������� �����.
        /// </summary>
        private int m_CurrentWave;

        /// <summary>
        /// ������� ���������� �����.
        /// </summary>
        private float m_LevelTime;

        /// <summary>
        /// ���-�� ���� �� ������.
        /// </summary>
        private int m_LevelStars = 0;

        /// <summary>
        /// ������� ��������� ������ �����.
        /// </summary>
        public static event IntToVoidDelegate OnWaveUpdate;

        /// <summary>
        /// ������� ���������� ������.
        /// </summary>
        public static event BoolDelegate LevelCompleted;

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

        /// <summary>
        /// ���-�� ���� �� ������.
        /// </summary>
        public int LevelStars => m_LevelStars;

        #endregion

        #endregion


        #region Unity Events

        private void Start()
        {
            // �������� ������� �������.
            if (LevelSequenceController.Instance != null)
            {
                if (LevelSequenceController.Instance.CurrentLevel != null) m_CurrentLevel = LevelSequenceController.Instance.CurrentLevel.LevelNumber;
                else Debug.Log("Level in LevelSequenseController is null!");
            }
            else Debug.Log("LevelSequenseController is null!");

            // ������� ����� - 0.
            m_CurrentWave = 0;

            // ����������� �� ������� ���������� ������ �����.
            EnemySpawner.WaveSpawnCompleted += SpawnCompleted;
        }

        private void OnDestroy()
        {
            // ���������� �� ������� ���������� ������ �����.
            EnemySpawner.WaveSpawnCompleted -= SpawnCompleted;
        }

        #endregion


        #region Private API

        /// <summary>
        /// ����� ���������� ������ �����.
        /// </summary>
        private void SpawnCompleted()
        {
            // ��������� ��������� ����� �������.
            if (UI_Interface_NextWave.Instance == null) { Debug.Log("UI_Interface_NextWave.Instance == null!"); }
            else UI_Interface_NextWave.Instance.gameObject.SetActive(true);
        }

        /// <summary>
        /// ������� ���-�� ����, ��������� �� ������.
        /// </summary>
        private void CountLevelStars()
        {
            // �������� ������������ ���-�� ��.
            int hp = Player.Instance.CurrentLives;

            // ������� ����� � ����������� �� ��.
            if (hp < 8) m_LevelStars = 1;
            if (hp >= 8 || hp < 18) m_LevelStars = 2;
            if (hp >= 18) m_LevelStars = 3;
        }

        #endregion


        #region Public API

        /// <summary>
        /// ������ ��������� ����� ������.
        /// </summary>
        public void StartNextWave()
        {
            // ������� �����++.
            m_CurrentWave++;
            // ���� ������� ����� > ���-�� ����.
            if (m_CurrentWave > m_NumberWaves)
            {
                // ������� ����� - ��������� �����.
                m_CurrentWave = m_NumberWaves;
                return;
            }
            // ����� ������� ��������� ������ �����.
            OnWaveUpdate?.Invoke(CurrentWave);
        }

        /// <summary>
        /// ��������� �������.
        /// </summary>
        /// <param name="isCompleted">true ���� ������� �������.</param>
        public void CompleteLevel(bool isCompleted)
        {
            // ���� ������� ������� - ��������� ���� ������.
            if (isCompleted) CountLevelStars();

            // ������� ������� ���������� ������.
            LevelCompleted?.Invoke(isCompleted);

            // �������� ���������� ����������� � ����� �������.
            if (MapCompletion.Instance != null) MapCompletion.SaveLevelResult(isCompleted, m_LevelStars);
            else Debug.Log("MapCompletion is null!");
        }

        #endregion

    }
}