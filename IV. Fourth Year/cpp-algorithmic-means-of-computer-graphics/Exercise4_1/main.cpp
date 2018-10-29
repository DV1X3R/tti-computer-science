#ifdef __APPLE__
#include <GLUT/glut.h>
#else
#include <GL/glut.h>
#endif

#include <cstdlib>

bool ortho = true;
int figure = 1;

void draw3Axis(double from, double to);
void initOrtho();
void initPerspective();
void processNormalKeys(unsigned char key, int x, int y);
void processSpecialKeys(int key, int x, int y);
void reshape(int w, int h);
void display();

int main(int argc, char *argv[])
{
    glutInit(&argc, argv);                       // Инициализация GLUT
    glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB); // Задание режима дисплея
    glutInitWindowSize(800, 600);                // Размер окна
    glutInitWindowPosition(1, 1);                // Позиция окна
    glutCreateWindow("Open GL Window");          // Создание окна
    glutReshapeFunc(reshape);                    // Функция обработки изменения размеров окна
    glutDisplayFunc(display);                    // Функция рисования изображения
    glutKeyboardFunc(processNormalKeys);         // регистрации обратного вызова для событий клавиатуры
    glutSpecialFunc(processSpecialKeys);         // регистрации обратного вызова для событий клавиатуры

    glClearColor(1, 1, 1, 0); // Установка цвета очистки экрана
    //glEnable(GL_CULL_FACE);
    //glCullFace(GL_BACK);

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
    glOrtho(-10, 10, -10, 10, -10, 10); // создание матрицы проекций в усеченном объем видимости (параллелепипед видимости) в левосторонней системе координат
}

void initPerspective()
{
    gluPerspective(60, 1, 0.1f, 25.0f);                    // центральная (перспективная) проекция
    gluLookAt(10, 5.0, 8.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0); // Положение наблюдателя (точки наблюдения)
}

void display()
{
    glClear(GL_COLOR_BUFFER_BIT); // Очищение окна установленным цветом очистки
    draw3Axis(-10, 10);           // Отрисовка осей
    glColor3f(0, 0, 0);

    switch (figure)
    {
    case 1:
        glutWireTeapot(2);
        break;
    case 2:
        glutWireSphere(2, 30, 30);
        break;
    case 3:
        glutWireCube(2);
        break;
    case 4:
        glutWireCone(2, 3, 50, 4);
        break;
    case 5:
        glutWireTorus(1, 2, 10, 10);
        break;
    case 6:
        glutWireTetrahedron();
        break;
    case 7:
        glutWireOctahedron();
        break;
    case 8:
        glutWireDodecahedron();
        break;
    case 9:
        glutWireIcosahedron();
        break;
    }

    glutSwapBuffers(); // GLUT_DOUBLE
}

void processNormalKeys(unsigned char key, int x, int y)
{
    switch (key)
    {
    case 27: // ESC
        exit(0);
        break;
    case 32:
        ortho = !ortho;
        reshape(glutGet(GLUT_WINDOW_WIDTH), glutGet(GLUT_WINDOW_HEIGHT));
        display();
        break;
    case 43: // + - увеличение объектов
        glMatrixMode(GL_MODELVIEW);
        glScaled(1.1, 1.1, 1.1);
        display();
        break;
    case 45: // - - уменьшение объектов
        glMatrixMode(GL_MODELVIEW);
        glScaled(1 / 1.1, 1 / 1.1, 1 / 1.1);
        display();
        break;
    }

    if (key > 48 && key < 58)
    {
        figure = key - 48;
        display();
    }
}

void processSpecialKeys(int key, int x, int y)
{
    switch (key)
    {
    case GLUT_KEY_UP:
        glMatrixMode(GL_MODELVIEW);
        glTranslated(0, 1, 0);
        display();
        break;
    case GLUT_KEY_DOWN:
        glMatrixMode(GL_MODELVIEW);
        glTranslated(0, -1, 0);
        display();
        break;
    case GLUT_KEY_LEFT:
        glMatrixMode(GL_MODELVIEW);
        glTranslated(-1, 0, 0);
        display();
        break;
    case GLUT_KEY_RIGHT:
        glMatrixMode(GL_MODELVIEW);
        glTranslated(1, 0, 0);
        display();
        break;
    case //GLUT_KEY_HOME: // HOME – вращение объектов против часовой стрелки вокруг оси OX
        GLUT_KEY_F1:
        glMatrixMode(GL_MODELVIEW);
        glRotated(15, 1, 0, 0);
        display();
        break;
    case //GLUT_KEY_END: // END – вращение объектов против часовой стрелки вокруг оси OY
        GLUT_KEY_F2:
        glMatrixMode(GL_MODELVIEW);
        glRotated(15, 0, 1, 0);
        display();
        break;
    case //GLUT_KEY_DELETE: // DELETE – вращение объектов против часовой стрелки вокруг оси OZ
        GLUT_KEY_F3:
        glMatrixMode(GL_MODELVIEW);
        glRotated(15, 0, 0, 1);
        display();
        break;
    case //GLUT_KEY_PAGE_UP: // PG UP – вращение объектов по часовой стрелки вокруг всех трех осей одновременно
        GLUT_KEY_F4:
        glMatrixMode(GL_MODELVIEW);
        glRotated(15, 1, 1, 1);
        display();
        break;
    }
}

void draw3Axis(double from, double to)
{
    glMatrixMode(GL_MODELVIEW);
    glPushMatrix();   // Сохранение предыдущей матрицы
    glLoadIdentity(); // Заменить на единичную
    if (!ortho)
        initPerspective();

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
