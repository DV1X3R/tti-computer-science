#ifdef __APPLE__
#include <GLUT/glut.h>
#else
#include <GL/glut.h>
#endif

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
    glViewport(0, 0, w, h);           // настройка окна: порт просмотра (область вывода)
    glMatrixMode(GL_PROJECTION);      // выбор матрицы, над которой будет производится работа
    glLoadIdentity();                 // заменяет текущую матрицу на единичную
    gluOrtho2D(-100, 100, -100, 100); // устанавливает для окна левый нижний угол (left, bottom) и правый верхний угол (right, top) в мировых координатах
    glMatrixMode(GL_MODELVIEW);       // выбор матрицы, над которой будет производится работа
    glLoadIdentity();                 // заменяет текущую матрицу на единичную
}

void display()
{
    glClearColor(1, 1, 1, 0);     // Установка цвета экрана
    glClear(GL_COLOR_BUFFER_BIT); // Очищение окна установленным цветом очистки

    // Отрисовка осей
    glColor3d(0, 0, 0);  // установка цвета
    glBegin(GL_LINES);   // каждая отдельная пара вершин задает линию
    glVertex2f(-100, 0); // горизонтальная линия - лево
    glVertex2f(100, 0);  // горизонтальная линия - право
    glVertex2f(0, -100); // вертикальная линия - низ
    glVertex2f(0, 100);  // вертикальная линия - верх
    glEnd();

    // Отрисовка функции
    glColor3d(0, 0, 1);     // установка цвета
    glBegin(GL_LINE_STRIP); // каждая пара вершин задает линию (т.е. конец предыдущей линии является началом следующей)
    for (double x = -100; x <= 100; x += 0.5)
        glVertex2f(x, abs(((1 / 4.0) * x) + 3 * cos(100 * x) * sin(x)));
    glEnd();

    glutSwapBuffers(); // GLUT_DOUBLE
}
