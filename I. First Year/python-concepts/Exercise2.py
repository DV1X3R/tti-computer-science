import random

print("3. Var \n First task - useless functions...: \n", end='')


def ffrom():
    return 1


def fto():
    return 2


def fsize():
    return 40


myArray = []
for x in range(fsize()):
    myArray.append(random.randint(ffrom(), fto()))
for x in range(1, fsize()+1):
    print(myArray[x-1], end="\t")
    if x % 5 == 0:
        print()


print("\n Second task - matrix:")
matrixX = [[], [], [], []]
matrixY = [[], [], [], []]
matrixRes = [[0, 0, 0, 0], [0, 0, 0, 0], [0, 0, 0, 0], [0, 0, 0, 0]]
for x in range(4):
    for y in range(4):
        matrixX[x].append(random.randint(1, 10))
        matrixY[x].append(random.randint(1, 10))
for i in range(len(matrixX)):
    for j in range(len(matrixY[0])):
        for k in range(len(matrixY)):
            matrixRes[i][j] += matrixX[i][k] * matrixY[k][j]
print("Matrix A:")
for n in matrixX:
    print(n)
print("Matrix B:")
for n in matrixY:
    print(n)
print ("Multiplication result:")
for n in matrixRes:
    print(n)


print ("\n Third task - func multiplication:")


def multiplicate(a, b):
    return int(a)*int(b)


print(multiplicate(input("Enter first number> "), input("Enter second number> ")))


print ("\n Fourth task - accessing by links and values:")


def multiplicate(a, b, c):
    return (float(a)+float(b)+float(c))/3


objects = [input("Enter first number> "), input("Enter second number> "), input("Enter third number> ")]
links = objects
print("By variable: ", multiplicate(objects[0], objects[1], objects[2]))
print("By address", multiplicate(links[0], links[1], links[2]))


print ("\n Fifth task - recursion factorial:")


def fact(n):
    if int(n) == 0:
        return 1
    return fact(n-1)*n


print(fact(int(input("Enter number> "))))
