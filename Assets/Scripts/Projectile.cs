using UnityEngine;
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
        [SerializeField] private ImpactEffect m_impactEffectPrefab;

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

        /// <summary>
        /// Флаг активного улучшения магических эффектов.
        /// </summary>
        private bool m_MagicEffectsEnabled;

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

            // Если хранилище улучшений есть на сцене.
            if (Upgrades.Instance)
            {
                // Если улучшение с магическими эффектами неактивно - выключает их.
                if (Upgrades.CheckActiveUpgrade(UpgradeList.MagicEffects))
                {
                    m_MagicEffectsEnabled = true;
                }
            }
            else Debug.Log("Upgrades is null!");

            // Выключает систему частиц, если улучшения нет.
            if (!m_MagicEffectsEnabled)
            {
                switch (m_Type)
                {
                    case TowerType.Artillery or TowerType.Mage:
                        gameObject.GetComponent<ParticleSystem>().Stop();
                        break;

                    case TowerType.Archer:
                        gameObject.GetComponent<TrailRenderer>().enabled = false;
                        break;
                }
            }
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
                        // Вычисляется наносимый урон.
                        int damage = CalculateDamage(m_Damage, m_Target.Armor, m_Target.TypeArmor, m_Type);

                        // Нанести урон объекту.
                        m_Target.ApplyDamage(damage);

                        // Создание эффекта при попадании, если он есть
                        if (m_impactEffectPrefab && m_MagicEffectsEnabled)
                        {
                            ImpactEffect impactEffect = Instantiate(m_impactEffectPrefab);
                            impactEffect.transform.position = transform.position;
                        }

                        // Если есть улучшение на волшебный огонь для магов и цель ещё не мертва.
                        if (m_Target && m_Type == TowerType.Mage && Upgrades.CheckActiveUpgrade(UpgradeList.MagicFire))
                        {
                            // Создаёт префаб огня на цели.
                            ImpactEffect magicFire = Instantiate(Upgrades.Instance.MagicFirePrefab, m_Target.transform.root);
                            magicFire.transform.position = m_Target.transform.position;
                        }

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

                // Вычисляется наносимый урон.
                int damage = CalculateDamage(m_Damage, m_Target.Armor, m_Target.TypeArmor, m_Type);

                // Нанести урон объекту.
                enemy.ApplyDamage(damage);
            }
        }

        /// <summary>
        /// Считает кол-во урона, которые необходимо нанести.
        /// </summary>
        /// <param name="damage">Начальный вектор урона.</param>
        /// <param name="armor">Кол-во брони цели.</param>
        /// <param name="armorType">Тип брони цели.</param>
        /// <param name="towerType">Тип урона башни.</param>
        /// <returns></returns>
        private int CalculateDamage(Vector2 damage, int armor, ArmorType armorType, TowerType towerType)
        {
            // Считается случайное значение урона из промежутка.
            int calculatedDamage = (int) Random.Range(damage.x, damage.y);
            // Считается урон в зависимости от брони.
            switch (armorType)
            {
                case ArmorType.Physics:
                    if (towerType == TowerType.Archer || towerType == TowerType.Artillery || towerType == TowerType.Infantry)
                    {
                        calculatedDamage -= armor;
                    }
                    break;

                case ArmorType.Magic:
                    if (towerType == TowerType.Mage)
                    {
                        calculatedDamage -= armor;
                    }
                    break;
            }
            // Минимальный урон - 1.
            if (calculatedDamage < 1) calculatedDamage = 1;

            return calculatedDamage;
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