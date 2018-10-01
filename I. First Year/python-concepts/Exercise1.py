import random

print("3. Var \n First task - Numbers between 11 and 12 excluding 15 and 18:")
for x in range(11, 21):
    if x not in [15, 18]:
        print(x, end=', ')


print("\n\n Second task - Find min number from the list:")
mySize = input("Enter an array size> ")
if int(mySize) > 0:
    myNumbers = []
    for x in range(int(mySize)):
        print("Enter", x+1, "number> ", end='')
        myNumbers.append(input())
    minNumber = myNumbers[0]
    for x in range(int(mySize)):
        if int(minNumber) > int(myNumbers[x]):
            minNumber = myNumbers[x]
    print(minNumber)


print("\n Third task - Count [D, d, P, p] letters in the string:")


def count(text, letter):
    counter = text.count(letter)
    print(letter, "\t", counter, "\t", end='')
    for x in range(counter):
        print("*", end='')
    print()


text = input("Enter text> ")
count(text, 'D')
count(text, 'd')
count(text, 'P')
count(text, 'p')


print("\n Fourth task - show strings with -ed words:")
myAmount = 5
myStrings = []
for x in range(myAmount):
    print("Enter", x+1, "string> ", end='')
    myStrings.append(input())
for x in range(myAmount):
    if myStrings[x][-2:] == "ed":
        print(myStrings[x])

print("\n Fifth task - calculate the number of occurrence of 'a' letter in a string:")
print("'a' was shown", input("Enter text> ").lower().count('a'), "times")

print("\n Sixth task - array reversing:")
n = 20
a = []
for x in range(n):
    a.append(random.randint(1, 45))
    print(a[x], end=' ')
print()
a.reverse()
for x in range(n):
    print(a[x], end=' ')
