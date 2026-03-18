using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class PlayScene : Scene
{
    private Wall wall;
    private Star star;

    private bool isGameOver;
    public event GameAction PlayAgainRequested;

    public override void Draw(ScreenBuffer buffer)
    {
        
    }

    public override void Load()
    {
        wall = new Wall(this);
        AddGameObject(wall);

        star = new Star(this);
        star.Spawn();
        AddGameObject(star);
    }

    public override void Unload()
    {
        ClearGameObjects();
    }

    public override void Update(float deltaTime)
    {
        if (isGameOver)
        {
            if (Input.IsKeyDown(ConsoleKey.Enter))
            {
                PlayAgainRequested?.Invoke();
            }
            return;
        }
    }
}