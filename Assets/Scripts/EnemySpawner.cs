using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Текущий режим спавна.
    /// </summary>
    public enum SpawnMode
    {
        Spawn,
        Pause,
        Completed
    }

    /// <summary>
    /// Класс, спавнящий игровые сущности.
    /// </summary>
    [RequireComponent(typeof(CircleArea))]
    public class EnemySpawner : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на настройки волны.
        /// </summary>
        [SerializeField] private SO_WaveProperties m_WaveProperties;

        /// <summary>
        /// Классический префаб игрового объекта.
        /// </summary>
        [SerializeField] private Enemy m_EnemyPrefab;

        /// <summary>
        /// Зона спавна сущностей.
        /// </summary>
        private CircleArea m_Area;

        /// <summary>
        /// Кол-во объектов, которые спавнятся за раз.
        /// </summary>
        [SerializeField] private int m_NumberSpawns;

        /// <summary>
        /// Как часто обновляется таймер спавна.
        /// </summary>
        private float m_RespawnTime;

        /// <summary>
        /// Внутренний таймер.
        /// </summary>
        private Timer m_Timer;

        /// <summary>
        /// Ссылка на маршрут движения.
        /// </summary>
        [SerializeField] private EnemyRoute m_EnemyRoute;

        /// <summary>
        /// Список врагов для спавна в текущей волне.
        /// </summary>
        private List<SO_Enemy> m_CurrentEnemyList;

        /// <summary>
        /// Список с кол-вом врагов для спавна.
        /// </summary>
        private List<int> m_CurrentEnemyCountList;

        /// <summary>
        /// Список со скоростью спавна врагов в волне.
        /// </summary>
        private List<float> m_RespawnTimeList;

        /// <summary>
        /// Текущий режим спавнера.
        /// </summary>
        private SpawnMode m_Mode;

        /// <summary>
        /// Текущий индекс объекта спавна.
        /// </summary>
        private int m_CurrentEnemyIndex;

        /// <summary>
        /// Статичный список всех спавнеров на сцене.
        /// </summary>
        private static HashSet<EnemySpawner> m_AllSpawners;

        /// <summary>
        /// Сохранённый режим спавна до паузы.
        /// </summary>
        private SpawnMode m_SpawnModeUntilPause;

        #region Links

        /// <summary>
        /// Текущий режим спавнера.
        /// </summary>
        public SpawnMode Mode => m_Mode;

        /// <summary>
        /// Ссылка только для прочтения для списка всех спавнеров на сцене.
        /// </summary>
        public static IReadOnlyCollection<EnemySpawner> AllSpawners => m_AllSpawners;

        #endregion

        #endregion


        #region UnityEvents

        private void Start()
        {
            // Если списка нет - создать новый список.
            if (m_AllSpawners == null) m_AllSpawners = new HashSet<EnemySpawner>();
            // Добавляет текущий объект в список.
            m_AllSpawners.Add(this);

            // На текущем объекте находится зона спавна.
            m_Area = GetComponent<CircleArea>();

            // Обнуляет таймер по таймеру спавна.
            m_Timer = new Timer(m_RespawnTime, true);

            // Спавнмод в паузу.
            m_Mode = SpawnMode.Pause;

            // Настройка спавнера.
            TuneSpawner();

            // Подписаться на событие паузы.
            PauseController.OnPaused += Paused;
        }

        private void OnDestroy()
        {
            // Отписаться от события паузы.
            PauseController.OnPaused -= Paused;

            // Убрать текущий спавнер из списка.
            m_AllSpawners.Remove(this);
        }

        private void FixedUpdate()
        {
            // Обновляет таймер.
            m_Timer.UpdateTimer();

            // Если время спавна ещё не настало, обновить внутренний таймер и выйти из метода.
            if (m_Timer.IsFinished)
            {
                UpdateSpawner();
            }
        }

        #endregion


        #region Private API

        /// <summary>
        /// Метод, спавнящий случайную сущность в случайной позиции.
        /// </summary>
        private void UpdateSpawner()
        {
            // Проверка на мод спавнера "Спавн".
            if (m_Mode != SpawnMode.Spawn) return;
            // Проверка на LevelController.
            if (LevelController.Instance == null) { Debug.Log("LevelController is null!"); return; }
            // Проверка на наличие Префаба объекта спавна.
            if (m_EnemyPrefab == null) { Debug.Log("m_EnemyPrefab is null!"); return; }

            // Если таймер спавна не совпадает со скоростью спавна врага.
            if (m_RespawnTimeList[m_CurrentEnemyIndex] != m_RespawnTime)
            {
                // Задаётся таймер спавна.
                m_RespawnTime = m_RespawnTimeList[m_CurrentEnemyIndex];
                // Обнуляет таймер по таймеру спавна.
                m_Timer = new Timer(m_RespawnTime, true);

                // Не спавнит в текущей итерации.
                return;
            }

            for(int i = 0; i < m_NumberSpawns; i++)
            {
                // Действия, когда объект спавна один.
                if (m_CurrentEnemyList.Count == 1)
                {
                    // Если спавнер ещё не создал всех врагов из волны.
                    if (m_CurrentEnemyCountList[m_CurrentEnemyIndex] > 0)
                    {
                        // Спавн врага.
                        SpawnEnemy();
                    }
                    // Если спавнер создал всех врагов.
                    else
                    {
                        // Спавн мод "Завершён".
                        m_Mode = SpawnMode.Completed;

                        // Обнуление индекса.
                        m_CurrentEnemyIndex = 0;

                        // Оповестить контроллёр о завершении спавна.
                        LevelController.Instance.SpawnCompleted();

                        return;
                    }
                }
                // Действия, если объектов для спавна несколько.
                else
                {
                    // Если спавнер ещё не создал всех врагов по указанному индексу из волны.
                    if (m_CurrentEnemyCountList[m_CurrentEnemyIndex] > 0)
                    {
                        // Спавн врага.
                        SpawnEnemy();
                    }
                    // Если спавнер создал всех врагов.
                    else
                    {
                        // Индекс врага из волны++.
                        m_CurrentEnemyIndex++;

                        // Если индекс больше, чем кол-во разных врагов в волне.
                        if (m_CurrentEnemyIndex > m_CurrentEnemyCountList.Count - 1)
                        {
                            // Спавн мод "Завершён".
                            m_Mode = SpawnMode.Completed;

                            // Обнуление индекса.
                            m_CurrentEnemyIndex = 0;

                            // Оповестить контроллёр о завершении спавна.
                            LevelController.Instance.SpawnCompleted();

                            return;
                        }
                        // Если ещё есть объекты для спавна.
                        else
                        {
                            // Спавн врага.
                            SpawnEnemy();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Метод, создающий и настраивающий врага. Вызывается только из метода UpdateSpawner.
        /// </summary>
        private void SpawnEnemy()
        {
            // Создать и записать созданного врага.
            Enemy enemy = Instantiate(m_EnemyPrefab);

            // Задаёт объекту указанные характеристики и убирает значение из листа.
            enemy.SetEnemyСharacteristics(m_CurrentEnemyList[m_CurrentEnemyIndex]);
            m_CurrentEnemyCountList[m_CurrentEnemyIndex]--;

            // Переместить врага в случайную зону в области спавна.
            enemy.transform.position = m_Area.GetRandomInsideZone();

            // Задать Врагу маршрут движения.
            enemy.SetRoute(m_EnemyRoute.Route);
        }

        /// <summary>
        /// Метод, сохраняющий локально настройки объекта спавна и их кол-во.
        /// </summary>
        private void TuneSpawner()
        {
            // Проверка на LevelController.
            if (LevelController.Instance == null) { Debug.Log("LevelController is null!"); return; }
            // Проверка на LevelController.
            if (m_WaveProperties == null) { Debug.Log("WaveProperties is null!"); return; }

            // Из настроек волны берётся список врагов.
            m_CurrentEnemyList = m_WaveProperties.GetEnemyInWave(LevelController.Instance.CurrentWave);

            // Если в текущей волне спавнить не нужно - спавн завершён.
            if (m_CurrentEnemyList == null || m_CurrentEnemyList.Count == 0)
            {
                m_Mode = SpawnMode.Completed;
                return;
            }

            // Из настроек волны берутся кол-во врагов, которых необходимо заспавнить в текущей волне и скорость спавна.
            m_CurrentEnemyCountList = m_WaveProperties.GetEnemyAmountInWave(LevelController.Instance.CurrentWave);
            m_RespawnTimeList = m_WaveProperties.GetEnemySpawnSpeed(LevelController.Instance.CurrentWave);

            // Задаёт время спавна.
            m_RespawnTime = m_RespawnTimeList[0];
            // Обнуляет таймер по таймеру спавна.
            m_Timer = new Timer(m_RespawnTime, true);
        }

        /// <summary>
        /// Приостановить/возобновить спавн.
        /// </summary>
        /// <param name="pause">true - пауза, false - возобновление.</param>
        private void Paused(bool pause)
        {
            // Если пауза - спавн в паузу, прошлый спавнмод сохраняется.
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
        /// Спавнить следующую волну.
        /// </summary>
        public void NextWave()
        {
            // Мод спавнера - спавн.
            m_Mode = SpawnMode.Spawn;

            // Настроить спавнер.
            TuneSpawner();
        }

        #endregion

    }
}