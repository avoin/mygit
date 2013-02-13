/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */
#include <iostream>
#include "ctext.h"
#include <cstring>
#include "cdialog.h"
#include "console.h"
#include "cframe.h"
#include "consoleplus.h"
#include <cstdio>
using namespace std;
namespace cio {
CText::CText(const char* address, int row, int col, int width, int height, bool read, bool* imode, const char* border): CField(row, col, width, height, (void*)border, true)	
{
	rstate_ = read;
	insert_ = imode;
	width_ = width;
	
	maxLength_ = 0;
	curRow_ = 0;
	curCol_ = 0;
	offRow_ = 0;
	offCol_ = 0;
	
	block_.set((const char*)address);
	height_ = height - 2;
}

CText::CText(int row, int col, int width, int height, bool read, bool* imode, const char* border): CField(row, col, width, height, (void*)border, true)
{
	rstate_ = read;
	insert_ = imode;
	width_ = width;
	
	maxLength_ = 0;
	curRow_ = 0;
	curCol_ = 0;
	offRow_ = 0;
	offCol_ = 0;
	
	height_ = height - 2;
}

void CText::draw(int fn)
{
	char* s = (char*)0;
	
	CFrame::draw(fn);
	

	for (int i = offRow_; i<block_.lastLine() && i < height_+offRow_; i++)
	{	
		s = new char[strlen(block_[i])+1];
		*data() = new char[strlen(block_[i])+1];
		
		strcpy (s, block_[i]);
		strcpy (*(char**)data(), s);
		CField::display(offCol_,i-offRow_);
		
		delete[] s;
		*data() = 0;
	}
	
}

int CText::edit()
{
	int keyPressed;
	//keyPressed = is.getKey()
	// console << "WTF";
	console >> keyPressed;
	// console.setPosition(0,0);
	// console << "HELLO " << keyPressed;
	char* s=(char*)0;
	int curStrLength;
	
	switch(keyPressed){
		case ENTER:
			//Only do something if in insert mode
			if (insert_){
				/*inserts a new line below the current line and moves
				the remaining lines of the block down one line*/
				block_.insert(curRow_+1);
			}
			break;
		case DOWN:
			//Move the cursor down one line
			 if (curRow_+1 > height_ && offRow_+curRow_ < block_.lastLine())
			 {
				offRow_++;
			 }
			 else if (curRow_ < height_)
			 {
				curRow_++;
			 }
			break;
		case UP:
			 if (curRow_-1 < 0 && offRow_ > 0)
			 {
				offRow_--;
			 }
			 else if (curRow_ > 0)
			 {
			 	 curRow_--;
			 }
			break;
		case BACKSPACE:
			//Move the cursor down one row
			break;
		case LEFT:
			//Redraws the field with the visible parts of its lines???
			if (curCol_-1 > 0)
			{
				curCol_-1;
			}
			else if (curCol_-1 <= 0 && offCol_ > 0)
			{
				offCol_--;
			}
			break;
		case RIGHT:
			//Redraws the field with the visible parts of its lines???
			if (curCol_+offCol_+1 > width_-2){
				offCol_++;
			}
			else
				curCol_++;
			break;
		case END:
			//Redraws the field with the visible parts of its lines???
			curCol_ = width_-2;
			offCol_ = curStrLength-2;
			break;
		case HOME:
			//Redraws the field at column one and moves all lines over
			curCol_ = 0;
			offCol_ = 0;
			break;
		//We're not sure what escape is meant to do because the assignment doesn't say and the test 8 is buggy for it
		/*case ESCAPE:
			break;*/
	}
	draw();
		if (s)
			delete [] s;
		if (*data())
			*data()=0;
		
		s = new char[strlen(block_[offRow_+curRow_])+1];
		*data() = new char[strlen(block_[offRow_+curRow_])+1];
	
		strcpy (s, block_[offRow_+curRow_]);
		strcpy (*(char**)data(), s);
		curStrLength = strlen(s);
		if (strlen(s) > maxLength_)
			maxLength_ = strlen(s);
			
		if (curRow_ < height_-2){
			CField::edit(maxLength_, &insert_, &offCol_, &curCol_, (keyPressed == UP ? curRow_ : offRow_+curRow_), true, rstate_);
		}
		strcpy(block_[offRow_+curRow_], *(char**)data());
	return keyPressed;
}

bool CText::editable() const
{
	return true;
}

void CText::set(const void* address)
{
	block_.set((const char*)address);
}

bool CText::readOnly() const
{
	return rstate_;
}

void CText::readOnly(bool state)
{
	rstate_ = state;
}

}