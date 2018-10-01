% Simpsons Family Tree
% https://i2.wp.com/www.mycaelab.com/wp-content/uploads/2016/07/Simpsons_Family_Tree_by_Marruche_web-1.jpg

% Примечаение: В данной работе пара считается женатой, если у них есть ребёнок.
% Структура children(<ребёнок>, <мать>, <отец>).

children('Bart', 'Marge', 'Homer').
children('Lisa', 'Marge', 'Homer').
children('Maggie', 'Marge', 'Homer').

children('Homer', 'Mona', 'Abraham').
children('Herb', 'Mona', 'Abraham').

children('Marge', 'Jackie', 'Clancy').
children('Patty', 'Jackie', 'Clancy').
children('Selma', 'Jackie', 'Clancy').

children('Ling', 'Selma', 'Lings Father').

% для проверки сводности и троюродности
children('Winnie', 'Jackie', 'Felix').
children('Mona', 'Penelope', 'Zuck').
children('Quad', 'Penelope', 'Zuck').
children('Trunk', 'Emma', 'Quad').
children('Chuck', 'Jessica', 'Trunk').

female('Lisa'). female('Maggie'). female('Marge'). female('Mona').
female('Patty'). female('Selma'). female('Jackie'). female('Ling'). 
% для проверки сводности и троюродности
female('Penelope'). female('Emma'). female('Jessica').

male('Bart'). male('Homer'). male('Herb'). male('Abraham').
male('Clancy'). male('Lings Father').
% для проверки сводности и троюродности
male('Quad'). male('Zuck'). male('Trunk'). male('Chuck').
male('Winnie'). male('Felix').

child(X, Y) :- children(X, Y, _); children(X, _, Y).  % ребёнок X матери или отца Y
parent(X, Y) :- child(Y, X).  % родитель X ребёнка Y

mother(X, Y) :- parent(X, Y), female(X).  % родитель X ребёнка Y (женщина)
father(X, Y) :- parent(X, Y), male(X).  % родитель X ребёнка Y (мужчина)
daughter(X, Y) :- child(X, Y), female(X).  % ребёнок X родителя Y (женщина)
son(X, Y) :- child(X, Y), male(X).  % ребёнок X родителя Y (мужчина)

wife(X, Y) :- children(_, X, Y).  % жена Y мужа X
husband(X, Y) :- children(_, Y, X).  % муж X жены Y

stepmother(X, Y) :- father(A, Y), wife(X, A), mother(Z, Y), X \== Z.  % жена X отца человека Y, которая не является мамой человека Y (мачеха)
stepfather(X, Y) :- mother(A, Y), husband(X, A), father(Z, Y), X \== Z.  % муж X матери человека Y, который не является отцом человека Y (отчим)
stepdaughter(X, Y) :- (stepmother(Y, X); stepfather(Y, X)), female(X).  % женщина X с мачехой или отчимом Y (падчерица)
stepson(X, Y) :- (stepmother(Y, X); stepfather(Y, X)), male(X).  % мужчина X с мачехой или отчимом Y (пасынок)

sister(X, Y) :- parent(A, Y), daughter(X, A), X \== Y.  % дочь X родителя ребёнка Y, которая не является самим Y
brother(X, Y) :- parent(A, Y), son(X, A), X \== Y.  % сын X родителя ребёнка Y, который не является самим Y
stepsister(X, Y) :- (stepmother(A, Y); stepfather(A, Y)), daughter(X, A).  % дочь X мачехи или отчима Y
stepbrother(X, Y) :- (stepmother(A, Y); stepfather(A, Y)), son(X, A).  % сын X мачехи или отчима Y

grandparent(X, Y) :- parent(A, Y), parent(X, A).  % родитель X родителя ребёнка Y
grandmother(X, Y) :- grandparent(X, Y), female(X).  % родитель X родителя ребёнка Y (женщина)
grandfather(X, Y) :- grandparent(X, Y), male(X).  % родитель X родителя ребёнка Y (мужчина)
granddaughter(X, Y) :- grandparent(Y, X), female(X).  % потомок X прародителя Y (женщина)
grandson(X, Y) :- grandparent(Y, X), male(X).  % потомок X прародителя Y (мужчина)

aunt(X, Y) :- parent(A, Y), sister(X, A).  % сестра X родителя Y
uncle(X, Y) :- parent(A, Y), brother(X, A).  % брат X родителя Y
niece(X, Y) :- (aunt(Y, X); uncle(Y, X)), female(X).  % человек X с тётей или дядей Y (женщина)
nephew(X, Y) :- (aunt(Y, X); uncle(Y, X)), male(X).  % человек X с тётей или дядей Y (мужчина)

cousine(X, Y) :- (aunt(A, Y); uncle(A, Y)), daughter(X, A).  % дочь X тёти или дяди человека Y
cousin(X, Y) :- (aunt(A, Y); uncle(A, Y)), son(X, A).  % сын X тёти или дяди человека Y
second_cousine(X, Y) :- parent(A, Y), (cousine(Z, A); cousin(Z, A)), daughter(X, Z).  % дочь X кузена или кузины родителя Y (троюродная сестра)
second_cousin(X, Y) :- parent(A, Y), (cousine(Z, A); cousin(Z, A)), son(X, Z).  % сын X кузена или кузины родителя Y (троюродный брат)

daughter_in_law(X, Y) :- son(A, Y), wife(X, A).  % жена X сына человека Y (невестка)
mother_in_law_f(X, Y) :- husband(A, Y), mother(X, A).  % мать X мужа человека Y (свекровь)
father_in_law_f(X, Y) :- husband(A, Y), father(X, A).  % отец X мужа человека Y (свёкор)
son_in_law(X, Y) :- daughter(A, Y), husband(X, A).  % муж X дочери человека Y (зять)
mother_in_law_m(X, Y) :- wife(A, Y), mother(X, A).  % мать X жены человека Y (тёща)
father_in_law_m(X, Y) :- wife(A, Y), father(X, A).  % отец X жены человека Y (тесть)

sister_in_law_f(X, Y) :- wife(A, Y), sister(X, A).  % сестра X жены человека Y (свояченица)
sister_in_law_m(X, Y) :- husband(A, Y), sister(X, A).  % сестра X мужа человека Y (золовка)
brother_in_law_f(X, Y) :- wife(A, Y), brother(X, A).  % брат X жены человека Y (шурин)
brother_in_law_m(X, Y) :- husband(A, Y), brother(X, A).  % брат X мужа человека Y (деверь)
