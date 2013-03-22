/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* cvalidline.cpp             */
/* __________________________ */

#include "cvalidline.h"

namespace cio {
	//First constructor that contains string to be passed into CLine
	CValidLine::CValidLine (const char* str, int row, int col, int linelength, int maxnochar, bool *insertmode, 
							bool (*validate)(const char*, CDialog&), void (*help)(CMessageStatus, CDialog&),
							bool visibility, const char* border): CLine(str, row, col, linelength, maxnochar, insertmode, visibility, border){
							
							_help=help;
							_validate=validate;
	}
	//Second constructor that does not contain string and uses default
	CValidLine::CValidLine (int row, int col, int linelength, int maxnochar, bool *insertmode, 
							bool (*validate)(const char*, CDialog&), void (*help)(CMessageStatus, CDialog&),
							bool visibility, const char* border): CLine(row, col, linelength, maxnochar, insertmode, visibility, border){

							_help=help;
							_validate=validate;
							
	}

	int CValidLine::edit(){
		
		
		int key = -1; //return key initialized
		int done = 0; //flag to check status of validation
		while (!done){  
		
			/*	If the frame() is NULL then this function works exactly like CLine::edit().*/
			if(!frame()){
				key = CLine::edit();
			/*If the frame() is not NULL:
			If a _help function exists, it calls the function passing CMessageStatus::C_SET_MESSAGE and frames()'s reference as arguments.*/
			}else{
				if(_help){
					_help(C_SET_MESSAGE, *(CDialog*)frame());
					/*Calls CLine's edit()*/
					key = CLine::edit();
					/*If a validation function exists, and the terminating key of CLine's edit() is a navigation key(see below)
						UP/DOWN/TAB/ENTER
					It will call the validation function on the Field's data, if the data is valid, it goes to next step, otherwise it will repeat calling CLine's edit().*/
					if(_validate && (key == C_MOVED_UP || key == C_MOVED_DOWN || key == TAB || key == ENTER)){
					  if(_validate((const char*)pdata(), *((CDialog*)frame()))){
					  /*After validation is done, if _help function exists, it will recall the help function using CMessageStatus::C_CLEAR_MESSAGE and frame()'s reference as arguments.*/
						if (_help)
							_help(C_CLEAR_MESSAGE, *((CDialog*)frame()));
					  }else{
						key = CLine::edit();
						}
					}else{
						done = 1;
					}
				}
			}
		}
		/*It will return the terminating key */
		return key;
	}
}