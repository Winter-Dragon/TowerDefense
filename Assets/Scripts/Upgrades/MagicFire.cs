using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    ///  Скрипт улучшения волшебного огня.
    /// </summary>
    public class MagicFire : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Враг, на котором висит огонь.
        /// </summary>
        private Enemy m_CurrentEnemy;

        /// <summary>
        /// Таймер.
        /// </summary>
        private Timer m_Timer;

        #endregion

        #region UnityEvents

        private void Start()
        {
            // Назначается цель.
            m_CurrentEnemy = transform.root.GetComponent<Enemy>();

            // Если врага нет - уничтожиться.
            if (!m_CurrentEnemy) Destroy(gameObject);

            // Создаётся таймер.
            m_Timer = new Timer(0.5f, true);
        }

        private void FixedUpdate()
        {
            if (m_Timer.IsFinished)
            {
                // Считается урон.
                float damage = m_CurrentEnemy.HitPoints / 100;
                if (damage < 1) damage = 1;

                // Наносится урон.
                m_CurrentEnemy.ApplyDamage((int)damage);
            }

            // Обновляется таймер.
            m_Timer.UpdateTimer();
        }

        #endregion
    }
}