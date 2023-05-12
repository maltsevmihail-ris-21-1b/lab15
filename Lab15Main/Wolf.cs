using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nothinginteresting
{
    class Wolf
    {
        private static Texture sprite = new Texture("C:\\Users\\malts\\OneDrive\\Рабочий стол\\лабы юры\\wolf (2).png");
        public Vector2f Position { get; set; }
        public Vector2f Velocity { get; set; }
        public CircleShape Shape { get; }

        public Wolf(Vector2f position, Vector2f velocity)
        {
            Position = position;
            Velocity = velocity;

            Shape = new CircleShape(40);
            Shape.Origin = new Vector2f(40, 40);
            Shape.Texture = sprite;
        }

        public void Update()
        {
            if (Shape.GetGlobalBounds().Left  <= 0 || Position.X + Shape.Radius >= 800)
                Velocity = new Vector2f(-Velocity.X, Velocity.Y);
            if (Shape.GetGlobalBounds().Top  <= 0 || Position.Y + Shape.Radius >= 600)
                Velocity = new Vector2f(Velocity.X, -Velocity.Y);
            
            Position += Velocity;
            Shape.Position = Position;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(Shape, states);
        }
    }
}
