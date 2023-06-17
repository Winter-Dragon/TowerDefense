using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Класс, проверяющий, все ли условия для завершения уровня выполнены.
    /// </summary>
    public class LevelController : Singleton<LevelController>
    {

        #region Properties and Components

        /// <summary>
        /// Кол-во волн на текущем уровне.
        /// </summary>
        [Min(1)]
        [SerializeField] private int m_NumberWaves;

        /// <summary>
        /// Текущий уровень.
        /// </summary>
        private int m_CurrentLevel;

        /// <summary>
        /// Текущая волна.
        /// </summary>
        private int m_CurrentWave;

        /// <summary>
        /// Текущее пройденное время.
        /// </summary>
        private float m_LevelTime;

        /// <summary>
        /// Кол-во звёзд на уровне.
        /// </summary>
        private int m_LevelStars = 0;

        /// <summary>
        /// Событие изменения номера волны.
        /// </summary>
        public static event IntToVoidDelegate OnWaveUpdate;

        /// <summary>
        /// Событие завершения уровня.
        /// </summary>
        public static event BoolDelegate LevelCompleted;

        #region Links

        /// <summary>
        /// Общее кол-во волн на уровне.
        /// </summary>
        public int NumberWaves => m_NumberWaves;

        /// <summary>
        /// Текущая волна.
        /// </summary>
        public int CurrentWave => m_CurrentWave;

        /// <summary>
        /// Текущий уровень.
        /// </summary>
        public int CurrentLevel => m_CurrentLevel;

        /// <summary>
        /// Текущее пройденное время уровня.
        /// </summary>
        public float LevelTime => m_LevelTime;

        /// <summary>
        /// Кол-во звёзд на уровне.
        /// </summary>
        public int LevelStars => m_LevelStars;

        #endregion

        #endregion


        #region Unity Events

        private void Start()
        {
            // Записать текущий уровень.
            if (LevelSequenceController.Instance != null)
            {
                if (LevelSequenceController.Instance.CurrentLevel != null) m_CurrentLevel = LevelSequenceController.Instance.CurrentLevel.LevelNumber;
                else Debug.Log("Level in LevelSequenseController is null!");
            }
            else Debug.Log("LevelSequenseController is null!");

            // Текущая волна - 0.
            m_CurrentWave = 0;

            // Подписаться на событие завершения спавна волны.
            EnemySpawner.WaveSpawnCompleted += SpawnCompleted;
        }

        private void OnDestroy()
        {
            // Отписаться от события завершения спавна волны.
            EnemySpawner.WaveSpawnCompleted -= SpawnCompleted;
        }

        #endregion


        #region Private API

        /// <summary>
        /// Метод завершения спавна волны.
        /// </summary>
        private void SpawnCompleted()
        {
            // Интерфейс следующей волны включён.
            if (UI_Interface_NextWave.Instance == null) { Debug.Log("UI_Interface_NextWave.Instance == null!"); }
            else UI_Interface_NextWave.Instance.gameObject.SetActive(true);
        }

        /// <summary>
        /// Считает кол-во звёзд, набранное на уровне.
        /// </summary>
        private void CountLevelStars()
        {
            // локально записывается кол-во ХП.
            int hp = Player.Instance.CurrentLives;

            // Считает звёзды в зависимости от ХП.
            if (hp < 8) m_LevelStars = 1;
            if (hp >= 8 || hp < 18) m_LevelStars = 2;
            if (hp >= 18) m_LevelStars = 3;
        }

        #endregion


        #region Public API

        /// <summary>
        /// Запуск следующей волны врагов.
        /// </summary>
        public void StartNextWave()
        {
            // Текущая волна++.
            m_CurrentWave++;
            // Если текущая волна > кол-во волн.
            if (m_CurrentWave > m_NumberWaves)
            {
                // Текущая волна - последняя волна.
                m_CurrentWave = m_NumberWaves;
                return;
            }
            // Вызов события изменения номера волны.
            OnWaveUpdate?.Invoke(CurrentWave);
        }

        /// <summary>
        /// Завершить уровень.
        /// </summary>
        /// <param name="isCompleted">true если уровень пройден.</param>
        public void CompleteLevel(bool isCompleted)
        {
            // Если уровень пройден - посчитать очки уровня.
            if (isCompleted) CountLevelStars();

            // Вызвать событие завершения уровня.
            LevelCompleted?.Invoke(isCompleted);

            // Записать результаты прохождения в карту уровней.
            if (MapCompletion.Instance != null) MapCompletion.SaveLevelResult(isCompleted, m_LevelStars);
            else Debug.Log("MapCompletion is null!");
        }

        #endregion

    }
}