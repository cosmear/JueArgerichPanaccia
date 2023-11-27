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

        /// Timeframe Calculation Properties
        private Time _time;


        public void Initialization()
        {
            Time.Initialize();

            _player = new Character(new Vector2(400, 500), 100);

            var newEnemies = Enemyfactory.CreateEnemies(EnemyType.Slow);
            GameObjectsList.AddRange(newEnemies);
            bulletPool = new BulletPool();

            // Puedes repetir con diferentes tipos de enemigos si es necesario
        }
        public void Update()
        {
            // Actualizar DeltaTime
            Time.Update();

            // Incrementar el temporizador de generación de enemigos
            enemySpawnTimer += Time.DeltaTime;

            // Verificar si es hora de generar nuevos enemigos
            if (enemySpawnTimer >= enemySpawnInterval)
            {
                var newEnemies = Enemyfactory.CreateEnemies(EnemyType.Slow); // o el tipo que prefieras
                GameObjectsList.AddRange(newEnemies);

                enemySpawnTimer = 0f; // Reiniciar el temporizador
            }

            _player.Update();
            bulletPool.Update();

            // Actualizar todos los objetos del juego
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
