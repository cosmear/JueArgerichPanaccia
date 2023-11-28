using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public enum GameStatus
    {
        mainMenu, level, victory, defeat
    }
    public class GameManager
    {
        
        private static GameManager instance;
        private LevelController levelController;
        public LevelController LevelController => levelController;
        private GameStatus gameStatus = GameStatus.mainMenu;
        private IntPtr mainMenuScreen = Engine.LoadImage("assets/MainMenu.png");
        private IntPtr winScreen = Engine.LoadImage("assets/Win.png");
        private IntPtr gameOverScreen = Engine.LoadImage("assets/GameOver.png");
        private int score;
        public event Action<int> OnEnemyTouchedBorder;
        public event Action<int> OnScoreChanged;
        private List<IScoreObserver> scoreObservers = new List<IScoreObserver>();
       // public static Font scoreFont;


        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }

                return instance;
            }
        }

        public class Font
        {
            // Font class implementation
        }
        public void Initialize()
        {
            Engine.Initialize();
            levelController = new LevelController();
            levelController.Initialization();
            OnEnemyTouchedBorder += AddScore;
           
           // scoreFont = new Font("assets/arial.ttf", 20);
        }
        public void Update()
        {
            switch (gameStatus)
            {
                case GameStatus.mainMenu:
                    if (Engine.KeyPress(Engine.KEY_ESP))
                    {
                        ChangeGameStatus(GameStatus.level);
                    }
                    break;
                case GameStatus.level:
                    levelController.Update();
                    break;
                case GameStatus.victory:
                    
                    break;
                case GameStatus.defeat:
                    
                    break;
            }

        }

        public void ChangeGameStatus(GameStatus gs)
        {
            gameStatus = gs;
        }
        public void AddScoreObserver(IScoreObserver observer)
        {
            scoreObservers.Add(observer);
        }
        private void AddScore(int points)
        {
            score += points;
            Console.WriteLine($"Puntos actuales: {score}");

            foreach (var observer in scoreObservers)
            {
                observer.OnScoreChange(score);
            }
            if (score >= 1000)
            {
                ChangeGameStatus(GameStatus.victory);
            }
        }

        public void EnemyTouchedBorder()
        {
            OnEnemyTouchedBorder?.Invoke(10); 
        }
        public void Render()
        {
            Engine.Clear();
            switch (gameStatus)
            {
                case GameStatus.mainMenu:
                    Engine.Draw(mainMenuScreen, 0, 0);
                    break;
                case GameStatus.level:
                    levelController.Render();
                    break;
                case GameStatus.victory:
                    Engine.Draw(winScreen, 0, 0);
                    break;
                case GameStatus.defeat:
                    Engine.Draw(gameOverScreen, 0, 0);
                    break;
            }
            Engine.Show();
        }
        //private void DrawScore()
        //{
            
        //    Engine.DrawText($"Puntaje: {score}", 280, 20, 8, 4, 4, GameManager.scoreFont);
        //}
    }
    
}

