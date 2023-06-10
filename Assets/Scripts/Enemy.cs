using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TowerDefense
{
    /// <summary>
    /// ������ ��������� ��.
    /// </summary>
    public enum AIBehaviour
    {
        Null,
        Walk
    }

    /// <summary>
    /// �������� ����� �����.
    /// </summary>
    [RequireComponent(typeof(CircleArea), typeof(Rigidbody2D))]
    public class Enemy : Destructible
    {

        #region Properties and Components

        /// <summary>
        /// ����� ��������� ������� ����� Inspector.
        /// </summary>
        [SerializeField] private AIBehaviour m_AIBehaviour;

        /// <summary>
        /// �������� ������������ �������.
        /// </summary>
        [SerializeField] private float m_Speed;

        /// <summary>
        /// ������� ������������ ��.
        /// </summary>
        [SerializeField] private List<CircleArea> m_Route;

        /// <summary>
        /// ������ ������ ����.
        /// </summary>
        [SerializeField] private float m_FindTargetRadius;

        /// <summary>
        /// �������� ����� �������.
        /// </summary>
        [SerializeField] private float m_AttackDelay;

        /// <summary>
        /// ������ �� ������ �������� �������.
        /// </summary>
        [SerializeField] private SpriteRenderer m_Sprite;

        /// <summary>
        /// ������ �� �������� �������� �� �������.
        /// </summary>
        [SerializeField] private AnimateSpriteFrames m_SpriteAnimator;

        /// <summary>
        /// ������ �� ��������� �������.
        /// </summary>
        [SerializeField] private BoxCollider2D m_BoxCollider;

        /// <summary>
        /// ��������� �������.
        /// </summary>
        private Rigidbody2D m_Rigidbody;

        /// <summary>
        /// ������ ������� ������� �������� �������.
        /// </summary>
        private Vector3 m_MovePosition;

        /// <summary>
        /// ����������, ������������ ������� ������ ������� ������������ �������.
        /// </summary>
        private int m_CurrentPositionIndex;

        /// <summary>
        /// ����, ������������ ������� �� ����� ��� ��������.
        /// </summary>
        private bool m_NewMovePositionSelected;

        /// <summary>
        /// Destructible ���� ��� �����.
        /// </summary>
        private Destructible m_SelectedTarget;

        /// <summary>
        /// ���� ����� � ������.
        /// </summary>
        private int m_LivesCost;

        #region Timers

        /// <summary>
        /// ������ �����.
        /// </summary>
        private Timer m_AttackTimer;

        #endregion

        #region Links

        


        #endregion

        #endregion


        #region Unity Events

#if UNITY_EDITOR
        /// <summary>
        /// �����, ���������� � ���������� ����� ��������� ��������.
        /// </summary>
        private void OnValidate()
        {
            // ������������� ����� ������ CircleArea �� �������� �������.
            GetComponent<CircleArea>().ChangeRadius(m_FindTargetRadius);
        }

        /// <summary>
        /// ������, �������� ����� ������������ ��.
        /// </summary>
        private void OnDrawGizmosSelected()
        {
            Handles.color = new Color(255, 0, 0, 1);
            Handles.DrawLine(transform.position, m_MovePosition);
        }
#endif

        protected override void Start()
        {
            // ������������ �������� ���������������� ������.
            base.Start();

            // ������������� ��������.
            InitTimers();

            // ����� ������ �� Rigidbody � �������.
            m_Rigidbody = GetComponent<Rigidbody2D>();

            // ����������� �� ������� �����.
            PauseController.OnPaused += Paused;
        }

        /// <summary>
        /// �������� ��� ����������� �������.
        /// </summary>
        protected override void OnDestroy()
        {
            // ��������� ������� ����� �� Destructuble.
            base.OnDestroy();

            // ���������� �� ������� �����.
            PauseController.OnPaused -= Paused;
        }

        private void FixedUpdate()
        {
            // �������� ��� �������.
            UpdateTimers();

            // �������� ��������� ��.
            UpdateAI();
        }

        #endregion


        #region Private API

        #region Timers

        /// <summary>
        /// �����, ���������������� ��� �������.
        /// </summary>
        private void InitTimers()
        {
            m_AttackTimer = new Timer(m_AttackDelay, false);
        }

        /// <summary>
        /// �����, ����������� ��� �������.
        /// </summary>
        private void UpdateTimers()
        {
            m_AttackTimer.UpdateTimer();
        }

        #endregion

        /// <summary>
        /// �����, ����������� �������� ��.
        /// </summary>
        private void UpdateAI()
        {
            // �������� ��.
            switch (m_AIBehaviour)
            {
                // �������� �� �������.
                case AIBehaviour.Null:
                    // ����� ���� ��� ��������.
                    ActionFindNewMovePosition();
                    break;

                // �������� ��������.
                case AIBehaviour.Walk:
                    // ����� ���� ��� ��������.
                    ActionFindNewMovePosition();
                    // ��������� � ����.
                    ActionMove();
                    break;
            }
        }

        /// <summary>
        /// �����, ������ ��������� ���� ��� ������������.
        /// </summary>
        private void ActionFindNewMovePosition()
        {
            // �������� ��.
            switch (m_AIBehaviour)
            {
                // ��������� Null.
                case AIBehaviour.Null:

                    // ���� ���� ������� - ������ �������� �� ��������.
                    if (m_Route != null) m_AIBehaviour = AIBehaviour.Walk;
                    break;

                // �������� Patrul
                case AIBehaviour.Walk:
                    // ���� ���� ������� ������������.
                    if (m_Route != null && m_Route.Count > 0)
                    {
                        // ����������, ������������, ��������� �� ������ � ���� ��������� ����� ��������.
                        bool isInsidePatrolZone = (m_Route[m_CurrentPositionIndex].transform.position - transform.position).sqrMagnitude < m_Route[m_CurrentPositionIndex].Radius * m_Route[m_CurrentPositionIndex].Radius;

                        // ���� ������� �� �������� ��������� ���� ��������.
                        if (isInsidePatrolZone)
                        {
                            // ������ ������� ++.
                            m_CurrentPositionIndex++;

                            // ���� ������ ������, ��� ���-�� ����� ������������.
                            if (m_CurrentPositionIndex >= m_Route.Count)
                            {
                                // �������� �� ������.
                                if (Player.Instance == null) Debug.Log("Player.Instance is null!");
                                // �������� ����� � ������, ���� ����� ����.
                                else Player.Instance.ChangeLives(-m_LivesCost);

                                // ���������� ������.
                                Destroy(gameObject);
                                return;
                            }

                            // ������� ������ ��������� ������� � ��������� ������� ����� ����� ������������.
                            Vector2 newPoint = m_Route[m_CurrentPositionIndex].GetRandomInsideZone();

                            // ������� ����� ������� �������� � �����.
                            m_MovePosition = newPoint;
                        }
                        // ���� ������ �� � ���� ������������.
                        else
                        {
                            // ��������, ������� �� ��� ����� ��� ��������.
                            if (m_NewMovePositionSelected) return;

                            // ������� ������ ��������� ������� � ��������� ������� ������� ����� ������������.
                            Vector2 newPoint = m_Route[m_CurrentPositionIndex].GetRandomInsideZone();

                            // ������� ����� ������� �������� � �����.
                            m_MovePosition = newPoint;

                            // ���� ������ ����� ��� ��������.
                            m_NewMovePositionSelected = true;
                        }

                        // ���� ������� ��� �������� ���.
                        if (m_MovePosition == null)
                        {
                            // ������� ������ ��������� ������� � ��������� ������� ����� �����.
                            Vector2 newPoint = m_Route[m_CurrentPositionIndex].GetRandomInsideZone();

                            // ������� ����� ������� �������� � �����.
                            m_MovePosition = newPoint;
                        }
                    }
                    // ���� ��� �������� ��������.
                    else
                    {
                        // ������� ��������� �� ��������� ��������� �������.
                        Debug.Log("Select Route! Object stay sleep.");

                        // ������� ������ �� � null.
                        m_AIBehaviour = AIBehaviour.Null;
                    }
                    break;
            }
        }

        /// <summary>
        /// ����� ������������ �����.
        /// </summary>
        private void ActionMove()
        {
            // ������ ��������������� ����� ������ � ��������� ����� � ��������� ���������.
            m_Rigidbody.MovePosition(Vector3.MoveTowards(transform.position, m_MovePosition, m_Speed * Time.fixedDeltaTime));

            // ������ �������� �������.
            Vector2 distance = m_MovePosition - transform.position;

            // ���� ������ X ��������� ������ - �� ������������� ������.
            if (distance.x > 0.01)
            {
                m_Sprite.flipX = false;
            }
            // ���� ������ X ��������� ����� - ���������� ������.
            if (distance.x < 0.01)
            {
                m_Sprite.flipX = true;
            }
        }

        /// <summary>
        /// ����� ���������� �� �����.
        /// </summary>
        /// <param name="pause">�����?</param>
        private void Paused(bool pause)
        {
            // ��������� ������ � ����������� �� ��������� �����.
            enabled = !pause;
        }

        #endregion


        #region Public API

        /// <summary>
        /// �����, ����������� ������ ������� ������� ��������.
        /// </summary>
        /// <param name="route">������ CircleArea.</param>
        public void SetRoute(List<CircleArea> route)
        {
            m_Route = route;
        }

        /// <summary>
        /// �����, �������� ������� �������������� ��� ������ ����� �������.
        /// </summary>
        /// <param name="characteristics">�������������� ������� SO_Enemy.</param>
        public void SetEnemy�haracteristics(SO_Enemy characteristics)
        {
            // ������� �������� ������������.
            m_Speed = characteristics.MoveSpeed;

            // ������� ��������� � ������.
            m_LivesCost = characteristics.LivesCost;

            // ��������� �������� ���� � ��������.
            m_SpriteAnimator.SetNewAnimationFrames(characteristics.WalkAnimations);

            // ������� �������� �������.
            m_Sprite.size = characteristics.SpriteScale;

            // ��������� ������ ������� ����������.
            m_BoxCollider.size = characteristics.ColliderSize;

            // �������� ������������� � Destructible.
            SetDestructible�haracteristics(characteristics);
        }

        #endregion

    }
}
