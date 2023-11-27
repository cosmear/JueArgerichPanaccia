using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Bullet : GameObject 
    {

        public static BulletPool Instance { get; } = new BulletPool();
        private Character player = GameManager.Instance.LevelController.Player;
      
        Animation idleAnimation;

        public Bullet(float x, float y, float speed) : base(new Vector2(x, y), speed)
        {
     
        }
        public bool IsActive { get; set; }
        public void ResetBullet(Vector2 newPosition, float newSpeed)
        {
           
            IsActive = true;
        }

        protected override void CreateAnimations()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/Bullet/Idle/{i}.png");
                idleTextures.Add(frame);
            }
            idleAnimation = new Animation("Idle", idleTextures, 0.1f, true);
            currentAnimation = idleAnimation;
        }

        public override void Update()
        {
            Move();
            CheckCollisions();

        }

        private void Move()
        {
            transform.Translate(new Vector2(0, -1), speed);

            if (transform.Position.y < 0)
            {

                DestroyBullet();
            }
        }

        private void CheckCollisions()
        {
            
            for (int i = 0;i < GameManager.Instance.LevelController.GameObjectsList.Count; i++) {
                GameObject obj = GameManager.Instance.LevelController.GameObjectsList[i];
                if (obj is IDamageable objDamage) {
                    
                    float distanceX = Math.Abs((obj.Transform.Position.x + (obj.Transform.Scale.x / 2)) - (transform.Position.x + (transform.Scale.x / 2)));
                    float distanceY = Math.Abs((obj.Transform.Position.y + (obj.Transform.Scale.y / 2)) - (transform.Position.y + (transform.Scale.y / 2)));

                    float sumHalfWidth = obj.Transform.Scale.x / 2 + transform.Scale.x / 2;
                    float sumHalfH = obj.Transform.Scale.y / 2 + transform.Scale.y / 2;

                    if (distanceX < sumHalfWidth && distanceY < sumHalfH)
                    {
                        objDamage.GetDamage();
                        DestroyBullet();
                        

                    }
                }
            }
        }

        public void DestroyBullet()
        {
            GameManager.Instance.LevelController.GameObjectsList.Remove(this);
            IsActive = false; 
        }


        public override void Render()
        {
            Engine.Draw(currentAnimation.CurrentFrame, transform.Position.x, transform.Position.y);
        }

    }
}