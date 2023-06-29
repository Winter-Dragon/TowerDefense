using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Инициализация видов башни: лучники, маги, пехота и артиллерия.
    /// </summary>
    public enum TowerType
    {
        /// <summary>
        /// Башня магических лучников.
        /// </summary>
        Archer,
        /// <summary>
        /// Башня огненных магов.
        /// </summary>
        Mage,
        /// <summary>
        /// Башня с пехотой.
        /// </summary>
        Infantry,
        /// <summary>
        /// Магическая артиллерия.
        /// </summary>
        Artillery
    }

    [RequireComponent(typeof(CircleCollider2D))]
    public class Tower : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Характеристики башни.
        /// </summary>
        [SerializeField] private SO_TowerProperties m_CurrentTowerProperties;

        /// <summary>
        /// Туррели башни.
        /// </summary>
        [SerializeField] private Turret[] m_Turrets;

        /// <summary>
        /// Стартовый радиус башни.
        /// </summary>
        [SerializeField] private float m_StartRadius;

        /// <summary>
        /// Список врагов, вошедших в радиус башни.
        /// </summary>
        public readonly List<Enemy> EnemyList = new();

        /// <summary>
        /// Коллайдер башни.
        /// </summary>
        private CircleCollider2D m_Collider;

        #region Links

        /// <summary>
        /// Радиус атаки башни.
        /// </summary>
        public float Radius => m_StartRadius;

        #endregion

        #endregion


        #region Unity Events

        private void Start()
        {
            // Назначить коллайдер.
            TryAssignCollider();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Если враг вошёл в коллайдер.
            if (collision.transform.root.TryGetComponent<Enemy>(out Enemy enemy))
            {
                // Добавить в список атаки.
                EnemyList.Add(enemy);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // Если враг вышел из коллайдера.
            if (collision.transform.root.TryGetComponent<Enemy>(out Enemy enemy))
            {
                // Удалить из списка атаки.
                EnemyList.Remove(enemy);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            // Назначить коллайдер текущего объекта.
            if (TryAssignCollider())
            {
                // Назначить радиус коллайдеру.
                m_Collider.radius = m_StartRadius;
                m_Collider.isTrigger = true;
            }
        }
#endif

        #endregion


        #region Private API

        /// <summary>
        /// Метод, назначающий коллайдер.
        /// </summary>
        /// <returns>true - коллайдер назначен, false - не назначен.</returns>
        private bool TryAssignCollider()
        {
            if (!m_Collider)
            {
                if (TryGetComponent<CircleCollider2D>(out CircleCollider2D collider))
                {
                    m_Collider = collider;
                    return true;
                }
                else { Debug.Log("Missing collider!"); return false; }
            }

            return true;
        }

        #endregion

    }
}