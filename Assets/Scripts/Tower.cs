using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ������������� ����� �����: �������, ����, ������ � ����������.
    /// </summary>
    public enum TowerType
    {
        /// <summary>
        /// ����� ���������� ��������.
        /// </summary>
        Archer,
        /// <summary>
        /// ����� �������� �����.
        /// </summary>
        Mage,
        /// <summary>
        /// ����� � �������.
        /// </summary>
        Infantry,
        /// <summary>
        /// ���������� ����������.
        /// </summary>
        Artillery
    }

    [RequireComponent(typeof(CircleCollider2D))]
    public class Tower : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// �������������� �����.
        /// </summary>
        [SerializeField] private SO_TowerProperties m_CurrentTowerProperties;

        /// <summary>
        /// ������� �����.
        /// </summary>
        [SerializeField] private Turret[] m_Turrets;

        /// <summary>
        /// ��������� ������ �����.
        /// </summary>
        [SerializeField] private float m_StartRadius;

        /// <summary>
        /// ������ ������, �������� � ������ �����.
        /// </summary>
        public readonly List<Enemy> EnemyList = new();

        /// <summary>
        /// ��������� �����.
        /// </summary>
        private CircleCollider2D m_Collider;

        #region Links

        /// <summary>
        /// ������ ����� �����.
        /// </summary>
        public float Radius => m_StartRadius;

        #endregion

        #endregion


        #region Unity Events

        private void Start()
        {
            // ��������� ���������.
            TryAssignCollider();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // ���� ���� ����� � ���������.
            if (collision.transform.root.TryGetComponent<Enemy>(out Enemy enemy))
            {
                // �������� � ������ �����.
                EnemyList.Add(enemy);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // ���� ���� ����� �� ����������.
            if (collision.transform.root.TryGetComponent<Enemy>(out Enemy enemy))
            {
                // ������� �� ������ �����.
                EnemyList.Remove(enemy);
            }
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            // ��������� ��������� �������� �������.
            if (TryAssignCollider())
            {
                // ��������� ������ ����������.
                m_Collider.radius = m_StartRadius;
                m_Collider.isTrigger = true;
            }
        }
#endif

        #endregion


        #region Private API

        /// <summary>
        /// �����, ����������� ���������.
        /// </summary>
        /// <returns>true - ��������� ��������, false - �� ��������.</returns>
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