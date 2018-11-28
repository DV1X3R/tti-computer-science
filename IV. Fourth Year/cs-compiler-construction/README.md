# Compiler Construction (C#) (4 year / 1 semester)

* Приложение 1. «Программирование лексического анализатора»  
1. Произвести анализ заданного программного фрагмента на языке PASCAL и выделить все типы имеющихся в нем лексем.  
2. Сформировать таблицу служебных и ключевых слов и разделителей, имеющихся в заданном фрагменте.  
3. Для выделенных лексем построить регулярную грамматику.  
4. Для полученной грамматики построить диаграмму состояний соответствующего конечного автомата.  
5. На основе автомата составить и отладить программу лексического анализатора.  

```
<keywords> := procedure | var | Byte | Char | array | of | Longint | String | Begin | if | and | then | else | End | or
<delimiters1> := . | ; | : | [ | ] | = | ( | ) | , | - | > | <
<dot> := .
<colon> := :
<delimiters2> := <dot> . | <colon> =
<identifier> := letter | <identifier> letter | <identifier> digit
<number> := digit | <number> digit 
<letter> := A | ... | Z | a | ... | z
<digit> := 0 | ... | 9
```

* Приложение 2. «Программирование синтаксического анализатора»  
1. Для заданного фрагмента программы на Паскале разработать грамматику для построения грамматического разбора с помощью рекурсивного спуска.  
2. В качестве процедуры SCAN использовать лексический анализатор, сконструированный в лабораторной работе No1.  
3. Написать и отладить процедуры рекурсивного спуска.  
4. Спроектировать тест для проверки работо- споcобности синтаксического анализатора.  
  
```
if State and sfDragging = 0 then Color := GetColor(1);

<ifStatement> := if <logicalExpression> then <assignmentStatement>

<logicalExpression> := <boolExpression> { <logicalOperator> <boolExpression> }
<boolExpression> := <assignableValue> [ <boolOperator> <assignableValue> ]

<logicalOperator> := and | or
<boolOperator> := = | < | >

<assignmentStatement> := <variableName> := <assignableValue> ;
<variableName> := <identifier>
<assignableValue> := <function> | <identifier> | <integer>

<function> := <functionName> ( <functionParam> )
<functionName> := <identifier>
<functionParam> :=  [ <comparableValue> { , <comparableValue> } ]
```

* Программный фрагмент
```pascal
procedure TIndicator.Draw;  
var  
	Color: Byte; Frame: Char;  
	L: array[0..1] of Longint;  
	S: String[15];  
	B: TDrawBuffer;  
Begin  
	if State and sfDragging = 0 then Color := GetColor(1);  
	else  
	MoveChar(B, Frame, Color, Size.X);  
	if Modified then WordRec(B[0].Lo := 15);  
	FormatStr(S, ' %d:%d ', L);  
	MoveStr(B[8 - Pos(':', S)], S, Color);  
	WriteBuf(0, 0, Size.X, 1, B);  
End;  
```
  
## But how does it look?
![Oops. Image was here](https://raw.githubusercontent.com/DV1X3R/tti-computer-science/master/IV.%20Fourth%20Year/cs-compiler-construction/screenshot.png)
