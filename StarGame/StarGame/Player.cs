using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Player : GameObject
{
    private (int X, int Y) _playerPosition;
    public (int X, int Y) PlayerPosition => _playerPosition;

    private float moveTimer;
    private float _moveInterval = 0.05f;
    public float MoveInterval {
        get => _moveInterval;
        set => _moveInterval = value;
    }

    public Player(Scene scene) : base(scene)
    {
        Name = "Player";

        _playerPosition = ((Wall.Left + Wall.Right) / 2, Wall.Bottom);
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.WriteText(_playerPosition.X, _playerPosition.Y, "A", ConsoleColor.Green);
    }

    public override void Update(float deltaTime)
    {
        moveTimer += deltaTime;
        if (moveTimer >= _moveInterval)
        {
            moveTimer = 0f;

            HandleInput();
        }
        
    }

    private void HandleInput()
    {

        if (Input.IsKey(ConsoleKey.LeftArrow))
        {
            _playerPosition.X--;
        }
        else if (Input.IsKey(ConsoleKey.RightArrow))
        {
            _playerPosition.X++;
        }

        if (_playerPosition.X == Wall.Left)
        {
            _playerPosition.X++;
        }
        else if (_playerPosition.X == Wall.Right)
        {
            _playerPosition.X--;
        }

    }
}
