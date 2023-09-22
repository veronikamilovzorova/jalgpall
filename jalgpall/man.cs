using jalgpall;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.Generic;

using System;
using System.Collections.Generic;

namespace Football
{
    public class Program
    {
       
            public static void Main(string[] args)
            {
                
                var stadiumWidth = 50;
                var stadiumHeight = 20;
                

                // Создаем команды
                var homeTeam = new Team("Neco");
                var awayTeam = new Team("Param");

                
                homeTeam.AddPlayer(new Player("player1"));
                homeTeam.AddPlayer(new Player("player2"));

                awayTeam.AddPlayer(new Player("player3"));
                awayTeam.AddPlayer(new Player("player4"));


            var game = new Game(homeTeam, awayTeam, new Stadium(stadiumWidth, stadiumHeight));
                game.Start();

                ConsoleKeyInfo keyInfo;
                do
                {
                    Console.Clear();
                    DrawField(game);
                    game.Move();

                    Console.WriteLine("Vajuge ESC väljumiseks.");
                    keyInfo = Console.ReadKey();
                } while (keyInfo.Key != ConsoleKey.Escape);
            }

            public static void DrawField(Game game)
            {
                var field = new char[game.Stadium.Width, game.Stadium.Height];

                for (int x = 0; x < game.Stadium.Width; x++)
                {
                    for (int y = 0; y < game.Stadium.Height; y++)
                    {
                        field[x, y] = ' ';
                    }
                }

                foreach (var player in game.HomeTeam.Players)
                {
                    var (x, y) = player.GetAbsolutePosition();
                    field[(int)x, (int)y] = '2';
                }

                foreach (var player in game.AwayTeam.Players)
                {
                    var (x, y) = player.GetAbsolutePosition();
                    field[(int)x, (int)y] = '6';
                }

                var ballPosition = game.GetBallPositionForTeam(game.HomeTeam);
                field[(int)ballPosition.Item1, (int)ballPosition.Item2] = 'O';

                for (int y = 0; y < game.Stadium.Height; y++)
                {
                    for (int x = 0; x < game.Stadium.Width; x++)
                    {
                        Console.Write(field[x, y]);
                    }
                    Console.WriteLine();
                }
            }
        }
    }