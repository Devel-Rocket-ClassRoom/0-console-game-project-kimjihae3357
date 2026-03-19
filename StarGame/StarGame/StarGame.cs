using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Text;

public class StarGame : GameApp
{
    private readonly SceneManager<Scene> _scenes = new SceneManager<Scene>();
    public StarGame() : base(40, 20)
    {

    }

    protected override void Draw()
    {
        _scenes.CurrentScene?.Draw(Buffer);
    }

    protected override void Initialize()
    {
        ChangeToTile();
    }

    protected override void Update(float deltaTime)
    {
        if (Input.IsKeyDown(ConsoleKey.Escape))
        {
            Quit();
            return;
        }
        _scenes.CurrentScene?.Update(deltaTime);

    }

    private void ChangeToTile()
    {
        var title = new TitleScene();
        title.StartRequested += ChangeToPlay;
        _scenes.ChangeScene(title);
    }

    private void ChangeToPlay()
    {
        var play = new PlayScene();
        play.PlayAgainRequested += ChangeToTile;
        _scenes.ChangeScene(play);
    }
}
