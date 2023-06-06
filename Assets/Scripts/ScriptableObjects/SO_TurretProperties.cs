using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{

    /// <summary>
    /// Класс, задающий характеристики туррелям. Работает из редактора, нельзя поставить на сцену.
    /// </summary>
    [CreateAssetMenu(fileName = "TurretProperties", menuName = "ScriptableObjects/CreateNewTurretProperties")]
    public sealed class SO_TurretProperties : ScriptableObject
    {

        #region Properties and Components

        /// <summary>
        /// Тип башни.
        /// </summary>
        [SerializeField] private TowerType m_Type;

        /// <summary>
        /// Уровень башни.
        /// </summary>
        [Range(1, 5)]
        [SerializeField] private int m_Tier;

        /// <summary>
        /// Стоимость башни в золоте.
        /// </summary>
        [Min(0)]
        [SerializeField] private int m_GoldCost;

        /// <summary>
        /// Ссылка на префаб Projectile.
        /// </summary>
        [SerializeField] private Projectile m_ProjectilePrefab;

        /// <summary>
        /// Урон снаряда.
        /// </summary>
        [SerializeField] private Vector2 m_Damage;

        /// <summary>
        /// Скорострельность туррели.
        /// </summary>
        [SerializeField] private float m_RateOfFire;

        /// <summary>
        /// Радиус атаки башни.
        /// </summary>
        [SerializeField] private float m_Radius;

        /// <summary>
        /// Самонаведение.
        /// </summary>
        [SerializeField] private bool m_Homing;

        /// <summary>
        /// Ссылка на аудио выстрела.
        /// </summary>
        [SerializeField] private AudioClip m_LaunchSFX;

        #region Links

        /// <summary>
        /// Тип башни.
        /// </summary>
        public TowerType Type => m_Type;

        /// <summary>
        /// Уровень башни.
        /// </summary>
        public int Tier => m_Tier;

        /// <summary>
        /// Стоимость башни в золоте.
        /// </summary>
        public int GoldCost => m_GoldCost;

        /// <summary>
        /// Ссылка на текущий Projectile.
        /// </summary>
        public Projectile ProjectilePrefab => m_ProjectilePrefab;

        /// <summary>
        /// Ссылка на урон снаряда.
        /// </summary>
        public Vector2 Damage => m_Damage;

        /// <summary>
        /// Ссылка на текущую скорострельность туррели.
        /// </summary>
        public float RateOfFire => m_RateOfFire;

        /// <summary>
        /// Радиус атаки башни.
        /// </summary>
        public float Radius => m_Radius;

        /// <summary>
        /// Ссылка, показывающая, самонаводящееся ли оружие.
        /// </summary>
        public bool Homing => m_Homing;

        /// <summary>
        /// Ссылка на текущий звук выстрела.
        /// </summary>
        public AudioClip LaunchSFX => m_LaunchSFX;

        #endregion

        #endregion

    }
}