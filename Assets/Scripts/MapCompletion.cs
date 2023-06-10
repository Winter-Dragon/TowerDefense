using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ������, �������� � ���� ����������� ���� �������.
    /// </summary>
    public class MapCompletion : Singleton<MapCompletion>
    {

        #region Properties and Components

        /// <summary>
        /// ������ �������� ����� ��� ���������� �����������.
        /// </summary>
        public const string filename = "completion.dat";

        /// <summary>
        /// ���������� ����������� ������.
        /// </summary>
        [Serializable]
        public class LevelScore
        {
            /// <summary>
            /// �������.
            /// </summary>
            public SO_Level Level;

            /// <summary>
            /// ���-�� ���� �� ������.
            /// </summary>
            public int LevelStars;
        }

        /// <summary>
        /// ������ � ������������ �������.
        /// </summary>
        private List <LevelScore> completionData = new();

        #endregion


        #region Unity Events

        private new void Awake()
        {
            // ������� ����� �� Singleton.
            base.Awake();

            // ������� ��������� ������.
            Saver<List<LevelScore>>.TryLoad(filename, ref completionData);
        }

        #endregion


        #region Private API

        /// <summary>
        /// ���������/��������� ���������� ������ � ������ completionData.
        /// </summary>
        /// <param name="level">������� ��� ����������.</param>
        /// <param name="levelStars">���-�� ���� �� ������.</param>
        private void SaveResult(SO_Level level, int levelStars)
        {
            // ����������, ���� �� ���������� �����������.
            bool saveResult = false;

            // ������������ ��� ���������� ������.
            foreach (LevelScore item in completionData)
            {
                // ���� ������� ��������� � ������� �������.
                if (item.Level == level)
                {
                    // ���� ���-�� ���� �� ������ ������ - �������������� ��������.
                    if (levelStars > item.LevelStars) item.LevelStars = levelStars;

                    // ���������� ���������.
                    saveResult = true;
                }
            }

            // ���� ���������� �� ���������.
            if (!saveResult)
            {
                // �������� ����� ����������.
                LevelScore newLevelScore = new()
                {
                    // ������������ �������� ������.
                    Level = LevelSequenceController.Instance.CurrentLevel,
                    LevelStars = levelStars
                };

                // ������� ����������� � ������.
                completionData.Add(newLevelScore);
            }

            // ���������� ������.
            Saver<List<LevelScore>>.Save(filename, completionData);
        }

        #endregion

        #region Public API

        /// <summary>
        /// ��������� ���������� ����������� ������.
        /// </summary>
        /// <param name="isCompleted">������� �� �������.</param>
        /// <param name="levelStars">���-�� ���� �� ������.</param>
        public static void SaveLevelResult(bool isCompleted, int levelStars)
        {
            // ���� ������� �� ������� - �� ���������.
            if (!isCompleted || levelStars == 0) return;

            // ��������� ���������� ������.
            if (LevelSequenceController.Instance != null)
            {
                if (LevelSequenceController.Instance.CurrentLevel != null) Instance.SaveResult(LevelSequenceController.Instance.CurrentLevel, levelStars);
                else Debug.Log("Level in LevelSequenseController is null!");
            }
            else Debug.Log("LevelSequenseController is null!");
        }

        /// <summary>
        /// �������� ���������� ���������� ������.
        /// </summary>
        /// <param name="level">�������.</param>
        /// <returns>���������� ���������� ����������� ������.</returns>
        public LevelScore GetLevelResult(SO_Level level)
        {
            foreach (LevelScore item in completionData)
            {
                // ���� ������� ��������� � ������� ������� - ���������� �������.
                if (item.Level == level) return item;
            }

            // ���� ������� �� ������� - ���������� null.
            return null;
        }

        /// <summary>
        /// ���������, ������� �� ������� � ��������� �������.
        /// </summary>
        /// <param name="levelNumber">����� ������.</param>
        /// <returns>true - ������, false - �� �������.</returns>
        public bool CheckLevelResultByLevelNumber(int levelNumber)
        {
            foreach (LevelScore item in completionData)
            {
                // ���� ������� ��������� � ������� ������� - ���������� ���.
                if (item.Level.LevelNumber == levelNumber) return true;
            }

            // ���� ������� �� ��� ������� - ���������� false.
            return false;
        }

        /// <summary>
        /// ������� ���������� ������.
        /// </summary>
        public static void ResetSavedData()
        {
            // �������� ������� ������.
            Instance.completionData.Clear();

            // ������� ���������� ����.
            Saver<List<LevelScore>>.Reset(filename);
        }

        #endregion

    }
}