using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ������ ��������� ���������.
    /// </summary>
    public enum UpgradeList
    {
        /// <summary>
        /// ��������� ���������� ������� � ����.
        /// </summary>
        MagicEffects,
        /// <summary>
        /// ����������� ������ ����� �������� �� 20%.
        /// </summary>
        ArchersDistance,
        /// <summary>
        /// �������� ����� ���������� �� 15%.
        /// </summary>
        ArtilleryAttackSpeed,
        /// <summary>
        /// ����� �������� ����� ��������� ������.
        /// </summary>
        MagicFire,
        /// <summary>
        /// ��������� � ��������� :)
        /// </summary>
        ComingSoon
    }

    /// <summary>
    /// ������� ����� ���� ���������.
    /// </summary>
    public class Upgrades : Singleton<Upgrades>
    {

        #region Properties and Components

        /// <summary>
        /// ������ ��������� ���������� ����.
        /// </summary>
        [SerializeField] private ImpactEffect m_MagicFirePrefab;

        /// <summary>
        /// ���������� ����������� ���������.
        /// </summary>
        [Serializable]
        public class Uprgade
        {
            /// <summary>
            /// �������.
            /// </summary>
            public UpgradeList upgrade;

            /// <summary>
            /// ��������� ��������� � ������.
            /// </summary>
            public int starsCount;
        }

        /// <summary>
        /// ������ ���� �������� ���������.
        /// </summary>
        public static List<Uprgade> AllActiveUpgrades;

        /// <summary>
        /// �������� ����� ���������� ���������.
        /// </summary>
        public const string filename = "upgrades.dat";

        #region Links

        /// <summary>
        /// ������ ��������� ���������� ����.
        /// </summary>
        public ImpactEffect MagicFirePrefab => m_MagicFirePrefab;

        #endregion

        #endregion


        #region Unity Events

        private new void Awake()
        {
            // ������� ����� �� Singleton.
            base.Awake();

            // ������� ��������� ������.
            Saver<List<Uprgade>>.TryLoad(filename, ref AllActiveUpgrades);
        }

        #endregion


        #region Public API

        /// <summary>
        /// ��������� ��������� � ������ �������� ���������.
        /// </summary>
        /// <param name="upgrade">��� ��������.</param>
        public void AddUpgrade(UpgradeList upgrade, int upgradeCount)
        {
            // ���� ������ ��� - ������� ���.
            if (AllActiveUpgrades == null) AllActiveUpgrades = new();

            // �������� ����� ����������.
            Uprgade newUpgrade = new()
            {
                // ������������ �������� ������.
                upgrade = upgrade,
                starsCount = upgradeCount
            };

            // �������� ������� ������ � ������.
            AllActiveUpgrades.Add(newUpgrade);

            // ���������� ������.
            Saver<List<Uprgade>>.Save(filename, AllActiveUpgrades);
        }

        /// <summary>
        /// �������� ���������.
        /// </summary>
        public void ResetSavedUpgrades()
        {
            // �������� ������� ������.
            AllActiveUpgrades.Clear();

            // ������� ���������� ����.
            Saver<List<UpgradeList>>.Reset(filename);
        }

        /// <summary>
        /// �������� ���-�� �������������� ����, ����������� �� ���������.
        /// </summary>
        /// <returns>���-�� ����, ����������� �� ���������.</returns>
        public static int GetCountUsedStars()
        {
            int stars = 0;

            // ������������ ��� ��������� � ��������� ���-�� ����������� ����.
            if (AllActiveUpgrades != null && AllActiveUpgrades.Count > 0)
            {
                foreach(Uprgade upgrade in AllActiveUpgrades)
                {
                    stars += upgrade.starsCount;
                }
            }

            return stars;
        }

        /// <summary>
        /// �����, ����������� ������� �� ��������� �� ������ ������.
        /// </summary>
        /// <param name="currentUpgrade">������������� ���������.</param>
        /// <returns>true - �������, false - ���������.</returns>
        public static bool CheckActiveUpgrade(UpgradeList currentUpgrade)
        {
            bool upgradeIsActive = false;

            if (AllActiveUpgrades != null && AllActiveUpgrades.Count > 0)
            {
                foreach (Uprgade upgrade in AllActiveUpgrades)
                {
                    if (upgrade.upgrade == currentUpgrade) upgradeIsActive = true;
                }
            }

            return upgradeIsActive;
        }

        #endregion

    }
}