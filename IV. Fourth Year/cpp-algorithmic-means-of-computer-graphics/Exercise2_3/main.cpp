#ifdef __APPLE__
#include <GLUT/glut.h>
#else
#include <GL/glut.h>
#endif

float heart[3][3] = {{0, 75, 0}, {-40, 100, 0}, {0, 15, 0}};

GLUnurbs *nurbs;
float node[6] = {0, 0, 0, 1, 1, 1};
void nurbsCurrentHeart();

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

    glColor3d(0, 1, 0); // Установка цвета рисования
    glLineWidth(5);     // Установка толщины линии
    nurbs = gluNewNurbsRenderer();

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

    nurbsCurrentHeart(); // отображение одной стороны
    heart[1][0] *= -1;   // инверсия 'x'
    nurbsCurrentHeart(); // отображение противоположной стороны

    glFlush(); // GLUT_SINGLE
}

void nurbsCurrentHeart()
{
    gluBeginCurve(nurbs);  // Начало определения кривой
    gluNurbsCurve(         // Задание атрибутов кривой
        nurbs,             // NURBS-объект
        6,                 // Количество параметрических узлов кривой, количество должно быть в два раза больше самих узлов
        &node[0],          // Указатель на массив, хранящий значения этих узлов. В первой половине массива значания 0, во второй половине 1
        3,                 // Смещение между точками в массиве (stride)
        &heart[0][0],      // Массив опорных точек
        3,                 // Количество опорных точек
        GL_MAP1_VERTEX_3); // Тип двумерного вычислителя. Чаще всего используются GL_MAP1_VERTEX_3 и GL_MAP1_VERTEX_4
    gluEndCurve(nurbs);    // Завершение определения кривой
}
