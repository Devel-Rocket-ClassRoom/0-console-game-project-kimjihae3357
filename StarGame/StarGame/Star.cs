using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Star : GameObject
{
    private (int X, int Y) _starPosition;
    public (int X, int Y) StarPosition => _starPosition;

    private static Random random = new Random();
    private float _timer;
    private const float FallInterval = 0.1f;

    private int _tailLength;
    private const int MaxTailLength = 3;

    public Star(Scene scene) : base(scene)
    {
        Name = "Star";
        Spawn();
    }

    public void Spawn()
    {
        _starPosition.X = random.Next(Wall.Left, Wall.Right + 1);
        _starPosition.Y = Wall.Top;
        _timer = 0;
    }

    public override void Draw(ScreenBuffer buffer)
    {
        int x = _starPosition.X;
        int y = _starPosition.Y;
        int tailLength = MaxTailLength;

        if (y > Wall.Bottom - MaxTailLength)
        {
            tailLength = Wall.Bottom - y;
            if (tailLength < 0)
                tailLength = 0;
        }

        // 꼬리 (위쪽)
        if (tailLength >= 3 && y - 3 >= Wall.Top + 5)
            buffer.SetCell(x, y - 3, '·', ConsoleColor.DarkYellow);

        if (tailLength >= 2 && y - 2 >= Wall.Top + 3)
            buffer.SetCell(x, y - 2, '·', ConsoleColor.DarkYellow);

        if (tailLength >= 1 && y - 1 >= Wall.Top + 1)
            buffer.SetCell(x, y - 1, '×', ConsoleColor.DarkYellow);
        
        // 머리 (맨 아래)
        if (y >= Wall.Top && y <= Wall.Bottom)
            buffer.SetCell(x, y, '★', ConsoleColor.Yellow);
    }

    public override void Update(float deltaTime)
    {
        _timer += deltaTime;

        if (_timer >= FallInterval)
        {
            _timer -= FallInterval;
            _starPosition.Y++;

            if (_tailLength > 0)
            {
                _tailLength--;
            }
            if (_starPosition.Y > Wall.Bottom)
            {
                Spawn();
            }
        }
    }
}