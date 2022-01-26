using SFML.Graphics;
using SFML.System;
namespace GameSpace
{
    public class Bomba
    {
        public Sprite sprite = new Sprite();
        
        public Bomba (Vector2f position) 
        {
           sprite.Texture = new Texture("Assets/imagens/bomba.png");
           sprite.Position = position;
        }
    }
}