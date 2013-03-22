/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */
#ifndef consoleplus_h
#define consoleplus_h
#include "cfg.h"

namespace cio {

void display(const char* str, int row, int col, int len);
int  edit(char* str, int row, int col, int fieldLen, int maxStrLength, 
bool* insertMode, int* offset, int* curPos, bool isTextEditor=false, bool isReadOnly=false);
void* capture(int row, int col, int height, int width);
void  restore(int row, int col, int height, int width, CDirection dir, void* capbuf);
void  release(void** capbuf);
void display(const char* format, int row, int col, int fieldLength, bool selected, const char* menuItem = 0);
int edit(const char* format, int row, int col, int fieldLength, bool* selected, bool isRadio, const char* menuItem = 0);

} // end namespace
#endif