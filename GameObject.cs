using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public abstract class GameObject
    {
        protected Transform transform;
        protected Renderer renderer;
        public Transform Transform => transform;

        protected float speed;
        protected Animation currentAnimation;

        public GameObject(Vector2 pos, float speed) 
        {
            transform = new Transform(pos, new Vector2(100, 100));

            this.speed = speed;
            CreateAnimations();

            renderer = new Renderer(currentAnimation);
        }

        protected virtual void CreateAnimations()
        {
            
        }

        public virtual void Update()
        {

        }

        public virtual void Render()
        {

        }


    }
}
