#ifdef __APPLE__
#include <GLUT/glut.h>
#else
#include <GL/glut.h>
#endif

#include <math.h>
#include <vector>
#include <fstream>

#define PI 3.141592653

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

int light_sample = 1;

void turnOnLight();
void turnOffLight();
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

    glutInit(&argc, argv);                                     // Инициализация GLUT
    glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA | GLUT_DEPTH); // Задание режима дисплея
    glutInitWindowSize(800, 600);                              // Размер окна
    glutInitWindowPosition(1, 1);                              // Позиция окна
    glutCreateWindow("Open GL Window");                        // Создание окна
    glutReshapeFunc(reshape);                                  // Функция обработки изменения размеров окна
    glutDisplayFunc(display);                                  // Функция рисования изображения
    glutKeyboardFunc(processNormalKeys);                       // регистрации обратного вызова для событий клавиатуры
    glutSpecialFunc(processSpecialKeys);                       // регистрации обратного вызова для событий клавиатуры

    glEnable(GL_DEPTH_TEST);
    glEnable(GL_CULL_FACE);
    glFrontFace(GL_CCW);
    glCullFace(GL_BACK);

    glClearColor(0.3, 0.3, 0.3, 0.0);
    glEnable(GL_LIGHTING);                           // рассчет освещения
    glLightModelf(GL_LIGHT_MODEL_TWO_SIDE, GL_TRUE); // двухсторонний расчет освещения
    //glEnable(GL_NORMALIZE);                          // автоматическое приведение нормалей к единичной длине
    //glShadeModel(GL_SMOOTH);
    //glEnable(GL_COLOR_MATERIAL);

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

    gluPerspective(60, 1, 0.1f, 500.0f);                            // центральная (перспективная) проекция
    gluLookAt(100.0, 100.0, 100.0, 50.0, 50.0, 0.0, 0.0, 1.0, 0.0); // Положение наблюдателя (точки наблюдения)
}

void display()
{
    glClear(GL_COLOR_BUFFER_BIT | GL_DEPTH_BUFFER_BIT);

    //glMatrixMode(GL_MODELVIEW);
    //glPushMatrix();
    //glLoadIdentity();
    //gluLookAt(100.0, 100.0, 100.0, 50.0, 50.0, 0.0, 0.0, 1.0, 0.0); // Положение наблюдателя (точки наблюдения)
    turnOnLight();
    //glPopMatrix();

    displayFileObject(letter_d);
    displayFileObject(letter_p);

    turnOffLight();

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
        break;
    case 43: // + - увеличение объектов
        glMatrixMode(GL_MODELVIEW);
        glTranslated(50, 50, 0);
        glScaled(1.2, 1.2, 1.2);
        glTranslated(-50, -50, 0);
        break;
    case 45: // - - уменьшение объектов
        glMatrixMode(GL_MODELVIEW);
        glTranslated(50, 50, 0);
        glScaled(1 / 1.2, 1 / 1.2, 1 / 1.2);
        glTranslated(-50, -50, 0);
        break;
    case '1':
        light_sample = 1;
        break;
    case '2':
        light_sample = 2;
        break;
    case '3':
        light_sample = 3;
        break;
    case '4':
        light_sample = 4;
        break;
    case '5':
        light_sample = 5;
        break;
    case '6':
        light_sample = 6;
        break;
    }
    glutPostRedisplay();
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

void turnOnLight()
{
    GLfloat material_diffuse[] = {1.0, 1.0, 1.0, 1.0}; // свойства материала
    glMaterialfv(GL_FRONT_AND_BACK, GL_DIFFUSE, material_diffuse);

    switch (light_sample)
    {
    case 1: // направленный источник света
    {
        GLfloat light0_diffuse[] = {0.4, 0.7, 0.2};
        GLfloat light0_direction[] = {0.0, 0.0, 1.0, 0.0};
        glEnable(GL_LIGHT0);
        glLightfv(GL_LIGHT0, GL_DIFFUSE, light0_diffuse);
        glLightfv(GL_LIGHT0, GL_POSITION, light0_direction);
        break;
    }
    case 2: // точечный источник света
        // убывание интенсивности с расстоянием
        // отключено (по умолчанию)
        {
            GLfloat light1_diffuse[] = {0.4, 0.7, 0.2};
            GLfloat light1_position[] = {0.0, 0.0, 1.0, 1.0};
            glEnable(GL_LIGHT1);
            glLightfv(GL_LIGHT1, GL_DIFFUSE, light1_diffuse);
            glLightfv(GL_LIGHT1, GL_POSITION, light1_position);
            break;
        }
    case 3: // точечный источник света
        // убывание интенсивности с расстоянием
        // задано функцией f(d) = 1.0 / (0.4 * d * d + 0.2 * d)
        {
            GLfloat light2_diffuse[] = {0.4, 0.7, 0.2};
            GLfloat light2_position[] = {0.0, 0.0, 1.0, 1.0};
            glEnable(GL_LIGHT2);
            glLightfv(GL_LIGHT2, GL_DIFFUSE, light2_diffuse);
            glLightfv(GL_LIGHT2, GL_POSITION, light2_position);
            glLightf(GL_LIGHT2, GL_CONSTANT_ATTENUATION, 0.0);
            glLightf(GL_LIGHT2, GL_LINEAR_ATTENUATION, 0.2);
            glLightf(GL_LIGHT2, GL_QUADRATIC_ATTENUATION, 0.4);
            break;
        }
    case 4: // прожектор
        // убывание интенсивности с расстоянием
        // отключено (по умолчанию)
        // половина угла при вершине 30 градусов
        // направление на центр плоскости
        {
            GLfloat light3_diffuse[] = {0.4, 0.7, 0.2};
            GLfloat light3_position[] = {0.0, 0.0, 1.0, 1.0};
            GLfloat light3_spot_direction[] = {0.0, 0.0, -1.0};
            glEnable(GL_LIGHT3);
            glLightfv(GL_LIGHT3, GL_DIFFUSE, light3_diffuse);
            glLightfv(GL_LIGHT3, GL_POSITION, light3_position);
            glLightf(GL_LIGHT3, GL_SPOT_CUTOFF, 30);
            glLightfv(GL_LIGHT3, GL_SPOT_DIRECTION, light3_spot_direction);
            break;
        }
    case 5: // прожектор
        // убывание интенсивности с расстоянием // отключено (по умолчанию)
        // половина угла при вершине 30 градусов // направление на центр плоскости
        // включен рассчет убывания интенсивности для прожектора
        {
            GLfloat light4_diffuse[] = {0.4, 0.7, 0.2};
            GLfloat light4_position[] = {0.0, 0.0, 1.0, 1.0};
            GLfloat light4_spot_direction[] = {0.0, 0.0, -1.0};
            glEnable(GL_LIGHT4);
            glLightfv(GL_LIGHT4, GL_DIFFUSE, light4_diffuse);
            glLightfv(GL_LIGHT4, GL_POSITION, light4_position);
            glLightf(GL_LIGHT4, GL_SPOT_CUTOFF, 30);
            glLightfv(GL_LIGHT4, GL_SPOT_DIRECTION, light4_spot_direction);
            glLightf(GL_LIGHT4, GL_SPOT_EXPONENT, 15.0);
            break;
        }
    case 6: // несколько источников света
    {
        GLfloat light5_diffuse[] = {1.0, 0.0, 0.0};
        GLfloat light5_position[] = {0.5 * cos(0.0), 0.5 * sin(0.0), 1.0, 1.0};
        glEnable(GL_LIGHT5);
        glLightfv(GL_LIGHT5, GL_DIFFUSE, light5_diffuse);
        glLightfv(GL_LIGHT5, GL_POSITION, light5_position);
        glLightf(GL_LIGHT5, GL_CONSTANT_ATTENUATION, 0.0);
        glLightf(GL_LIGHT5, GL_LINEAR_ATTENUATION, 0.4);
        glLightf(GL_LIGHT5, GL_QUADRATIC_ATTENUATION, 0.8);
        GLfloat light6_diffuse[] = {0.0, 1.0, 0.0};
        GLfloat light6_position[] = {0.5 * cos(2 * PI / 3), 0.5 * sin(2 * PI / 3),
                                     1.0, 1.0};
        glEnable(GL_LIGHT6);
        glLightfv(GL_LIGHT6, GL_DIFFUSE, light6_diffuse);
        glLightfv(GL_LIGHT6, GL_POSITION, light6_position);
        glLightf(GL_LIGHT6, GL_CONSTANT_ATTENUATION, 0.0);
        glLightf(GL_LIGHT6, GL_LINEAR_ATTENUATION, 0.4);
        glLightf(GL_LIGHT6, GL_QUADRATIC_ATTENUATION, 0.8);
        GLfloat light7_diffuse[] = {0.0, 0.0, 1.0};
        GLfloat light7_position[] = {0.5 * cos(4 * PI / 3), 0.5 * sin(4 * PI / 3),
                                     1.0, 1.0};
        glEnable(GL_LIGHT7);
        glLightfv(GL_LIGHT7, GL_DIFFUSE, light7_diffuse);
        glLightfv(GL_LIGHT7, GL_POSITION, light7_position);
        glLightf(GL_LIGHT7, GL_CONSTANT_ATTENUATION, 0.0);
        glLightf(GL_LIGHT7, GL_LINEAR_ATTENUATION, 0.4);
        glLightf(GL_LIGHT7, GL_QUADRATIC_ATTENUATION, 0.8);
        break;
    }
    }
}

void turnOffLight()
{
    glDisable(GL_LIGHT0);
    glDisable(GL_LIGHT1);
    glDisable(GL_LIGHT2);
    glDisable(GL_LIGHT3);
    glDisable(GL_LIGHT4);
    glDisable(GL_LIGHT5);
    glDisable(GL_LIGHT6);
    glDisable(GL_LIGHT7);
}

void displayFileObject(FileObject &fo)
{
    for (int i = 0; i < fo.faces.size(); i++)
    {
        glBegin(GL_QUADS);
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
