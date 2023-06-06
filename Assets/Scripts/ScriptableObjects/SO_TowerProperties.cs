using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ������������� ����� �����: �������, ����, ������ � ����������.
    /// </summary>
    public enum TowerType
    {
        Archer,
        Mage,
        Infantry,
        Artillery
    }

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
        /// ������� ������� ���� �����.
        /// </summary>
        [SerializeField] private SO_TurretProperties[] m_Turrets;

        /// <summary>
        /// ������ �����.
        /// </summary>
        [SerializeField] private GameObject m_TurretPrefab;

        #region Links

        /// <summary>
        /// ������� ������� ���� �����.
        /// </summary>
        public SO_TurretProperties[] Turrets => m_Turrets;

        /// <summary>
        /// ������ �����.
        /// </summary>
        public GameObject TurretPrefab => m_TurretPrefab;

        #endregion

        #endregion

    }
}