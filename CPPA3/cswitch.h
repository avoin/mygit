/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */
#ifndef _CSWITCH_H_
#define _CSWITCH_H_
#include "cfield.h"
#include "clabel.h"
namespace cio {
class CSwitch: public CField {
CLabel _label;
char _format[4];
int _length;
bool _radio;
bool _flag;

public:
CSwitch(bool SelectState, const char* format, const char* label, int row, int col, int width, bool Radio = false);
CSwitch(const CSwitch& source);                           //a copy constructor that receives a reference to a CSwitch object. 
CSwitch& operator=(const CSwitch& source);                //an assignment operator that receives a reference to an unmodifable CSwitch object. 
virtual ~CSwitch();
void draw(int fn= C_NO_FRAME);
int edit();
bool editable() const;
void set(const void* address);
bool selected() const;
void selected(bool SelectState); 

};
}
#endif