using System;
using System.Collections.Generic;
using Tao.Sdl;

namespace MyGame
{
    class Program
    {

        
        
        static void Main(string[] args)
        {
            Initialize();

            while (true)
            {
                GameManager.Instance.Update();

                GameManager.Instance.Render();

                Sdl.SDL_Delay(20);
            }
        }

        private static void Initialize()
        {

            GameManager.Instance.Initialize();
       
        }

        public static void Update()
        {

            GameManager.Instance.Update();

        }

        public static void Render()

        {
            GameManager.Instance.Render();
        }

    }

}
