using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    /// <summary>
    /// ������, ���������� �� �������� � ������ ����������� ������ �� ����� �������.
    /// </summary>
    public class MapLevel : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// ������ �� �������.
        /// </summary>
        [SerializeField] private SO_Level m_Level;

        /// <summary>
        /// ������ �� ���������� �������.
        /// </summary>
        [SerializeField] private UX_LevelPoint m_UX;

        /// <summary>
        /// ������ �� �������� ����.
        /// </summary>
        [SerializeField] private Image[] m_ActiveStarsImage;

        #endregion


        #region Unity Events

        private void Start()
        {
            if (m_UX == null) { Debug.Log("UX_LevelPoint == null!"); return; }

            // �������� �� ������� �����.
            m_UX.OnClicked += StartLevel;

            // ��������� ��� �����.
            for (int i = 0; i < m_ActiveStarsImage.Length; i++) m_ActiveStarsImage[i].gameObject.SetActive(false);

            // ��������� ���������� ���������.
            SetLevelData();
        }

        private void OnDestroy()
        {
            // ���������� �� ������� �����.
            m_UX.OnClicked -= StartLevel;
        }

        #endregion


        #region Public API

        /// <summary>
        /// ��������� ��������� �������.
        /// </summary>
        public void StartLevel()
        {
            if (m_Level == null) { Debug.Log("m_Level == null!"); return; }
            if (m_Level.LevelName == null || m_Level.LevelName == "") { Debug.Log($"LevelName in Level {m_Level} == null!"); return; }

            // ��������� ��������� �������.
            LevelSequenceController.Instance.LoadLevel(m_Level);
        }

        /// <summary>
        /// ���������� ����������� ������ � ����������� �� ����������� ���������� �����������.
        /// </summary>
        public void SetLevelData()
        {
            if (MapCompletion.Instance == null) { Debug.Log("MapComplition.Instance == null!"); return; }

            // �������� ���������� ���������� ������.
            var levelSavedResult = MapCompletion.Instance.GetLevelResult(m_Level);

            // ���� ��������� �� �������.
            if (levelSavedResult == null)
            {
                // ���� ������� ������ - �����.
                if (m_Level.LevelNumber == 1) return;

                // ���� ������� ���������� ������� - �����.
                if (MapCompletion.Instance.CheckLevelResultByLevelNumber(m_Level.LevelNumber - 1)) return;

                gameObject.SetActive(false);
            }
            // ���� ��������� �������.
            else
            {
                // �������� ��������� ���-�� ���� �� ������.
                int stars = levelSavedResult.LevelStars;

                // ������������ ����� � ����������� �� ����������� ��������.
                if (stars >= 1) m_ActiveStarsImage[0].gameObject.SetActive(true);
                if (stars >= 2) m_ActiveStarsImage[1].gameObject.SetActive(true);
                if (stars >= 3) m_ActiveStarsImage[2].gameObject.SetActive(true);
            }
        }

        #endregion

    }
}