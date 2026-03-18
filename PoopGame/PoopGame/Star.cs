using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Star : GameObject
{
    private (int X, int Y) _position;
    public (int X, int Y) Position => _position;

    private Random _random = new Random();
    private float _timer;

    public Star(Scene scene) : base(scene)
    {
        Name = "Star";
    }

    public void Spawn()
    {
        int x = _random.Next(Wall.Left, Wall.Right + 1);
        int y = Wall.Top;

        _position = (x, y);
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.SetCell(_position.X, _position.Y, '★', ConsoleColor.Yellow);
    }

    public override void Update(float deltaTime)
    {
        _timer += deltaTime;

        if (_timer >= -0.1f)
        {
            _timer = 0;

            _position.Y++;

            if (_position.Y > Wall.Bottom)
            {
                Spawn();
            }
        }
    }
}