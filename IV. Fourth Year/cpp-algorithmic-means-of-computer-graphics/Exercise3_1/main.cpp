#ifdef __APPLE__
#include <GLUT/glut.h>
#else
#include <GL/glut.h>
#endif

#include <cstdlib>

int width = 800, height = 600;

void processNormalKeys(unsigned char key, int x, int y);
void processSpecialKeys(int key, int x, int y);
void reshape(int w, int h);
void display();

int main(int argc, char *argv[])
{
	glutInit(&argc, argv);						 // Инициализация GLUT
	glutInitDisplayMode(GLUT_DOUBLE | GLUT_RGB); // Задание режима дисплея
	glutInitWindowSize(width, height);			 // Размер окна
	glutInitWindowPosition(1, 1);				 // Позиция окна
	glutCreateWindow("Open GL Window");			 // Создание окна
	glutReshapeFunc(reshape);					 // Функция обработки изменения размеров окна
	glutDisplayFunc(display);					 // Функция рисования изображения
	// регистрации обратного вызова для событий клавиатуры
	glutKeyboardFunc(processNormalKeys);
	glutSpecialFunc(processSpecialKeys);
	glutMainLoop();

	return 0;
}

void reshape(int w, int h)
{
	width = w;
	height = h;
	glViewport(0, 0, w, h);		 // настройка окна: порт просмотра (область вывода)
	glMatrixMode(GL_PROJECTION); // выбор матрицы, над которой будет производится работа
	glLoadIdentity();			 // заменяет текущую матрицу на единичную
	gluOrtho2D(0, w, 0, h);		 // устанавливает для окна левый нижний угол (left, bottom) и правый верхний угол (right, top) в мировых координатах
	glMatrixMode(GL_MODELVIEW);  // выбор матрицы, над которой будет производится работа
	glLoadIdentity();			 // заменяет текущую матрицу на единичную
}

void display()
{
	glClearColor(1, 1, 1, 0);	 // Установка цвета экрана
	glClear(GL_COLOR_BUFFER_BIT); // Очищение окна установленным цветом очистки

	glBegin(GL_QUADS);
	glColor3f(1.0, 1.0, 1.0);
	glVertex2i(250, 450);
	glColor3f(0.0, 0.0, 1.0);
	glVertex2i(250, 150);
	glColor3f(0.0, 1.0, 0.0);
	glVertex2i(550, 150);
	glColor3f(1.0, 0.0, 0.0);
	glVertex2i(550, 450);
	glEnd();

	glMatrixMode(GL_MODELVIEW);
	glPushMatrix();
	glLoadIdentity();

	// Отрисовка осей
	glColor3d(0, 0, 0);				 // установка цвета
	glBegin(GL_LINES);				 // каждая отдельная пара вершин задает линию
	glVertex2f(0, height / 2.0);	 // горизонтальная линия - лево
	glVertex2f(width, height / 2.0); // горизонтальная линия - право
	glVertex2f(width / 2.0, 0);		 // вертикальная линия - низ
	glVertex2f(width / 2.0, height); // вертикальная линия - верх
	glEnd();

	glPopMatrix();

	glutSwapBuffers(); // GLUT_DOUBLE
}

void processNormalKeys(unsigned char key, int x, int y)
{
	if (key == 27) // ESC
		exit(0);
	if (key == 65) // A
	{
		glMatrixMode(GL_MODELVIEW);
		glTranslated(25, 25, 0);
		display();
	}
}

void processSpecialKeys(int key, int x, int y)
{
	switch (key)
	{
	case GLUT_KEY_UP:
		glMatrixMode(GL_MODELVIEW);
		glTranslated(0, 25, 0);
		display();
		break;
	case GLUT_KEY_DOWN:
		glMatrixMode(GL_MODELVIEW);
		glTranslated(0, -25, 0);
		display();
		break;
	case GLUT_KEY_LEFT:
		glMatrixMode(GL_MODELVIEW);
		glTranslated(-25, 0, 0);
		display();
		break;
	case GLUT_KEY_RIGHT:
		glMatrixMode(GL_MODELVIEW);
		glTranslated(25, 0, 0);
		display();
		break;
	}
}
