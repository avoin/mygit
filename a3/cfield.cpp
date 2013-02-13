/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */
#include "cfield.h"
namespace cio{
CField::CField(int row, int col, int width, int height, void* data, 
bool visible, const char* border) : CFrame(row, col, width, height, visible, border) {
	fData_ = data;
}

CField::~CField() {}
//An empty Destructor that does absolutly nothing!

void CField::sdata(void* lol){
	fData_=lol;
}
void** CField::data(){
	return &fData_;
}

const void* CField::pdata() const{
	return fData_; 
}

void CField::display(int offset, int curRow){
    CFrame::display((const char*)fData_+offset, curRow);
}

void CField::display(const char* format, const char* menuItem){
	CFrame::display(format, (bool)fData_, menuItem);
}

int CField::edit(int maxStrLength, bool* insertMode, int* offset, int* curPos, int curRow, bool isTextBlock, bool isReadOnly){
	return CFrame::edit((char*)fData_, maxStrLength, insertMode, offset, curPos, curRow, isTextBlock, isReadOnly);
}

int CField::edit(const char* format, int fieldLength, bool isRadio, const char* menuItem) {
	return CFrame::edit(format, fieldLength, (bool*)fData_, isRadio, menuItem);
}

}