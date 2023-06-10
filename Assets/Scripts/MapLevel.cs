using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// Скрипт, отвечающий за хранение и запуск конкретного уровня на карте уровней.
    /// </summary>
    public class MapLevel : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на уровень.
        /// </summary>
        [SerializeField] private SO_Level m_Level;

        /// <summary>
        /// Ссылка на обработчик нажатий.
        /// </summary>
        [SerializeField] private UX_LevelPoint m_UX;

        /// <summary>
        /// Ссылка на картинки звёзд.
        /// </summary>
        [SerializeField] private Image[] m_ActiveStarsImage;

        #endregion


        #region Unity Events

        private void Start()
        {
            if (m_UX == null) { Debug.Log("UX_LevelPoint == null!"); return; }

            // Подписка на событие клика.
            m_UX.OnClicked += StartLevel;

            // Отключить все звёзды.
            for (int i = 0; i < m_ActiveStarsImage.Length; i++) m_ActiveStarsImage[i].gameObject.SetActive(false);

            // Запросить сохранённый результат.
            SetLevelData();
        }

        private void OnDestroy()
        {
            // Отписаться от события клика.
            m_UX.OnClicked -= StartLevel;
        }

        #endregion


        #region Public API

        /// <summary>
        /// Загружает указанный уровень.
        /// </summary>
        public void StartLevel()
        {
            if (m_Level == null) { Debug.Log("m_Level == null!"); return; }
            if (m_Level.LevelName == null || m_Level.LevelName == "") { Debug.Log($"LevelName in Level {m_Level} == null!"); return; }

            // Загрузить указанный уровень.
            LevelSequenceController.Instance.LoadLevel(m_Level);
        }

        /// <summary>
        /// Регулирует отображение уровня в зависимости от сохранённого результата прохождения.
        /// </summary>
        public void SetLevelData()
        {
            if (MapCompletion.Instance == null) { Debug.Log("MapComplition.Instance == null!"); return; }

            // Получает сохранённые результаты уровня.
            var levelSavedResult = MapCompletion.Instance.GetLevelResult(m_Level);

            // Если результат не сохранён.
            if (levelSavedResult == null)
            {
                // Если уровень первый - выйти.
                if (m_Level.LevelNumber == 1) return;

                // Если пройден предыдущий уровень - выйти.
                if (MapCompletion.Instance.CheckLevelResultByLevelNumber(m_Level.LevelNumber - 1)) return;

                gameObject.SetActive(false);
            }
            // Если результат сохранён.
            else
            {
                // Локально сохраняет кол-во звёзд на уровне.
                int stars = levelSavedResult.LevelStars;

                // Отрисовывает звёзды в зависимости от сохранённого значения.
                if (stars >= 1) m_ActiveStarsImage[0].gameObject.SetActive(true);
                if (stars >= 2) m_ActiveStarsImage[1].gameObject.SetActive(true);
                if (stars >= 3) m_ActiveStarsImage[2].gameObject.SetActive(true);
            }
        }

        #endregion

    }
}