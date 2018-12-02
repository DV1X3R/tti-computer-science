using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CompilerGUI.Compiler
{
    // 2. Синтаксический анализатор
    class Parser
    {
        // Логи для отладки
        public List<ParserLog> Logs { get; private set; } = new List<ParserLog>();

        private ObservableCollection<Lexeme> lexemes;
        private int index;
        private string result;
        private string resultLocal;

        private WfpGenerator wfp;

        private List<string> ifList = new List<string>() { "if" };
        private List<string> thenList = new List<string>() { "then" };
        private List<string> logicaloperatorList = new List<string>() { "or", "and" };
        private List<string> booloperatorList = new List<string>() { "=", "<", ">" };
        private List<string> eqsignList = new List<string>() { ":=" };
        private List<string> endsignList = new List<string>() { ";" };
        private List<string> paramstartList = new List<string>() { "(" };
        private List<string> paramendList = new List<string>() { ")" };
        private List<string> paramdelList = new List<string>() { "," };

        public Parser(Scanner scanner, WfpGenerator wfpGenerator)
        {
            lexemes = scanner.Lexemes;
            wfp = wfpGenerator;
        }

        private void CheckLexeme(string expected, LexemeType lexemeType, List<string> values = null)
        {
            if (index > lexemes.Count - 1 || lexemes[index].Type != lexemeType || (values != null && !values.Contains(lexemes[index].Value)))
                ThrowMismatch(expected);
            else
                WriteMatch(expected);
        }

        private void WriteMatch(string expected)
        {
            resultLocal = lexemes[index].Value;
            result += resultLocal + " ";
            Logs.Add(new ParserLog(ParserLogType.Match, lexemes[index], expected, result));
            index += 1;
        }

        private void ThrowMismatch(string expected)
        {
            var log = new ParserLog(ParserLogType.Mismatch, (index > lexemes.Count - 1) ? null : lexemes[index], expected, result);
            Logs.Add(log);
            throw new ParserException(log);
        }

        // <ifStatement> := if <logicalExpression> then <statement>
        private void CheckIfStatement()
        {
            CheckIf();
            CheckLogicalExpression();
            CheckThen();
            wfp.UpdateAssign();
            CheckAssignmentStatement();
            wfp.UpdateEnd();
        }
        private void CheckIf() => CheckLexeme("[if]", LexemeType.KEY, ifList);
        private void CheckThen() => CheckLexeme("[then]", LexemeType.KEY, thenList);

        // <logicalExpression> := <boolExpression> { <logicalOperator> <logicalExpression> }
        private void CheckLogicalExpression()
        {
            CheckBoolExpression();
            try
            {
                CheckLogicalOperator();
                switch (resultLocal)
                {
                    case "and": wfp.CompareAnd(); break;
                    case "or": wfp.CompareOr(); break;
                    default: throw new System.ArgumentOutOfRangeException($"Logical Operator '{resultLocal}' is not supported");
                }
            }
            catch (ParserException)
            {
                wfp.CompareAnd();
                return;
            }
            CheckLogicalExpression();
        }

        // <boolExpression> := <assignableValue> [ <boolOperator> <assignableValue> ]
        private void CheckBoolExpression()
        {
            CheckAssignableValue();

            string boolOperator;
            try
            {
                CheckBoolOperator();
                boolOperator = resultLocal;
                wfp.SetCompareWith();
            }
            catch (ParserException)
            {
                wfp.SetCompareOpBE();
                return;
            }

            CheckAssignableValue();

            switch (boolOperator)
            {
                case "=": wfp.SetCompareOpBE(); break;
                case ">": wfp.SetCompareOpBG(); break;
                case "<": wfp.SetCompareOpBL(); break;
                default: throw new System.ArgumentOutOfRangeException($"Bool Operator '{boolOperator}' is not supported");
            }
        }

        // <logicalOperator> := and | or
        private void CheckLogicalOperator() => CheckLexeme("<logicalOperator>", LexemeType.KEY, logicaloperatorList);

        // <boolOperator> := = | < | >
        private void CheckBoolOperator() => CheckLexeme("<boolOperator>", LexemeType.DL1, booloperatorList);

        // <assignmentStatement> := <variableName> := <assignableValue> ;
        private void CheckAssignmentStatement()
        {
            CheckVariableName();
            string variableName = resultLocal;
            CheckAssignmentEqSign();
            CheckAssignableValue();
            CheckAssignmentEndSign();
            wfp.Assign(variableName);
        }
        private void CheckAssignmentEqSign() => CheckLexeme("[:=]", LexemeType.DL2, eqsignList);
        private void CheckAssignmentEndSign() => CheckLexeme("[;]", LexemeType.DL1, endsignList);

        // <variableName> := <identifier>
        private void CheckVariableName() => CheckLexeme("<variableName>", LexemeType.IDN);

        // <assignableValue> := <function> | <identifier> | <integer>
        private void CheckAssignableValue()
        {
            int recIndex = index;
            string recResult = result;

            try
            {
                CheckFunction();
                return;
            }
            catch (ParserException)
            {
                index = recIndex;
                result = recResult;
            }

            try
            {
                CheckLexeme("<identifier>", LexemeType.IDN);
                wfp.AddAssignable(resultLocal);
                return;
            }
            catch (ParserException)
            {
                index = recIndex;
                result = recResult;
            }

            try
            {
                CheckLexeme("<integer>", LexemeType.INT);
                wfp.AddAssignable(resultLocal);
                return;
            }
            catch (ParserException)
            {
                index = recIndex;
                result = recResult;
            }

            ThrowMismatch("<assignableValue>");
        }

        // <function> := <functionName> ( <functionParam> )
        private void CheckFunction()
        {
            CheckFunctionName();
            string potentialFunction = resultLocal;
            CheckFunctionParamStartSign();
            wfp.AddFunction(potentialFunction);
            CheckFunctionParams();
            CheckFunctionParamEndSign();
            wfp.Call();
        }
        private void CheckFunctionParamStartSign() => CheckLexeme("[(]", LexemeType.DL1, paramstartList);
        private void CheckFunctionParamEndSign() => CheckLexeme("[)]", LexemeType.DL1, paramendList);

        // <functionName> := <identifier>
        private void CheckFunctionName() => CheckLexeme("<functionName>", LexemeType.IDN);

        // <functionParam> :=  [ <assignableValue> { , <assignableValue> } ]
        private void CheckFunctionParams(bool required = false)
        {
            if (required)
            {
                CheckAssignableValue();
            }
            else
            {
                try { CheckAssignableValue(); }
                catch (ParserException) { return; }
            }

            wfp.AddFunctionParam();

            try { CheckFunctionParamFunctionParamDel(); }
            catch (ParserException) { return; }

            CheckFunctionParams(true);
        }
        private void CheckFunctionParamFunctionParamDel() => CheckLexeme("[,]", LexemeType.DL1, paramdelList);

        public void Parse()
        {
            Logs.Clear();
            index = 0;
            result = "";

            wfp.ResetLocals();

            // DEBUG: Check if after Begin
            for (int i = 0; i < lexemes.Count; i++)
            {
                if (lexemes[i].Value == "Begin" && lexemes[i].Type == LexemeType.KEY)
                {
                    index = i + 1;
                    Logs.Add(new ParserLog(ParserLogType.Start, null, "#N/A", result));
                    CheckIfStatement();
                    Logs.Add(new ParserLog(ParserLogType.Success, null, "#N/A", result));
                    return;
                }
            }

            var log = new ParserLog(ParserLogType.Mismatch, null, "Begin if", null);
            Logs.Add(log);
            throw new ParserException(log);
        }

    }
}
