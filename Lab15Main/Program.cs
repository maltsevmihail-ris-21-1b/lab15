
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Net.Sockets;
using static SFML.Window.Mouse;

namespace nothinginteresting
{
    public static class Program
    {
        public static Texture sprite = new Texture("C:\\Users\\malts\\OneDrive\\Рабочий стол\\лабы юры\\background.png");
        public static Sprite s = new Sprite(sprite);
        public static List<Sheep> sheeps = new List<Sheep>();
        public static List<Thread> sheepThreads = new List<Thread>();
        public static RenderWindow window = new RenderWindow(new VideoMode(800, 600), "My Game", Styles.Close);
        static void Main(string[] args)
        {
            
            window.SetFramerateLimit(60);

            Start();

        }
        public static void Start()
        {
            int N = 4;
            for (int i = 0; i < N; i++)
            {
                SpawnSheep();
            }

            Wolf wolf = new Wolf(new Vector2f(400, 300), new Vector2f(0, 3));

            Thread wolfThread = new Thread(() =>
            {
                while (true)
                {
                    wolf.Update();
                    Thread.Sleep(30);
                }
            });


            wolfThread.Priority = ThreadPriority.Highest;
            wolfThread.Start();


            while (window.IsOpen)
            {
                window.Clear(Color.Black);

                List<Sheep> toRemove = new List<Sheep>();

                foreach (Sheep sheep in sheeps)
                {
                    if (wolf.Shape.GetGlobalBounds().Intersects(sheep.Shape.GetGlobalBounds()))
                    {
                        toRemove.Add(sheep);
                        if (wolf.Shape.Radius <= 70)
                            wolf.Shape.Radius += 3;
                    }
                }
                foreach(Sheep sheep in toRemove)
                {
                    sheeps.Remove(sheep);
                }
                toRemove.Clear();

                for (int i = 0; i < sheeps.Count; i++)
                {
                    for (int j = i + 1; j < sheeps.Count; j++)
                    {
                        Sheep otherSheep = sheeps[j];

                        if (sheeps[i].Shape.GetGlobalBounds().Intersects(otherSheep.Shape.GetGlobalBounds()) && sheeps[i].Ticks > 70 && otherSheep.Ticks > 70)
                        {
                            SpawnSheep();
                            sheeps[i].Ticks = 0;
                            otherSheep.Ticks = 0;
                        }
                        sheeps[i].Ticks++;
                        otherSheep.Ticks++;
                    }
                }

                foreach (Sheep sheep in toRemove)
                {
                    sheeps.Remove(sheep);
                }
                toRemove.Clear();

                window.Draw(s);

                foreach (Sheep sheep in sheeps)
                {
                    sheep.Draw(window,RenderStates.Default);
                }
                
                wolf.Draw(window, RenderStates.Default);
                Console.WriteLine(wolf.Position.Y);
                Console.WriteLine("\t\t\t" + sheeps[0].Position.Y);
                window.Display();
            }
        }
        public static void  SpawnSheep()
        {
            if (sheeps.Count <= 10)
            {
                Sheep newSheep = (new Sheep(new Vector2f(new Random().Next(1, 5) * 100, new Random().Next(1, 6) * 100), new Vector2f(0, -3)));
                sheeps.Add(newSheep);
                Thread sheepThread = new Thread(() =>
                {
                    while (true)
                    {
                        newSheep.Update();
                        Thread.Sleep(30);
                    }
                });
                sheepThread.Priority = ThreadPriority.Lowest;
                sheepThreads.Add(sheepThread);
                sheepThreads[sheepThreads.Count - 1].Start();
            }
        }
    }
}