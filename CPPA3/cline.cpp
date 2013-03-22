/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */

#include <cstring>
#include "cline.h"
#include "consoleplus.h"

namespace cio{

	CLine::CLine(const char* Str, int Row, int Col, int Width,
	int Maxdatalen, bool* Insertmode,
	bool Bordered,
	const char* Border): CField (Row, Col, Width, Bordered ? 3 : 1, (void*)Str, Bordered, Border) {
		alloc = false;
		cursor=0;
		offset=0;
		_maxDatalen=Maxdatalen;
		//_insMode=Insertmode;
	}
	CLine::CLine(int Row, int Col, int Width,
	int Maxdatalen, bool* Insertmode,
	bool Bordered,
	const char* Border): CField (Row, Col, Width, Bordered ? 3 : 1, NULL, Bordered, Border) {
		cursor=0;
		offset=0;
		_maxDatalen=Maxdatalen;
		//_insMode=Insertmode;
		allocateAndCopy("");
	}
	CLine::~CLine(){
		if(alloc)
		delete [] (char*)pdata();
	}
	void CLine::allocateAndCopy(const char* Str){
		alloc=true;
		*data()=new char[_maxDatalen+1];
		std::strcpy(*(char**)data(),Str);
		(*(char**)data())[_maxDatalen]='\0';
	}
	void CLine::draw(int Refresh){
		CFrame::draw(Refresh);
		display(offset);
	}
	int CLine::edit(){
		return CField::edit(_maxDatalen, &_insMode,&offset,&cursor);
	}
	bool CLine::editable()const{
		return true;
	}
	void CLine::set(const void* Str){
		if(alloc)
			delete [] (char*)pdata();
		allocateAndCopy((const char*)Str);
	}
}