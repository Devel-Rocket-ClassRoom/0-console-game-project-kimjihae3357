using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class TitleScene : Scene
{
    public event GameAction StartRequested;

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.WriteTextCentered(6, "★ S T A R ★", ConsoleColor.Yellow);
        buffer.WriteTextCentered(7, "G A M E", ConsoleColor.Yellow);
        buffer.WriteTextCentered(10, "Arrow Keys(← →): Move");
        buffer.WriteTextCentered(11, "ESC: Quit");
        buffer.WriteTextCentered(15, "Press ENTER to Start", ConsoleColor.Green);
    }

    public override void Load()
    {
        
    }

    public override void Unload()
    {
        
    }

    public override void Update(float deltaTime)
    {
        if (Input.IsKeyDown(ConsoleKey.Enter))
        {
            StartRequested?.Invoke();
        }
    }
}
