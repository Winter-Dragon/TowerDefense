using UnityEngine;

namespace TowerDefense
{
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
        /// Уровень башни.
        /// </summary>
        [Range(1, 5)]
        [SerializeField] private int m_Tier;

        /// <summary>
        /// Префаб башни.
        /// </summary>
        [SerializeField] private GameObject m_TurretPrefab;

        /// <summary>
        /// Стоимость башни в золоте.
        /// </summary>
        [Min(0)]
        [SerializeField] private int m_GoldCost;

        #region Links

        /// <summary>
        /// Уровень башни.
        /// </summary>
        public int Tier => m_Tier;

        /// <summary>
        /// Префаб башни.
        /// </summary>
        public GameObject TurretPrefab => m_TurretPrefab;

        /// <summary>
        /// Стоимость башни в золоте.
        /// </summary>
        public int GoldCost => m_GoldCost;

        #endregion

        #endregion

    }
}