#ifdef __APPLE__
#include <GLUT/glut.h>
#else
#include <GL/glut.h>
#endif

float heart[3][3] = {{0, 75, 0}, {-40, 100, 0}, {0, 15, 0}};
void displayCurrentHeart();

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

    glColor3d(1, 0, 0);         // Установка цвета рисования
    glLineWidth(5);             // Установка толщины линии
    glEnable(GL_MAP1_VERTEX_3); // Активизация процедур отображения кривых

    glutMainLoop();

    return 0;
}

void reshape(int w, int h)
{
    glViewport(0, 0, w, h);      // настройка окна: порт просмотра (область вывода)
    glMatrixMode(GL_PROJECTION); // выбор матрицы, над которой будет производится работа
    glLoadIdentity();            // заменяет текущую матрицу на единичную
    gluOrtho2D(-50, 50, 0, 100); // устанавливает для окна левый нижний угол (left, bottom) и правый верхний угол (right, top) в мировых координатах
    glMatrixMode(GL_MODELVIEW);  // выбор матрицы, над которой будет производится работа
    glLoadIdentity();            // заменяет текущую матрицу на единичную
}

void display()
{
    glClearColor(1, 1, 1, 0);     // Установка цвета экрана
    glClear(GL_COLOR_BUFFER_BIT); // Очищение окна установленным цветом очистки

    displayCurrentHeart(); // отображение одной стороны
    heart[1][0] *= -1;     // инверсия 'x'
    displayCurrentHeart(); // отображение противоположной стороны

    glFlush(); // GLUT_SINGLE
}

void displayCurrentHeart()
{
    int steps = 30; // Количество прямых линий на кривой

    glMap1f(GL_MAP1_VERTEX_3, 0.0, 1.0, 3, 3, *heart); // Задание параметров кривых
    glBegin(GL_LINE_STRIP);
    for (int i = 0; i <= steps; i++)
        glEvalCoord1f((float)i / steps);
    glEnd();
}
