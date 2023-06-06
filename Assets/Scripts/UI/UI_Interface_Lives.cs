using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TowerDefense
{
    /// <summary>
    /// Класс интерфейса, обновляющий значение жизней.
    /// </summary>
    public class UI_Interface_Lives : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Текстовое поле с золотом.
        /// </summary>
        private TextMeshProUGUI m_LivesText;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Привязываемся к текущему тексту.
            m_LivesText = GetComponent<TextMeshProUGUI>();

            // Если игрок есть - подписаться на событие изменение жизней и передавать информацию в метод.
            if (Player.Instance != null)
            {
                UpdateLiveText(Player.Instance.CurrentLives);

                Player.OnLiveUpdate += UpdateLiveText;
            }
                
            else Debug.Log("Player.Instance is null!");
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод, меняющий жизни в интерфейсе.
        /// </summary>
        /// <param name="lives">Значение золота.</param>
        public void UpdateLiveText(int lives)
        {
            m_LivesText.text = lives.ToString();
        }

        #endregion

    }
}