using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Класс, отвечающий за обработку кликов на экране камеры.
    /// </summary>
    public class WorldSpace : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// UX элемент, отслеживающий нажатия.
        /// </summary>
        [SerializeField] private UX_NullBuildSite m_UX;

        #endregion


        #region Unity Events

        private void Start()
        {
            m_UX.OnClicked += ClickToWorldSpace;
        }

        #endregion


        #region Private API

        /// <summary>
        /// Метод, срабатывающий когда игрок кликает на игровой экран.
        /// </summary>
        private void ClickToWorldSpace()
        {
            // Проверка на наличие интерфейса покупки башен.
            if (UI_Interface_BuyTower.Instance == null) { Debug.Log("UI_Interface_BuyTower.Instance == null!"); return; }
            // Если интерфейс есть - скрыть его.
            else UI_Interface_BuyTower.Instance.SetStateInterface(false);
        }

        #endregion

    }
}