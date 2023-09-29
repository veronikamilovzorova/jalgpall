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

namespace jalgpall
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

            
            // Заполняем поле значками по бокам
            for (int x = 0; x < game.Stadium.Width; x++)
            {
                for (int y = 0; y < game.Stadium.Height; y++)
                {
                    if (x == 0 || x == game.Stadium.Width - 1)
                    {
                        field[x, y] = '|'; // Значок вертикальной черты для боковых границ
                    }
                    else if (y == 0 || y == game.Stadium.Height - 1)
                    {
                        field[x, y] = '-'; // Значок горизонтальной черты для верхней и нижней границ
                    }
                    else
                    {
                        field[x, y] = ' '; // Пустое пространство внутри поля
                    }
                }
            }

            // Определите размеры ворот
            int goalWidth = 6; // Ширина ворот
            int goalHeight = 4; // Высота ворот

            // Определите позиции ворот (используем целые числа)
            int leftGoalX = 0;
            int leftGoalY = game.Stadium.Height / 2 - goalHeight / 2;
            int rightGoalX = game.Stadium.Width - goalWidth;
            int rightGoalY = game.Stadium.Height / 2 - goalHeight / 2;

            // Отрисовка ворот домашней команды (без внутренней части)
            for (int y = leftGoalY; y < leftGoalY + goalHeight; y++)
            {
                field[leftGoalX, y] = '+'; // Левая вертикальная черта
                
            }

            for (int x = leftGoalX + 1; x < leftGoalX + goalWidth - 1; x++)
            {
                field[x, leftGoalY] = '-'; // Верхняя горизонтальная черта
                field[x, leftGoalY + goalHeight - 1] = '-'; // Нижняя горизонтальная черта
            }

            // Отрисовка ворот гостевой команды (без внутренней части)
            for (int y = rightGoalY; y < rightGoalY + goalHeight; y++)
            {
                
                field[rightGoalX + goalWidth - 1, y] = '+'; // Правая вертикальная черта
            }

            for (int x = rightGoalX + 1; x < rightGoalX + goalWidth - 1; x++)
            {
                field[x, rightGoalY] = '-'; // Верхняя горизонтальная черта
                field[x, rightGoalY + goalHeight - 1] = '-'; // Нижняя горизонтальная черта
            }









            // Отрисуйте игроков и мяч как обычно
            foreach (var player in game.HomeTeam.Players)
            {
                var (x, y) = player.GetAbsolutePosition();
                var xOffset = new Random().Next(-1, 2);
                var yOffset = new Random().Next(-1, 2);
                field[(int)x + xOffset, (int)y + yOffset] = '2';
            }

            foreach (var player in game.AwayTeam.Players)
            {
                var (x, y) = player.GetAbsolutePosition();
                var xOffset = new Random().Next(-1, 2);
                var yOffset = new Random().Next(-1, 2);
                field[(int)x + xOffset, (int)y + yOffset] = '6';
            }

            var ballPosition = game.GetBallPositionForTeam(game.HomeTeam);
            field[(int)ballPosition.Item1, (int)ballPosition.Item2] = 'O';

            Console.Clear(); // Очищаем консоль перед каждой перерисовкой

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