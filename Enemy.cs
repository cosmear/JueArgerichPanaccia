using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Enemy : GameObject, IDamageable
    {
        
        private Character player = GameManager.Instance.LevelController.Player;

        Animation idleAnimation;


        public Enemy(Vector2 position, float speed) : base(position, speed)
        {
            
        }

        protected override void CreateAnimations()
        {
            List<IntPtr> idleTextures = new List<IntPtr>();
            for (int i = 0; i < 4; i++)
            {
                IntPtr frame = Engine.LoadImage($"assets/Enemy/Idle/{i}.png");
                idleTextures.Add(frame);
            }
            idleAnimation = new Animation("Idle", idleTextures, 0.1f, true);
            currentAnimation = idleAnimation;
        }

        public override void Update()
        {
            MoveEnemy();
            CheckCollisions();

        }

        protected virtual void MoveEnemy()
        {
            transform.Translate(new Vector2(0, 1), speed);

            if (transform.Position.x > 1000)
            {
                transform.SetNewPosition(new Vector2(0 - transform.Scale.x, transform.Position.y));
            }
            if (HasTouchedBottomBorder())
            {
                GameManager.Instance.EnemyTouchedBorder();
                DestroyEnemy();
            }
        
    }
        private bool HasTouchedBottomBorder()
        {
            float screenHeight = 768; // Asegúrate de que este método obtiene la altura de la pantalla correctamente
            return transform.Position.y + transform.Scale.y >= screenHeight;
        }

        private void CheckCollisions()
        {
            // Obtener las distancias centradas
            float distanceX = Math.Abs((player.Transform.Position.x + (player.Transform.Scale.x / 2)) - (transform.Position.x + (transform.Scale.x / 2)));
            float distanceY = Math.Abs((player.Transform.Position.y + (player.Transform.Scale.y / 2)) - (transform.Position.y + (transform.Scale.y / 2)));

            // Calcular las sumas de las mitades de las anchuras y alturas, pero reducidas
            float collisionReductionFactor = 0.5f; // Ajusta este valor para cambiar el tamaño de la colisión
            float sumHalfWidth = (player.Transform.Scale.x / 2 + transform.Scale.x / 2) * collisionReductionFactor;
            float sumHalfHeight = (player.Transform.Scale.y / 2 + transform.Scale.y / 2) * collisionReductionFactor;

            // Verificar colisión
            if (distanceX < sumHalfWidth && distanceY < sumHalfHeight)
            {
                player.dead();
            }
        }

        private void DestroyEnemy()
        {
            
            GameManager.Instance.LevelController.GameObjectsList.Remove(this);
            
        }


        public override void Render()
        {
            renderer.Render(transform);
        }

        public void GetDamage()
        {
            GameManager.Instance.LevelController.GameObjectsList.Remove(this);
            
        }


    }
}