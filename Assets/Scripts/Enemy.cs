using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    /// <summary>
    /// Список поведений ИИ.
    /// </summary>
    public enum AIBehaviour
    {
        Null,
        Walk
    }

    /// <summary>
    /// Основной класс врага.
    /// </summary>
    [RequireComponent(typeof(CircleArea), typeof(Rigidbody2D))]
    public class Enemy : Destructible
    {

        #region Properties and Components

        /// <summary>
        /// Выбор поведения объекта через Inspector.
        /// </summary>
        [SerializeField] private AIBehaviour m_AIBehaviour;

        /// <summary>
        /// Скорость передвижения объекта.
        /// </summary>
        [SerializeField] private float m_Speed;

        /// <summary>
        /// Маршрут передвижения ИИ.
        /// </summary>
        [SerializeField] private List<CircleArea> m_Route;

        /// <summary>
        /// Радиус поиска цели.
        /// </summary>
        [SerializeField] private float m_FindTargetRadius;

        /// <summary>
        /// Скорость атаки объекта.
        /// </summary>
        [SerializeField] private float m_AttackDelay;

        /// <summary>
        /// Ссылка на спрайт рендерер объекта.
        /// </summary>
        [SerializeField] private SpriteRenderer m_Sprite;

        /// <summary>
        /// Ссылка на аниматор спрайтов на объекте.
        /// </summary>
        [SerializeField] private AnimateSpriteFrames m_SpriteAnimator;

        /// <summary>
        /// Ссылка на коллайдер объекта.
        /// </summary>
        [SerializeField] private BoxCollider2D m_BoxCollider;

        /// <summary>
        /// Ригидбади объекта.
        /// </summary>
        private Rigidbody2D m_Rigidbody;

        /// <summary>
        /// Вектор целевой позиции движения объекта.
        /// </summary>
        private Vector3 m_MovePosition;

        /// <summary>
        /// Переменная, отображающая текущий индекс позиции передвижения объекта.
        /// </summary>
        private int m_CurrentPositionIndex;

        /// <summary>
        /// Флаг, обозначающий выбрана ли точка для движения.
        /// </summary>
        private bool m_NewMovePositionSelected;

        /// <summary>
        /// Destructible цели для атаки.
        /// </summary>
        private Destructible m_SelectedTarget;

        /// <summary>
        /// Цена юнита в жизнях.
        /// </summary>
        private int m_LivesCost;

        /// <summary>
        /// Статичный эвент уничтожения врага.
        /// </summary>
        public static event EmptyDelegate EnemyDestroy;

        #region Timers

        /// <summary>
        /// Таймер атаки.
        /// </summary>
        private Timer m_AttackTimer;

        #endregion

        #region Links

        


        #endregion

        #endregion


        #region Unity Events

#if UNITY_EDITOR
        /// <summary>
        /// Метод, работающий в инспекторе после изменения значений.
        /// </summary>
        private void OnValidate()
        {
            // Автоматически задаёт радиус CircleArea от текущего радиуса.
            GetComponent<CircleArea>().ChangeRadius(m_FindTargetRadius);
        }

        /// <summary>
        /// Гизмос, рисующий линию передвижения ИИ.
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Handles.color = new Color(255, 0, 0, 1);
            Handles.DrawLine(transform.position, m_MovePosition);
        }
#endif

        protected override void Start()
        {
            // Использовать свойства переопределённого метода.
            base.Start();

            // Инициализация таймеров.
            InitTimers();

            // Взять ссылку на Rigidbody с объекта.
            m_Rigidbody = GetComponent<Rigidbody2D>();

            // Подписаться на событие паузы.
            PauseController.OnPaused += Paused;
        }

        /// <summary>
        /// Действия при уничтожении объекта.
        /// </summary>
        protected override void OnDestroy()
        {
            // Выполнять базовый метод из Destructuble.
            base.OnDestroy();

            // Вызов события уничтожения врага.
            EnemyDestroy?.Invoke();

            // Отписаться от события паузы.
            PauseController.OnPaused -= Paused;
        }

        private void FixedUpdate()
        {
            // Обновить все таймеры.
            UpdateTimers();

            // Обновить поведение ИИ.
            UpdateAI();
        }

        #endregion


        #region Private API

        #region Timers

        /// <summary>
        /// Метод, инициализирующий все таймеры.
        /// </summary>
        private void InitTimers()
        {
            m_AttackTimer = new Timer(m_AttackDelay, false);
        }

        /// <summary>
        /// Метод, обновляющий все таймеры.
        /// </summary>
        private void UpdateTimers()
        {
            m_AttackTimer.UpdateTimer();
        }

        #endregion

        /// <summary>
        /// Метод, обновляющий действия ИИ.
        /// </summary>
        private void UpdateAI()
        {
            // Действия ИИ.
            switch (m_AIBehaviour)
            {
                // действие не выбрано.
                case AIBehaviour.Null:
                    // Найти цель для движенмя.
                    ActionFindNewMovePosition();
                    break;

                // Действие движения.
                case AIBehaviour.Walk:
                    // Найти цель для движенмя.
                    ActionFindNewMovePosition();
                    // Двигаться к цели.
                    ActionMove();
                    break;
            }
        }

        /// <summary>
        /// Метод, ищущий следующую цель для передвижения.
        /// </summary>
        private void ActionFindNewMovePosition()
        {
            // Действия ИИ.
            switch (m_AIBehaviour)
            {
                // Дейвствие Null.
                case AIBehaviour.Null:

                    // Если есть маршрут - меняет действие на движение.
                    if (m_Route != null) m_AIBehaviour = AIBehaviour.Walk;
                    break;

                // Действие Patrul
                case AIBehaviour.Walk:
                    // Если есть маршрут передвижения.
                    if (m_Route != null && m_Route.Count > 0)
                    {
                        // Переменная, отображающая, находится ли объект в зоне следующей точки движения.
                        bool isInsidePatrolZone = (m_Route[m_CurrentPositionIndex].transform.position - transform.position).sqrMagnitude < m_Route[m_CurrentPositionIndex].Radius * m_Route[m_CurrentPositionIndex].Radius;

                        // Если позиция ИИ настигла следующую зону движения.
                        if (isInsidePatrolZone)
                        {
                            // Индекс позиции ++.
                            m_CurrentPositionIndex++;

                            // Если индекс больше, чем кол-во точек передвижения.
                            if (m_CurrentPositionIndex >= m_Route.Count)
                            {
                                // Проверка на игрока.
                                if (Player.Instance == null) Debug.Log("Player.Instance is null!");
                                // Отнимает жизни у игрока, если игрок есть.
                                else Player.Instance.ChangeLives(-m_LivesCost);

                                // Уничтожить объект.
                                Destroy(gameObject);
                                return;
                            }

                            // Задаётся вектор следующей позиции в случайной позиции новой точки передвижения.
                            Vector2 newPoint = m_Route[m_CurrentPositionIndex].GetRandomInsideZone();

                            // Задаётся новая позиция движения к точке.
                            m_MovePosition = newPoint;
                        }
                        // Если объект не в зоне передвижения.
                        else
                        {
                            // Проверка, выбрана ли уже точка для движения.
                            if (m_NewMovePositionSelected) return;

                            // Задаётся вектор следующей позиции в случайной позиции текущей точки передвижения.
                            Vector2 newPoint = m_Route[m_CurrentPositionIndex].GetRandomInsideZone();

                            // Задаётся новая позиция движения к точке.
                            m_MovePosition = newPoint;

                            // Флаг выбора точки для движения.
                            m_NewMovePositionSelected = true;
                        }

                        // Если позиции для движения нет.
                        if (m_MovePosition == null)
                        {
                            // Задаётся вектор следующей позиции в случайной позиции новой точки.
                            Vector2 newPoint = m_Route[m_CurrentPositionIndex].GetRandomInsideZone();

                            // Задаётся новая позиция движения к точке.
                            m_MovePosition = newPoint;
                        }
                    }
                    // Если нет маршрута движения.
                    else
                    {
                        // Вывести сообщение об изменении состояния объекта.
                        Debug.Log("Select Route! Object stay sleep.");

                        // Перевод режима ИИ в null.
                        m_AIBehaviour = AIBehaviour.Null;
                    }
                    break;
            }
        }

        /// <summary>
        /// Метод передвижения врага.
        /// </summary>
        private void ActionMove()
        {
            // Объект интерполируется через физику к указанной точке с указанной скоростью.
            m_Rigidbody.MovePosition(Vector3.MoveTowards(transform.position, m_MovePosition, m_Speed * Time.fixedDeltaTime));

            // Вектор движения объекта.
            Vector2 distance = m_MovePosition - transform.position;

            // Если вектор X направлен вправо - не разворачивать спрайт.
            if (distance.x > 0.01)
            {
                m_Sprite.flipX = false;
            }
            // Если вектор X направлен влево - развернуть спрайт.
            if (distance.x < 0.01)
            {
                m_Sprite.flipX = true;
            }
        }

        /// <summary>
        /// Метод постановки на паузу.
        /// </summary>
        /// <param name="pause">Пауза?</param>
        private void Paused(bool pause)
        {
            // Отключает скрипт в зависимости от состояния паузы.
            enabled = !pause;
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод, позволяющий задать объекту маршрут движения.
        /// </summary>
        /// <param name="route">Массив CircleArea.</param>
        public void SetRoute(List<CircleArea> route)
        {
            m_Route = route;
        }

        /// <summary>
        /// Метод, задающий объекту характеристики при спавне через спавнер.
        /// </summary>
        /// <param name="characteristics">Характеристики объекта SO_Enemy.</param>
        public void SetEnemyСharacteristics(SO_Enemy characteristics)
        {
            // Задаётся скорость передвижения.
            m_Speed = characteristics.MoveSpeed;
            // Задаётся стоимость в жизнях.
            m_LivesCost = characteristics.LivesCost;
            // Установка анимации бега в аниматор.
            m_SpriteAnimator.SetNewAnimationFrames(characteristics.WalkAnimations);
            // Задаётся скейлинг спрайта.
            m_Sprite.size = characteristics.SpriteScale;
            // Установка нового размера коллайдера.
            m_BoxCollider.size = characteristics.ColliderSize;

            // Передача характеристик в Destructible.
            SetDestructibleСharacteristics(characteristics);
        }

        #endregion

    }
}
