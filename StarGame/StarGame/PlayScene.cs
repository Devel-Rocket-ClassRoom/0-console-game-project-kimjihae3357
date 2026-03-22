using Framework.Engine;
using static Fruits;

public class PlayScene : Scene
{
    private Wall wall;
    private Player player;

    private List<Star> stars = new List<Star>();
    private List<Fruits> fruits = new List<Fruits>();
    private static Random random = new Random();

    private float mainTimer;
    private float feverTimer;

    private float starSpawnTimer;
    private float starSpawnInterval = 1.0f;
    private int maxStarCount = 3;

    private float fruitSpawnTimer;
    private float fruitSpawnInterval = 0.5f;
    private int maxFruitCount = 5;

    private int colorIndex;
    private ConsoleColor[] colors =
    {
        ConsoleColor.White,
        ConsoleColor.Red,
        ConsoleColor.Yellow,
        ConsoleColor.White,
        ConsoleColor.Green,
        ConsoleColor.Cyan,
        ConsoleColor.Blue,
        ConsoleColor.Magenta
    };

    private bool isGameOver;
    private bool isFever;
    private int score = 0;
    public event GameAction PlayAgainRequested;

    public override void Draw(ScreenBuffer buffer)
    {
        DrawGameObjects(buffer);

        buffer.WriteText(0, 0, "타이머:", ConsoleColor.Yellow);
        buffer.WriteText(7, 0, $"{mainTimer:F1}초", ConsoleColor.White);
        buffer.WriteText(15, 0, "점수:", ConsoleColor.Yellow);
        buffer.WriteText(20, 0, $"{score}", ConsoleColor.White);

        buffer.WriteText(1, 19, "과일점수:", ConsoleColor.White);
        buffer.WriteText(10, 19, "o", ConsoleColor.Cyan);
        buffer.WriteText(11, 19, "10점", ConsoleColor.White);
        buffer.WriteText(14, 19, "●", ConsoleColor.Red);
        buffer.WriteText(15, 19, "20점", ConsoleColor.White);
        buffer.WriteText(18, 19, "♣", ConsoleColor.Blue);
        buffer.WriteText(19, 19, "30점", ConsoleColor.White);

        if (isGameOver)
        {
            buffer.WriteTextCentered(6, "게임 오버", ConsoleColor.Red);

            buffer.WriteTextCentered(8, $"점수: {score}", ConsoleColor.White);
            buffer.WriteTextCentered(9, $"타이머: {mainTimer:F1}", ConsoleColor.White);

            buffer.WriteTextCentered(11, "다시 할래?", ConsoleColor.Red);
            buffer.WriteTextCentered(12, "ENTER를 누르세요", ConsoleColor.Green);

        }

        if (isFever)
        {
            colorIndex = (colorIndex + 1) % colors.Length;
            buffer.WriteTextCentered(3, "☆ F E V E R  T I M E ☆", colors[colorIndex]);
        }
    }

    public override void Load()
    {
        isGameOver = false;
        starSpawnTimer = 0f;
        mainTimer = 0;


        maxStarCount = random.Next(3, 5);
        maxFruitCount = random.Next(2, 5);

        wall = new Wall(this);
        AddGameObject(wall);

        player = new Player(this);
        AddGameObject(player);
    }

    public override void Unload()
    {
        ClearGameObjects();
        //stars.Clear();
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
        mainTimer += deltaTime;
        UpdateGameObjects(deltaTime);

        // 일반
        if (!isFever)
        {
            starSpawnTimer += deltaTime;
            fruitSpawnTimer += deltaTime;

            if (stars.Count < maxStarCount && starSpawnTimer >= starSpawnInterval)
            {
                starSpawnTimer = 0f;

                Star star = new Star(this);
                stars.Add(star);
                AddGameObject(star);

                starSpawnInterval = random.Next(0, 15) * 0.1f;

            }

            if (fruits.Count < maxFruitCount && fruitSpawnTimer >= fruitSpawnInterval)
            {
                fruitSpawnTimer = 0f;

                Fruits fruit = new Fruits(this, isFever);
                fruits.Add(fruit);
                AddGameObject(fruit);
            }
        }
        // 피버 타임 일때
        if (isFever)
        {
            
            for (int i = stars.Count - 1; i >= 0; i--)
            {
                RemoveGameObject(stars[i]);
                stars.RemoveAt(i);
            }

            fruitSpawnTimer += deltaTime;
            feverTimer += deltaTime;

            if (fruits.Count < maxFruitCount && fruitSpawnTimer >= fruitSpawnInterval)
            {
                fruitSpawnTimer = 0f;

                Fruits fruit = new Fruits(this, isFever);
                fruits.Add(fruit);
                AddGameObject(fruit);
            }

            if (feverTimer >= 5.0f)
            {
                player.MoveInterval = 0.05f;
                maxFruitCount = 5;
                fruitSpawnInterval = 0.5f;
                feverTimer = 0f;
                fruitSpawnTimer = 0f;
                isFever = false;
            }
        }

        // 별 맞으면 게임오버
        for (int i = 0; i < stars.Count; i++)
        {

            if (stars[i].StarPosition.X == player.PlayerPosition.X &&
                stars[i].StarPosition.Y == player.PlayerPosition.Y)
            {
                isGameOver = true;
                return;
            }
            // 바닥 충돌처리 (별 사라짐)
            if (stars[i].StarPosition.Y > Wall.Bottom)
            {
                RemoveGameObject(stars[i]);
                stars.RemoveAt(i);
                i--;
            }
        }

        // 과일 먹으면 점수 UP
        for (int j = 0; j < fruits.Count; j++)
        {
            if (fruits[j].FruitPosition.X == player.PlayerPosition.X &&
                fruits[j].FruitPosition.Y == player.PlayerPosition.Y)
            {
                switch (fruits[j].Type)
                {
                    case FruitType.fruit1:
                        score += 10;
                        break;

                    case FruitType.fruit2:
                        score += 20;
                        break;

                    case FruitType.fruit3:
                        score += 30;
                        break;

                    case FruitType.feverFruit:
                        FeverTime();
                        isFever = true;
                        break;
                }
                RemoveGameObject(fruits[j]);
                fruits.RemoveAt(j);
                j--;
                continue;
            }
            // 바닥 충돌처리 (과일 사라짐)
            if (fruits[j].FruitPosition.Y > Wall.Bottom)
            {
                RemoveGameObject(fruits[j]);
                fruits.RemoveAt(j);
                j--;
            }
        }


    }

    public void FeverTime()
    {

        player.MoveInterval = 0.01f;
        feverTimer = 0;
        starSpawnTimer = 0f;

        maxFruitCount = 30;
        fruitSpawnInterval = 0.05f;

    }
}