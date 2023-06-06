using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Базовый класс всех интерактивных игровых объектов на сцене.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// Название объекта для пользователя.
        /// </summary>
        [SerializeField] private string m_Nickname;

        #region Links

        /// <summary>
        /// Ссылка на имя объекта.
        /// </summary>
        public string Nickname => m_Nickname;

        #endregion

        #endregion

    }
}