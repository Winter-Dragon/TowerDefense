/// <summary>
/// Пустой делегат. Принимает null, отдаёт null.
/// </summary>
public delegate void EmptyDelegate();

/// <summary>
/// Пустой делегат, передающий значение int.
/// </summary>
/// <param name="value">Новое значение параметра int.</param>
public delegate void IntToVoidDelegate(int value);

/// <summary>
/// Пустой делегат, передающий значение bool.
/// </summary>
/// <param name="value">True если выражение верно, false - неверно.</param>
public delegate void BoolDelegate(bool value);

/// <summary>
/// Общий класс для всех делегатов в проекте.
/// </summary>
public class Delegates
{
    
}
