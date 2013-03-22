/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */

#ifndef CDialog_H
#define CDialog_H
#include "cframe.h"
namespace cio{
class CField;

class CDialog : public CFrame{
    bool edit_; //whether or not any of the fields are editble
    int nFields_; //how many fields
    int fPosition_; //cursor position of current field
    CField* field_[C_MAX_NO_FIELDS]; //holds an array of cfield objects
    bool allocated_[C_MAX_NO_FIELDS]; //holds true or false for allocation of a CField associated with _field       
public:
    CDialog(CFrame* Frame=(CFrame*)0, int row=-1, int col=-1, int width=-1, int height=-1, 
            bool visible=false, const char* border=C_BORDER_CHARS); // | constructor, calls CFrame's constructor
    bool editable() const; //returns if the dialog box is editable |
    int numFields() const; //returns # of fields in the dialog |
    int curIndex() const; //returns cursor index |
    CField& curField() const; // returns a reference to the field at the field cursor |
    int add(CField*, bool=false); //adds the field pointed to if there is room |
    int add(CField& , bool=false); //adds the field pointed to if there is room |
    CDialog& operator<<(CField*); //adds the field pointed to and returns a reference to the dialog |
    CDialog& operator<<(CField&); //adds the field pointed to and returns a reference to the dialog |
    void draw(int=C_FULL_FRAME); //draws the dialog |
    int edit(int=C_FULL_FRAME); //edits the dialog box if editable
    virtual ~CDialog(); // |
    CField& operator[](unsigned int index); //returns a reference to the field object at index
};
}
#endif

