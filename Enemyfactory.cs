using System.Collections.Generic;
using System;
using MyGame;

namespace MyGame
{


    public enum EnemyType
    {
        Slow,
        Fast
    }


    public static class Enemyfactory
    {
        private static readonly int screenWidth = 1024;
        private static readonly int laneWidth = screenWidth / 5;
        private static int waveCount = 0; // Añadido para contar las oleadas
        private static float fastEnemySpeed = 125;
        static Enemyfactory()
        {
            GameManager.Instance.OnScoreChanged += HandleScoreChange;
        }
        private static void HandleScoreChange(int currentScore)
        {
            if (currentScore >= 200)
            {
                IncreaseFastEnemySpeed();
            }
        }
        private static void IncreaseFastEnemySpeed()
        {
            fastEnemySpeed =140; // Ajusta este factor según sea necesario
                                    // Asegúrate de no aumentar la velocidad más de una vez
        }
        public static List<Enemy> CreateEnemies(EnemyType type)
        {
           
            var enemies = new List<Enemy>();
            var lanes = new List<int> { 0, 1, 2, 3, 4 };
            Shuffle(lanes);
            lanes.RemoveAt(4); // Eliminar un carril para dejarlo libre

            foreach (var lane in lanes)
            {
                float posX = lane * laneWidth + laneWidth / 2;
                Vector2 position = new Vector2(posX, 0);

                if (waveCount % 3 == 2)
                {
                    FastEnemy fastEnemy = new FastEnemy(position, fastEnemySpeed);
                    
                    enemies.Add(fastEnemy);
                }
                else
                {
                    enemies.Add(new Enemy(position, (type == EnemyType.Slow ? 100 : 200)));
                    
                }
            }

                waveCount++;
            return enemies;
        }



        private static void Shuffle<T>(IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}