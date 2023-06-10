using UnityEngine;
using TMPro;

namespace TowerDefense
{
    /// <summary>
    /// ����� ����������, ����������� �������� ������.
    /// </summary>
    public class UI_Interface_Gold : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ��������� ���� � �������.
        /// </summary>
        private TextMeshProUGUI m_GoldText;

        #endregion


        #region Unity Events

        private void Start()
        {
            // ������������� � �������� ������.
            m_GoldText = GetComponent<TextMeshProUGUI>();

            // ���� ����� ���� - ����������� �� ������� ��������� ������ � ���������� ���������� � �����.
            if (Player.Instance != null)
            {
                UpdateGoldText(Player.Instance.CurrentGold);

                Player.OnGoldUpdate += UpdateGoldText;
            }
            else Debug.Log("Player.Instance is null!");
        }

        private void OnDestroy()
        {
            // ���������� �� ������� ��������� ������.
            Player.OnGoldUpdate -= UpdateGoldText;
        }

        #endregion


        #region Public API

        /// <summary>
        /// �����, �������� ������ � ����������.
        /// </summary>
        /// <param name="gold">�������� ������.</param>
        public void UpdateGoldText(int gold)
        {
            m_GoldText.text = gold.ToString();
        }

        #endregion

    }
}