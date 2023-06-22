using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Проверяет текущую волну и дёргает нужные глобальные сущности в зависимости от волны.
    /// </summary>
    public class WaveManager : Singleton<WaveManager>
    {

        #region Properties and Components

        /// <summary>
        /// Общее кол-во волн на уровне.
        /// </summary>
        private int m_MaxWave;

        #endregion


        #region Unity Events

        private void Start()
        {
            if (LevelController.Instance == null) { Debug.Log("LevelController null."); return; }

            // Подписаться на событие изменения номера волны.
            LevelController.OnWaveUpdate += WaveUpdate;

            // Локально сохранить номер последней волны.
            m_MaxWave = LevelController.Instance.NumberWaves;
        }

        private void OnDestroy()
        {
            // Отписаться от событий.
            LevelController.OnWaveUpdate -= WaveUpdate;
            Enemy.EnemyDestroy -= LastWaveUpdate;
        }

        #endregion


        #region Private API

        /// <summary>
        /// Метод, локально сохраняющий номер волны.
        /// </summary>
        /// <param name="currentWave">Номер текущей волны.</param>
        private void WaveUpdate(int currentWave)
        {
            // Если волна последняя - мониторить смерти врагов.
            if (currentWave == m_MaxWave) Enemy.EnemyDestroy += LastWaveUpdate;
        }

        /// <summary>
        /// Метод, проверяющий все ли враги на уровне уничтожены.
        /// </summary>
        private void LastWaveUpdate()
        {
            // Проверка, был ли создан список врагов.
            if (Destructible.AllDestructibles == null) { Debug.Log("Destructible.AllDestructibles == null! Враги не были созданы."); enabled = false; return; }

            // Если все враги уничтожены - дёрнуть контроллёр завершить уровень и отписаться от события.
            if (Destructible.AllDestructibles.Count == 0)
            {
                LevelController.Instance.CompleteLevel(true);
                Enemy.EnemyDestroy -= LastWaveUpdate;
            }
        }

        #endregion

    }
}