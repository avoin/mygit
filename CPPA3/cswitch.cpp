/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */
#include "cswitch.h"
#include <cstring>
#include "consoleplus.h";
using namespace std;
namespace cio {

CSwitch::CSwitch(bool SelectState, const char* format, const char* label, int row, int col, int width, bool Radio)
		:CField(row, col, width, 1),_label(label, 0, 4, width-4){
	_label.frame(this);
	std::strcpy(_format, format);
	_length = std::strlen(format);
	_radio = Radio;
	_flag=SelectState;
	fData_ = &_flag;
}

//a copy constructor that receives a reference to a CSwitch object.
CSwitch::CSwitch(const CSwitch& source):CField(source), _label(source._label){
	if(&source!=this){
		std::strcpy(_format, source._format);
		_length=std::strlen(source._format);
		_radio=source._radio;
		_flag=source._flag;
		fData_ = &_flag;
	}
	
}

//an assignment operator that receives a reference to an unmodifable CSwitch object.
CSwitch& CSwitch::operator=(const CSwitch& source){
	if(&source!=this){
		*(CField*)this=source;
		_label=source._label;
		std::strcpy(_format, source._format);
		_length=std::strlen(source._format);
		_radio=source._radio;
		_flag=source._flag;
		fData_ = &_flag;
	}
	
	return *this;
}

CSwitch::~CSwitch(){
}

void CSwitch::draw(int fn){
	cio::display(_format, absRow(), absCol(), std::strlen(_format), _flag);
	//CField::display(_format);
	_label.draw(fn);
}

int CSwitch::edit(){
	int key = CField::edit(_format, _length, _radio);
	if(key==ENTER){
		key=ESCAPE;
	}
	return key;
}

bool CSwitch::editable() const{
	return true;
}

void CSwitch::set(const void* address){
	_flag=address;
}

bool CSwitch::selected() const{
	return _flag;
}

void CSwitch::selected(bool SelectState){
	_flag=SelectState;
}
}