using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// �������� ����� �������, ����������� �� ��������.
    /// </summary>
    [RequireComponent(typeof(AudioSource), typeof(CircleCollider2D))]
    public class Turret : MonoBehaviour
    {

        #region Properties and Compinents

        /// <summary>
        /// ������ �� �������������� �������.
        /// </summary>
        [SerializeField] private SO_TurretProperties m_TurretProperties;

        /// <summary>
        /// ������ �� Audio Source � �������.
        /// </summary>
        private AudioSource m_AudioSource;

        /// <summary>
        /// ������ ������, �������� � ������ �����.
        /// </summary>
        private List<Enemy> m_Enemy = new List<Enemy>();

        /// <summary>
        /// ��������� �����.
        /// </summary>
        private CircleCollider2D m_Collider;

        #region Timers

        /// <summary>
        /// ������ �� ���������� ��������.
        /// </summary>
        private Timer m_RefiteTimer;

        /// <summary>
        /// ����� ������.
        /// </summary>
        private Timer m_AudioTimer;

        #endregion

        #region Links

        /// <summary>
        /// ������ �� ������� �������������� �������.
        /// </summary>
        public SO_TurretProperties CurrentTurretProperties => m_TurretProperties;

        #endregion

        #endregion


        #region Unity Events

        private void Start()
        {
            // ��������� ��������� �������� �������.
            m_Collider = GetComponent<CircleCollider2D>();
            m_Collider.radius = m_TurretProperties.Radius;

            // ������ ���������� ���������� � �������� ������ ���� �� Turret Properties.
            m_AudioSource = GetComponent<AudioSource>();
            m_AudioSource.clip = m_TurretProperties.LaunchSFX;

            // ������������� ��������.
            InitTimers();

            // ������������� �� ������� �����.
            PauseController.OnPaused += Paused;
        }

        private void FixedUpdate()
        {
            // ���������� ������� � ��������.
            UpdateTimers();

            // ���� ������ � ������� > 0 - ��������.
            if (m_Enemy.Count > 0) Fire();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // ���� ���� ����� � ���������.
            if (collision.transform.root.TryGetComponent<Enemy>(out Enemy enemy))
            {
                // �������� � ������ �����.
                m_Enemy.Add(enemy);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // ���� ���� ����� �� ����������.
            if (collision.transform.root.TryGetComponent<Enemy>(out Enemy enemy))
            {
                // ������� �� ������ �����.
                m_Enemy.Remove(enemy);
            }
        }

        #endregion


        #region Private API

        #region Timers

        /// <summary>
        /// �����, ���������������� �������.
        /// </summary>
        private void InitTimers()
        {
            if (m_TurretProperties != null) m_RefiteTimer = new Timer(m_TurretProperties.RateOfFire, false);
            if (m_AudioSource != null) m_AudioTimer = new Timer(m_AudioSource.clip.length, false);
        }

        /// <summary>
        /// �����, ����������� �������.
        /// </summary>
        private void UpdateTimers()
        {
            if (m_RefiteTimer != null) m_RefiteTimer.UpdateTimer();
            if (m_AudioTimer != null) m_AudioTimer.UpdateTimer();
        }

        #endregion

        /// <summary>
        /// �����, ����������� ������� ��������.
        /// </summary>
        private void Fire()
        {
            // �������� �� ������ �������� � �������� �� ��.
            if (m_RefiteTimer == null || !m_RefiteTimer.IsFinished) return;

            // �������� Projectile, ���������� ������� � �����������.
            Projectile projectile = Instantiate(m_TurretProperties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            // �������� ������� ������� �������������� ������� � ���� ��� �����.
            projectile.TuneProjectile(m_Enemy[0], GetComponent<Turret>());

            // ��������� ���� ��������.
            //if (m_AudioTimer != null || !m_AudioTimer.IsFinished)
            //{
            //  m_AudioSource.Play();
            //m_AudioTimer.RestartTimer();
            //}

            // ��������� ������ �������.
            m_RefiteTimer.RestartTimer();
        }

        /// <summary>
        /// ������������� ������ �����.
        /// </summary>
        /// <param name="paused">true - �����, false - �������������.</param>
        private void Paused(bool paused)
        {
            // �������������/������������ ������ �������.
            enabled = !paused;
        }

        #endregion

    }
}