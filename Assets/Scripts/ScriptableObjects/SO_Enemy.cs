using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ��� ����� � ������.
    /// </summary>
    public enum ArmorType
    {
        /// <summary>
        /// ��� �����.
        /// </summary>
        None,
        /// <summary>
        /// ������ �� ���������� ����.
        /// </summary>
        Physics,
        /// <summary>
        /// ������ �� ���������� ����.
        /// </summary>
        Magic
    }

    /// <summary>
    /// �����, ������������� ������� ��� � �������������� ������.
    /// </summary>
    [CreateAssetMenu(fileName = "EnemyProperties", menuName = "ScriptableObjects/CreateNewEnemyProperties")]
    public class SO_Enemy : ScriptableObject
    {

        #region Properties and Components

        [Header("Animations")]
        /// <summary>
        /// �������� ������.
        /// </summary>
        [SerializeField] private Sprite[] m_WalkAnimations;

        /// <summary>
        /// �������� �����.
        /// </summary>
        [SerializeField] private Sprite[] m_AttackAnimations;

        [Header("Base")]
        /// <summary>
        /// ���-�� ������.
        /// </summary>
        [Min(1)]
        [SerializeField] private int m_HitPoints;

        /// <summary>
        /// ���-�� �����, ������� ����������� ��� ����� �����.
        /// </summary>
        [SerializeField] private int m_Armor;

        /// <summary>
        /// ��� �����.
        /// </summary>
        [SerializeField] private ArmorType m_TypeArmor;

        /// <summary>
        /// �������� ������������.
        /// </summary>
        [Min(0.01f)]
        [SerializeField] private float m_MoveSpeed;

        /// <summary>
        /// ��������� ������� � ������. ���� ������ ������� �� ������ - �������� �������� � ������.
        /// </summary>
        [Min(1)]
        [SerializeField] private int m_LivesCost;

        /// <summary>
        /// ��������� ����� � ������.
        /// </summary>
        [Min(0)]
        [SerializeField] private int m_GoldCost;

        [Header("Other")]
        /// <summary>
        /// ������ ������� �������.
        /// </summary>
        [SerializeField] private Vector2 m_SpriteScale;

        /// <summary>
        /// ������ Box Collider �� �������.
        /// </summary>
        [SerializeField] private Vector2 m_ColliderSize;

        #region Links

        /// <summary>
        /// �������� ������.
        /// </summary>
        public Sprite[] WalkAnimations => m_WalkAnimations;

        /// <summary>
        /// �������� �����.
        /// </summary>
        public Sprite[] AttackAnimations => m_AttackAnimations;

        /// <summary>
        /// ���-�� HP.
        /// </summary>
        public int HitPoints => m_HitPoints;

        /// <summary>
        /// �������� �����, ������� ����������� ��� ��������� �����.
        /// </summary>
        public int Armor => m_Armor;

        /// <summary>
        /// ��� �����.
        /// </summary>
        public ArmorType TypeArmor => m_TypeArmor;

        /// <summary>
        /// �������� ������������.
        /// </summary>
        public float MoveSpeed => m_MoveSpeed;

        /// <summary>
        /// ��������� ����� � ������ ��� ������.
        /// </summary>
        public int LivesCost => m_LivesCost;

        /// <summary>
        /// ��������� ����� � ������.
        /// </summary>
        public int GoldCost => m_GoldCost;

        /// <summary>
        /// ������ ������� �������.
        /// </summary>
        public Vector2 SpriteScale => m_SpriteScale;

        /// <summary>
        /// ������ Box Collider �� �������.
        /// </summary>
        public Vector2 ColliderSize => m_ColliderSize;

        #endregion

        #endregion

    }
}
