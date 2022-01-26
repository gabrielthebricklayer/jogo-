using SFML.Graphics;
using SFML.System;
using System;
namespace GameSpace
{
    public class Inimigo
    {
        private readonly Random random = new Random();
        public Sprite sprite = new Sprite();
        public Vector2f posicao = new Vector2f();
  
        public Inimigo () 
        {
            this.sprite.Texture = new Texture("Assets/imagens/meteoro.png");
            this.posicao.X = (float)this.random.Next(0, 800);
            this.posicao.Y = (float)this.random.Next(0, 50) * -1;
            this.sprite.Position = this.posicao;
        }
    }
}