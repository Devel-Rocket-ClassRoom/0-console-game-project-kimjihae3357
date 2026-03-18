using System;
using System.Threading;

namespace Framework.Engine
{
    public abstract class GameApp
    {
        private const int k_TargetFrameTime = 33;
        private bool _isRunning;

        protected ScreenBuffer Buffer { get; private set; }

        public event GameAction GameStarted;
        public event GameAction GameStopped;

        protected GameApp(int width, int height)
        {
            Buffer = new ScreenBuffer(width, height);//화면 그리기 크기
        }

        public void Run() 
        {
            Console.CursorVisible = false;// 깜박거리는거 없애줌
            Console.Clear();

            _isRunning = true;
            Initialize();
            GameStarted?.Invoke();

            int previousTime = Environment.TickCount;

            while (_isRunning) //게임루프
            {
                int currentTime = Environment.TickCount;
                float deltaTime = (currentTime - previousTime) / 1000f;
                previousTime = currentTime;

                Input.Poll();
                Update(deltaTime);
                Buffer.Clear();
                Draw();
                Buffer.Present();

                int elapsed = Environment.TickCount - currentTime;
                int sleepTime = k_TargetFrameTime - elapsed; //프레임제한 걸기
                if (sleepTime > 0)
                {
                    Thread.Sleep(sleepTime);
                }
            }

            GameStopped?.Invoke();
            Console.CursorVisible = true;
            Console.ResetColor();
            Console.Clear();
        }

        protected void Quit()
        {
            _isRunning = false;
        }

        protected abstract void Initialize(); //게임 전체 초기화
        protected abstract void Update(float deltaTime);
        protected abstract void Draw();
    }
}
