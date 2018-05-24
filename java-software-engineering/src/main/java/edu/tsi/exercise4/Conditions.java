package edu.tsi.exercise4;

public class Conditions {

    public enum language {
        none,
        csharp,
        python,
        c,
        java
    }

    public static language languageSelector(boolean answer1, boolean answer2) {
        language value = language.none;
        System.out.println("\t ______");
        System.out.println("1. Easy way or hard way: " + (answer1 ? "Hard" : "Easy"));
        System.out.println("2. Job or startup: " + (answer2 ? "Startup" : "Job"));
        if(!answer1 && !answer2) value = language.csharp;
        else if(!answer1 && answer2) value = language.python;
        else if(answer1 && !answer2) value = language.c;
        else if(answer1 && answer2) value = language.java;
        System.out.println("Language: " + value.toString());
        return value;
    }

}
