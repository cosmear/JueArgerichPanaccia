using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyGame
{
    public class Character : GameObject, IDestructible, IScoreObserver
    {
        
        private Animation idleAnimation;

        private float timeBetweenShoots = 10f;
        private DateTime timeLastShoot;
        private float PointToIncreasedSpeed = 100;
        private int score = 0;
        private bool speedIncreased = false;
        private const int ScreenWidth = 1024;
        private const int ScreenHeight = 1024;


        public Character(Vector2 pos, float speed) :base(pos, speed)
        {
            
        }

        protected override void CreateAnimations()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/Ship/Idle/{i}.png");
                idleTextures.Add(frame);
            }
            idleAnimation = new Animation("Idle", idleTextures, 0.1f, true);
            currentAnimation = idleAnimation;
        }


        public override void Update()
        {

            Vector2 newPosition = transform.Position;

            if (Engine.KeyPress(Engine.KEY_LEFT))
            {
                newPosition.x = Math.Max(0, newPosition.x - (speed + 150) * Time.DeltaTime);
            }
            if (Engine.KeyPress(Engine.KEY_RIGHT))
            {
                newPosition.x = Math.Min(ScreenWidth, newPosition.x + (speed + 150) * Time.DeltaTime);
            }
            if (Engine.KeyPress(Engine.KEY_UP))
            {
                newPosition.y = Math.Max(0, newPosition.y - speed * Time.DeltaTime);
            }
            if (Engine.KeyPress(Engine.KEY_DOWN))
            {
                newPosition.y = Math.Min(ScreenHeight, newPosition.y + speed * Time.DeltaTime);
            }

            transform.SetNewPosition(newPosition);
            if (Engine.KeyPress(Engine.KEY_ESP))
                {
                    Shoot();
                }

           
        }

        private void Shoot()
        {
            DateTime currentTime = DateTime.Now;
            if ((currentTime - timeLastShoot).TotalSeconds >= timeBetweenShoots)
            {
                Bullet newBullet = new Bullet(0, 0, 200);
                if (newBullet != null)
                {
                    newBullet.Transform.SetNewPosition(transform.PositionCenter());
                    GameManager.Instance.LevelController.GameObjectsList.Add(newBullet);
                    timeLastShoot = currentTime;
                }
            }
        }

        public void OnScoreChange(int newScore)
        {
            score = newScore;

            if (score >= PointToIncreasedSpeed && !speedIncreased)
            {
                IncreaseSpeed();
                speedIncreased = true;
            }
        }
        private void IncreaseSpeed()
        {
            
            speed *= 1.5f;
            Console.WriteLine($"Valocidad Aumentada");
        }

        public override void Render()
        {
           
            renderer.Render(transform);
        }

        public void Dead()
        {
            GameManager.Instance.ChangeGameStatus(GameStatus.defeat);
            
        }

    }
}
