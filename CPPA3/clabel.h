/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */

#ifndef _CLabel_H_
#define _CLabel_H_
#include "cfield.h"
namespace cio {
class CLabel : public CField{
	void allocateAndCopy(const char* Str);
public:
	CLabel(const char *Str, int Row, int Col, int Len  = -1);
	CLabel(int Row, int Col, int Len);
	void draw(int fn=C_NO_FRAME);
	void set(const void* Str);
	CLabel(const CLabel& L);
	bool editable() const;
	int edit();
	~CLabel();	
};
}//end of cio
#endif