using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ExpressedEngine.ExpressedEngine;
using System.Windows.Forms;

namespace ExpressedEngine
{
    class DemoGame : ExpressedEngine.ExpressedEngine
    {
        Sprite2D player;
        //Sprite2D player2;

        bool left;
        bool right;
        bool up;
        bool down;

        Vector2 lastPos = Vector2.Zero();

        string[,] Map =
        {
            //{"g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g"},
            //{"g","c","c","c","c","c","c","c","g",".",".",".",".",".",".","g"},
            //{"g","c",".",".",".",".",".","c","g","c","c","c","c","c",".","g"},
            //{"g","c",".","g",".",".",".","c","g","c",".","g",".","c",".","g"},
            //{"g","c",".","g",".",".",".","c","g","c","g","g","g","c","g","g"},
            //{"g","c",".","g",".",".",".","c","g","c",".","c","g","c","g","g"},
            //{"g","c",".","g",".",".",".","c","g",".",".","c","g","c","g","g"},
            //{"g","c",".","g",".",".",".","c","g","g","g","c","g","c","g","g"},
            //{"g","c",".","g","g","g","g","c","g",".",".","c","g","c","g","g"},
            //{"g","c",".","g",".","p","g","c",".",".",".","c","g","c","g","g"},
            //{"g","c",".",".",".",".","g","c","c","c","c","c","g","c","g","g"},
            //{"g","c",".",".",".",".","g","p",".",".","c","c","g","c","c","g"},
            //{"g","g","g","g","g","g","g","g","g","g","g","g","g","g","g","g"},

            {".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","."},
            {".",".",".",".",".",".",".",".",".",".",".",".","c",".",".","."},
            {".",".",".",".",".",".",".",".",".",".","g","g","g",".",".","."},
            {".",".",".",".",".",".",".",".",".","g",".",".",".",".",".","."},
            {".",".",".",".","p",".",".",".","g",".",".",".",".",".",".","."},
            {".",".",".",".",".",".",".","g",".",".",".",".",".",".",".","."},
            {".",".",".","g","g","g","g","g","g","g","g","g",".",".",".","."},
            {".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","."},
            {".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","."},
            {".",".",".",".",".",".",".",".",".",".",".",".",".",".",".","."},

        };

        public DemoGame() : base(new Vector2(615, 515),"Expressed Engine Demo") { }

        public override void OnLoad()
        {
            BackgroundColor = Color.Black;

            CameraZoom = new Vector2(.7f, .7f);
            
            Sprite2D groundRef = new Sprite2D("Tiles/Blue tiles/tileBlue_03");
            Sprite2D coinRef = new Sprite2D("Items/yellowCrystal");

            //player = new Shape2D(new Vector2(10,10), new Vector2(10,10),"Test");
            //player = new Sprite2D(new Vector2(10, 10), new Vector2(36, 45), "Players/Player Grey/playerGrey_walk1", "Player");

            for (int i = 0; i < Map.GetLength(1); i++)
            {
                for (int j = 0; j < Map.GetLength(0);  j++)
                {
                    if(Map[j,i] == "g")
                    { 
                        new Sprite2D(new Vector2(i * 50, j * 50), new Vector2(50, 50), groundRef, "Ground").CreateStatic();
                    }
                    
                    if(Map[j,i] == "c")
                    { 
                        new Sprite2D(new Vector2(i * 50 + 15, j * 50 + 15), new Vector2(25, 25), coinRef, "Coin").CreateStatic();
                    }
                    
                    if(Map[j,i] == "p")
                    { 
                        player = new Sprite2D(new Vector2(i * 50 , j * 50), new Vector2(30, 40), "Players/Player Green/playerGreen_walk1", "Player");
                        player.CreateDynamic();
                    }
                }
            }

            //player = new Sprite2D(new Vector2(100, 100), new Vector2(30, 40), "Players/Player Green/playerGreen_walk1", "Player");
            //player2 = new Sprite2D(new Vector2(100, 30), new Vector2(30, 40), "Players/Player Green/playerGreen_walk1", "Player");

            
        }

        public override void OnDraw()
        {
            
        }

        int times = 0;

        public override void OnUpdate()
        {
            //CameraPosition.X += .1f;
            //CameraPosition.Y += .1f;
            //
            //CameraAngle += 2;

            if(player == null)
            {
                return;
            }

            times++;

            

            if(up)
            {
                //player.Position.Y -= 1f;
                //player.AddForce(new Vector2(0, -100000), Vector2.Zero());
                player.SetVelocity(new Vector2(0, -100000));
            }

            if (down)
            {
                //player.Position.Y += 1f;
                player.AddForce(new Vector2(0, 100000), Vector2.Zero());
                //player.SetVelocity(new Vector2(0, 100000));
            }

            if (left)
            {
                //player.Position.X -= 1f;
                player.AddForce(new Vector2(-100000, 0), Vector2.Zero());
                //player.SetVelocity(new Vector2(-100000, 0));
            }

            if (right)
            {
                //player.Position.X += 1f;
                player.AddForce(new Vector2(100000, 0), Vector2.Zero());
                //player.SetVelocity(new Vector2(100000, 0));
            }

            player.UpdatePosition();

            Sprite2D coin = player.IsColliding("Coin");

            if (coin != null)
            {
                coin.DestroySelf();
            }

            //if (player.IsColliding("Ground") != null)
            //{
            //    //Log.Info($"COLLIDING! {times}");
            //    //times++;
            //
            //    //player.Position.X = lastPos.X;
            //    //player.Position.Y = lastPos.Y;
            //}
            //
            //else
            //{
            //    //lastPos.X = player.Position.X;
            //    //lastPos.Y = player.Position.Y;
            //}
        }

        

        public override void GetKeyDown(KeyEventArgs e)
        {
            if(e.KeyCode == Keys.W)
            {
                up = true;
            }
            
            if(e.KeyCode == Keys.S)
            {
                down = true;
            }

            if (e.KeyCode == Keys.A)
            {
                left = true;
            }

            if (e.KeyCode == Keys.D)
            {
                right = true;
            }
        }

        public override void GetKeyUp(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                up = false;
            }

            if (e.KeyCode == Keys.S)
            {
                down = false;
            }

            if (e.KeyCode == Keys.A)
            {
                left = false;
            }

            if (e.KeyCode == Keys.D)
            {
                right = false;
            }
        }
    }
}
