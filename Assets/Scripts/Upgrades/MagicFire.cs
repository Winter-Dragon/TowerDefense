using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    ///  ������ ��������� ���������� ����.
    /// </summary>
    public class MagicFire : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ����, �� ������� ����� �����.
        /// </summary>
        private Enemy m_CurrentEnemy;

        /// <summary>
        /// ������.
        /// </summary>
        private Timer m_Timer;

        #endregion

        #region UnityEvents

        private void Start()
        {
            // ����������� ����.
            m_CurrentEnemy = transform.root.GetComponent<Enemy>();

            // ���� ����� ��� - ������������.
            if (!m_CurrentEnemy) Destroy(gameObject);

            // �������� ������.
            m_Timer = new Timer(0.5f, true);
        }

        private void FixedUpdate()
        {
            if (m_Timer.IsFinished)
            {
                // ��������� ����.
                float damage = m_CurrentEnemy.HitPoints / 100;
                if (damage < 1) damage = 1;

                // ��������� ����.
                m_CurrentEnemy.ApplyDamage((int)damage);
            }

            // ����������� ������.
            m_Timer.UpdateTimer();
        }

        #endregion
    }
}