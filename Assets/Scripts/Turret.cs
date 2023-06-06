using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Основной класс туррели, позволяющий ей стрелять.
    /// </summary>
    [RequireComponent(typeof(AudioSource), typeof(CircleCollider2D))]
    public class Turret : MonoBehaviour
    {

        #region Properties and Compinents

        /// <summary>
        /// Ссылка на характеристики туррели.
        /// </summary>
        [SerializeField] private SO_TurretProperties m_TurretProperties;

        /// <summary>
        /// Ссылка на Audio Source у туррели.
        /// </summary>
        private AudioSource m_AudioSource;

        /// <summary>
        /// Список врагов, вошедших в радиус башни.
        /// </summary>
        private List<Enemy> m_Enemy = new List<Enemy>();

        /// <summary>
        /// Коллайдер башни.
        /// </summary>
        private CircleCollider2D m_Collider;

        #region Timers

        /// <summary>
        /// Таймер до следующего выстрела.
        /// </summary>
        private Timer m_RefiteTimer;

        /// <summary>
        /// Аудио таймер.
        /// </summary>
        private Timer m_AudioTimer;

        #endregion

        #region Links

        /// <summary>
        /// Ссылка на текущие характеристики туррели.
        /// </summary>
        public SO_TurretProperties CurrentTurretProperties => m_TurretProperties;

        #endregion

        #endregion


        #region Unity Events

        private void Start()
        {
            // Назначить коллайдер текущего объекта.
            m_Collider = GetComponent<CircleCollider2D>();
            m_Collider.radius = m_TurretProperties.Radius;

            // Задать переменную аудиосурса и записать нужный клип из Turret Properties.
            m_AudioSource = GetComponent<AudioSource>();
            m_AudioSource.clip = m_TurretProperties.LaunchSFX;

            // Инициализация таймеров.
            InitTimers();

            // Подписываемся на событие паузы.
            PauseController.OnPaused += Paused;
        }

        private void FixedUpdate()
        {
            // Обновление времени в таймерах.
            UpdateTimers();

            // Если массив с врагами > 0 - стрелять.
            if (m_Enemy.Count > 0) Fire();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Если враг вошёл в коллайдер.
            if (collision.transform.root.TryGetComponent<Enemy>(out Enemy enemy))
            {
                // Добавить в список атаки.
                m_Enemy.Add(enemy);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // Если враг вышел из коллайдера.
            if (collision.transform.root.TryGetComponent<Enemy>(out Enemy enemy))
            {
                // Удалить из списка атаки.
                m_Enemy.Remove(enemy);
            }
        }

        #endregion


        #region Private API

        #region Timers

        /// <summary>
        /// Метод, инициализирующий таймеры.
        /// </summary>
        private void InitTimers()
        {
            if (m_TurretProperties != null) m_RefiteTimer = new Timer(m_TurretProperties.RateOfFire, false);
            if (m_AudioSource != null) m_AudioTimer = new Timer(m_AudioSource.clip.length, false);
        }

        /// <summary>
        /// Метод, обновляющий таймеры.
        /// </summary>
        private void UpdateTimers()
        {
            if (m_RefiteTimer != null) m_RefiteTimer.UpdateTimer();
            if (m_AudioTimer != null) m_AudioTimer.UpdateTimer();
        }

        #endregion

        /// <summary>
        /// Метод, позволяющий туррели стрелять.
        /// </summary>
        private void Fire()
        {
            // Проверка на таймер стрельбы и завершён ли он.
            if (m_RefiteTimer == null || !m_RefiteTimer.IsFinished) return;

            // Создание Projectile, добавление позиции и направления.
            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            // Передать снаряду текущие характеристики туррели и цель для атаки.
            projectile.TuneProjectile(m_Enemy[0], GetComponent<Turret>());

            // Проиграть звук выстрела.
            //if (m_AudioTimer != null || !m_AudioTimer.IsFinished)
            //{
            //  m_AudioSource.Play();
            //m_AudioTimer.RestartTimer();
            //}

            // Повторный запуск таймера.
            m_RefiteTimer.RestartTimer();
        }

        /// <summary>
        /// Приостановить работу башен.
        /// </summary>
        /// <param name="paused">true - пауза, false - возобновление.</param>
        private void Paused(bool paused)
        {
            // Останавливает/возобновляет работу скрипта.
            enabled = !paused;
        }

        #endregion

    }
}