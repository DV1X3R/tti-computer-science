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
    Point xy0;
    int angle = 0;
} letter_d, letter_p;

void displayFileObject(FileObject &fo);
void readFromFile(const char *filePath, FileObject &fo);
void processNormalKeys(unsigned char key, int x, int y);
void processSpecialKeys(int key, int x, int y);
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
    glutKeyboardFunc(processNormalKeys);         // регистрации обратного вызова для событий клавиатуры
    glutSpecialFunc(processSpecialKeys);         // регистрации обратного вызова для событий клавиатуры

    glColor3d(0, 0, 0);         // Установка цвета рисования
    glLineWidth(1);             // Установка толщины линии
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

void processNormalKeys(unsigned char key, int x, int y)
{
    switch (key)
    {
    case 27: // ESC
        exit(0);
        break;
    case 43: // + - увеличение объектов
        glMatrixMode(GL_MODELVIEW);
        glTranslated(50, 50, 0);
        glScaled(1.2, 1.2, 1);
        glTranslated(-50, -50, 0);
        display();
        break;
    case 45: // - - уменьшение объектов
        glMatrixMode(GL_MODELVIEW);
        glTranslated(50, 50, 0);
        glScaled(1 / 1.2, 1 / 1.2, 1);
        glTranslated(-50, -50, 0);
        display();
        break;
    }
}

void processSpecialKeys(int key, int x, int y)
{
    switch (key)
    {
    case GLUT_KEY_UP:
        glMatrixMode(GL_MODELVIEW);
        glTranslated(0, 5, 0);
        display();
        break;
    case GLUT_KEY_DOWN:
        glMatrixMode(GL_MODELVIEW);
        glTranslated(0, -5, 0);
        display();
        break;
    case GLUT_KEY_LEFT:
        glMatrixMode(GL_MODELVIEW);
        glTranslated(-5, 0, 0);
        display();
        break;
    case GLUT_KEY_RIGHT:
        glMatrixMode(GL_MODELVIEW);
        glTranslated(5, 0, 0);
        display();
        break;
    case //GLUT_KEY_HOME: // HOME – вращение объектов против часовой стрелки
        GLUT_KEY_F1:
        glMatrixMode(GL_MODELVIEW);
        glTranslated(50, 50, 0);
        glRotated(15, 0, 0, 1);
        glTranslated(-50, -50, 0);
        display();
        break;
    case //GLUT_KEY_END: // END – вращение объектов по часовой стрелки
        GLUT_KEY_F2:
        glMatrixMode(GL_MODELVIEW);
        glTranslated(50, 50, 0);
        glRotated(-15, 0, 0, 1);
        glTranslated(-50, -50, 0);
        display();
        break;
    case //GLUT_KEY_PAGE_UP: // PG UP – вращение объектов (две буквы) в противоположные стороны – одна по часовой стрелке и в то же время вторая буква против часовой стрелки
        GLUT_KEY_F3:
        letter_d.angle -= 15;
        letter_p.angle += 15;
        display();
        break;
    }
}

void displayFileObject(FileObject &fo)
{
    // Каждый объект должен дополнительно поворачиваться индивидуально
    glMatrixMode(GL_MODELVIEW);
    glPushMatrix();
    glTranslated(fo.xy0.x, fo.xy0.y, 0);
    glRotated(fo.angle, 0, 0, 1);
    glTranslated(-fo.xy0.x, -fo.xy0.y, 0);

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

    glPopMatrix();
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

    // X0, Y0
    f >> fo.xy0.x;
    f >> fo.xy0.y;
}
