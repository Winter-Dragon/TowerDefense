using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// �����, �������� �������������� ������. �������� �� ���������, ������ ��������� �� �����.
    /// </summary>
    [CreateAssetMenu(fileName = "TowerProperties", menuName = "ScriptableObjects/CreateNewTowerProperties")]
    public class SO_TowerProperties : ScriptableObject
    {

        #region Properties and Components

        /// <summary>
        /// ��� �����.
        /// </summary>
        [SerializeField] private TowerType m_Type;

        /// <summary>
        /// ������� �����.
        /// </summary>
        [Range(1, 5)]
        [SerializeField] private int m_Tier;

        /// <summary>
        /// ������ �����.
        /// </summary>
        [SerializeField] private GameObject m_TurretPrefab;

        /// <summary>
        /// ��������� ����� � ������.
        /// </summary>
        [Min(0)]
        [SerializeField] private int m_GoldCost;

        #region Links

        /// <summary>
        /// ������� �����.
        /// </summary>
        public int Tier => m_Tier;

        /// <summary>
        /// ������ �����.
        /// </summary>
        public GameObject TurretPrefab => m_TurretPrefab;

        /// <summary>
        /// ��������� ����� � ������.
        /// </summary>
        public int GoldCost => m_GoldCost;

        #endregion

        #endregion

    }
}