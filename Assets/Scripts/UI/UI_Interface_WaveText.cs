using UnityEngine;
using TMPro;

namespace TowerDefense
{
    /// <summary>
    /// Класс интерфейса отображения волн.
    /// </summary>
    public class UI_Interface_WaveText : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на текст номера волны.
        /// </summary>
        private TextMeshProUGUI m_WaveText;

        /// <summary>
        /// Общее кол-во волн.
        /// </summary>
        private int m_WavesCount;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Найти компонент с текстом.
            m_WaveText = GetComponent<TextMeshProUGUI>();

            // Проверка на контроллёр уровня.
            if (LevelController.Instance == null) { Debug.Log("LevelController.Instance == null!"); return; }

            // Записывается общее кол-во волн.
            m_WavesCount = LevelController.Instance.NumberWaves;

            // Подписаться на событие изменения волны.
            LevelController.OnWaveUpdate += WaveUpdate;

            // Вызывается метод изменения интерфейса.
            WaveUpdate(LevelController.Instance.CurrentWave);
        }

        #endregion


        #region Private API

        /// <summary>
        /// Метод, обновляющий текстовый интерфейс.
        /// </summary>
        /// <param name="wave">Номер текущей волны.</param>
        private void WaveUpdate(int wave)
        {
            m_WaveText.text = wave.ToString() + " / " + m_WavesCount.ToString();
        }

        #endregion

    }
}