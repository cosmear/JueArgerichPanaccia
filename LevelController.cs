using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class LevelController
    {
        private IntPtr image = Engine.LoadImage("assets/fondo.png");
        public List<GameObject> GameObjectsList = new List<GameObject>();
        private float enemySpawnTimer = 0f;
        private float enemySpawnInterval = 4f;
        private BulletPool bulletPool;
        private Character _player;


        public Character Player => _player;

        
        private Time _time;


        public void Initialization()
        {
            Time.Initialize();

            _player = new Character(new Vector2(400, 500), 100);

            var newEnemies = Enemyfactory.CreateEnemies(EnemyType.Slow);
            GameObjectsList.AddRange(newEnemies);
            bulletPool = new BulletPool();
            GameManager.Instance.AddScoreObserver(_player);
            
        }
        public void Update()
        {
            
            Time.Update();
            enemySpawnTimer += Time.DeltaTime;

            
            if (enemySpawnTimer >= enemySpawnInterval)
            {
                var newEnemies = Enemyfactory.CreateEnemies(EnemyType.Slow); 
                GameObjectsList.AddRange(newEnemies);

                enemySpawnTimer = 0f; 
            }

            _player.Update();
            bulletPool.Update();

            
            for (int i = 0; i < GameObjectsList.Count; i++)
            {
                GameObjectsList[i].Update();
            }
        }




        public void Render()
        {
            Engine.Clear();

            Engine.Draw(image, 0, 0);

            _player.Render();
            for (int i = 0; i < GameObjectsList.Count; i++)
            {
                GameObjectsList[i].Render();
            }

            Engine.Show();
        }
    }
}
