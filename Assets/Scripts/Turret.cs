using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// �������� ����� �������, ����������� �� ��������.
    /// </summary>
    [RequireComponent(typeof(AudioSource))]
    public class Turret : MonoBehaviour
    {

        #region Properties and Compinents

        /// <summary>
        /// ������ �� �������������� �������.
        /// </summary>
        [SerializeField] private SO_TurretProperties m_TurretProperties;

        /// <summary>
        /// ������ �� ������� �����.
        /// </summary>
        private Tower m_CurrentTower;

        /// <summary>
        /// ������ �� Audio Source � �������.
        /// </summary>
        private AudioSource m_AudioSource;

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
            // ���� � ��� ������� ���� ������ ����� - ����������� �����.
            if (gameObject.transform.root.TryGetComponent<Tower>(out Tower tower)) m_CurrentTower = tower;
            else { Debug.Log("Script \"Tower\" absent! Turret disabled."); enabled = false; }

            
            // ������ ���������� ���������� � �������� ������ ���� �� Turret Properties.
            m_AudioSource = GetComponent<AudioSource>();
            if (m_AudioSource && m_TurretProperties.LaunchSFX) m_AudioSource.clip = m_TurretProperties.LaunchSFX;

            // ������������� ��������.
            InitTimers();

            // ������������� �� ������� �����.
            PauseController.OnPaused += Paused;
        }

        private void OnDestroy()
        {
            // ���������� �� ������� �����.
            PauseController.OnPaused -= Paused;
        }

        private void FixedUpdate()
        {
            // ���������� ������� � ��������.
            UpdateTimers();

            // ���� ������ � ������� > 0 - ��������.
            if (m_CurrentTower.EnemyList.Count > 0) Fire();
        }

        #endregion


        #region Private API

        #region Timers

        /// <summary>
        /// �����, ���������������� �������.
        /// </summary>
        private void InitTimers()
        {
            
            if (m_TurretProperties != null)
            {
                // ���� ���� ��������� �� ��������� �������� ����� ���������� - ���������� ���.
                if (m_TurretProperties.Type == TowerType.Artillery && Upgrades.CheckActiveUpgrade(UpgradeList.ArtilleryAttackSpeed))
                {
                    float newRateOfFire = m_TurretProperties.RateOfFire * 1.15f;
                    m_RefiteTimer = new Timer(newRateOfFire, false);
                }
                // ���� ��������� ��� - �������� ����� �����������.
                else
                {
                    m_RefiteTimer = new Timer(m_TurretProperties.RateOfFire, false);
                }
            }
            // ���� �������� ����� ���� - �������� ������.
            if (m_AudioSource && m_AudioSource.clip) m_AudioTimer = new Timer(m_AudioSource.clip.length, false);
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
            projectile.TuneProjectile(m_CurrentTower.EnemyList[0], GetComponent<Turret>());

            // ��������� ���� ��������.
            if (m_AudioTimer != null && !m_AudioTimer.IsFinished)
            {
                m_AudioSource.Play();
                m_AudioTimer.RestartTimer();
            }

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