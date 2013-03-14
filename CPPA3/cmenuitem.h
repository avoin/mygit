/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */

#ifndef CMenu_H
#define CMenu_H
#include "cfield.h"

namespace cio {
class CMenuItem: public CField
{
	char* menu_;
	char* format_;
	int width_;
	bool state_;
	//bool* address_;
public:
	CMenuItem(bool, const char*, const char*, int, int, int);
	~CMenuItem();
	CMenuItem(const CMenuItem&);
	CMenuItem& operator=(const CMenuItem&);	
	void draw(int = C_NO_FRAME);
	int edit();
	bool editable() const;
	void set(const void*);
	bool selected() const;
	void selected(bool);
	const char* text() const;
};
}//end of cio
#endif