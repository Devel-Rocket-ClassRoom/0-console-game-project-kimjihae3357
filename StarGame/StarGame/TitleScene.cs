using Framework.Engine;

public class TitleScene : Scene
{
    public event GameAction StartRequested;

    private int colorIndex;
    private float blinkTimer;
    private ConsoleColor[] colors =
    {
        ConsoleColor.Yellow,
        ConsoleColor.White,
        ConsoleColor.Cyan
    };

    public override void Draw(ScreenBuffer buffer)
    {
        buffer.WriteTextCentered(6, "★ 별 똥 별 ★", colors[colorIndex]);
        buffer.WriteTextCentered(7, "피 하 기", ConsoleColor.Yellow);
        buffer.WriteTextCentered(10, "좌우 화살표(← →): 움직이기");
        buffer.WriteTextCentered(11, "ESC: 게임종료");
        buffer.WriteTextCentered(15, "ENTER 누르면 시작", ConsoleColor.Green);
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

        blinkTimer += deltaTime;

        if (blinkTimer >= 0.2f)
        {
            blinkTimer = 0f;
            colorIndex = (colorIndex + 1) % colors.Length;
        }
    }


}
