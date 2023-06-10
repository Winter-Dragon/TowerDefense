using System.Collections.Generic;
using UnityEngine;
using System;

namespace TowerDefense
{
    /// <summary>
    /// Класс, настраивающий отдельные волны на уровне: указывет тип врага, волна, на которой он появится, его кол-во и скорость спавна.
    /// </summary>
    [Serializable]
    public class WaveList
    {
        [SerializeField] private int m_Wave;
        [SerializeField] private SO_Enemy m_Enemy;
        [SerializeField] private int m_Amount;
        [SerializeField] private float m_SpawnSpeed;

        public int Wave => m_Wave;
        public SO_Enemy Enemy => m_Enemy;
        public int Amount => m_Amount;
        public float SpawnSpeed => m_SpawnSpeed;
    }

    /// <summary>
    /// Класс создания волн врагов на уровне.
    /// </summary>
    [CreateAssetMenu(fileName = "WaveProperties", menuName = "ScriptableObjects/CreateNewWaveProperties")]
    public class SO_WaveProperties : ScriptableObject
    {

        #region Properties and Components

        /// <summary>
        /// Номер уровня.
        /// </summary>
        [SerializeField] private int m_level;

        /// <summary>
        /// Кол-во волн на уровне.
        /// </summary>
        [SerializeField] private int m_NumberWaves;

        /// <summary>
        /// Массив, настраивающий волны на уровне.
        /// </summary>
        [SerializeField] private List<WaveList> m_Waves = new List<WaveList>();

        #region Links

        /// <summary>
        /// Номер уровня.
        /// </summary>
        public int LevelNumber => m_level;

        /// <summary>
        /// Кол-во волн на уровне.
        /// </summary>
        public int NumberWaves => m_NumberWaves;

        /// <summary>
        /// Массив с настройками волн.
        /// </summary>
        public List<WaveList> Waves => m_Waves;

        #endregion

        #endregion


        #region Public API

        /// <summary>
        /// Метод, возвращающий список врагов в выбранной волне на уровне.
        /// </summary>
        /// <param name="wave">Номер волны.</param>
        /// <returns>Список врагов в волне.</returns>
        public List<SO_Enemy> GetEnemyInWave(int wave)
        {
            // Созание списка настроек врагов.
            List<SO_Enemy> enemyList = new List<SO_Enemy>();

            // Прохождение по массиву волн.
            for(int i = 0; i < m_Waves.Count; i++)
            {
                // Если номер волны меньше, чем заданный номер - продолжить цикл по след элементу.
                if (wave > m_Waves[i].Wave) continue;

                // Если волна совпадает с волной в массиве.
                if (wave == m_Waves[i].Wave)
                {
                    // Добавляет в список врага из этой волны.
                    enemyList.Add(m_Waves[i].Enemy);
                    continue;
                }

                // Если номер волны больше, чем заданный номер - выход из цикла.
                if (wave < m_Waves[i].Wave) break;
            }

            // Возвращает список врагов.
            return enemyList;
        }

        /// <summary>
        /// Метод, возвращающий массив с кол-вом врагов в волне.
        /// Метод применяется после метода GetEnemyInWave, если видов врагов в волне > 1.
        /// </summary>
        /// <param name="wave">Номер волны.</param>
        /// <returns>Количество врагов в волне.</returns>
        public List<int> GetEnemyAmountInWave(int wave)
        {
            // Создание массива с кол-вом.
            List<int> enemyAmount = new List<int>();

            // Прохождение по массиву волн.
            for (int i = 0; i < m_Waves.Count; i++)
            {
                // Если номер волны меньше, чем заданный номер - продолжить цикл по след элементу.
                if (wave > m_Waves[i].Wave) continue;

                // Если волна совпадает с волной в массиве.
                if (wave == m_Waves[i].Wave)
                {
                    // Добавляет в список врага из этой волны.
                    enemyAmount.Add(m_Waves[i].Amount);
                    continue;
                }

                // Если номер волны больше, чем заданный номер - выход из цикла.
                if (wave < m_Waves[i].Wave) break;
            }

            // Возвращает массив с кол-вом врагов.
            return enemyAmount;
        }

        /// <summary>
        /// Метод, возвращающий массив со скоростью спавна врагов в волне.
        /// Метод применяется после метода GetEnemyInWave, если видов врагов в волне > 1.
        /// </summary>
        /// <param name="wave">Номер волны.</param>
        /// <returns>Скорость спавна врагов в волне.</returns>
        public List<float> GetEnemySpawnSpeed(int wave)
        {
            // Создание массива со скоростью спавна каждого врага.
            List<float> enemySpawnTime = new List<float>();

            // Прохождение по массиву волн.
            for (int i = 0; i < m_Waves.Count; i++)
            {
                // Если номер волны меньше, чем заданный номер - продолжить цикл по след элементу.
                if (wave > m_Waves[i].Wave) continue;

                // Если волна совпадает с волной в массиве.
                if (wave == m_Waves[i].Wave)
                {
                    // Добавляет в список скорость спавна врага из этой волны.
                    enemySpawnTime.Add(m_Waves[i].SpawnSpeed);
                    continue;
                }

                // Если номер волны больше, чем заданный номер - выход из цикла.
                if (wave < m_Waves[i].Wave) break;
            }

            // Возвращает массив со скоростью спавна врагов в волне.
            return enemySpawnTime;
        }

        #endregion

    }
}