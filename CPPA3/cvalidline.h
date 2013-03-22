/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* cvalidline.cpp             */
/* __________________________ */

#ifndef CVALIDLINE_H
#define CVALIDLINE_H
#include "cdialog.h"
#include "cline.h"


namespace cio {

class CValidLine : public CLine {

	void (*_help)(CMessageStatus, CDialog&);
	bool (*_validate)(const char*, CDialog&);

public:
CValidLine (const char* str, int row, int col, int linelength, int maxnochar, bool* insertmode, 
			bool (*validate)(const char*, CDialog&) = C_NO_VALIDATIONFUNC, void (*help)(CMessageStatus, CDialog&) = C_NO_HELPFUNC,
			bool visibility = false, const char* border = C_BORDER_CHARS);
CValidLine (int row, int col, int linelength, int maxnochar, bool* insertmode, bool (*validate)(const char*, CDialog&) = C_NO_VALIDATIONFUNC,
			void (*help)(CMessageStatus, CDialog&) = C_NO_HELPFUNC, bool visibilty = false,
			const char* border = C_BORDER_CHARS);
int edit();
};
}
#endif