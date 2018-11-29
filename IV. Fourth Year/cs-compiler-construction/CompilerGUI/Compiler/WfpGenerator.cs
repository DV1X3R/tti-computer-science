using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompilerGUI.Compiler
{
    class WfpGenerator
    {
        public ObservableCollection<Quad> Quads { get; private set; } = new ObservableCollection<Quad>();

        public WfpGenerator()
        {
            //Quads.Add(new Quad());
            /*
            if State and sfDragging = 0 then Color := GetColor(1);

            (1) BLOCK
            (2) BE, 3, State, TRUE
            (3) :=, TRUE, , T1
            (4) BE, 5, sfDragging, 0
            (5) :=, TRUE, , T2
            (6) BE, 7, T1, T2
            (7) PAR, , , 1
            (8) CALL, GetColor, , T3
            (7) :=, Color, , T3
            (8) BLOCKEND
            */
        }
    }
}
