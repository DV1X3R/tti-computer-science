#ifdef __APPLE__
#include <GLUT/glut.h>
#else
#include <GL/glut.h>
#endif

#include <iostream>
#include <math.h>

void reshape(int w, int h);
void display();

int main(int argc, char *argv[])
{
    glutInit(&argc, argv);                        // Инициализация GLUT
    glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGBA); // Задание режима дисплея
    glutInitWindowSize(800, 600);                 // Размер окна
    glutInitWindowPosition(1, 1);                 // Позиция окна
    glutCreateWindow("Open GL Window");           // Создание окна
    glutReshapeFunc(reshape);                     // Функция обработки изменения размеров окна
    glutDisplayFunc(display);                     // Функция рисования изображения
    glutMainLoop();

    return 0;
}

void reshape(int w, int h)
{
    printf(">Reshape \n");
    glViewport(0, 0, w, h); // Настройка окна
    glMatrixMode(GL_PROJECTION);
    glLoadIdentity();

    gluOrtho2D(-100, 100, -100, 100); // Мировые координаты
    glMatrixMode(GL_MODELVIEW);
    glLoadIdentity();
}

void display()
{
    printf(">Display \n");
    glClearColor(1, 1, 1, 0);
    glClear(GL_COLOR_BUFFER_BIT);

    // Отрисовка осей
    glColor3d(0, 0, 0);
    glBegin(GL_LINES);
    glVertex2f(-100, 0);
    glVertex2f(100, 0);
    glVertex2f(0, 100);
    glVertex2f(0, -100);
    glEnd();

    // Отрисовка функции
    glColor3d(0, 0, 1);
    glBegin(GL_LINE_STRIP);
    for (double x = -100; x <= 100; x += 0.5)
        glVertex2f(x, abs(((1 / 4.0) * x) + 3 * cos(100 * x) * sin(x)));
    glEnd();

    glutSwapBuffers(); // GLUT_DOUBLE
}
