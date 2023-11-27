using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyGame
{
    public class Character : GameObject
    {
        
        private Animation idleAnimation;

        private float timeBetweenShoots = 1f;
        private DateTime timeLastShoot;
       

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
            if (Engine.KeyPress(Engine.KEY_LEFT))
            {
                transform.Translate(new Vector2(-1, 0), speed* 3);
            }
            if (Engine.KeyPress(Engine.KEY_RIGHT))
            {
                transform.Translate(new Vector2(1, 0), speed*3);
            }
            if (Engine.KeyPress(Engine.KEY_UP))
            {
                transform.Translate(new Vector2(0, -1), speed);
            }
            if (Engine.KeyPress(Engine.KEY_DOWN))
            {
                transform.Translate(new Vector2(0, 1), speed);
            }
            if (Engine.KeyPress(Engine.KEY_ESP))
            {
                Shoot();
            }
            if (Engine.KeyPress(Engine.KEY_ESC)) { }
            currentAnimation.Update();

        }

        private void Shoot()
        {
            DateTime currentTime = DateTime.Now;
            if ((currentTime - timeLastShoot).TotalSeconds >= timeBetweenShoots)
            {
                Bullet newBullet = new Bullet(0,0,200);
                if (newBullet != null) { 
                    newBullet.Transform.SetNewPosition(transform.PositionCenter());
                    GameManager.Instance.LevelController.GameObjectsList.Add(newBullet);
                    timeLastShoot = currentTime;
                }
            }
                
        }


        public override void Render()
        {
            renderer.Render(transform);
        }

        public void dead()
        {
            GameManager.Instance.ChangeGameStatus(GameStatus.defeat);
        }

    }
}
