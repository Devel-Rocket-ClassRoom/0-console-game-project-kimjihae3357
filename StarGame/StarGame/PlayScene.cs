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
    private Star star;

    private List<Star> stars = new List<Star>();
    private List<Fruits> fruits = new List<Fruits>();
    private static Random random = new Random();

    private float mainTimer = 0f;
    private float starSpawnTimer;
    private float starSpawnInterval = 1.0f;
    private int maxStarCount = 3;

    private bool isGameOver;
    public event GameAction PlayAgainRequested;

    public override void Draw(ScreenBuffer buffer)
    {
        DrawGameObjects(buffer);

        buffer.WriteText(1, 19, "과일:", ConsoleColor.White);
        buffer.WriteText(5, 19, "o", ConsoleColor.Cyan);
        buffer.WriteText(6, 19, "10점", ConsoleColor.White);
        buffer.WriteText(9, 19, "●", ConsoleColor.Red);
        buffer.WriteText(10, 19, "20점", ConsoleColor.White);
        buffer.WriteText(13, 19, "♣", ConsoleColor.Blue);
        buffer.WriteText(14, 19, "30점", ConsoleColor.White);

        if (isGameOver)
        {
            buffer.WriteTextCentered(8, "게임 오버", ConsoleColor.Red);
            
        }
    }

    public override void Load()
    {
        isGameOver = false;
        stars.Clear();
        starSpawnTimer = 0f;

        maxStarCount = random.Next(3, 5);

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


        if (stars.Count < maxStarCount && starSpawnTimer >= starSpawnInterval)
        {
            starSpawnTimer = 0f;

            Star star = new Star(this);
            stars.Add(star);
            AddGameObject(star);

            starSpawnInterval = random.Next(10, 20) * 0.1f;
            
        }
        /*
        if (StarPosition == player.PlayerPosition)
        {
            isGameOver = true;
            return;
        }
        */
    }
    /*
    private void CheckCollision()
    {
        for (int i = 0;  i < fruits.Count; i++)
        {
            if (fruits[i].FruitPosition.X == player.PlayerPosition.X &&
                fruits[i].FruitPosition.Y == player.PlayerPosition.Y)
            {
                var fruit = fruits[i];

                switch (fruit.Type)
                {
                    case 
                }
            }
        }
    }*/

    public void FeverTime()
    {
        
    }
}