#ifdef __APPLE__
#include <GLUT/glut.h>
#else
#include <GL/glut.h>
#endif

#include <math.h>
#include <vector>
#include <fstream>

using namespace std;

const char *D_PATH = "Exercise5_1/letter_d.txt";
const char *P_PATH = "Exercise5_1/letter_p.txt";

struct Point
{
    int x, y, z;
};

struct FileObject
{
    vector<Point> points;
    vector< vector<int> > faces;
} letter_d, letter_p;

bool ortho = true;

void displayFileObject(FileObject &fo);
void readFromFile(const char *filePath, FileObject &fo);
void draw3Axis(double from, double to);
void initOrtho();
void initPerspective();
void processNormalKeys(unsigned char key, int x, int y);
void processSpecialKeys(int key, int x, int y);
void reshape(int w, int h);
void display();

int main(int argc, char *argv[])
{
    readFromFile(D_PATH, letter_d);
    readFromFile(P_PATH, letter_p);

    glutInit(&argc, argv);                        // Инициализация GLUT
    glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA); // Задание режима дисплея
    glutInitWindowSize(800, 600);                 // Размер окна
    glutInitWindowPosition(1, 1);                 // Позиция окна
    glutCreateWindow("Open GL Window");           // Создание окна
    glutReshapeFunc(reshape);                     // Функция обработки изменения размеров окна
    glutDisplayFunc(display);                     // Функция рисования изображения
    glutKeyboardFunc(processNormalKeys);          // регистрации обратного вызова для событий клавиатуры
    glutSpecialFunc(processSpecialKeys);          // регистрации обратного вызова для событий клавиатуры

    glutMainLoop();

    return 0;
}

void reshape(int w, int h)
{
    glViewport(0, 0, w, h);      // настройка окна: порт просмотра (область вывода)
    glMatrixMode(GL_PROJECTION); // выбор матрицы, над которой будет производится работа
    glLoadIdentity();            // заменяет текущую матрицу на единичную
    glMatrixMode(GL_MODELVIEW);  // выбор матрицы, над которой будет производится работа
    glLoadIdentity();            // заменяет текущую матрицу на единичную

    if (ortho)
        initOrtho();
    else
        initPerspective();
}

void initOrtho()
{
    glOrtho(0, 100, 0, 100, 0, 100); // создание матрицы проекций в усеченном объем видимости (параллелепипед видимости) в левосторонней системе координат
}

void initPerspective()
{
    gluPerspective(60, 1, 0.1f, 500.0f);                            // центральная (перспективная) проекция
    gluLookAt(100.0, 100.0, 100.0, 50.0, 50.0, 0.0, 0.0, 1.0, 0.0); // Положение наблюдателя (точки наблюдения)
}

void display()
{
    glClearColor(1, 1, 1, 0);     // Установка цвета экрана
    glClear(GL_COLOR_BUFFER_BIT); // Очищение окна установленным цветом очистки

    draw3Axis(-5, 5); // Отрисовка осей

    displayFileObject(letter_d);
    displayFileObject(letter_p);

    glutSwapBuffers(); // GLUT_DOUBLE
}

void processNormalKeys(unsigned char key, int x, int y)
{
    switch (key)
    {
    case 27: // ESC
        exit(0);
        break;
    case 13: // ENTER
        readFromFile(D_PATH, letter_d);
        readFromFile(P_PATH, letter_p);
        display();
        break;
    case 32:
        ortho = !ortho;
        reshape(glutGet(GLUT_WINDOW_WIDTH), glutGet(GLUT_WINDOW_HEIGHT));
        display();
        break;
    case 43: // + - увеличение объектов
        glMatrixMode(GL_MODELVIEW);
        glTranslated(50, 50, 0);
        glScaled(1.2, 1.2, 1.2);
        glTranslated(-50, -50, 0);
        display();
        break;
    case 45: // - - уменьшение объектов
        glMatrixMode(GL_MODELVIEW);
        glTranslated(50, 50, 0);
        glScaled(1 / 1.2, 1 / 1.2, 1 / 1.2);
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
    case //GLUT_KEY_HOME: // HOME – вращение объектов против часовой стрелки вокруг оси OX
        GLUT_KEY_F1:
        glMatrixMode(GL_MODELVIEW);
        glTranslated(50, 50, 0);
        glRotated(10, 1, 0, 0);
        glTranslated(-50, -50, 0);
        display();
        break;
    case //GLUT_KEY_END: // END – вращение объектов против часовой стрелки вокруг оси OY
        GLUT_KEY_F2:
        glMatrixMode(GL_MODELVIEW);
        glTranslated(50, 50, 0);
        glRotated(10, 0, 1, 0);
        glTranslated(-50, -50, 0);
        display();
        break;
    case //GLUT_KEY_DELETE: // DELETE – вращение объектов против часовой стрелки вокруг оси OZ
        GLUT_KEY_F3:
        glMatrixMode(GL_MODELVIEW);
        glTranslated(50, 50, 0);
        glRotated(10, 0, 0, 1);
        glTranslated(-50, -50, 0);
        display();
        break;
    case //GLUT_KEY_PAGE_UP: // PG UP – вращение объектов по часовой стрелки вокруг всех трех осей одновременно
        GLUT_KEY_F4:
        glMatrixMode(GL_MODELVIEW);
        glTranslated(50, 50, 0);
        glRotated(10, 1, 1, 1);
        glTranslated(-50, -50, 0);
        display();
        break;
    case //GLUT_KEY_PAGE_DOWN: // PG UP – вращение объектов против часовой стрелки вокруг всех трех осей одновременно
        GLUT_KEY_F5:
        glMatrixMode(GL_MODELVIEW);
        glTranslated(50, 50, 0);
        glRotated(-10, 1, 1, 1);
        glTranslated(-50, -50, 0);
        display();
        break;
    }
}

void draw3Axis(double from, double to)
{
    glMatrixMode(GL_MODELVIEW);
    glPushMatrix();   // Сохранение предыдущей матрицы
    glLoadIdentity(); // Заменить на единичную

    if (ortho)
        initOrtho();
    else
        initPerspective();

    glTranslated(50, 50, 0);

    glBegin(GL_LINES);      // каждая отдельная пара вершин задает линию
    glColor3d(1, 0, 0);     // red
    glVertex3f(from, 0, 0); // x from
    glVertex3f(to, 0, 0);   // x to
    glColor3d(0, 1, 0);     // green
    glVertex3f(0, from, 0); // y from
    glVertex3f(0, to, 0);   // y to
    glColor3d(0, 0, 1);     // blue
    glVertex3f(0, 0, from); // z from
    glVertex3f(0, 0, to);   // z to
    glEnd();

    glPopMatrix(); // Восстановить исходную матрицу
}

void displayFileObject(FileObject &fo)
{
    for (int i = 0; i < fo.faces.size(); i++)
    {
        glBegin(GL_LINE_LOOP);
        for (int j = 0; j < fo.faces[i].size(); j++)
        {
            int x = fo.points[fo.faces[i][j]].x;
            int y = fo.points[fo.faces[i][j]].y;
            int z = fo.points[fo.faces[i][j]].z;

            if (z < 0)
                glColor3f(0.0, 1.0, 0.0);
            else
                glColor3f(1.0, 0.0, 0.0);

            glVertex3f(x, y, z);
        }
        glEnd();
    }
}

void readFromFile(const char *filePath, FileObject &fo)
{
    memset(&fo, 0, sizeof(fo));
    fstream f(filePath, ios::in);
    int count, facesCount;

    // Points
    f >> count;
    Point tmpPoint;
    for (int i = 0; i < count; i++)
    {
        f >> tmpPoint.x >> tmpPoint.y >> tmpPoint.z;
        fo.points.push_back(tmpPoint);
    }

    // Faces
    f >> count;
    vector<int> tmpFaces;
    for (int i = 0; i < count; i++)
    {
        f >> facesCount;
        for (int n = 0; n < facesCount; n++)
        {
            int p;
            f >> p;
            tmpFaces.push_back(p);
        }
        fo.faces.push_back(tmpFaces);
        tmpFaces.clear();
    }
}
