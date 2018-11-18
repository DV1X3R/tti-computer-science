using System;
using System.Collections.Generic;

namespace Compiler.Parser
{
    // 2. Синтаксический анализатор
    public class CompilerParser
    {
        private Action<ParserLog> logger;
        private List<Lexeme> lexemes;
        private int index;
        private string result;

        public CompilerParser(Action<ParserLog> logger)
        {
            this.logger = logger ?? (x => { });
        }

        private void CheckEOF(string expected)
        {
            if (index > lexemes.Count - 1)
            {
                var log = new ParserLog(ParserLogType.ThrowEOF, null, expected, result);
                logger(log);
                throw new ParserException(log);
            }
        }

        // <ifStatement> := if <logicalExpression> then <statement>
        private void CheckIfStatement()
        {
            CheckIf();
            CheckLogicalExpression();
            CheckThen();
            CheckAssignmentStatement();
        }
        private void CheckIf()
        {
            string expected = "[if]";

            CheckEOF(expected);
            var lexeme = lexemes[index];
            if (lexeme.Value == "if" && lexeme.Type == LexemeType.KEY)
            {
                result += lexeme.Value + " ";
                logger(new ParserLog(ParserLogType.Ok, lexeme, expected, result));
                index += 1;
            }
            else
            {
                var log = new ParserLog(ParserLogType.ThowNotFound, lexeme, expected, result);
                logger(log);
                throw new ParserException(log);
            }
        }
        private void CheckThen()
        {
            string expected = "[then]";

            CheckEOF(expected);
            var lexeme = lexemes[index];
            if (lexeme.Value == "then" && lexeme.Type == LexemeType.KEY)
            {
                result += lexeme.Value + " ";
                logger(new ParserLog(ParserLogType.Ok, lexeme, expected, result));
                index += 1;
            }
            else
            {
                var log = new ParserLog(ParserLogType.ThowNotFound, lexeme, expected, result);
                logger(log);
                throw new ParserException(log);
            }
        }

        // <logicalExpression> := <boolExpression> { <logicalOperator> <boolExpression> }
        private void CheckLogicalExpression()
        {
            CheckBoolExpression();
            try { CheckLogicalOperator(); }
            catch (ParserException) { return; }
            CheckLogicalExpression();
        }

        // <boolExpression> := <comparableValue> [ <boolOperator> <comparableValue> ]
        private void CheckBoolExpression()
        {
            CheckComparableValue();
            try { CheckBoolOperator(); }
            catch (ParserException) { return; }
            CheckComparableValue();
        }


        // <comparableValue> := <identifier> | <integer>
        private void CheckComparableValue()
        {
            string expected = "<comparableValue>";

            CheckEOF(expected);
            var lexeme = lexemes[index];
            if (lexeme.Type == LexemeType.IDN || lexeme.Type == LexemeType.INT)
            {
                result += lexeme.Value + " ";
                logger(new ParserLog(ParserLogType.Ok, lexeme, expected, result));
                index += 1;
            }
            else
            {
                var log = new ParserLog(ParserLogType.ThowNotFound, lexeme, expected, result);
                logger(log);
                throw new ParserException(log);
            }
        }

        // <logicalOperator> := and | or
        private void CheckLogicalOperator()
        {
            string expected = "<logicalOperator>";

            CheckEOF(expected);
            var lexeme = lexemes[index];
            if ((lexeme.Value == "and" || lexeme.Value == "or") && lexeme.Type == LexemeType.KEY)
            {
                result += lexeme.Value + " ";
                logger(new ParserLog(ParserLogType.Ok, lexeme, expected, result));
                index += 1;
            }
            else
            {
                var log = new ParserLog(ParserLogType.ThowNotFound, lexeme, expected, result);
                logger(log);
                throw new ParserException(log);
            }
        }

        // <boolOperator> := = | < | > | <= | >=
        private void CheckBoolOperator()
        {
            string expected = "<boolOperator>";

            CheckEOF(expected);
            var lexeme = lexemes[index];
            if (((lexeme.Value == "=" || lexeme.Value == "<" || lexeme.Value == ">") && lexeme.Type == LexemeType.DL1) ||
                ((lexeme.Value == "<=" || lexeme.Value == ">=") && lexeme.Type == LexemeType.DL2))
            {
                result += lexeme.Value + " ";
                logger(new ParserLog(ParserLogType.Ok, lexeme, expected, result));
                index += 1;
            }
            else
            {
                var log = new ParserLog(ParserLogType.ThowNotFound, lexeme, expected, result);
                logger(log);
                throw new ParserException(log);
            }
        }


        // <assignmentStatement> := <variableName> := <assignableValue> ;
        private void CheckAssignmentStatement(int seed = 0)
        {
            int recIndex = index;
            string recResult = result;

            CheckVariableName();

            CheckAssignmentEqSign();
            CheckAssignableValue(seed);

            try { CheckAssignmentEndSign(); return; }
            catch (ParserException)
            {
                index = recIndex;
                result = recResult;
                CheckAssignmentStatement(seed + 1);
            }
        }
        private void CheckAssignmentEqSign()
        {
            string expected = "[:=]";

            CheckEOF(expected);
            var lexeme = lexemes[index];
            if (lexeme.Value == ":=" && lexeme.Type == LexemeType.DL2)
            {
                result += lexeme.Value + " ";
                logger(new ParserLog(ParserLogType.Ok, lexeme, expected, result));
                index += 1;
            }
            else
            {
                var log = new ParserLog(ParserLogType.ThowNotFound, lexeme, expected, result);
                logger(log);
                throw new ParserException(log);
            }
        }
        private void CheckAssignmentEndSign()
        {
            string expected = "[;]";

            CheckEOF(expected);
            var lexeme = lexemes[index];
            if (lexeme.Value == ";" && lexeme.Type == LexemeType.DL1)
            {
                result += lexeme.Value + " ";
                logger(new ParserLog(ParserLogType.Ok, lexeme, expected, result));
                index += 1;
            }
            else
            {
                var log = new ParserLog(ParserLogType.ThowNotFound, lexeme, expected, result);
                logger(log);
                throw new ParserException(log);
            }
        }

        // <variableName> := <identifier>
        private void CheckVariableName()
        {
            string expected = "<variableName>";

            CheckEOF(expected);
            var lexeme = lexemes[index];
            if (lexeme.Type == LexemeType.IDN)
            {
                result += lexeme.Value + " ";
                logger(new ParserLog(ParserLogType.Ok, lexeme, expected, result));
                index += 1;
            }
            else
            {
                var log = new ParserLog(ParserLogType.ThowNotFound, lexeme, expected, result);
                logger(log);
                throw new ParserException(log);
            }
        }

        // <assignableValue> := <comparableValue> | <boolExpression> | <function> 
        private void CheckAssignableValue(int seed)
        {
            string expected = "<assignableValue>";

            switch (seed)
            {
                case 0: CheckComparableValue(); break;
                case 1: CheckBoolExpression(); break;
                case 2: CheckFunction(); break;
                default:
                    CheckEOF(expected);
                    var lexeme = lexemes[index];
                    var log = new ParserLog(ParserLogType.ThowNotFound, lexeme, expected, result);
                    logger(log);
                    throw new ParserException(log);
            }
        }


        // <function> := <functionName> ( <functionParam> )
        private void CheckFunction()
        {
            CheckFunctionName();
            CheckFunctionParamStartSign();
            CheckFunctionParam();
            CheckFunctionParamEndSign();
        }
        private void CheckFunctionParamStartSign()
        {
            string expected = "[(]";

            CheckEOF(expected);
            var lexeme = lexemes[index];
            if (lexeme.Value == "(" && lexeme.Type == LexemeType.DL1)
            {
                result += lexeme.Value + " ";
                logger(new ParserLog(ParserLogType.Ok, lexeme, expected, result));
                index += 1;
            }
            else
            {
                var log = new ParserLog(ParserLogType.ThowNotFound, lexeme, expected, result);
                logger(log);
                throw new ParserException(log);
            }
        }
        private void CheckFunctionParamEndSign()
        {
            string expected = "[)]";

            CheckEOF(expected);
            var lexeme = lexemes[index];
            if (lexeme.Value == ")" && lexeme.Type == LexemeType.DL1)
            {
                result += lexeme.Value + " ";
                logger(new ParserLog(ParserLogType.Ok, lexeme, expected, result));
                index += 1;
            }
            else
            {
                var log = new ParserLog(ParserLogType.ThowNotFound, lexeme, expected, result);
                logger(log);
                throw new ParserException(log);
            }
        }

        // <functionName> := <identifier>
        private void CheckFunctionName()
        {
            string expected = "<functionName>";

            CheckEOF(expected);
            var lexeme = lexemes[index];
            if (lexeme.Type == LexemeType.IDN)
            {
                result += lexeme.Value + " ";
                logger(new ParserLog(ParserLogType.Ok, lexeme, expected, result));
                index += 1;
            }
            else
            {
                var log = new ParserLog(ParserLogType.ThowNotFound, lexeme, expected, result);
                logger(log);
                throw new ParserException(log);
            }
        }

        // <functionParam> :=  [ <comparableValue> { , <comparableValue> } ]
        private void CheckFunctionParam(bool required = false)
        {
            if (required)
            {
                CheckComparableValue();
            }
            else
            {
                try { CheckComparableValue(); }
                catch (ParserException) { return; }
            }
            try { CheckFunctionParamFunctionParamDel(); }
            catch (ParserException) { return; }
            CheckFunctionParam(true);
        }
        private void CheckFunctionParamFunctionParamDel()
        {
            string expected = "[,]";

            CheckEOF(expected);
            var lexeme = lexemes[index];
            if (lexeme.Value == "," && lexeme.Type == LexemeType.DL1)
            {
                result += lexeme.Value + " ";
                logger(new ParserLog(ParserLogType.Ok, lexeme, expected, result));
                index += 1;
            }
            else
            {
                var log = new ParserLog(ParserLogType.ThowNotFound, lexeme, expected, result);
                logger(log);
                throw new ParserException(log);
            }
        }

        public void Parse(List<Lexeme> lexemes)
        {
            this.lexemes = lexemes;
            this.index = 0;
            this.result = "";

            // DEBUG: Check only first IF
            for (int i = 0; i < lexemes.Count; i++)
            {
                if (lexemes[i].Value == "if" && lexemes[i].Type == LexemeType.KEY)
                {
                    index = i;
                    logger(new ParserLog(ParserLogType.Start, null, "#N/A", result));
                    CheckIfStatement();
                    logger(new ParserLog(ParserLogType.Success, null, "#N/A", result));
                    break;
                }
            }
        }

    }
}
