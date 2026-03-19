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
        buffer.SetCell(_starPosition.X, _starPosition.Y, '★', ConsoleColor.Yellow);
    }

    public override void Update(float deltaTime)
    {
        _timer += deltaTime;

        if (_timer >= FallInterval)
        {
            _timer -= FallInterval;
            _starPosition.Y++;

            if (_starPosition.Y > Wall.Bottom)
            {
                Spawn();
            }
        }
    }
}