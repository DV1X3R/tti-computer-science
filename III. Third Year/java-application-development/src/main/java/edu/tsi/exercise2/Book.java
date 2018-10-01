package edu.tsi.exercise2;

public class Book implements Comparable<Book> {

    private String author;
    private String name;
    private int timesPublished;

    public String getAuthor() {
        return author;
    }

    public String getName() {
        return name;
    }

    public int getTimesPublished() {
        return timesPublished;
    }

    public Book(String BookAuthor, String BookName, int TimesPublished) {
        author = BookAuthor;
        name = BookName;
        timesPublished = TimesPublished;
    }

    @Override
    public String toString() {
        return String.format("The book \"%s\" by %s which sold %s copies", name, author, timesPublished);
    }

    @Override
    public int compareTo(Book o) {
        if (this.timesPublished != o.timesPublished)
            return this.timesPublished - o.timesPublished;
        else
            return this.name.compareTo(o.name);
    }

}
