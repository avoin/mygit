/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */
#include <iostream>
#include "cmenuitem.h"
#include <cstring>
#include "console.h"
using namespace std;

// change menutitem.cpp to cmenuitem.cpp

namespace cio{
CMenuItem::CMenuItem (bool state, const char* format, const char* label, int row, int col, int width): CField(row, col, width, 1)
{
	//address_ = NULL;
	menu_ = new char[strlen(label)+1];
	//menu_ = new char[width+1];
	format_ = new char[strlen(format)+1];
	width_ = width;
	
	state_ = state;
	
	strcpy(menu_, label);
	/*
	for (int i = 0; i < width; i++)
	{
		if (i < strlen(label))
			menu_[i] = label[i];
		else
			menu_[i] = ' ';
	}
	menu_[width] = '\0';
	*/
	strcpy(format_, format);
	
	
/*
	int length = strlen(menu_)+strlen(format_);
	if (*data())
		delete[] *data();
	*data() = new char[length+1];
	char* s = new char[length+1];
	
	s[0] = format_[0];
	for (int i = 1; i < length; i++)
	{
		s[i] = label[i-1];
	}
	s[length-1] = format_[1];
	s[length] = '\0';
*/

/*
	int length = strlen(menu) + width;
	*data() = new char[length+1];
	char* s = new char[length+1];
	
	s[0] = format_[0];
	for (int i = 1; i<length; i++)
	{
		s[i] = label[i-1];
	}
	s[length-1] = format_[1];
	s[length] = '\0';
*/

	char* s = new char[width+1];
	//console.setPosition(0,0);
	//cout << "length = " << width;
	s[0] = format_[0];
	//strncat(s, label, strlen(label));
	for (int i = 1; i < width; i++)
	{
		if (i < strlen(label)+1)
			s[i] = label[i-1];
		else
			s[i] = ' ';
	}
	s[width-1] = format_[1];
	s[width] = '\0';
	
	
	if (*data())
		delete[] *data();
	*data() = new char[width+1];
	strcpy (*(char**)data(), s);
	
	//static int b = 1;
	//console.setPosition(b++, 0);
	//cout << "string = " << s << " & label = " << menu_ << " & format = " << format_ << " & pdata = " << (char*)pdata() << endl;
	
	/*
	if (state_)
		address_ = &state_;
	*/
	
}

CMenuItem::~CMenuItem ()
{
/*
	static int r = 0;
	console.setPosition(r++,0);
	cout << "BEFORE: &state_ = " << state_ << " & &address_ = " << address_ << " & menu_ = " << menu_;
	if (!address_)
		state_ = (bool*)0;
	console.setPosition(r++,0);
	cout << "AFTER: &state_ = " << state_ << " & &address_ = " << address_ << " & menu_ = " << menu_;
	*/
	delete[] menu_;
	delete[] format_;
}

CMenuItem::CMenuItem(const CMenuItem& source)
{
	if (source.menu_ != NULL)
	{
		menu_ = new char[strlen(source.menu_)+1];
		strcpy(menu_,source.menu_);
	}
	else
		menu_ = (char*)0;
		
	if (source.format_ != NULL)
	{
		format_ = new char[strlen(source.format_)+1];
		strcpy(format_,source.format_);
	}
	else
		format_ = (char*)0;
}

CMenuItem& CMenuItem::operator=(const CMenuItem& source)
{
	if (this != &source)
	{
		if (menu_ != (char*)0)
			delete[] menu_;
		if (format_ != (char*)0)
			delete[] format_;
		
		if (source.menu_ != (char*)0)
		{
			menu_ = new char[strlen(source.menu_)+1];
			strcpy(menu_,source.menu_);
		}
		else
			menu_ = (char*)0;
			
		if (source.format_ != (char*)0)
		{
			format_ = new char[strlen(source.format_)+1];
			strcpy(format_,source.format_);
		}
		else
			format_ = (char*)0;
	}
	
	return *this;
}

void CMenuItem::draw(int fn)
{
	//CField::display((char*)data());
	if (selected())
		CField::display((char*)pdata());
	else
		CField::display(menu_);
	//CField::display((char*)pdata());
	CFrame::draw(fn);
	
	
	// unsure how your code works, checked out other classes to get an idea of how you used the draw functions - neil
}

int CMenuItem::edit()
{
	// figure out what to pass to edit (check email)
	// supposed to pass width and true
	
	// either width of menu_ (strlen(menu_)) or width stored(?)
	//return CField::edit((char*)data(), width_, true, menu_);
	//return CField::edit(format_, width_, true, (char*)data());
	//return selected()?CField::edit(format_, width_, true, menu_):CField::edit(" ", width_, false, menu_);
	//return CField::edit(format_, width_, true, menu_);
	//return 1;
	
	int keyPressed;
		
	if (selected())
		CField::display((char*)pdata());
		//CField::display(format_,menu_);
	else
		CField::display(menu_);

	console >> keyPressed;
	
	switch (keyPressed)
	{
		case SPACE: 
			if (!selected())
			{
				state_ = (bool*)1;
			}
			break;
		case ENTER:
			state_ = (bool*)1;
			//address_ = &state_;
			//address_ = (bool*)1;
			keyPressed = ESCAPE;
			break;	
	}
	
	return keyPressed;
	
}

bool CMenuItem::editable() const
{
	return true;
}

void CMenuItem::set(const void* address)
{
	//bool s = (bool*)address;
	//bool fa = (bool*)*data();
	//bool fa = (bool*)*data();
	//fa = s;
	bool s = (bool*)address;
	state_ = s;
	/*
	bool fa = (bool*)*data();
	fa = s;
	*/
}

bool CMenuItem::selected() const
{
	//return (bool&)*data();
	//console.setPosition(0,0);
	//cout << "menu = " << menu_ << " & state = " << state_ << endl;
	// state_ not working properly, it doesnt return the correct state
	// state_ may have to be stored in the CField.fData()
	// the character data might have to be stored in a new instance var
	return state_;
}

void CMenuItem::selected(bool state)
{
	//bool fa = (bool&)*data();
	//fa = state;
	state_ = state;
	//console.setPosition(0,0);
	//cout << "menu = " << menu_ << " & state = " << state_ << endl;
}

const char* CMenuItem::text() const
{
	//console.setPosition(0,0);
	//cout << "********" << menu_ << "********" << endl;
	//const char* str = data();
	return menu_;
	//return str;
}

}