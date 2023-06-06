using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    /// <summary>
    /// ������� ����� ���� ������������� ������� �������� �� �����.
    /// </summary>
    public abstract class Entity : MonoBehaviour
    {

        #region Properties and Components

        /// <summary>
        /// �������� ������� ��� ������������.
        /// </summary>
        [SerializeField] private string m_Nickname;

        #region Links

        /// <summary>
        /// ������ �� ��� �������.
        /// </summary>
        public string Nickname => m_Nickname;

        #endregion

        #endregion

    }
}