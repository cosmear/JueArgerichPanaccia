using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class Renderer
    {
        private Animation _anim;

        public Renderer(Animation anim)
        {

            _anim = anim;
        }

        public void Render(Transform transform)
        {
            Engine.Draw(_anim.CurrentFrame, transform.Position.x, transform.Position.y);
        }
    }
}
