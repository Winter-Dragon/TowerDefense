using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Делегат изменения ресурсов у игрока.
    /// </summary>
    /// <param name="value">Новое значение ресурсов игрока.</param>
    public delegate void ResourcesUpdate(int value);

    /// <summary>
    /// Базовый класс игрока.
    /// </summary>
    public class Player : Singleton<Player>
    {

        #region Properties and Components

        /// <summary>
        /// Количество дополнительных жизней.
        /// </summary>
        [SerializeField] private int m_NumberLives;

        /// <summary>
        /// Начальное кол-во золота.
        /// </summary>
        [SerializeField] private int m_StartGold;

        /// <summary>
        /// Событие изменения золота у игрока.
        /// </summary>
        public static event ResourcesUpdate OnGoldUpdate;

        /// <summary>
        /// Событие изменения жизней у игрока.
        /// </summary>
        public static event ResourcesUpdate OnLiveUpdate;

        /// <summary>
        /// Текущее количество жизней.
        /// </summary>
        private int m_CurrentLives;

        /// <summary>
        /// Кол-во золота у игрока.
        /// </summary>
        private int m_Gold;

        #region Links

        /// <summary>
        /// Ссылка на текущее значение жизней.
        /// </summary>
        public int CurrentLives => m_CurrentLives;

        /// <summary>
        /// Текущее значение золота игрока.
        /// </summary>
        public int CurrentGold => m_Gold;

        #endregion

        #endregion


        #region UnityEvents

        private void Start()
        {
            // Задаётся начальное кол-во жизней и золота.
            m_CurrentLives = m_NumberLives;
            m_Gold = m_StartGold;

            // Вызов событий обновления золота и жизней.
            OnGoldUpdate?.Invoke(m_Gold);
            OnLiveUpdate?.Invoke(m_CurrentLives);
        }

        #endregion


        #region private API



        #endregion


        #region Public API

        /// <summary>
        /// Обнулить переменные в данном классе.
        /// </summary>
        public void Restart()
        {
            // Возвращает исходное значение HP.
            m_CurrentLives = m_NumberLives;
        }

        /// <summary>
        /// Метод, добавляющий/убавляющий HP.
        /// </summary>
        /// <param name="lives">Кол-во жизней, которые прибавятся к текущему значению. При отрицательном значении жизни отнимутся.</param>
        public void ChangeLives(int lives)
        {
            // Изменение кол-ва жизней.
            m_CurrentLives += lives;

            // Если жизни закончились.
            if (m_CurrentLives <= 0)
            {
                // обнуление жизней.
                m_CurrentLives = 0;

                // Проверка на наличие контроллёра результатов уровня.
                if (UI_LevelResultPanel.Instance == null) { Debug.Log("UI_LevelResultPanel.Instance == null!"); return; }
                // Вызов поражения уровня.
                UI_LevelResultPanel.Instance.DisplayLevelResult(false);
            }

            // Вызов события изменения HP.
            OnLiveUpdate?.Invoke(m_CurrentLives);
        }

        /// <summary>
        /// Добавить золото игроку.
        /// </summary>
        /// <param name="gold">Кол-во золота.</param>
        public void ChangeGold(int gold)
        {
            m_Gold += gold;

            // Вызов события изменения золота.
            OnGoldUpdate?.Invoke(m_Gold);
        }

        #endregion

    }
}