using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    /// <summary>
    /// �������� ����� ��������, ����������� ��������.
    /// </summary>
    public class Projectile : Entity
    {

        #region Properties and Components

        /// <summary>
        /// �������� �������.
        /// </summary>
        [SerializeField] private float m_Velocity;

        /// <summary>
        /// ����� ����� �������.
        /// </summary>
        [SerializeField] private float m_LifeTime;

        /// <summary>
        /// ������ �� ������ �������, ������������� ��� ���������.
        /// </summary>
        [SerializeField] private ImpactEffect m_impactEffectPrefab;

        /// <summary>
        /// ������ ������ ������� � ����������.
        /// </summary>
        [Header("Only for TowerType.Artillery!")]
        [SerializeField] private float m_ExplosionRadius;

        /// <summary>
        /// ���� �������.
        /// </summary>
        private Vector2 m_Damage;

        /// <summary>
        /// ���������� ������.
        /// </summary>
        private Timer m_Timer;

        /// <summary>
        /// �������, ������� ����������.
        /// </summary>
        private Turret m_Turret;

        /// <summary>
        /// ���� ���������������� ��������.
        /// </summary>
        private Enemy m_Target;

        /// <summary>
        /// ������� ������� ����.
        /// </summary>
        private Vector2 m_PreviousTargetPosition;

        /// <summary>
        /// ����������, ������������, ��� ������� ���� ��������� ����.
        /// </summary>
        private bool m_SetTarget;

        /// <summary>
        /// ��� ������������ �����.
        /// </summary>
        private TowerType m_Type;

        /// <summary>
        /// ���� ��������� ��������� ���������� ��������.
        /// </summary>
        private bool m_MagicEffectsEnabled;

        #region Links

        /// <summary>
        /// �������� ������� (������/���).
        /// </summary>
        public float Velocity => m_Velocity;

        #endregion

        #endregion


        #region Unity Events

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            // ���������� ���������� � �������� ��������.
            Handles.color = Color.magenta;
            Handles.DrawWireDisc(transform.position, transform.forward, m_ExplosionRadius);
        }
#endif

        private void Start()
        {
            // �������� ������� ������� ����� �������.
            m_Timer = new Timer(m_LifeTime, false);

            // ���� ��������� ��������� ���� �� �����.
            if (Upgrades.Instance)
            {
                // ���� ��������� � ����������� ��������� ��������� - ��������� ��.
                if (Upgrades.CheckActiveUpgrade(UpgradeList.MagicEffects))
                {
                    m_MagicEffectsEnabled = true;
                }
            }
            else Debug.Log("Upgrades is null!");

            // ��������� ������� ������, ���� ��������� ���.
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

                    // ���� ���� �� ��������� - ���������� ������.
                    if (m_Target == null) { Destroy(gameObject); return; }

                    // ������� ���� - ���������� ������� �������.
                    m_PreviousTargetPosition = m_Target.transform.position;

                    break;

                case TowerType.Artillery:

                    // ���� ���� ��� � ��� �� ���� ��������� - ���������� ������.
                    if (m_Target == null && m_SetTarget == false) { Destroy(gameObject); return; }

                    // ������� ���� - ���������� ������� �������.
                    if (m_SetTarget == false) m_PreviousTargetPosition = m_Target.transform.position;

                    // ������� ���� ��������� ����.
                    m_SetTarget = true;
                    break;
            }

            // ���� ������ �� ������ ����.
            if ((Vector2) transform.position != m_PreviousTargetPosition)
            {
                // ����������, �������� ��������, �� ������� ��������� ������ � ������ �����.
                float StepLengh = Time.fixedDeltaTime * m_Velocity;

                // ������������ �������.
                transform.position = Vector2.MoveTowards(transform.position, m_PreviousTargetPosition, StepLengh);

                // �������� ������ �����������.
                Vector2 direction = m_PreviousTargetPosition - (Vector2) transform.position;

                // ����������� ������� = ������ �����������.
                transform.up = direction;

                // ���� ������ ������ ����.
                if ((Vector2) transform.position == m_PreviousTargetPosition)
                {
                    // ���� ���� ��� �� ����������.
                    if (m_Target != null)
                    {
                        // ����������� ��������� ����.
                        int damage = CalculateDamage(m_Damage, m_Target.Armor, m_Target.TypeArmor, m_Type);

                        // ������� ���� �������.
                        m_Target.ApplyDamage(damage);

                        // �������� ������� ��� ���������, ���� �� ����
                        if (m_impactEffectPrefab && m_MagicEffectsEnabled)
                        {
                            ImpactEffect impactEffect = Instantiate(m_impactEffectPrefab);
                            impactEffect.transform.position = transform.position;
                        }

                        // ���� ���� ��������� �� ��������� ����� ��� ����� � ���� ��� �� ������.
                        if (m_Target && m_Type == TowerType.Mage && Upgrades.CheckActiveUpgrade(UpgradeList.MagicFire))
                        {
                            // ������ ������ ���� �� ����.
                            ImpactEffect magicFire = Instantiate(Upgrades.Instance.MagicFirePrefab, m_Target.transform.root);
                            magicFire.transform.position = m_Target.transform.position;
                        }

                        // ���� ��� ���������� - ������� �����.
                        if (m_Type == TowerType.Artillery)
                        {
                            InstantiateExplosion();
                        }
                    }

                    // ���������� ������.
                    Destroy(gameObject);
                }
            }

            // ���������� �������.
            m_Timer.UpdateTimer();

            // ���������� ������, ���� ������� ����� �����.
            if (m_Timer.IsFinished) Destroy(gameObject);
        }

        #endregion


        #region Private API

        /// <summary>
        /// �����, ��������� ���� �������� �������.
        /// </summary>
        private void InstantiateExplosion()
        {
            // ������� ��� ���������� � ���� ������.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, m_ExplosionRadius);

            // �������� �� ����������� ��������.
            foreach (Collider2D collider in colliders)
            {
                // ������� Enemy � ������������ �������.
                Enemy enemy = collider.transform.root.GetComponent<Enemy>();

                // ���� Enemy ��� || Enemy - ���������� ����, ����� �� ���������� ��������.
                if (enemy == null || enemy == m_Target) continue;

                // ����������� ��������� ����.
                int damage = CalculateDamage(m_Damage, m_Target.Armor, m_Target.TypeArmor, m_Type);

                // ������� ���� �������.
                enemy.ApplyDamage(damage);
            }
        }

        /// <summary>
        /// ������� ���-�� �����, ������� ���������� �������.
        /// </summary>
        /// <param name="damage">��������� ������ �����.</param>
        /// <param name="armor">���-�� ����� ����.</param>
        /// <param name="armorType">��� ����� ����.</param>
        /// <param name="towerType">��� ����� �����.</param>
        /// <returns></returns>
        private int CalculateDamage(Vector2 damage, int armor, ArmorType armorType, TowerType towerType)
        {
            // ��������� ��������� �������� ����� �� ����������.
            int calculatedDamage = (int) Random.Range(damage.x, damage.y);
            // ��������� ���� � ����������� �� �����.
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
            // ����������� ���� - 1.
            if (calculatedDamage < 1) calculatedDamage = 1;

            return calculatedDamage;
        }

        #endregion


        #region Public API

        /// <summary>
        /// �����, �������� ��� ����������� ��������� Projectile ����� ��� ��������.
        /// </summary>
        /// <param name="target">���� ��� �����.</param>
        /// <param name="turret">��������� �������.</param>
        public void TuneProjectile(Enemy target, Turret turret)
        {
            // �������� �� ������� �������.
            if (turret == null) { Debug.Log("Turret is null!"); return; }
            // �������� �� ������� ����.
            if (target == null) { Destroy(gameObject); return; }

            // �������� ����������� ���� ��� �����.
            m_Target = target;

            // ����� �������, ���� � ���.
            m_Turret = turret;
            m_Damage = m_Turret.CurrentTurretProperties.Damage;
            m_Type = m_Turret.CurrentTurretProperties.Type;
        }

        #endregion

    }
}