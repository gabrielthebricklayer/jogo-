using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Collections.Generic;
using System;
namespace GameSpace
{
    public class Game
    {
       private readonly Random random = new Random();
       private int qtdinimigos;
       private int score = 0;
       private const float velocidadenave = 1f;
       private const float velocidadebomba = 0.3f;
       private const float velocidadeinimigo = 0.3f;
       private VideoMode mode; 
       private RenderWindow window; // Tela gráfica
       private Sprite cenario = new Sprite(); 
       private Sprite gameover = new Sprite();
       private Nave nave = new Nave(); // Instancia objeto da classe Nave
       private Vector2f posicaonave = new Vector2f();    
       private Vector2f posicaobomba = new Vector2f(); 
       private Vector2f posicaoinimigo = new Vector2f(); 
       public  List<Bomba> lstbomba = new List<Bomba>();       // lista de bombas lançadas pela nave
       public  List<Inimigo> lstinimigo = new List<Inimigo>(); // lista de inimigos que surgem
       public  Font font = new Font("assets/fontes/ALGER.TTF");
       public  Text text;

       public Game() // Construtor da classe Game
       {          
           this.mode = new VideoMode(800, 600);
           this.window = new RenderWindow(mode, "GameSpace", Styles.Titlebar);
           this.cenario.Texture = new Texture("Assets/imagens/cenario.png");
           this.gameover.Texture = new Texture("Assets/imagens/gameover.png");  
           // Obtém posição inicial da nave
           this.posicaonave = nave.sprite.Position;  
           // Criação dos inimigos
           this.qtdinimigos = 5;                     
           for (int i = 0; i < this.qtdinimigos - 1; i++) 
           {
               this.lstinimigo.Add(new Inimigo());
           }                 
       }  
     
       // Game Loop
       public void run()  
       {    
           
            while (this.window.IsOpen)
            {    
                
                this.eventosTeclado();
                this.atualizar();                                
                this.desenhar();                        
                this.window.Display();
                this.window.DispatchEvents();  
              
               
            }
       }    

       private void eventosTeclado()
       {
           // Direção da nave
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))  
            { 
                this.posicaonave.X -= velocidadenave;
            }
             if (posicaonave.X > 740)
                    {
                        posicaonave.X =  +1;
                    }     

            if (Keyboard.IsKeyPressed(Keyboard.Key.Right)) 
            {
                this.posicaonave.X += velocidadenave;
            }
            if (posicaonave.X < 0)
                    {
                        posicaonave.X = +1;
                    }     
            


            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                this.posicaonave.Y -= velocidadenave;
            }
            if (posicaonave.Y < 0)
                    {
                        posicaonave.X = +1;
                    }     

            if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                this.posicaonave.Y += velocidadenave;
            }    
            if (posicaonave.Y > 580)
                    {
                        posicaonave.Y = +1;
                    }     

            // Tiro da Nave
            if (Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
               this.posicaobomba = this.posicaonave;
                // Deslocamento para ajustar imagens nave e bomba
                this.posicaobomba.X = posicaonave.X + 21f;
                // Deslocamento para ajustar imagens nave e bomba
                this.posicaobomba.Y = posicaonave.Y - 50f;           
                // Acrescenta uma bomba na lista
                this.lstbomba.Add(new Bomba(this.posicaobomba));              
            }
            // Fim de jogo
            if (score == 4)
            {
                this.window.Close();
            }
       }
       private void atualizar()
       {
            // Posição da Nave
            this.nave.sprite.Position = posicaonave;
            // Navegação das bombas e desativa as que ultrapassaram os limites do cenário 
            for (int i = 0; i < this.lstbomba.Count; i++) 
            {
                this.posicaobomba.Y -= velocidadebomba;
                this.lstbomba[i].sprite.Position = this.posicaobomba;
                if (this.lstbomba[i].sprite.Position.Y < 30)
                {
                    this.lstbomba.Remove(lstbomba[i]);
                }             
            }
            // Atualiza a posição dos inimigos na cena
            for (int i = 0; i < this.lstinimigo.Count; i++)
            {
                this.posicaoinimigo = this.lstinimigo[i].sprite.Position;
                this.posicaoinimigo.Y += velocidadeinimigo;
                this.lstinimigo[i].sprite.Position = posicaoinimigo;
                if (this.lstinimigo[i].sprite.Position.Y > 600) 
                {
                    this.posicaoinimigo.X = random.Next(0, 800);            
                    this.posicaoinimigo.Y = -50;
                    this.lstinimigo[i].sprite.Position = posicaoinimigo;
               }
            }
            // Verifica se a bomba atingiu o inimigo
            if (this.lstbomba.Count >= 0)
            {
                for (int i = 0; i < this.lstbomba.Count; i++) 
                    {
                        for (int j = 0; j< this.lstinimigo.Count; j++)
                        {
                            if ( colisao(lstbomba[i].sprite, lstinimigo[j].sprite))
                            {
                                this.lstbomba.Remove(lstbomba[i]);
                                this.lstinimigo.Remove(lstinimigo[j]);
                                this.score += 1;
                            }
                        }             
                    }
            }
       }

       private void desenhar()
       {
            // Cenario
            this.window.Draw(this.cenario);
            // Nave
            this.window.Draw(this.nave.sprite);  
            // Bombas             
            foreach (var bomba in this.lstbomba)
            {   
                    this.window.Draw(bomba.sprite);    
            }
            // Inimigos
            foreach (var inimigo in this.lstinimigo)
            {               
               this.window.Draw(inimigo.sprite);    
            }   
            // Desenha o placar
            Text textContent = new Text(text + score.ToString(), font, 30);
            window.Draw(textContent);
     
           
            
       }

       private bool colisao(Sprite sprite01, Sprite sprite02)
       {
            if (sprite01.GetGlobalBounds().Intersects(sprite02.GetGlobalBounds()))
            {
                return true;
            }

            return false;
       }
   }
}