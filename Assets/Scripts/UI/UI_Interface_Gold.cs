using UnityEngine;
using TMPro;

namespace TowerDefense
{
    /// <summary>
    /// Класс интерфейса, обновляющий значение золота.
    /// </summary>
    public class UI_Interface_Gold : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Текстовое поле с золотом.
        /// </summary>
        private TextMeshProUGUI m_GoldText;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Привязываемся к текущему тексту.
            m_GoldText = GetComponent<TextMeshProUGUI>();

            // Если игрок есть - подписаться на событие изменение золота и передавать информацию в метод.
            if (Player.Instance != null)
            {
                UpdateGoldText(Player.Instance.CurrentGold);

                Player.OnGoldUpdate += UpdateGoldText;
            }
            else Debug.Log("Player.Instance is null!");
        }

        private void OnDestroy()
        {
            // Отписаться от события изменения золота.
            Player.OnGoldUpdate -= UpdateGoldText;
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод, меняющий золото в интерфейсе.
        /// </summary>
        /// <param name="gold">Значение золота.</param>
        public void UpdateGoldText(int gold)
        {
            m_GoldText.text = gold.ToString();
        }

        #endregion

    }
}