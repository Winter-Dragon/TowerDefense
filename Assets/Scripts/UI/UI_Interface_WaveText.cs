using UnityEngine;
using TMPro;

namespace TowerDefense
{
    /// <summary>
    /// ����� ���������� ����������� ����.
    /// </summary>
    public class UI_Interface_WaveText : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� ����� ������ �����.
        /// </summary>
        private TextMeshProUGUI m_WaveText;

        /// <summary>
        /// ����� ���-�� ����.
        /// </summary>
        private int m_WavesCount;

        #endregion


        #region Unity Events

        private void Start()
        {
            // ����� ��������� � �������.
            m_WaveText = GetComponent<TextMeshProUGUI>();

            // �������� �� ��������� ������.
            if (LevelController.Instance == null) { Debug.Log("LevelController.Instance == null!"); return; }

            // ������������ ����� ���-�� ����.
            m_WavesCount = LevelController.Instance.NumberWaves;

            // ����������� �� ������� ��������� �����.
            LevelController.OnWaveUpdate += WaveUpdate;

            // ���������� ����� ��������� ����������.
            WaveUpdate(LevelController.Instance.CurrentWave);
        }

        #endregion


        #region Private API

        /// <summary>
        /// �����, ����������� ��������� ���������.
        /// </summary>
        /// <param name="wave">����� ������� �����.</param>
        private void WaveUpdate(int wave)
        {
            m_WaveText.text = wave.ToString() + " / " + m_WavesCount.ToString();
        }

        #endregion

    }
}