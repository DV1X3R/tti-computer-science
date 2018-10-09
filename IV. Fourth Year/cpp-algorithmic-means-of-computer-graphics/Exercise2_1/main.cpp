#ifdef __APPLE__
#include <GLUT/glut.h>
#else
#include <GL/glut.h>
#endif

#include <math.h>
#include <vector>
#include <fstream>
#include <iostream>

using namespace std;

const char *POINTS_PATH = "Exercise1_3/points.txt";
const char *CODES_PATH = "Exercise1_3/codes.txt";

struct Point
{
    int x, y;
} currentPoint = {0, 0};

vector<Point> point;
vector<int> code;
GLdouble plane[4] = {-1, 1, 0, 0}; // нижний правый угол (-x + y = 0)
//GLdouble plane[4] = {-1, -1, 0, 100}; // верхний правый угол (-x - y + 100 = 0)
//GLdouble plane[4] = {1, 1, 0, -100}; // нижний левый угол (x + y - 100 = 0)
//GLdouble plane[4] = {1, -1, 0, 0}; // верхний левый угол (x - y = 0)

void readPointsFromFile(const char *filePath, vector<Point> &pointVector);
void readCodesFromFile(const char *filePath, vector<int> &codeVector);
void reshape(int w, int h);
void display();

int main(int argc, char *argv[])
{
    readPointsFromFile(POINTS_PATH, point);
    readCodesFromFile(CODES_PATH, code);

    glutInit(&argc, argv);                        // Инициализация GLUT
    glutInitDisplayMode(GLUT_SINGLE | GLUT_RGBA); // Задание режима дисплея
    glutInitWindowSize(800, 600);                 // Размер окна
    glutInitWindowPosition(1, 1);                 // Позиция окна
    glutCreateWindow("Open GL Window");           // Создание окна
    glutReshapeFunc(reshape);                     // Функция обработки изменения размеров окна
    glutDisplayFunc(display);                     // Функция рисования изображения

    glColor3d(0, 0, 0); // Установка цвета рисования
    GLint *planesCount = new GLint();
    glGetIntegerv(GL_MAX_CLIP_PLANES, planesCount); // Узнать количество плоскостей
    printf("Clip Planes Count: %i \n", *planesCount);
    glClipPlane(GL_CLIP_PLANE0, plane); // Задание плоскости отсечения
    glEnable(GL_CLIP_PLANE0); // Включение плоскости отсечения

    glutMainLoop();

    return 0;
}

void reshape(int w, int h)
{
    glViewport(0, 0, w, h);      // настройка окна: порт просмотра (область вывода)
    glMatrixMode(GL_PROJECTION); // выбор матрицы, над которой будет производится работа
    glLoadIdentity();            // заменяет текущую матрицу на единичную
    gluOrtho2D(0, 100, 0, 100);  // устанавливает для окна левый нижний угол (left, bottom) и правый верхний угол (right, top) в мировых координатах
    glMatrixMode(GL_MODELVIEW);  // выбор матрицы, над которой будет производится работа
    glLoadIdentity();            // заменяет текущую матрицу на единичную
}

void display()
{
    glClearColor(1, 1, 1, 0);     // Установка цвета экрана
    glClear(GL_COLOR_BUFFER_BIT); // Очищение окна установленным цветом очистки

    for (int i = 0; i < code.size(); i++)
    {
        if (code[i] > 0)
        { // если код > 0 (+), то отрисовка линии с индексом [код - 1]
            glBegin(GL_LINES);
            glVertex2i(currentPoint.x, currentPoint.y);
            glVertex2i(point[code[i] - 1].x, point[code[i] - 1].y);
            glEnd();
        } // если код < 0 (-) или конец итерации
        // запись координат текущей точки: индекс = (код под модулю) - 1
        currentPoint.x = point[abs(code[i]) - 1].x;
        currentPoint.y = point[abs(code[i]) - 1].y;
    }

    glFlush(); // GLUT_SINGLE
}

void readPointsFromFile(const char *filePath, vector<Point> &pointVector)
{
    fstream f(filePath, ios::in);
    Point tmpPoint;

    while (f)
    {
        f >> tmpPoint.x >> tmpPoint.y;
        pointVector.push_back(tmpPoint);
    }

    f.close();
}

void readCodesFromFile(const char *filePath, vector<int> &codeVector)
{
    fstream f(filePath, ios::in);
    int tmpCode;

    while (f)
    {
        f >> tmpCode;
        codeVector.push_back(tmpCode);
    }

    f.close();
}
