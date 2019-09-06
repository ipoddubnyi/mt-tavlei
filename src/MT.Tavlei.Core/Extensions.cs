using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MT.Tavlei.Core.Common;
using MT.Tavlei.Core.Types;

namespace MT.Tavlei.Core
{
    public static class Extensions
    {
        public static char ToChar(this FigureType type)
        {
            switch (type)
            {
                case FigureType.Attacker:
                    return 'A';
                case FigureType.Defender:
                    return 'D';
                case FigureType.King:
                    return 'K';
            }

            throw new TavleiRuntimeException("Неизвестный тип фигуры.");
        }

        public static string ToBoardCoordinates(this Point point)
        {
            return string.Format("{0}{1}", (char)('а' + point.X), point.Y + 1);
        }
    }
}
