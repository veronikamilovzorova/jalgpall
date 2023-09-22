using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jalgpall
{
    public class Player
    {
        public string Name { get; } //mängija nimi
        public double X { get; private set; } //mängija x kordinaat
        public double Y { get; private set; } //mängija y kordinaat
        private double _vx, _vy; //mängija ja palli kaugus
        public Team? Team { get; set; } = null;

        private const double MaxSpeed = 5; //max mängija kiirus
        private const double MaxKickSpeed = 25; //max löögikiirus
        private const double BallKickDistance = 10; //löögikaugus

        private Random _random = new Random(); //juhuslik arv
        //konstruktorid 
        public Player(string name) //sõltub sõnest ja sõne võrdleb Namega
        {
            Name = name;
        }

        public Player(string name, double x, double y, Team team) //mängija põllul 
        {
            Name = name;
            X = x;
            Y = y;
            Team = team;
        }

        public void SetPosition(double x, double y) //sozdanie to4ek pozicij
        {
            X = x;
            Y = y;
        }

        public (double, double) GetAbsolutePosition() //poly4enie to4noj poziciii
        {
            return Team!.Game.GetPositionForTeam(Team, X, Y);
        }

        public double GetDistanceToBall() //poly4enie distancii mja4a
        {
            var ballPosition = Team!.GetBallPosition(); //pozicija mja4a ravnjaetsja
            var dx = ballPosition.Item1 - X; //raspredelenie
            var dy = ballPosition.Item2 - Y;
            return Math.Sqrt(dx * dx + dy * dy); //pods4et
        }

        public void MoveTowardsBall() //
        {
            var ballPosition = Team!.GetBallPosition(); //pyt' k mjazy
            var dx = ballPosition.Item1 - X;
            var dy = ballPosition.Item2 - Y;
            var ratio = Math.Sqrt(dx * dx + dy * dy) / MaxSpeed;
            _vx = dx / ratio;
            _vy = dy / ratio;
        }

        public void Move()
        {
            if (this.Team.GetClosestPlayerToBall() != this)
            {
                _vx = 0;
                _vy = 0;
            }

            if (GetDistanceToBall() < BallKickDistance)
            {
                this.Team.SetBallSpeed(
                    MaxKickSpeed * _random.NextDouble(),
                    MaxKickSpeed * (_random.NextDouble() - 0.5)
                                    );
            }
            // Вычислите новые координаты игрока на основе текущей скорости.
            var newX = X + _vx;  // Новая координата X игрока.
            var newY = Y + _vy;
            var newAbsolutePosition = Team.Game.GetPositionForTeam(Team, newX, newY); // Вычислите новую абсолютную позицию игрока с учетом команды.
            if (Team.Game.Stadium.IsIn(newAbsolutePosition.Item1, newAbsolutePosition.Item2)) // Проверьте, находится ли новая позиция игрока в пределах игрового поля.
            {
                // Если да, обновите координаты игрока.
                X = newX;  // Обновите координату X игрока.
                Y = newY;
            }
            else
            {
                // Если новая позиция за пределами поля, остановите его движение.
                _vx = _vy = 0; // Установите скорости по осям X и Y в 0.
            }
        }
    }
}