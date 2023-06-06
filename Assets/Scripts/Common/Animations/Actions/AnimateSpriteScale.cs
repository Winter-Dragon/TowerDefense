using UnityEngine;

/// <summary>
/// ����� ���������, ���������� ����� �������.
/// </summary>
public class AnimateSpriteScale : AnimationBase
{

    #region Properties and Components

    /// <summary>
    /// ������ �� Sprite Renderer �������.
    /// </summary>
    [SerializeField] private SpriteRenderer m_Renderer;

    /// <summary>
    /// ������������ ������ ������� �� ��� X.
    /// </summary>
    [SerializeField] private AnimationCurve m_CurveX;

    /// <summary>
    /// ������������ ������ ������� �� ��� Y.
    /// </summary>
    [SerializeField] private AnimationCurve m_CurveY;

    /// <summary>
    /// ������ � ������� �������� �������.
    /// </summary>
    private Vector2 m_InitialSize;

    #endregion


    #region Unity Events

    private void Start()
    {
        // ������� ������ = ��������� ������.
        m_InitialSize = m_Renderer.size;
    }

    #endregion


    #region Protected API

    /// <summary>
    /// ��������������� ������ ���������� � ��������.
    /// </summary>
    public override void PrepareAnimation()
    {
        // X � Y ���������� � ����������� �� 1-� ����� ������ �� �����.
        var x = m_CurveX.Evaluate(0) * m_InitialSize.x;
        var y = m_CurveY.Evaluate(0) * m_InitialSize.y;

        // ������� ����� ������ �� ��������� X � Y.
        m_Renderer.size = new Vector2(x, y);
    }

    /// <summary>
    /// ���������������� ����� ��������� ������� ��������.
    /// </summary>
    protected override void AnimateFrame()
    {
        // X � Y ���������� � ����������� ���������������� �������� ��������.
        var x = m_CurveX.Evaluate(NormalizedAnimationTime) * m_InitialSize.x;
        var y = m_CurveY.Evaluate(NormalizedAnimationTime) * m_InitialSize.y;

        // ������� ����� �����.
        m_Renderer.size = new Vector2(x, y);
    }

    /// <summary>
    /// ���������������� ����� ��� ���������� ��������.
    /// </summary>
    protected override void OnAnimationEnd() { }
    /// <summary>
    /// ���������������� ����� ��� ������ ��������.
    /// </summary>
    protected override void OnAnimationStart() { }

    #endregion

}