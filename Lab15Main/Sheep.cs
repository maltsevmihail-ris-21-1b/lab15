using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;

namespace nothinginteresting
{
    public class Sheep
    {
        private static Texture sprite = new Texture("C:\\Users\\malts\\OneDrive\\Рабочий стол\\лабы юры\\sheep.png");
        public static int sheepCount = 0;
        public Vector2f Position { get; set; }
        public Vector2f Velocity { get; set; }
        public CircleShape Shape { get; }
        public int Ticks { get; set; }

        public Sheep(Vector2f position, Vector2f velocity)
        {
            Position = position;
            Velocity = velocity;

            Shape = new CircleShape(20);    
            Shape.Origin = new Vector2f(20, 20);
            Shape.FillColor = SFML.Graphics.Color.White;
            Shape.Texture = sprite;
            Ticks = 0;
        }

        public void Update()
        {
            if (Position.X <= 10 || Position.X + Shape.Radius >= 800)
                Velocity = new Vector2f(-Velocity.X, Velocity.Y); 
            if (Position.Y <= 10 || Position.Y + Shape.Radius >= 600)
                Velocity = new Vector2f(Velocity.X,-Velocity.Y);
            Position += Velocity;
            Shape.Position = Position;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Shape, states);
        }
    }
}
