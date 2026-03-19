using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Timers;

public class Fruits : GameObject
{
    private (int X, int Y) _fruitPosition;
    public (int X, int Y) FruitPosition => _fruitPosition;
    private static Random random = new Random();
    private float _timer;
    private const float FallInterval = 0.05f;

    public FruitType Type {  get; private set; }

    public enum FruitType
    {
        fruit1,
        fruit2,
        fruit3
    }


    public Fruits(Scene scene) : base(scene)
    {
        Name = "Fruits";
        Spawn();
    }

    public void Spawn()
    {
        _fruitPosition.X = random.Next(Wall.Left, Wall.Right + 1);
        _fruitPosition.Y = Wall.Top;
        _timer = 0;

        Type = (FruitType)random.Next(0, 3);
    }

    public override void Draw(ScreenBuffer buffer)
    {
        switch (Type)
        {
            case FruitType.fruit1:
                buffer.SetCell(_fruitPosition.X, _fruitPosition.Y, 'o', ConsoleColor.Cyan);
                break;

            case FruitType.fruit2:
                buffer.SetCell(_fruitPosition.X, _fruitPosition.Y, '●', ConsoleColor.Red);
                break;

            case FruitType.fruit3:
                buffer.SetCell(_fruitPosition.X, _fruitPosition.Y, '♣', ConsoleColor.Blue);
                break;
        }
 
    }

    public override void Update(float deltaTime)
    {
        _timer += deltaTime;

        if (_timer >= FallInterval)
        {
            _timer -= FallInterval;
            _fruitPosition.Y++;

            if (_fruitPosition.Y > Wall.Bottom)
            {
                Spawn();
            }
        }
    }
}
