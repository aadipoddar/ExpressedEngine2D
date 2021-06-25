using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Box2DX.Dynamics;
using Box2DX.Collision;
using Box2DX.Common;


namespace ExpressedEngine.ExpressedEngine
{
    public class Sprite2D
    {
        public Vector2 Position = null;
        public Vector2 Scale = null;

        public string Directory = "";
        public string Tag = "";

        public Bitmap Sprite = null;

        public bool IsReference = false;

        BodyDef bodyDef = new BodyDef();
        Body body;

        public Sprite2D(Vector2 Position, Vector2 Scale, string Directory, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Directory = Directory;
            this.Tag = Tag;

            Image tmp = Image.FromFile($"Assets/Sprites/{Directory}.png");

            Bitmap sprite = new Bitmap(tmp);
            Sprite = sprite;

            Log.Info($"[SHAPE2D] ({Tag}) - Has been registered!");

            ExpressedEngine.RegisterSprite(this);
        }

        public Sprite2D(string Directory)
        {
            this.IsReference = true;
            this.Directory = Directory;
            

            Image tmp = Image.FromFile($"Assets/Sprites/{Directory}.png");

            Bitmap sprite = new Bitmap(tmp);
            Sprite = sprite;

            Log.Info($"[SHAPE2D] ({Tag}) - Has been registered!");

            ExpressedEngine.RegisterSprite(this);
        }

        public Sprite2D(Vector2 Position, Vector2 Scale, Sprite2D refernce, string Tag)
        {
            this.Position = Position;
            this.Scale = Scale;
            this.Tag = Tag;

            Sprite = refernce.Sprite;

            Log.Info($"[SHAPE2D] ({Tag}) - Has been registered!");

            ExpressedEngine.RegisterSprite(this);
        }

        public void CreateStatic()
        {
            // Define the ground body.
            bodyDef = new BodyDef();
            bodyDef.Position = new Vec2(this.Position.X, this.Position.Y);

            // Call the body factory which  creates the ground box shape.
            // The body is also added to the world.
            body = ExpressedEngine.world.CreateBody(bodyDef);

            // Define the ground box shape.
            PolygonDef shapeDef = new PolygonDef();

            // The extents are the half-widths of the box.
            shapeDef.SetAsBox(Scale.X/2, Scale.Y/2);

            // Add the ground shape to the ground body.
            body.CreateShape(shapeDef);
        }

        public void CreateDynamic()
        {
            // Define the dynamic body. We set its position and call the body factory.
            bodyDef = new BodyDef();
            bodyDef.Position = new Vec2(this.Position.X, this.Position.Y);
            body = ExpressedEngine.world.CreateBody(bodyDef);

            // Define another box shape for our dynamic body.
            PolygonDef shapeDef = new PolygonDef();
            shapeDef.SetAsBox(15, 15);

            // Set the box density to be non-zero, so it will be dynamic.
            shapeDef.Density = 1.0f;

            // Override the default friction.
            shapeDef.Friction = 0.3f;

            // Add the shape to the body.
            body.CreateShape(shapeDef);

            // Now tell the dynamic body to compute it's mass properties base
            // on its shape.
            body.SetMassFromShapes();
        }

        public void AddForce(Vector2 force, Vector2 point)
        {
            //body.SetBullet(true);
            //body.SetLinearVelocity(new Vec2(force.X, force.Y));
            //body.ApplyForce(new Vec2(force.X, force.Y), new Vec2(point.X, point.Y));
            body.ApplyImpulse(new Vec2(force.X, force.Y), new Vec2(point.X, point.Y));
        }
        
        public void SetVelocity(Vector2 vel)
        {
            //body.SetLinearVelocity(new Vec2(force.X, force.Y));
            body.SetLinearVelocity(new Vec2(vel.X, vel.Y));
        }

        public void UpdatePosition()
        {
            Log.Warning(body.GetPosition().X + "|" + body.GetPosition().Y);
            this.Position.X = body.GetPosition().X;
            this.Position.Y = body.GetPosition().Y;
        }

        public bool IsColliding(Sprite2D a , Sprite2D b)
        {
            if(a.Position.X < b.Position.X + b.Scale.X &&
                a.Position.X + a.Scale.X > b.Position.X &&
                a.Position.Y < b.Position.Y + b.Scale.Y &&
                a.Position.Y + a.Scale.Y > b.Position.Y)
            {
                return true;
            }
            return false;
        }
        
        public Sprite2D IsColliding(string tag)
        {
            /*if(a.Position.X < b.Position.X + b.Scale.X &&
                a.Position.X + a.Scale.X > b.Position.X &&
                a.Position.Y < b.Position.Y + b.Scale.Y &&
                a.Position.Y + a.Scale.Y > b.Position.Y)
            {
                return true;
            }*/

            foreach (Sprite2D b in ExpressedEngine.AllSprites)
            {
                if (b.Tag == tag)
                {
                    if (Position.X < Position.X + Scale.X &&
                         Position.X + Scale.X > Position.X &&
                         Position.Y < Position.Y + Scale.Y &&
                         Position.Y + Scale.Y > Position.Y)
                    {
                        return b;

                    }
                }
            }

            return null;
        }

        public void DestroySelf()
        {
            ExpressedEngine.UnRegisterSprite(this);
        }
    }
}
