using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Уничтожаемый объект на сцене, имеет HP.
    /// </summary>
    public class Destructible : Entity
    {

        #region Properties and Components

        /// <summary>
        /// Объект игнорирует повреждения.
        /// </summary>
        [SerializeField] private bool m_Indestructible;

        /// <summary>
        /// Стартовое значение HP.
        /// </summary>
        [SerializeField] private int m_HitPoints;

        /// <summary>
        /// Номер команды.
        /// </summary>
        [SerializeField] private int m_TeamID;

        /// <summary>
        /// События, выполняющиеся после смерти объекта.
        /// </summary>
        public event EmptyDelegate EventsOnDeath;

        /// <summary>
        /// Константа нейтральной команды - 0.
        /// </summary>
        public const int TEAM_ID_NEUTRAL = 0;

        /// <summary>
        /// Текущее значение HP.
        /// </summary>
        private int m_CurrentHitPoints;

        /// <summary>
        /// Броня объекта.
        /// </summary>
        private int m_Armor;

        /// <summary>
        /// Тип брони объекта.
        /// </summary>
        private ArmorType m_TypeArmor;

        /// <summary>
        /// Статичный список всех Destructible элементов.
        /// </summary>
        private static HashSet<Destructible> m_AllDestructibles;

        /// <summary>
        /// Стоимость юнита в золоте.
        /// </summary>
        private int m_GoldCost;

        #region Links

        /// <summary>
        /// Отображает, игнорирует ли объект повреждения.
        /// </summary>
        public bool IsIndestructibly => m_Indestructible;

        /// <summary>
        /// Максимальное значение HP объекта.
        /// </summary>
        public int HitPoints => m_HitPoints;

        /// <summary>
        /// Текущее значение HP объекта.
        /// </summary>
        public int CurrentHitPoints => m_CurrentHitPoints;

        /// <summary>
        /// Броня объекта.
        /// </summary>
        public int Armor => m_Armor;

        /// <summary>
        /// Тип брони объекта.
        /// </summary>
        public ArmorType TypeArmor => m_TypeArmor;

        /// <summary>
        /// Номер команды объекта.
        /// </summary>
        public int TeamID => m_TeamID;

        /// <summary>
        /// Ссылка только для прочтения для списка всех элементов Destructible.
        /// </summary>
        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        #endregion

        #endregion


        #region Unity Events

        protected virtual void Start()
        {
            // Обнуляет значение HP.
            m_CurrentHitPoints = m_HitPoints;
        }

        /// <summary>
        /// Метод, вызывающийся при появлении объекта.
        /// </summary>
        protected virtual void OnEnable()
        {
            // Если списка нет - создать новый список.
            if (m_AllDestructibles == null) m_AllDestructibles = new HashSet<Destructible>();

            // Добавляет текущий объект в список.
            m_AllDestructibles.Add(this);
        }

        /// <summary>
        /// Действия при уничтожении объекта.
        /// </summary>
        protected virtual void OnDestroy()
        {
            // Удалить текущий объект из списка.
            m_AllDestructibles.Remove(this);
        }

        #endregion


        #region Private API

        /// <summary>
        /// Переопределяемое событие уничножение объекта, когда HP < 0.
        /// </summary>
        protected virtual void OnDeath()
        {
            // Проверка на null и вызов события смерти.
            EventsOnDeath?.Invoke();

            // Добавить золото игроку.
            if (Player.Instance == null) Debug.Log("Player.Instance is null!");
            else Player.Instance.ChangeGold(m_GoldCost);

            // Уничтожение объекта
            Destroy(gameObject);
        }

        /// <summary>
        /// Метод, задающий характеристики объекту при спавне.
        /// </summary>
        /// <param name="characteristics">Характеристики объекта SO_Enemy.</param>
        protected void SetDestructibleСharacteristics(SO_Enemy characteristics)
        {
            // Задаётся значение HP.
            ChangeHitPoints(characteristics.HitPoints);

            // Задаётся броня.
            m_Armor = characteristics.Armor;
            m_TypeArmor = characteristics.TypeArmor;

            // Задаётся стоимость в золоте.
            m_GoldCost = characteristics.GoldCost;
        }

        #endregion


        #region Public API

        /// <summary>
        /// Применение урона к объекту.
        /// </summary>
        /// <param name="damage">Урон, наносимый объекту.</param>
        public void ApplyDamage(int damage)
        {
            // Проверка, может ли объект получать урон.
            if (m_Indestructible) return;

            // Отнимает HP объекта.
            m_CurrentHitPoints -= damage;

            // Если HP <= 0, запускает событие смерти.
            if (m_CurrentHitPoints <= 0)
            {
                OnDeath();
            }
        }

        /// <summary>
        /// Метод, меняющий текущее значение HP объекта.
        /// </summary>
        /// <param name="hp">Новое значение HP.</param>
        public void ChangeHitPoints(int hp)
        {
            // Меняет стартовое значение HP.
            m_HitPoints = hp;
            // Меняет текущее значение HP.
            m_CurrentHitPoints = hp;

            // Проверка на 0.
            if (hp <= 0) OnDeath();
        }

        #endregion

    }
}
