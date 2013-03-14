/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */
#ifndef CText_H
#define CText_H
#include "cfield.h"
#include "keys.h"
#include "cblock.h"
namespace cio {
	class CText: public CField {
		bool rstate_;
		bool insert_;
		int width_;
		int maxLength_;
		int curRow_;
		int curCol_;
		int offRow_;
		int offCol_;
		CBlock block_;
		
		int height_;
    public:
		CText(const char*, int, int, int, int, bool, bool*, const char* = C_BORDER_CHARS);
		CText(int, int, int, int, bool, bool*, const char* = C_BORDER_CHARS);
		void draw(int = C_FULL_FRAME);
		int edit();
		bool editable() const;
		void set(const void*);
		bool readOnly() const;
		void readOnly(bool);
    };
}
#endif