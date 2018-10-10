#ifdef __APPLE__
#include <GLUT/glut.h>
#else
#include <GL/glut.h>
#endif

#include <math.h>
#include <vector>
#include <fstream>

using namespace std;

const char *D_PATH = "Exercise2_4/letter_d.txt";
const char *P_PATH = "Exercise2_4/letter_p.txt";

struct Point
{
    int x, y;
} currentPoint = {0, 0};

struct FileObject
{
    vector<Point> points;
    vector<int> strips;
    vector<int> beziers;
} letter_d, letter_p;

void displayFileObject(FileObject &fo);
void readFromFile(const char *filePath, FileObject &fo);
void reshape(int w, int h);
void display();

int main(int argc, char *argv[])
{
    readFromFile(D_PATH, letter_d);
    readFromFile(P_PATH, letter_p);

    glutInit(&argc, argv);                       // Инициализация GLUT
    glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB); // Задание режима дисплея
    glutInitWindowSize(800, 600);                // Размер окна
    glutInitWindowPosition(1, 1);                // Позиция окна
    glutCreateWindow("Open GL Window");          // Создание окна
    glutReshapeFunc(reshape);                    // Функция обработки изменения размеров окна
    glutDisplayFunc(display);                    // Функция рисования изображения

    glColor3d(0, 0, 0);         // Установка цвета рисования
    glLineWidth(2);             // Установка толщины линии
    glEnable(GL_MAP1_VERTEX_3); // Активизация процедур отображения кривых

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

    displayFileObject(letter_d);
    displayFileObject(letter_p);

    glFlush(); // GLUT_SINGLE
}

void displayFileObject(FileObject &fo)
{
    // Strips
    for (int i = 0; i < fo.strips.size(); i++)
    {
        if (fo.strips[i] > 0)
        { // если код > 0 (+), то отрисовка линии с индексом [код - 1]
            glBegin(GL_LINES);
            glVertex2i(currentPoint.x, currentPoint.y);
            glVertex2i(fo.points[fo.strips[i] - 1].x, fo.points[fo.strips[i] - 1].y);
            glEnd();
        } // если код < 0 (-) или конец итерации
        // запись координат текущей точки: индекс = (код под модулю) - 1
        currentPoint.x = fo.points[abs(fo.strips[i]) - 1].x;
        currentPoint.y = fo.points[abs(fo.strips[i]) - 1].y;
    }

    // Beziers
    vector<int> tempBezier;
    for (int i = 0; i < fo.beziers.size(); i++)
    {
        int current = fo.beziers[i];
        tempBezier.push_back(current);
        int bezierCount = tempBezier.size();

        if (current < 0)
        {
            float currentBezier[bezierCount][3];
            for (int x = 0; x < bezierCount; x++)
            {
                currentBezier[x][0] = fo.points[abs(tempBezier[x]) - 1].x;
                currentBezier[x][1] = fo.points[abs(tempBezier[x]) - 1].y;
                currentBezier[x][2] = 0;
            }

            glMap1f( // Задание параметров кривых
                GL_MAP1_VERTEX_3,
                0.0, 1.0,              // Минимальное и максимальное значения параметра кривой t
                3,                     // Смещение между точками в массиве (stride)
                bezierCount,           // Число точек в массиве
                &currentBezier[0][0]); // Значения контрольных точек

            glBegin(GL_LINE_STRIP);
            for (int x = 0; x <= 30; x++)
                glEvalCoord1f((float)x / 30);
            glEnd();

            tempBezier.clear();
        }
    }
}

void readFromFile(const char *filePath, FileObject &fo)
{
    fstream f(filePath, ios::in);
    int count;

    // Points
    f >> count;
    Point tmpPoint;
    for (int i = 0; i < count; i++)
    {
        f >> tmpPoint.x >> tmpPoint.y;
        fo.points.push_back(tmpPoint);
    }

    // Strips
    f >> count;
    int tmpStrip;
    for (int i = 0; i < count; i++)
    {
        f >> tmpStrip;
        fo.strips.push_back(tmpStrip);
    }

    // Beziers
    f >> count;
    int tmpBezier;
    for (int i = 0; i < count; i++)
    {
        f >> tmpBezier;
        fo.beziers.push_back(tmpBezier);
    }
}
