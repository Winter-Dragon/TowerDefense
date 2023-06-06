using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


/// <summary>
/// Базовый класс анимации.
/// </summary>
public abstract class AnimationBase : MonoBehaviour
{

    #region Properties and Components

    /// <summary>
    /// Полное время анимации.
    /// </summary>
    [SerializeField] protected float m_AnimatoinTime;

    /// <summary>
    /// Скейлинг времени анимации.
    /// </summary>
    [SerializeField] protected float m_AnimationScale;

    /// <summary>
    /// Флаг повторяющейся анимации.
    /// </summary>
    [SerializeField] private bool m_Looping;

    /// <summary>
    /// Флаг обратной анимации.
    /// </summary>
    [SerializeField] private bool m_Reverse;

    /// <summary>
    /// Событие старта анимации.
    /// </summary>
    private UnityEvent m_EventStart;

    /// <summary>
    /// Событие конца анимации.
    /// </summary>
    private UnityEvent m_EventEnd;

    /// <summary>
    /// Приватный флаг, отображающий проигрывается ли анимация.
    /// </summary>
    private bool m_IsAnimationPlaying;

    /// <summary>
    /// Приватный таймер анимации.
    /// </summary>
    private Timer m_Timer;

    #region Links

    /// <summary>
    /// Полное время анимации с учётом скейлинга.
    /// </summary>
    public float AnimationTime => m_AnimatoinTime / m_AnimationScale;

    /// <summary>
    /// Возвращает нормализованное значение анимации от 0 до 1.
    /// </summary>
    public float NormalizedAnimationTime
    {
        get
        {
            var t = Mathf.Clamp01(m_Timer.GetCurrentTime() / m_AnimatoinTime);
            return m_Reverse ? (1.0f - t) : t;
        }
    }

    /// <summary>
    /// Ссылка на эвент старта анимации.
    /// </summary>
    public UnityEvent OnEventStart => m_EventStart;

    /// <summary>
    /// Ссылка на эвент конца анимации.
    /// </summary>
    public UnityEvent OnEventEnd => m_EventEnd;

    #endregion

    #endregion


    #region UnityEvents

    private void Start()
    {
        // Инициализация таймера.
        m_Timer = new Timer(AnimationTime, m_Looping);
    }

    private void FixedUpdate()
    {
        // Если анимация проигрывается - выйти.
        if (!m_IsAnimationPlaying) return;

        // Обновить таймер.
        m_Timer.UpdateTimer();

        // Анимировать фрейм.
        AnimateFrame();

        // Если таймер завершён.
        if (m_Timer.IsFinished)
        {
            // Если анимация не повторяется - завершить анимацию.
            if (!m_Looping)
            {
                StopAnimation();
            }
        }
    }

    #endregion


    #region Public API

    /// <summary>
    /// Установить новое значение скейла анимации.
    /// </summary>
    /// <param name="scale">Значение скейла.</param>
    public void SetAnimationScale(float scale)
    {
        m_AnimationScale = scale;
    }

    /// <summary>
    /// Метод запуска анимации.
    /// </summary>
    /// <param name="prepare">true если нужна подготовка к анимации.</param>
    public void StartAnimation(bool prepare = true)
    {
        if (m_IsAnimationPlaying) return;

        if (prepare) PrepareAnimation();

        m_IsAnimationPlaying = true;

        OnAnimationStart();

        m_EventStart?.Invoke();
    }

    /// <summary>
    /// Метод приостановки анимации.
    /// </summary>
    public void StopAnimation()
    {
        if (!m_IsAnimationPlaying) return;

        m_IsAnimationPlaying = false;

        OnAnimationEnd();

        m_EventEnd?.Invoke();
    }

    #endregion

    /// <summary>
    /// Анимируем текущий фрейм анимации.
    /// </summary>
    protected abstract void AnimateFrame();
    protected abstract void OnAnimationStart();
    protected abstract void OnAnimationEnd();
    /// <summary>
    /// Подготовка начального состояния анимации.
    /// </summary>
    public abstract void PrepareAnimation();
}