using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ����� ��������� �����.
    /// </summary>
    public class TowerBuyController : Singleton<TowerBuyController>
    {

        #region Public API

        /// <summary>
        /// ����������� ��������� �����.
        /// </summary>
        /// <param name="constructionSite">����� ���������.</param>
        /// <param name="towerAsset">�������������� �����.</param>
        /// <param name="goldCost">��������� ��������� �����.</param>
        public void TryBuildTower(ConstructionSite constructionSite, SO_TowerProperties towerAsset, int goldCost)
        {
            // �������� �� ������.
            if (Player.Instance == null) { Debug.Log("Player.Instance == null!"); return; }
            // �������� �� ������� ������.
            if (Player.Instance.CurrentGold < goldCost) return;

            // ��������� �����.
            constructionSite.BuildTower(towerAsset.TurretPrefab);

            // ������ ������ � ������.
            Player.Instance.ChangeGold(-goldCost);

            // �������� �� ������� ���������� ������� �����.
            if (UI_Interface_BuyTower.Instance == null) { Debug.Log("UI_Interface_BuyTower.Instance == null!"); return; }
            // ���� ��������� ���� - ������ ���.
            else UI_Interface_BuyTower.Instance.SetStateInterface(false);
        }

        #endregion

    }
}
