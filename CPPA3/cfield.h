/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */
#ifndef CField_H
#define CField_H
#include "cframe.h"
namespace cio {
class CField : public CFrame {
protected:
    void* fData_;

public:
	virtual bool editable() const = 0;
	virtual void set(const void*) = 0;
    virtual int  edit() = 0;
	CField(int row = 0, int col = 0, int width = 0, int height=0, void* data = NULL, bool visible = false , const char* border=C_BORDER_CHARS);   
	~CField();
	void** data();
	void sdata(void* lol);
	const void* pdata() const;
	void display(int offset, int curRow = 0);
	void display(const char* format, const char* menuItem = 0);
	int edit(int maxStrLength, bool* insertMode, int* offset, int* curPos, int curRow = 0, bool isTextBlock = false, bool isReadOnly = false);
	int edit(const char* format, int fieldLength, bool isRadio, const char* menuItem = 0);
};
}
#endif


