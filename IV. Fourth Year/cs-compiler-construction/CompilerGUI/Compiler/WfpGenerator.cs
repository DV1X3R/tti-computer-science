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

        private int locals = 0;

        private int compareWith = -1;
        private string compareOp = "";
        private Stack<Quad> gotoEnd = new Stack<Quad>();
        private Stack<Quad> gotoAssign = new Stack<Quad>();

        private Stack<string> functions = new Stack<string>();
        private Stack<int> functionParamsCount = new Stack<int>();
        private Stack<int> functionParams = new Stack<int>();

        public void SetCompareWith() => compareWith = locals - 1;
        public void SetCompareOpBE() => compareOp = "BE";
        public void SetCompareOpBG() => compareOp = "BG";
        public void SetCompareOpBL() => compareOp = "BL";

        public void CompareAnd()
        {
            Quads.Add(new Quad(Quads.Count, compareOp , (Quads.Count + 2).ToString(), compareWith == -1 ? "TRUE" : "_T" + compareWith,  "_T" + (locals - 1)));
            var q = new Quad(Quads.Count, "BR", "(END)", null, null);
            Quads.Add(q);
            gotoEnd.Push(q);
            compareWith = -1;
        }

        public void CompareOr()
        {
            var q = new Quad(Quads.Count, compareOp, "(ASSIGNMENT)", compareWith == -1 ? "TRUE" : "_T" + compareWith, "_T" + (locals - 1));
            Quads.Add(q);
            gotoAssign.Push(q);
            compareWith = -1;
        }

        public void UpdateAssign()
        {
            foreach(var q in gotoAssign)
                q.UpdateBR(Quads.Count.ToString());
            gotoAssign.Clear();
        }

        public void UpdateEnd()
        {
            foreach (var q in gotoEnd)
                q.UpdateBR(Quads.Count.ToString());
            gotoEnd.Clear();
            Quads.Add(new Quad(Quads.Count, "END", null, null, null));
        }

        public void ResetLocals()
        {
            locals = 0;
        }

        public void Assign(string name) => Quads.Add(new Quad(Quads.Count, ":=", "_T" + (locals - 1), null, name));
        public void AddAssignable(string name) => Quads.Add(new Quad(Quads.Count, ":=", name, null, "_T" + locals++));

        public void AddFunction(string name)
        {
            functions.Push(name);
            functionParamsCount.Push(0);
        }

        public void AddFunctionParam()
        {
            int n = functionParamsCount.Pop() + 1;
            functionParamsCount.Push(n);
            functionParams.Push(locals - 1);
        }

        public void Call()
        {
            int p = functionParamsCount.Pop();
            for (int i = 0; i < p; i++)
                Quads.Add(new Quad(Quads.Count, "PAR", null, null, "_T" + (functionParams.Pop())));
            Quads.Add(new Quad(Quads.Count, "CALL", functions.Pop(), null, "_T" + locals++));
        }

    }
}
