namespace CompilerGUI.Compiler
{
    class Quad
    {
        public int Index { get; private set; }
        public string Operator { get; private set; }
        public string Operand1 { get; private set; }
        public string Operand2 { get; private set; }
        public string Result { get; private set; }

        public Quad(int i, string op, string op1, string op2, string res)
        {
            Index = i;
            Operator = op;
            Operand1 = op1;
            Operand2 = op2;
            Result = res;
        }

        public void UpdateBR(string op1)
        {
            Operand1 = op1;
        }

    }
}
