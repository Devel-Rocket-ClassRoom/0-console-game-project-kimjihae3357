using Framework.Engine;

public class Fruits : GameObject
{
    private (int X, int Y) _fruitPosition;
    public (int X, int Y) FruitPosition => _fruitPosition;
    private static Random random = new Random();
    private float _timer;
    private const float FallInterval = 0.08f;

    public FruitType Type { get; private set; }

    public enum FruitType
    {
        fruit1,
        fruit2,
        fruit3,
        feverFruit
    }

    private int colorIndex;
    private ConsoleColor[] colors =
    {
        ConsoleColor.Magenta,
        ConsoleColor.White,
    };


    public Fruits(Scene scene, bool isFever) : base(scene)
    {
        Name = "Fruits";
        Spawn(isFever);
    }

    public void Spawn(bool isFever)
    {
        _fruitPosition.X = random.Next(Wall.Left, Wall.Right + 1);
        _fruitPosition.Y = Wall.Top;
        _timer = 0;

        if (isFever)
        {
            Type = (FruitType)random.Next(0, 3);
        }
        else
        {
            int rand = random.Next(0, 10);
            if (rand == 0)
            {
                Type = FruitType.feverFruit;
            }
            else
            {
                Type = (FruitType)random.Next(0, 3);
            }

        }
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

            case FruitType.feverFruit:
                colorIndex = (colorIndex + 1) % colors.Length;
                buffer.SetCell(_fruitPosition.X, _fruitPosition.Y, '☆', colors[colorIndex]);
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
        }
    }

}
