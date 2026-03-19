using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class Player : GameObject
{
    //private const float k_MoveInterval = 0.15;
    private (int X, int Y) _playerPosition;
    public (int X, int Y) PlayerPosition => _playerPosition;

    private float moveTimer;
    private float moveSpeed = 0.05f;

    public Player(Scene scene) : base(scene)
    {
        Name = "Player";

        _playerPosition = ((Wall.Left + Wall.Right) / 2, Wall.Bottom);
    }

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.SetCell(_playerPosition.X, _playerPosition.Y, 'A', ConsoleColor.Green);
    }

    public override void Update(float deltaTime)
    {
        moveTimer += deltaTime;
        if (moveTimer >= moveSpeed)
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

    }
}
