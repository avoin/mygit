/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */

#include "consoleplus.h"
#include "console.h"
#include "keys.h"
#include <cstring>
using namespace std;
namespace cio {

void display(const char* format, int row, int col, int fieldLength, bool selected, const char* menuItem){
	char* lol;
	char temp;
	if (menuItem == 0)
	{
		if (format)
		{
			cio::display(format, row, col, fieldLength);
			if (!selected){
				lol=new char[strlen(format+1)];
				std::strcpy(lol, format);
                lol[1]=' ';
				cio::display(lol, row, col, fieldLength);
            }
        }
	}
	else if (menuItem != 0)
	{
		cio::display(menuItem, row, col+1, fieldLength);
		if (format && selected)
		{
			temp=format[0];
			cio::display(&temp,row,col,1);
			temp=format[1];
			cio::display(&temp,row,(strlen(menuItem)+col+1), 1);
		}
	}
	
	console.setPosition(row, col+1);
}

int edit(const char* format, int row, int col, int fieldLength, bool* selected, bool isRadio, const char* menuItem ){
	
	int keyPressed;
	bool condition = true;

	do
	{
		display(format, row, col, fieldLength, *selected, menuItem);
		console >> keyPressed;
		switch(keyPressed)
		{
			case SPACE:
				if (isRadio)
					*selected = true;
				else{
					if(*selected){
						*selected=false;
					}
					else{
						*selected=true;
					}
                }
				condition = false;
				break;
			case ENTER:
				if(menuItem)
				{
					if (isRadio)
						*selected = true;
					else{
                        if(*selected){
                            *selected=false;
                        }
                        else{
                            *selected=true;
                        }
                    }
					keyPressed = C_MENU_SELECT_AND_QUIT;
				}
				else{
					if (isRadio){
						*selected = true;
					}
					else{
                        if(*selected){
                            *selected=false;
                        }
                        else{
                            *selected=true;
                        }
                    }					
					keyPressed=ENTER;
					condition=false;
				}
				break;
			default:
				condition = false;
				break;
		}
		display(format, row, col, fieldLength, *selected, menuItem);
	}while(condition != false);
	display(format, row, col, fieldLength, *selected, menuItem);
	
	return keyPressed;
}
}