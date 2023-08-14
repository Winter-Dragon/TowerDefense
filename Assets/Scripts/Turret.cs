using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Основной класс туррели, позволяющий ей стрелять.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class Turret : MonoBehaviour
    {

        #region Properties and Compinents

        /// <summary>
        /// Ссылка на характеристики туррели.
        /// </summary>
        [SerializeField] private SO_TurretProperties m_TurretProperties;

        /// <summary>
        /// Ссылка на текущую башню.
        /// </summary>
        private Tower m_CurrentTower;

        /// <summary>
        /// Ссылка на Audio Source у туррели.
        /// </summary>
        private AudioSource m_AudioSource;

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
            // Если у рут объекта есть скрипт башни - назначается башня.
            if (gameObject.transform.root.TryGetComponent<Tower>(out Tower tower)) m_CurrentTower = tower;
            else { Debug.Log("Script \"Tower\" absent! Turret disabled."); enabled = false; }

            
            // Задать переменную аудиосурса и записать нужный клип из Turret Properties.
            m_AudioSource = GetComponent<AudioSource>();
            if (m_AudioSource && m_TurretProperties.LaunchSFX) m_AudioSource.clip = m_TurretProperties.LaunchSFX;

            // Инициализация таймеров.
            InitTimers();

            // Подписываемся на событие паузы.
            PauseController.OnPaused += Paused;
        }

        private void OnDestroy()
        {
            // Отписаться от события паузы.
            PauseController.OnPaused -= Paused;
        }

        private void FixedUpdate()
        {
            // Обновление времени в таймерах.
            UpdateTimers();

            // Если массив с врагами > 0 - стрелять.
            if (m_CurrentTower.EnemyList.Count > 0) Fire();
        }

        #endregion


        #region Private API

        #region Timers

        /// <summary>
        /// Метод, инициализирующий таймеры.
        /// </summary>
        private void InitTimers()
        {
            
            if (m_TurretProperties != null)
            {
                // Если есть улучшение на ускорение скорости атаки артиллерии - активирует его.
                if (m_TurretProperties.Type == TowerType.Artillery && Upgrades.CheckActiveUpgrade(UpgradeList.ArtilleryAttackSpeed))
                {
                    float newRateOfFire = m_TurretProperties.RateOfFire * 1.15f;
                    m_RefiteTimer = new Timer(newRateOfFire, false);
                }
                // Если улучшения нет - скорость атаки стандартная.
                else
                {
                    m_RefiteTimer = new Timer(m_TurretProperties.RateOfFire, false);
                }
            }
            // Если источник звука есть - создаётся таймер.
            if (m_AudioSource && m_AudioSource.clip) m_AudioTimer = new Timer(m_AudioSource.clip.length, false);
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
            projectile.TuneProjectile(m_CurrentTower.EnemyList[0], GetComponent<Turret>());

            // Проиграть звук выстрела.
            if (m_AudioTimer != null && !m_AudioTimer.IsFinished)
            {
                m_AudioSource.Play();
                m_AudioTimer.RestartTimer();
            }

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