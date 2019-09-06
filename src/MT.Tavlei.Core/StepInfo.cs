using System.Collections.Generic;
using System.Text;
using MT.Tavlei.Core.Common;
using MT.Tavlei.Core.Types;

namespace MT.Tavlei.Core
{
    public class StepInfo
    {
        public FigureType Figure { get; set; }
        public Point From { get; set; }
        public Point To { get; set; }
        public Dictionary<FigureType, Point> Captures { get; set; }
        public bool GameOver { get; set; }

        public StepInfo(FigureType figure, int x0, int y0, int x1, int y1)
        {
            Figure = figure;
            From = new Point(x0, y0);
            To = new Point(x1, y1);
            Captures = new Dictionary<FigureType, Point>();
            GameOver = false;
        }

        public override string ToString()
        {
            char step = '-';

            if (Captures.Count > 0)
                step = ':';

            if (GameOver)
                step = 'x';

            var buffer = new StringBuilder();
            buffer.AppendFormat("{0} {1}{2}{3}",
                Figure.ToChar(),
                From.ToBoardCoordinates(),
                step,
                To.ToBoardCoordinates()    
            );

            return buffer.ToString();
        }
    }
}
