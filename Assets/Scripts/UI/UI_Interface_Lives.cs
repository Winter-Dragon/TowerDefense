using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TowerDefense
{
    /// <summary>
    /// ����� ����������, ����������� �������� ������.
    /// </summary>
    public class UI_Interface_Lives : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ��������� ���� � �������.
        /// </summary>
        private TextMeshProUGUI m_LivesText;

        #endregion


        #region Unity Events

        private void Start()
        {
            // ������������� � �������� ������.
            m_LivesText = GetComponent<TextMeshProUGUI>();

            // ���� ����� ���� - ����������� �� ������� ��������� ������ � ���������� ���������� � �����.
            if (Player.Instance != null)
            {
                UpdateLiveText(Player.Instance.CurrentLives);

                Player.OnLiveUpdate += UpdateLiveText;
            }
                
            else Debug.Log("Player.Instance is null!");
        }

        #endregion


        #region Public API

        /// <summary>
        /// �����, �������� ����� � ����������.
        /// </summary>
        /// <param name="lives">�������� ������.</param>
        public void UpdateLiveText(int lives)
        {
            m_LivesText.text = lives.ToString();
        }

        #endregion

    }
}