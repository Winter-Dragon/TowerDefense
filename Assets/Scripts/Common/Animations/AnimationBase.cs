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
    public event EmptyDelegate OnEventStart;

    /// <summary>
    /// Событие конца анимации.
    /// </summary>
    public event EmptyDelegate OnEventEnd;

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
        // Если анимация уже проигрывается - выйти.
        if (m_IsAnimationPlaying) return;

        // Если есть подготовка к анимации - запустить.
        if (prepare) PrepareAnimation();

        // Флаг проигрывания анимации true.
        m_IsAnimationPlaying = true;

        // Метод старта анимации.
        OnAnimationStart();

        // Вызов события старта анимации.
        OnEventStart?.Invoke();
    }

    /// <summary>
    /// Метод приостановки анимации.
    /// </summary>
    public void StopAnimation()
    {
        // Если анимация приостановлена - выйти.
        if (!m_IsAnimationPlaying) return;

        // Флаг проигрывания анимации false.
        m_IsAnimationPlaying = false;

        // Метод завершения анимации.
        OnAnimationEnd();

        // Вызов события конца анимации.
        OnEventEnd?.Invoke();
    }

    #endregion


    #region Abstract API

    /// <summary>
    /// Анимируем текущий фрейм анимации.
    /// </summary>
    protected abstract void AnimateFrame();
    /// <summary>
    /// Действия при старте анимации.
    /// </summary>
    protected abstract void OnAnimationStart();
    /// <summary>
    /// Действия при завершении анимации.
    /// </summary>
    protected abstract void OnAnimationEnd();
    /// <summary>
    /// Подготовка начального состояния анимации.
    /// </summary>
    public abstract void PrepareAnimation();

    #endregion

}