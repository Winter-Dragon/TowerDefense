using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ������������ ������ �� �����, ����� HP.
    /// </summary>
    public class Destructible : Entity
    {

        #region Properties and Components

        /// <summary>
        /// ������ ���������� �����������.
        /// </summary>
        [SerializeField] private bool m_Indestructible;

        /// <summary>
        /// ��������� �������� HP.
        /// </summary>
        [SerializeField] private int m_HitPoints;

        /// <summary>
        /// ����� �������.
        /// </summary>
        [SerializeField] private int m_TeamID;

        /// <summary>
        /// �������, ������������� ����� ������ �������.
        /// </summary>
        public event EmptyDelegate EventsOnDeath;

        /// <summary>
        /// ��������� ����������� ������� - 0.
        /// </summary>
        public const int TEAM_ID_NEUTRAL = 0;

        /// <summary>
        /// ������� �������� HP.
        /// </summary>
        private int m_CurrentHitPoints;

        /// <summary>
        /// ����� �������.
        /// </summary>
        private int m_Armor;

        /// <summary>
        /// ��� ����� �������.
        /// </summary>
        private ArmorType m_TypeArmor;

        /// <summary>
        /// ��������� ������ ���� Destructible ���������.
        /// </summary>
        private static HashSet<Destructible> m_AllDestructibles;

        /// <summary>
        /// ��������� ����� � ������.
        /// </summary>
        private int m_GoldCost;

        #region Links

        /// <summary>
        /// ����������, ���������� �� ������ �����������.
        /// </summary>
        public bool IsIndestructibly => m_Indestructible;

        /// <summary>
        /// ������������ �������� HP �������.
        /// </summary>
        public int HitPoints => m_HitPoints;

        /// <summary>
        /// ������� �������� HP �������.
        /// </summary>
        public int CurrentHitPoints => m_CurrentHitPoints;

        /// <summary>
        /// ����� �������.
        /// </summary>
        public int Armor => m_Armor;

        /// <summary>
        /// ��� ����� �������.
        /// </summary>
        public ArmorType TypeArmor => m_TypeArmor;

        /// <summary>
        /// ����� ������� �������.
        /// </summary>
        public int TeamID => m_TeamID;

        /// <summary>
        /// ������ ������ ��� ��������� ��� ������ ���� ��������� Destructible.
        /// </summary>
        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        #endregion

        #endregion


        #region Unity Events

        protected virtual void Start()
        {
            // �������� �������� HP.
            m_CurrentHitPoints = m_HitPoints;
        }

        /// <summary>
        /// �����, ������������ ��� ��������� �������.
        /// </summary>
        protected virtual void OnEnable()
        {
            // ���� ������ ��� - ������� ����� ������.
            if (m_AllDestructibles == null) m_AllDestructibles = new HashSet<Destructible>();

            // ��������� ������� ������ � ������.
            m_AllDestructibles.Add(this);
        }

        /// <summary>
        /// �������� ��� ����������� �������.
        /// </summary>
        protected virtual void OnDestroy()
        {
            // ������� ������� ������ �� ������.
            m_AllDestructibles.Remove(this);
        }

        #endregion


        #region Private API

        /// <summary>
        /// ���������������� ������� ����������� �������, ����� HP < 0.
        /// </summary>
        protected virtual void OnDeath()
        {
            // �������� �� null � ����� ������� ������.
            EventsOnDeath?.Invoke();

            // �������� ������ ������.
            if (Player.Instance == null) Debug.Log("Player.Instance is null!");
            else Player.Instance.ChangeGold(m_GoldCost);

            // ����������� �������
            Destroy(gameObject);
        }

        /// <summary>
        /// �����, �������� �������������� ������� ��� ������.
        /// </summary>
        /// <param name="characteristics">�������������� ������� SO_Enemy.</param>
        protected void SetDestructible�haracteristics(SO_Enemy characteristics)
        {
            // ������� �������� HP.
            ChangeHitPoints(characteristics.HitPoints);

            // ������� �����.
            m_Armor = characteristics.Armor;
            m_TypeArmor = characteristics.TypeArmor;

            // ������� ��������� � ������.
            m_GoldCost = characteristics.GoldCost;
        }

        #endregion


        #region Public API

        /// <summary>
        /// ���������� ����� � �������.
        /// </summary>
        /// <param name="damage">����, ��������� �������.</param>
        public void ApplyDamage(int damage)
        {
            // ��������, ����� �� ������ �������� ����.
            if (m_Indestructible) return;

            // �������� HP �������.
            m_CurrentHitPoints -= damage;

            // ���� HP <= 0, ��������� ������� ������.
            if (m_CurrentHitPoints <= 0)
            {
                OnDeath();
            }
        }

        /// <summary>
        /// �����, �������� ������� �������� HP �������.
        /// </summary>
        /// <param name="hp">����� �������� HP.</param>
        public void ChangeHitPoints(int hp)
        {
            // ������ ��������� �������� HP.
            m_HitPoints = hp;
            // ������ ������� �������� HP.
            m_CurrentHitPoints = hp;

            // �������� �� 0.
            if (hp <= 0) OnDeath();
        }

        #endregion

    }
}
