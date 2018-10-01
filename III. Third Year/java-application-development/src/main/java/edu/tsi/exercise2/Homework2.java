package edu.tsi.exercise2;

import java.util.*;

public class Homework2 {

    public static void main(String[] args) {

        Map<String, String> awards = new HashMap();
        awards.put("Shakespeare", "Too much drama");
        awards.put("Swift", "Survival guide");
        awards.put("Austen", "Did not read");
        awards.put("Dumas", "Sweet revenge");

        List<Book> books = Arrays.asList(
                new Book("Dumas", "The Count of Monte Cristo", 1245)
                , new Book("Shakespeare", "Romeo and Juliet", 4500)
                , new Book("Austen", "Pride and Prejudice", 1000)
                , new Book("Swift", "Gulliver's Travels", 1000)
                , new Book("Tolstoy", "War and Peace", 1000)
        );

        Collections.sort(books);

        for (Book b : books) {
            String award = awards.get(b.getAuthor());

            if (award != null)
                award = String.format(", received %s award.", award);
            else
                award = ", received no award.";

            System.out.println(b.toString() + award);
        }
    }

}
