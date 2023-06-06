using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{

    /// <summary>
    /// �����, �������� �������������� ��������. �������� �� ���������, ������ ��������� �� �����.
    /// </summary>
    [CreateAssetMenu(fileName = "TurretProperties", menuName = "ScriptableObjects/CreateNewTurretProperties")]
    public sealed class SO_TurretProperties : ScriptableObject
    {

        #region Properties and Components

        /// <summary>
        /// ��� �����.
        /// </summary>
        [SerializeField] private TowerType m_Type;

        /// <summary>
        /// ������� �����.
        /// </summary>
        [Range(1, 5)]
        [SerializeField] private int m_Tier;

        /// <summary>
        /// ��������� ����� � ������.
        /// </summary>
        [Min(0)]
        [SerializeField] private int m_GoldCost;

        /// <summary>
        /// ������ �� ������ Projectile.
        /// </summary>
        [SerializeField] private Projectile m_ProjectilePrefab;

        /// <summary>
        /// ���� �������.
        /// </summary>
        [SerializeField] private Vector2 m_Damage;

        /// <summary>
        /// ���������������� �������.
        /// </summary>
        [SerializeField] private float m_RateOfFire;

        /// <summary>
        /// ������ ����� �����.
        /// </summary>
        [SerializeField] private float m_Radius;

        /// <summary>
        /// �������������.
        /// </summary>
        [SerializeField] private bool m_Homing;

        /// <summary>
        /// ������ �� ����� ��������.
        /// </summary>
        [SerializeField] private AudioClip m_LaunchSFX;

        #region Links

        /// <summary>
        /// ��� �����.
        /// </summary>
        public TowerType Type => m_Type;

        /// <summary>
        /// ������� �����.
        /// </summary>
        public int Tier => m_Tier;

        /// <summary>
        /// ��������� ����� � ������.
        /// </summary>
        public int GoldCost => m_GoldCost;

        /// <summary>
        /// ������ �� ������� Projectile.
        /// </summary>
        public Projectile ProjectilePrefab => m_ProjectilePrefab;

        /// <summary>
        /// ������ �� ���� �������.
        /// </summary>
        public Vector2 Damage => m_Damage;

        /// <summary>
        /// ������ �� ������� ���������������� �������.
        /// </summary>
        public float RateOfFire => m_RateOfFire;

        /// <summary>
        /// ������ ����� �����.
        /// </summary>
        public float Radius => m_Radius;

        /// <summary>
        /// ������, ������������, ��������������� �� ������.
        /// </summary>
        public bool Homing => m_Homing;

        /// <summary>
        /// ������ �� ������� ���� ��������.
        /// </summary>
        public AudioClip LaunchSFX => m_LaunchSFX;

        #endregion

        #endregion

    }
}