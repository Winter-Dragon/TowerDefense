using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    /// <summary>
    /// Основной класс снарядов, выпускаемых туррелью.
    /// </summary>
    public class Projectile : Entity
    {

        #region Properties and Components

        /// <summary>
        /// Скорость снаряда.
        /// </summary>
        [SerializeField] private float m_Velocity;

        /// <summary>
        /// Время жизни снаряда.
        /// </summary>
        [SerializeField] private float m_LifeTime;

        /// <summary>
        /// Ссылка на префаб объекта, появляющегося при попадании.
        /// </summary>
        [SerializeField] private GameObject m_impactEffectPrefab;

        /// <summary>
        /// Радиус взрыва снаряда у артиллерии.
        /// </summary>
        [Header("Only for TowerType.Artillery!")]
        [SerializeField] private float m_ExplosionRadius;

        /// <summary>
        /// Урон снаряда.
        /// </summary>
        private Vector2 m_Damage;

        /// <summary>
        /// Внутренний таймер.
        /// </summary>
        private Timer m_Timer;

        /// <summary>
        /// Туррель, которая выстрелила.
        /// </summary>
        private Turret m_Turret;

        /// <summary>
        /// Цель самонаводящегося выстрела.
        /// </summary>
        private Enemy m_Target;

        /// <summary>
        /// Прошлая позиция цели.
        /// </summary>
        private Vector2 m_PreviousTargetPosition;

        /// <summary>
        /// Переменная, отображающая, что снаряду была назначена цель.
        /// </summary>
        private bool m_SetTarget;

        /// <summary>
        /// Тип выстрелившей башни.
        /// </summary>
        private TowerType m_Type;

        #region Links

        /// <summary>
        /// Скорость снаряда (тайлов/сек).
        /// </summary>
        public float Velocity => m_Velocity;

        #endregion

        #endregion


        #region Unity Events

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            // Нарисовать окружность с заданным радиусом.
            Handles.color = Color.magenta;
            Handles.DrawWireDisc(transform.position, transform.forward, m_ExplosionRadius);
        }
#endif

        private void Start()
        {
            // Создание таймера времени жизни снаряда.
            m_Timer = new Timer(m_LifeTime, false);
        }

        private void FixedUpdate()
        {
            switch (m_Type)
            {
                case TowerType.Archer or TowerType.Mage:

                    // Если цель не назначена - уничтожить снаряд.
                    if (m_Target == null) { Destroy(gameObject); return; }

                    // Позиция цели - предыдущая позиция объекта.
                    m_PreviousTargetPosition = m_Target.transform.position;

                    break;

                case TowerType.Artillery:

                    // Если цели нет и она не была назначена - уничтожить снаряд.
                    if (m_Target == null && m_SetTarget == false) { Destroy(gameObject); return; }

                    // Позиция цели - предыдущая позиция объекта.
                    if (m_SetTarget == false) m_PreviousTargetPosition = m_Target.transform.position;

                    // Снаряду была назначена цель.
                    m_SetTarget = true;
                    break;
            }

            // Если снаряд не настиг цель.
            if ((Vector2) transform.position != m_PreviousTargetPosition)
            {
                // Переменная, хранящая смещение, на которое смещается снаряд в каждом кадре.
                float StepLengh = Time.fixedDeltaTime * m_Velocity;

                // Интерполяция снаряда.
                transform.position = Vector2.MoveTowards(transform.position, m_PreviousTargetPosition, StepLengh);

                // Создаётся вектор направления.
                Vector2 direction = m_PreviousTargetPosition - (Vector2) transform.position;

                // Направление снаряда = вектор направления.
                transform.up = direction;

                // Если снаряд настиг цель.
                if ((Vector2) transform.position == m_PreviousTargetPosition)
                {
                    // Если цель ещё не уничтожена.
                    if (m_Target != null)
                    {
                        // Вычисляется рандомное значение из вектора урона, из него вычитается броня цели. Минимальное значение урона - 1.
                        int damage = (int)Random.Range(m_Damage.x, m_Damage.y) - m_Target.Armor;
                        if (damage < 1) damage = 1;

                        // Нанести урон объекту.
                        m_Target.ApplyDamage(damage);

                        // Если тип артиллерия - создать взрыв.
                        if (m_Type == TowerType.Artillery)
                        {
                            InstantiateExplosion();
                        }
                    }

                    // Уничтожить снаряд.
                    Destroy(gameObject);
                }
            }

            // Обновление таймера.
            m_Timer.UpdateTimer();

            // Уничтожить объект, если истекло время жизни.
            if (m_Timer.IsFinished) Destroy(gameObject);
        }

        #endregion


        #region Private API

        /// <summary>
        /// Метод, наносящий урон объектам взрывом.
        /// </summary>
        private void InstantiateExplosion()
        {
            // Находим все коллайдеры в зоне взрыва.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, m_ExplosionRadius);

            // Проходим по коллайдерам массивом.
            foreach (Collider2D collider in colliders)
            {
                // Находим Enemy в родительском объекте.
                Enemy enemy = collider.transform.root.GetComponent<Enemy>();

                // Если Enemy нет || Enemy - предыдущая цель, выйти из выполнения итерации.
                if (enemy == null || enemy == m_Target) continue;

                // Вычисляется рандомное значение из вектора урона, из него вычитается броня цели. Минимальное значение урона - 1.
                int damage = (int)Random.Range(m_Damage.x, m_Damage.y) - m_Target.Armor;
                if (damage < 1) damage = 1;

                // Нанести урон объекту.
                enemy.ApplyDamage(damage);
            }
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод, задающий все необходимые параметры Projectile после его создания.
        /// </summary>
        /// <param name="target">Цель для атаки.</param>
        /// <param name="turret">Атакующая туррель.</param>
        public void TuneProjectile(Enemy target, Turret turret)
        {
            // Проверка на наличие туррели.
            if (turret == null) { Debug.Log("Turret is null!"); return; }
            // Проверка на наличие цели.
            if (target == null) { Destroy(gameObject); return; }

            // Локально сохраняется цель для атаки.
            m_Target = target;

            // Задаёт туррель, урон и тип.
            m_Turret = turret;
            m_Damage = m_Turret.CurrentTurretProperties.Damage;
            m_Type = m_Turret.CurrentTurretProperties.Type;
        }

        #endregion

    }
}