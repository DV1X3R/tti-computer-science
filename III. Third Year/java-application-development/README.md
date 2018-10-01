# Application development with Java (3 year / 2 semester)

## Exercise 1

День программиста отмечается ежегодно в 256 день года. Напишите консольное приложение, которое определяет на какую дату выпадет этот день в заданном году в интервале с 1800 до 2800. Не забудьте учесть високосные года, и что Латвия перешла с Юлианского на Григорианский календарь только в 1918 году. Переход состоялся 1 февраля 1918, за ним шло сразу 15 февраля 1918.

## Exercise 2

1. Создайте класс Book, который будет имплементировать интерфейс Comparable
2. У класса Book должны быть следующие поля:
```
private String author;
private String name;
private int timesPublished;
```
3. Перепешите метод класса compareTo, чтобы иметь возможность сравнивать книги:
* Сначала книги сравниваются по timesPublished, от меньшего к большему.
* Если для 2х книг поле timesPublished совпадает, то сравнивать также наименования книг в алфавитном порядке.
4. Перепешите метод класса toString, чтобы вызов этого метода возвращал строку с соответвующими значениями для текущего объекта класса Book:
```
The book “<book name>” by <book author> which sold <times published> copies
```
5. Создайте класс Homework2 с методом main
6. В методе main создайте Map awards со значениями:
```
Shakespeare, Too much drama
Swift, Survival guide
Austen, Did not read
Dumas, Sweet revenge
```
7. Также создайте List books со следующими книжками:
```
Автор, название книги, тираж:
"Dumas", "The Count of Monte Cristo", 1245
"Shakespeare", "Romeo and Juliet", 4500
"Austen", "Pride and Prejudice", 1000
"Swift", "Gulliver's Travels", 1000
"Tolstoy", "War and Peace", 1000
```
8. Отсортируйте books в возрастающем порядке.
9. Пройдитесь по списку и распечатайте значения в формате:
```
The book “<book name>” by <book author> which sold <times published> copies, received <award for this book> award
```
Примеры:
1. Если в Map есть информация о книгах автора, то
```
The book "Romeo and Juliet" by Shakespeare which sold 4500 copies, received Too much drama award.
```
2. Если информации нет:
```
The book "Romeo and Juliet" by Shakespeare which sold 4500 copies, received no award
```

## Exercise 3

При помощи потоков реализуйте следующее задание:

Есть пекарня, где пекарь печет маффины и выставляет их на витрину. В магазин заходят покупатели и покупают маффины. Потребитель может купить товар только при его наличии в пекарне, то есть, когда пекарь его добавил.
На консоль должно выводиться:
* Количество маффинов, которые изготавливает пекарь
* Количество маффинов, которое приобретает покупатель
Пример:
```
Baker baked: 10
Customer bought: 2 muffins
Customer bought: 1 muffins
Customer bought: 7 muffins
Baker baked: 2
Customer wants to buy: 6 muffins, but actually bought 2 
Baker baked: 3
Customer bought: 1 muffins
Customer wants to buy: 10 muffins, but actually bought 2
Baker baked: 10
Customer bought: 5 muffins
Customer bought: 2 muffins
Customer wants to buy: 10 muffins, but actually bought 3
...
```
Советы:
* Для данного задания подойдет шаблон Producer-Consumer
* Методы, которые отвечают за поставку и покупку маффинов должны быть синхронизированы.
