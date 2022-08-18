#include "Lab2GUI.h"
#include <QtWidgets/QApplication>

int main(int argc, char *argv[])
{
	QApplication a(argc, argv);
	Lab2GUI w;
	w.show();
	return a.exec();
}
