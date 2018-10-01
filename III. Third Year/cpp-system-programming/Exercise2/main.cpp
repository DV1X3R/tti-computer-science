#include <windows.h>
#include <String>
#include <vector>

using namespace std;

struct ClickLabel { int x, y; };
vector<ClickLabel> ClickLabels;

struct ClickLine { int sx, sy, ex, ey; };
vector<ClickLine> ClickLines;
ClickLine clTmp;

LRESULT CALLBACK WndProc(HWND, UINT, WPARAM, LPARAM); // Callback функцию вызывает операционная система

int WINAPI WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int nCmdShow) {
	// nCmdShow - Параметры отображения окна
	HWND hwnd; // Дескриптор окна
	MSG msg; // Тип команды
	WNDCLASS w; // Определяет общие атрибуты к группе окон
	memset(&w, 0, sizeof(WNDCLASS)); // Обнуление переменных

	w.style = CS_HREDRAW | CS_VREDRAW; // Стиль - Перерисовка при изменении размеров окна
	w.lpfnWndProc = WndProc; // Оконная процедура
	w.hInstance = hInstance; // Дескриптор инстанции
	w.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH); // Закраска окна
	w.lpszClassName = TEXT("Ex2Class"); // Имя класса
	RegisterClass(&w); // Регистрация класса окна

	hwnd = CreateWindow(
		TEXT("Ex2Class")	// Имя класса, по которому производится идентификация
		, TEXT("Excercise 2")	// Имя окна
		, WS_OVERLAPPEDWINDOW	// Стиль окна (An overlapped has a title bar and a border)
		, 500, 300		// x, y
		, 500, 500		// Width, Height
		, NULL, NULL, hInstance, NULL); // Parent, Menu, Instance, lpParam

	ShowWindow(hwnd, nCmdShow); // Отображает или прячет окно (В зависимости от nCmdShow)
	UpdateWindow(hwnd); // Посылает сообщение WM_PAINT

						// Цикл обработки сообщений
	while (GetMessage(&msg, NULL, 0, 0)) {
		TranslateMessage(&msg); // получить сообщения от клавиатуры более высокого уровня
		DispatchMessage(&msg); // пересылает сообщение оконной процедуре
	}

	return msg.wParam;
}

LRESULT CALLBACK WndProc(HWND hwnd, UINT Message, WPARAM wparam, LPARAM lparam) {
	HDC hDC; // Экземпляр контекста устройства
	SetCursor(LoadCursor(NULL, IDC_ARROW));

	//MessageBox(hwnd, "RIGHT BUTTON CLICK", "", MB_OK);
	//SetPixel(hDC, LOWORD(lparam), HIWORD(lparam), 255);

	switch (Message) {
		// Старт линии
	case WM_LBUTTONDOWN:
		SetCursor(LoadCursor(NULL, IDC_HAND));
		clTmp.sx = LOWORD(lparam);
		clTmp.sy = HIWORD(lparam);
		break;

		// Продление линии
	case WM_MOUSEMOVE:
		if (wparam & MK_LBUTTON) {
			clTmp.ex = LOWORD(lparam);
			clTmp.ey = HIWORD(lparam);
			if (!(wparam & MK_CONTROL))
				InvalidateRect(hwnd, NULL, true); // Очистка окна при следующем WM_PAINT
		}
		break;

		// Сохранение линии
	case WM_LBUTTONUP:
		if (!(wparam & MK_CONTROL))
			ClickLines.push_back(clTmp);
		memset(&clTmp, 0, sizeof(ClickLine));
		InvalidateRect(hwnd, NULL, true);
		break;

		// Сохранение Label
	case WM_RBUTTONDOWN:
		SetCursor(LoadCursor(NULL, IDC_HAND));
		ClickLabel cl;
		cl.x = LOWORD(lparam);
		cl.y = HIWORD(lparam);
		ClickLabels.push_back(cl);
		break;

	case WM_PAINT:
		hDC = GetDC(hwnd);

		// Отрисовка сохранённых Label'ов
		for (auto it = ClickLabels.begin(); it != ClickLabels.end(); it++) {
			wstring c = TEXT("Clicked x:") + to_wstring(it->x) + TEXT(" y:") + to_wstring(it->y);
			TextOut(hDC, it->x, it->y, c.c_str(), c.length());
		}

		// Отрисовка сохранённых линий
		for (auto it = ClickLines.begin(); it != ClickLines.end(); it++) {
			MoveToEx(hDC, it->sx, it->sy, NULL);
			LineTo(hDC, it->ex, it->ey);
		}

		// Отрисовка временной линии
		if (clTmp.sx != 0 && clTmp.ex != 0) {
			MoveToEx(hDC, clTmp.sx, clTmp.sy, NULL);
			LineTo(hDC, clTmp.ex, clTmp.ey);
		}

		ReleaseDC(hwnd, hDC);
		break;

		// Обязательные сообщения для обработки
	case WM_DESTROY:
		PostQuitMessage(0);
		break;
	default:
		// обработка сообщений Windows, которые не обрабатываются в программе
		return DefWindowProc(hwnd, Message, wparam, lparam);
	}

	return 0;
}
