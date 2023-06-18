using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Список возможных улучшений.
    /// </summary>
    public enum UpgradeList
    {
        /// <summary>
        /// Добавляет магические эффекты в игру.
        /// </summary>
        MagicEffects,
        /// <summary>
        /// Увеличивает радиус атаки лучников на 20%.
        /// </summary>
        ArchersDistance,
        /// <summary>
        /// Ускоряет атаку артиллерии на 15%.
        /// </summary>
        ArtilleryAttackSpeed,
        /// <summary>
        /// Атаки огненных магов поджигают врагов.
        /// </summary>
        MagicFire,
        /// <summary>
        /// Улучшение в доработке :)
        /// </summary>
        ComingSoon
    }

    /// <summary>
    /// Базовый класс всех улучшений.
    /// </summary>
    public class Upgrades : Singleton<Upgrades>
    {

        #region Properties and Components

        /// <summary>
        /// Префаб улучшения волшебного огня.
        /// </summary>
        [SerializeField] private ImpactEffect m_MagicFirePrefab;

        /// <summary>
        /// Сохранялка конкретного улучшения.
        /// </summary>
        [Serializable]
        public class Uprgade
        {
            /// <summary>
            /// Уровень.
            /// </summary>
            public UpgradeList upgrade;

            /// <summary>
            /// Стоимость улучшения в звёздах.
            /// </summary>
            public int starsCount;
        }

        /// <summary>
        /// Список всех активных апгрейдов.
        /// </summary>
        public static List<Uprgade> AllActiveUpgrades;

        /// <summary>
        /// Название файла сохранения улучшений.
        /// </summary>
        public const string filename = "upgrades.dat";

        #region Links

        /// <summary>
        /// Префаб улучшения волшебного огня.
        /// </summary>
        public ImpactEffect MagicFirePrefab => m_MagicFirePrefab;

        #endregion

        #endregion


        #region Unity Events

        private new void Awake()
        {
            // Базовый метод от Singleton.
            base.Awake();

            // Попытка загрузить данные.
            Saver<List<Uprgade>>.TryLoad(filename, ref AllActiveUpgrades);
        }

        #endregion


        #region Public API

        /// <summary>
        /// Добавляет улучшение в список активных улучшений.
        /// </summary>
        /// <param name="upgrade">Тип апгрейда.</param>
        public void AddUpgrade(UpgradeList upgrade, int upgradeCount)
        {
            // Если списка нет - создать его.
            if (AllActiveUpgrades == null) AllActiveUpgrades = new();

            // Создаётся новая сохранялка.
            Uprgade newUpgrade = new()
            {
                // Записываются значения уровня.
                upgrade = upgrade,
                starsCount = upgradeCount
            };

            // Добавить текущий объект в список.
            AllActiveUpgrades.Add(newUpgrade);

            // Сохранение данных.
            Saver<List<Uprgade>>.Save(filename, AllActiveUpgrades);
        }

        /// <summary>
        /// Сбросить улучшения.
        /// </summary>
        public void ResetSavedUpgrades()
        {
            // Очистить текущий список.
            AllActiveUpgrades.Clear();

            // Удалить сохранённый файл.
            Saver<List<UpgradeList>>.Reset(filename);
        }

        /// <summary>
        /// Получить кол-во использованных звёзд, потраченных на улучшения.
        /// </summary>
        /// <returns>Кол-во звёзд, потраченных на улучшения.</returns>
        public static int GetCountUsedStars()
        {
            int stars = 0;

            // Перебираются все улучшения и считается кол-во потраченных звёзд.
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
        /// Метод, проверяющий активно ли улучшение на данный момент.
        /// </summary>
        /// <param name="currentUpgrade">Запрашиваемое улучшение.</param>
        /// <returns>true - активно, false - неактивно.</returns>
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