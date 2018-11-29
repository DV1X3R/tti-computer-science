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
            result += lexemes[index].Value + " ";
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
            CheckAssignmentStatement();
        }
        private void CheckIf() => CheckLexeme("[if]", LexemeType.KEY, ifList);
        private void CheckThen() => CheckLexeme("[then]", LexemeType.KEY, thenList);

        // <logicalExpression> := <boolExpression> { <logicalOperator> <boolExpression> }
        private void CheckLogicalExpression()
        {
            CheckBoolExpression();
            try { CheckLogicalOperator(); }
            catch (ParserException) { return; }
            CheckLogicalExpression();
        }

        // <boolExpression> := <assignableValue> [ <boolOperator> <assignableValue> ]
        private void CheckBoolExpression()
        {
            CheckAssignableValue();
            try { CheckBoolOperator(); }
            catch (ParserException) { return; }
            CheckAssignableValue();
        }

        // <logicalOperator> := and | or
        private void CheckLogicalOperator() => CheckLexeme("<logicalOperator>", LexemeType.KEY, logicaloperatorList);

        // <boolOperator> := = | < | >
        private void CheckBoolOperator() => CheckLexeme("<boolOperator>", LexemeType.DL1, booloperatorList);

        // <assignmentStatement> := <variableName> := <assignableValue> ;
        private void CheckAssignmentStatement()
        {
            CheckVariableName();
            CheckAssignmentEqSign();
            CheckAssignableValue();
            CheckAssignmentEndSign();
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

            try { CheckFunction(); return; }
            catch (ParserException)
            {
                index = recIndex;
                result = recResult;
            }

            try { CheckLexeme("<identifier>", LexemeType.IDN); return; }
            catch (ParserException)
            {
                index = recIndex;
                result = recResult;
            }

            try { CheckLexeme("<integer>", LexemeType.INT); return; }
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
            CheckFunctionParamStartSign();
            CheckFunctionParam();
            CheckFunctionParamEndSign();
        }
        private void CheckFunctionParamStartSign() => CheckLexeme("[(]", LexemeType.DL1, paramstartList);
        private void CheckFunctionParamEndSign() => CheckLexeme("[)]", LexemeType.DL1, paramendList);

        // <functionName> := <identifier>
        private void CheckFunctionName() => CheckLexeme("<functionName>", LexemeType.IDN);

        // <functionParam> :=  [ <assignableValue> { , <assignableValue> } ]
        private void CheckFunctionParam(bool required = false)
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
            try { CheckFunctionParamFunctionParamDel(); }
            catch (ParserException) { return; }
            CheckFunctionParam(true);
        }
        private void CheckFunctionParamFunctionParamDel() => CheckLexeme("[,]", LexemeType.DL1, paramdelList);

        public void Parse()
        {
            Logs.Clear();
            index = 0;
            result = "";

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
