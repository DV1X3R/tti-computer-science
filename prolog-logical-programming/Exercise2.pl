% Ввод размера списка
sizeIn(Size) :-
    write("Enter an array size >"), read(Size).

% Ввод границ диапазона
rangeIn(Min, Max) :-
    write("Enter min value >"), read(Min),
    write("Enter max value >"), read(Max).

% Заполнение списка
arrayIn([Head | Tail], Length) :-
    Length > 0, % если список заполнен - false (выход из рекурсии)
    LenghtR is Length - 1, % уменьшаем счётчик элементов
    write("Enter a number >"), read(Head),
    arrayIn(Tail, LenghtR). % рекурсивное заполнение списка
arrayIn([], 0). % true, при Length = 0

% Подсчёт элементов
arrayFind([Head | Tail], Min, Max, Count) :-
    arrayFind(Tail, Min, Max, CountR), % рекурсивный подсчёт в хвосте списка
    % если Head соответствует условиям, то инкремент счётчика, иначе - значение CountR
    ((Head > Min, Head < Max, Count is CountR + 1); Count is CountR).
arrayFind([], _, _, C) :- C is 0. % конец рекурсии - стартовая точка для Count

program :-
    sizeIn(Length), arrayIn(List, Length), rangeIn(Min, Max),
    write("List: "), writeln(List),
	arrayFind(List, Min, Max, Count), % узнаём количество элементов попадающих в диапазон
	write("Number of matching elements: "), writeln(Count).

