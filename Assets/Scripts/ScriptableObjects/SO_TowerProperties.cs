using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Инициализация видов башни: лучники, маги, пехота и артиллерия.
    /// </summary>
    public enum TowerType
    {
        Archer,
        Mage,
        Infantry,
        Artillery
    }

    /// <summary>
    /// Класс, задающий характеристики башням. Работает из редактора, нельзя поставить на сцену.
    /// </summary>
    [CreateAssetMenu(fileName = "TowerProperties", menuName = "ScriptableObjects/CreateNewTowerProperties")]
    public class SO_TowerProperties : ScriptableObject
    {

        #region Properties and Components

        /// <summary>
        /// Вид башни.
        /// </summary>
        [SerializeField] private TowerType m_Type;

        /// <summary>
        /// Туррели данного вида башни.
        /// </summary>
        [SerializeField] private SO_TurretProperties[] m_Turrets;

        /// <summary>
        /// Префаб башни.
        /// </summary>
        [SerializeField] private GameObject m_TurretPrefab;

        #region Links

        /// <summary>
        /// Туррели данного вида башни.
        /// </summary>
        public SO_TurretProperties[] Turrets => m_Turrets;

        /// <summary>
        /// Префаб башни.
        /// </summary>
        public GameObject TurretPrefab => m_TurretPrefab;

        #endregion

        #endregion

    }
}