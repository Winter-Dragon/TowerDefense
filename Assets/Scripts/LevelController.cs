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
        /// Текущий уровень.
        /// </summary>
        [Min(1)]
        [SerializeField] private int m_CurrentLevel;

        /// <summary>
        /// Кол-во волн на текущем уровне.
        /// </summary>
        [Min(1)]
        [SerializeField] private int m_NumberWaves;

        /// <summary>
        /// Текущая волна.
        /// </summary>
        private int m_CurrentWave;

        /// <summary>
        /// Текущее пройденное время.
        /// </summary>
        private float m_LevelTime;

        /// <summary>
        /// Событие изменения номера волны.
        /// </summary>
        public static event IntToVoidDelegate OnWaveUpdate;

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

        #endregion

        #endregion


        #region Unity Events

        private void Start()
        {
            // Текущая волна - 0.
            m_CurrentWave = 0;
        }

        #endregion


        #region Private API

        /// <summary>
        /// Попробовать вывести результаты уровня.
        /// </summary>
        /// <param name="isCompleted">true если уровень пройден.</param>
        private void CheckLevelResult(bool isCompleted)
        {
            // Проверка на панель результатов.
            if (UI_LevelResultPanel.Instance == null) { Debug.Log("(UI_LevelResultPanel.Instance == null!"); return; }

            // Передача информации в панель результатов.
            UI_LevelResultPanel.Instance.LevelCompleted(isCompleted);
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод, проверяющий, все ли спавнеры завершили спавн. Если все завершили - открывает интерфейс следующей волны.
        /// </summary>
        public void SpawnCompleted()
        {
            // Проверка на наличие объектов в списке спавнеров.
            if (EnemySpawner.AllSpawners == null || EnemySpawner.AllSpawners.Count == 0) { Debug.Log("EnemySpawner.AllSpawners is null!"); return; }

            // Прохождение циклом по всем спавнерам.
            foreach (EnemySpawner spawner in EnemySpawner.AllSpawners)
            {
                // Если спавн не завершён - выйти из метода.
                if (spawner.Mode != SpawnMode.Completed) return;
            }

            // Интерфейс следующей волны включён.
            if (UI_Interface_NextWave.Instance == null) { Debug.Log("UI_Interface_NextWave.Instance == null!"); }
            else UI_Interface_NextWave.Instance.gameObject.SetActive(true);
        }

        public void StartNextWave()
        {
            // Текущая волна++.
            m_CurrentWave++;
            // Если текущая волна > кол-во волн.
            if (m_CurrentWave > m_NumberWaves)
            {
                // Текущая волна - последняя волна.
                m_CurrentWave = m_NumberWaves;

                // Вывести рещультаты уровня.
                CheckLevelResult(true);
                return;
            }
            // Вызов события изменения номера волны.
            OnWaveUpdate?.Invoke(CurrentWave);

            // Проверка на наличие объектов в списке спавнеров.
            if (EnemySpawner.AllSpawners == null || EnemySpawner.AllSpawners.Count == 0) { Debug.Log("EnemySpawner.AllSpawners is null!"); return; }

            // Прохождение циклом по всем спавнерам.
            foreach (EnemySpawner spawner in EnemySpawner.AllSpawners)
            {
                // Запуск следующей волны спавна.
                spawner.NextWave();
            }
        }

        #endregion

    }
}