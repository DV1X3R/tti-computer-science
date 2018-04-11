% Simpsons Family Tree
% https://i2.wp.com/www.mycaelab.com/wp-content/uploads/2016/07/Simpsons_Family_Tree_by_Marruche_web-1.jpg

children('Bart', 'Homer', 'Marge').
children('Lisa', 'Homer', 'Marge').
children('Maggie', 'Homer', 'Marge').

children('Homer', 'Abraham', 'Mona').
children('Herb', 'Abraham', 'Mona').

children('Marge', 'Clancy', 'Jackie').
children('Patty', 'Clancy', 'Jackie').
children('Selma', 'Clancy', 'Jackie').

children('Ling', 'Unknown Father', 'Selma').

female('Lisa'). female('Maggie'). female('Marge'). female('Mona'). female('Jackie'). female('Patty'). female('Selma'). female('Ling').
male('Bart'). male('Homer'). male('Abraham'). male('Herb'). male('Clancy'). male('Unknown Father').

child(X, Y) :- children(X, Y, _); children(X, _, Y).  % Y - отец или мать
parent(X, Y) :- child(Y, X).  % родитель X ребёнка Y
mother(X, Y) :- parent(X, Y), female(X).  % родитель X ребёнка Y (женщина)
father(X, Y) :- parent(X, Y), male(X).  % родитель X ребёнка Y (мужчина)
daughter(X, Y) :- child(X, Y), female(X).  % ребёнок X родителя Y (женщина)
son(X, Y) :- child(X, Y), male(X).  % ребёнок X родителя Y (мужчина)
sister(X, Y) :- mother(A, Y), daughter(X, A), X \== Y.  % дочь X мамы ребёнка Y
brother(X, Y) :- mother(A, Y), son(X, A), X \== Y.  % сын X мамы ребёнка Y

grandparent(X, Y) :- parent(A, Y), parent(X, A).  % родитель X родителя ребёнка Y
grandmother(X, Y) :- grandparent(X, Y), female(X).  % родитель X родителя ребёнка Y (женщина)
grandfather(X, Y) :- grandparent(X, Y), male(X).  % родитель X родителя ребёнка Y (мужчина)

granddaughter(X, Y) :- grandparent(Y, X), female(X).  % потомок X прародителя Y (женщина)
grandson(X, Y) :- grandparent(Y, X), male(X).  % потомок X прародителя Y (мужчина)

aunt(X, Y) :- parent(A, Y), sister(X, A).  % сестра X родителя Y
uncle(X, Y) :- parent(A, Y), brother(X, A).  % брат X родителя Y

niece(X, Y) :- (aunt(Y, X); uncle(Y, X)), female(X).  % человек X с тётей Y или дядей Y (женщина)
nephew(X, Y) :- (aunt(Y, X); uncle(Y, X)), male(X).  % человек X с тётей Y или дядей Y (мужчина)

cousine(X, Y) :- (aunt(A, Y); uncle(A, Y)), daughter(X, A).  % дочь X тёти или дяди человека Y
cousin(X, Y) :- (aunt(A, Y); uncle(A, Y)), son(X, A).  % сын X тёти или дяди человека Y
