#ifdef __APPLE__
#include <GLUT/glut.h>
#else
#include <GL/glut.h>
#endif

#include <math.h>
#include <vector>
#include <fstream>

using namespace std;

const char *POINTS_PATH = "Exercise1_2/points.txt";
const char *POINTS_ECO_PATH = "Exercise1_2/pointsEco.txt";

struct Point
{
    int x, y;
} currentPoint = {0, 0};

vector<Point> point;
vector<int> code;

void drawNormal();
void drawEco();
void readFromFile(const char *filePath, vector<Point> &pointVector, vector<int> &codeVector);
void reshape(int w, int h);
void display();

int main(int argc, char *argv[])
{
    glutInit(&argc, argv);                       // Инициализация GLUT
    glutInitDisplayMode(GLUT_SINGLE | GLUT_RGB); // Задание режима дисплея
    glutInitWindowSize(800, 600);                // Размер окна
    glutInitWindowPosition(1, 1);                // Позиция окна
    glutCreateWindow("Open GL Window");          // Создание окна
    glutReshapeFunc(reshape);                    // Функция обработки изменения размеров окна
    glutDisplayFunc(display);                    // Функция рисования изображения
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

    glColor3d(1, 0, 0); // установка цвета
    drawNormal();       // Неэкономный способ
    glColor3d(0, 1, 0); // установка цвета
    drawEco();          // Экономный способ

    glFlush(); // GLUT_SINGLE
}

void drawNormal()
{
    // Очистка векторов (если они использовался ранее)
    // Чтение из файла (координаты для неэкономного способа)
    point.clear();
    code.clear();
    readFromFile(POINTS_PATH, point, code);

    for (int i = 0; i < code.size(); i++)
    {
        if (code[i] == 1)
        { // если код 1, то отрисовка линии
            glBegin(GL_LINES);
            glVertex2i(currentPoint.x, currentPoint.y);
            glVertex2i(point[i].x, point[i].y);
            glEnd();
        } // если код 0 или конец итерации
        // запись координат текущей точки
        currentPoint.x = point[i].x;
        currentPoint.y = point[i].y;
    }
}

void drawEco()
{
    // Очистка векторов (если они использовался ранее)
    // Чтение из файла (координаты для экономного способа)
    point.clear();
    code.clear();
    readFromFile(POINTS_ECO_PATH, point, code);

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
}

void readFromFile(const char *filePath, vector<Point> &pointVector, vector<int> &codeVector)
{
    fstream f(filePath, ios::in);

    int count, tmpCode;
    Point tmpPoint;

    f >> count; // координаты
    for (int i = 0; i < count; i++)
    {
        f >> tmpPoint.x >> tmpPoint.y;
        pointVector.push_back(tmpPoint);
    }

    f >> count; // коды
    for (int i = 0; i < count; i++)
    {
        f >> tmpCode;
        codeVector.push_back(tmpCode);
    }

    f.close();
}
