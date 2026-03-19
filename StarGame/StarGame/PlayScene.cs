using Framework.Engine;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using static Fruits;

public class PlayScene : Scene
{
    private Wall wall;
    private Player player;

    private List<Star> stars = new List<Star>();
    private List<Fruits> fruits = new List<Fruits>();
    private static Random random = new Random();

    private float mainTimer = 0f;

    private float starSpawnTimer;
    private float starSpawnInterval = 1.0f;
    private int maxStarCount = 3;

    private float fruitSpawnTimer;
    private float fruitSpawnInterval = 1.0f;
    private int maxFruitCount = 3;

    private bool isGameOver;
    private int score = 0;
    public event GameAction PlayAgainRequested;

    public override void Draw(ScreenBuffer buffer)
    {
        DrawGameObjects(buffer);

        buffer.WriteText(0, 0, "타이머:", ConsoleColor.White);
        buffer.WriteText(8, 0, "점수:", ConsoleColor.Yellow);
        buffer.WriteText(11, 0, $"{score}", ConsoleColor.White);

        buffer.WriteText(1, 19, "과일점수:", ConsoleColor.White);
        buffer.WriteText(7, 19, "o", ConsoleColor.Cyan);
        buffer.WriteText(8, 19, "10점", ConsoleColor.White);
        buffer.WriteText(11, 19, "●", ConsoleColor.Red);
        buffer.WriteText(12, 19, "20점", ConsoleColor.White);
        buffer.WriteText(15, 19, "♣", ConsoleColor.Blue);
        buffer.WriteText(16, 19, "30점", ConsoleColor.White);

        if (isGameOver)
        {
            buffer.WriteTextCentered(6, "GMAE OVER", ConsoleColor.Red);

            buffer.WriteTextCentered(8, $"Score: {score}", ConsoleColor.White);
            buffer.WriteTextCentered(9, "Timer:", ConsoleColor.White);

            buffer.WriteTextCentered(11, "R E T R Y ?", ConsoleColor.Red);
            buffer.WriteTextCentered(12, "press: ENTER", ConsoleColor.Green);

        }
    }

    public override void Load()
    {
        isGameOver = false;
        stars.Clear();
        starSpawnTimer = 0f;

        maxStarCount = random.Next(3, 5);
        maxFruitCount = random.Next(2, 3);

        wall = new Wall(this);
        AddGameObject(wall);

        player = new Player(this);
        AddGameObject(player);

        Star star = new Star(this);
        stars.Add(star);
        AddGameObject(star);

        Fruits fruit = new Fruits(this);
        fruits.Add(fruit);
        AddGameObject(fruit);

        

        
    }

    public override void Unload()
    {
        ClearGameObjects();
        stars.Clear();
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
        UpdateGameObjects(deltaTime);

        starSpawnTimer += deltaTime;
        fruitSpawnTimer += deltaTime;


        if (stars.Count < maxStarCount && starSpawnTimer >= starSpawnInterval)
        {
            starSpawnTimer = 0f;

            Star star = new Star(this);
            stars.Add(star);
            AddGameObject(star);

            starSpawnInterval = random.Next(10, 20) * 0.1f;
            
        }

        if (fruits.Count < maxFruitCount && fruitSpawnTimer >= fruitSpawnInterval)
        {
            fruitSpawnTimer = 0f;

            Fruits fruit = new Fruits(this);
            fruits.Add(fruit);
            AddGameObject(fruit);

            fruitSpawnInterval = random.Next(10, 20) * 0.1f;

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
            else if (stars[i].StarPosition.Y == Wall.Bottom)
            {
                RemoveGameObject(stars[i]);
                stars.RemoveAt(i);
                i--;
            }
        }

        // 과일 먹으면 점수 UP
        for (int j = 0;  j < fruits.Count; j++ )
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
                }
                RemoveGameObject(fruits[j]);
                fruits.RemoveAt(j);
                j--;
            }
            // 바닥 충돌처리 (과일 사라짐)
            else if (fruits[j].FruitPosition.Y == Wall.Bottom)
            {
                RemoveGameObject(fruits[j]);
                fruits.RemoveAt(j);
                j--;
            }
        }
    }

    public void FeverTime()
    {
        
    }
}