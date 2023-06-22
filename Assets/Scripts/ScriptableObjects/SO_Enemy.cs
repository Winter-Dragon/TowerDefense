using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Тип брони у врагов.
    /// </summary>
    public enum ArmorType
    {
        /// <summary>
        /// Нет брони.
        /// </summary>
        None,
        /// <summary>
        /// Защита от физических атак.
        /// </summary>
        Physics,
        /// <summary>
        /// Защита от магических атак.
        /// </summary>
        Magic
    }

    /// <summary>
    /// Класс, настраивающий внешний вид и характеристики врагов.
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyProperties", menuName = "ScriptableObjects/CreateNewEnemyProperties")]
    public class SO_Enemy : ScriptableObject
    {

        #region Properties and Components

        [Header("Animations")]
        /// <summary>
        /// Анимация ходьбы.
        /// </summary>
        [SerializeField] private Sprite[] m_WalkAnimations;

        /// <summary>
        /// Анимация атаки.
        /// </summary>
        [SerializeField] private Sprite[] m_AttackAnimations;

        [Header("Base")]
        /// <summary>
        /// Кол-во жизней.
        /// </summary>
        [Min(1)]
        [SerializeField] private int m_HitPoints;

        /// <summary>
        /// Кол-во урона, которое блокируется при атаке башни.
        /// </summary>
        [SerializeField] private int m_Armor;

        /// <summary>
        /// Тип брони.
        /// </summary>
        [SerializeField] private ArmorType m_TypeArmor;

        /// <summary>
        /// Скорость передвижения.
        /// </summary>
        [Min(0.01f)]
        [SerializeField] private float m_MoveSpeed;

        /// <summary>
        /// Стоимость объекта в жизнях. Если объект доходит до финиша - отнимает значение у игрока.
        /// </summary>
        [Min(1)]
        [SerializeField] private int m_LivesCost;

        /// <summary>
        /// Стоимость юнита в золоте.
        /// </summary>
        [Min(0)]
        [SerializeField] private int m_GoldCost;

        [Header("Other")]
        /// <summary>
        /// Размер спрайта объекта.
        /// </summary>
        [SerializeField] private Vector2 m_SpriteScale;

        /// <summary>
        /// Размер Box Collider на объекте.
        /// </summary>
        [SerializeField] private Vector2 m_ColliderSize;

        #region Links

        /// <summary>
        /// Анимация ходьбы.
        /// </summary>
        public Sprite[] WalkAnimations => m_WalkAnimations;

        /// <summary>
        /// Анимация атаки.
        /// </summary>
        public Sprite[] AttackAnimations => m_AttackAnimations;

        /// <summary>
        /// Кол-во HP.
        /// </summary>
        public int HitPoints => m_HitPoints;

        /// <summary>
        /// Значение урона, которое блокируется при получении урона.
        /// </summary>
        public int Armor => m_Armor;

        /// <summary>
        /// Тип брони.
        /// </summary>
        public ArmorType TypeArmor => m_TypeArmor;

        /// <summary>
        /// Скорость передвижения.
        /// </summary>
        public float MoveSpeed => m_MoveSpeed;

        /// <summary>
        /// Стоимость юнита в жизнях для игрока.
        /// </summary>
        public int LivesCost => m_LivesCost;

        /// <summary>
        /// Стоимость юнита в золоте.
        /// </summary>
        public int GoldCost => m_GoldCost;

        /// <summary>
        /// Размер спрайта объекта.
        /// </summary>
        public Vector2 SpriteScale => m_SpriteScale;

        /// <summary>
        /// Размер Box Collider на объекте.
        /// </summary>
        public Vector2 ColliderSize => m_ColliderSize;

        #endregion

        #endregion

    }
}
