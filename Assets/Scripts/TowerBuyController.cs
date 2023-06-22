using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// Класс постройки башни.
    /// </summary>
    public class TowerBuyController : Singleton<TowerBuyController>
    {

        #region Public API

        /// <summary>
        /// Попробовать построить башню.
        /// </summary>
        /// <param name="constructionSite">Место постройки.</param>
        /// <param name="towerAsset">Характеристики башни.</param>
        /// <param name="goldCost">Стоимость постройки башни.</param>
        public void TryBuildTower(ConstructionSite constructionSite, SO_TowerProperties towerAsset, int goldCost)
        {
            // Проверка на игрока.
            if (Player.Instance == null) { Debug.Log("Player.Instance == null!"); return; }
            // Проверка на наличие золота.
            if (Player.Instance.CurrentGold < goldCost) return;

            // Построить башню.
            constructionSite.BuildTower(towerAsset.TurretPrefab);

            // Убрать золото у игрока.
            Player.Instance.ChangeGold(-goldCost);

            // Проверка на наличие интерфейса покупки башен.
            if (UI_Interface_BuyTower.Instance == null) { Debug.Log("UI_Interface_BuyTower.Instance == null!"); return; }
            // Если интерфейс есть - скрыть его.
            else UI_Interface_BuyTower.Instance.SetStateInterface(false);
        }

        #endregion

    }
}
