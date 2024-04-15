using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Procon123.Models
{
    using System;

    namespace Procon123.Models
    {
        public class Worker
        {
            public int Id { get; set; }
            public int Row { get; set; }
            public int Column { get; set; }

            public void Move(Direction direction)
            {
                switch (direction)
                {
                    case Direction.Up:
                        Row--;
                        break;
                    case Direction.Down:
                        Row++;
                        break;
                    case Direction.Left:
                        Column--;
                        break;
                    case Direction.Right:
                        Column++;
                        break;
                    default:
                        throw new ArgumentException("Invalid direction.");
                }
            }
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
    }

}