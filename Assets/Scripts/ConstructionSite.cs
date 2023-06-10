using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Класс места для постройки башни.
    /// </summary>
    public class ConstructionSite : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Ссылка на обработчик кликов.
        /// </summary>
        [SerializeField] private UX_BuildSite m_UX;

        #endregion


        #region Unity Events

        private void Start()
        {
            // Подписка на событие клика.
            m_UX.OnClicked += ClickEvent;
        }

        private void OnDestroy()
        {
            // Отписаться от события клика.
            m_UX.OnClicked -= ClickEvent;
        }

        #endregion


        #region Private API


        private void ClickEvent()
        {
            // Проверка на наличие интерфейса покупки на сцене.
            if (UI_Interface_BuyTower.Instance == null) { Debug.Log("UI_Interface_BuyTower.Instance == null!"); return; }

            // Отобразить интерфейс в заданной позиции.
            UI_Interface_BuyTower.Instance.DisplayInterface(transform.position);
        }

        #endregion


        #region Public API

        /// <summary>
        /// Метод постройки башни на месте, где расположен construstion site.
        /// </summary>
        /// <param name="tower">Префаб башни.</param>
        public void BuildTower(GameObject tower)
        {
            // Создать башню, задать позицию.
            tower = Instantiate(tower);
            tower.transform.position = transform.position;

            // Уничтожить текущий объект.
            Destroy(gameObject);
        }

        #endregion

    }
}
