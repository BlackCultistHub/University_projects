#pragma once

#include <QtWidgets/QMainWindow>
#include "ui_Lab2GUI.h"

class Lab2GUI : public QMainWindow
{
	Q_OBJECT

public:
	Lab2GUI(QWidget *parent = Q_NULLPTR);

private:
	Ui::Lab2GUIClass ui;
};
