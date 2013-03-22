/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */
#include "console.h"
#include "cfield.h"
#include "cdialog.h"

namespace cio {
CDialog::CDialog(CFrame* Frame, int row, int col, int width, int height, bool visible, const char* border)
         :CFrame(row, col, width, height, visible, border, Frame){
    edit_=false;
    nFields_=0;
    fPosition_=0;
    for(int i=0; i<C_MAX_NO_FIELDS; i++){
        field_[i]=0;
        allocated_[i]=false;
    }
}
bool CDialog::editable() const{
    return edit_;
}

int CDialog::numFields() const{
    return nFields_;
}

int CDialog::curIndex() const{
    return fPosition_;
}

CField& CDialog::curField() const{
    return *field_[fPosition_];
}

int CDialog::add(CField* source, bool sourceB){
    int index = 0;
    if(nFields_ < C_MAX_NO_FIELDS) {
        source->frame(this);
        field_[nFields_]=source;
        allocated_[nFields_]=sourceB;
        if(edit_||source->editable()){
            edit_=true;
        }
        index=nFields_;
        nFields_++;
    }
    return index;    
}

int CDialog::add(CField& source, bool sourceB){
    return add(&source, sourceB);
}

CDialog& CDialog::operator<<(CField* source){
    add(source);
    return *this;
}

CDialog& CDialog::operator<<(CField& source){
    add(source);
    return *this;
}

void CDialog::draw(int field){
    if (field == C_FULL_FRAME) {
        //console.pause();
        CFrame::draw();
        field = 0;
    }
    if(field == 0) {
        for (int i = 0; i < nFields_; i++) {
            field_[i]->draw();
        }
    }
    else {
        field_[(field-1)%nFields_]->draw();
    }
}

int CDialog::edit(int field){
    int key=0;
    bool down=true;
    int rc=0;
    if(!edit_){
        draw(field);
        rc=1;
        console>>key;
    }
    else if(field<=0||edit_){
        draw(field);
    }
    else if(field>0){
        fPosition_=(field-1)%field;
    }

    while(!rc){
        key=field_[fPosition_]->edit();
        switch(key){
            case C_NOT_EDITABLE:                    
                if(down){
                    if(fPosition_<nFields_-1){
                        fPosition_++;
                    }
                    else{
                        fPosition_=0;
                    }
                }
                else{
                    if(fPosition_>0){
                        fPosition_--;
                    }
                    else{
                        fPosition_=nFields_-1;
                    }
                }
                break;
            case ENTER:
                down=true;            
                if(fPosition_<nFields_-1){
                    fPosition_++;
                }
                else{
                    fPosition_=0;
                }
                break;
            case TAB:
                down=true;            
                if(fPosition_<nFields_-1){
                    fPosition_++;
                }
                else{
                    fPosition_=0;
                }
                break;
            case DOWN:
                down=true;
                if(fPosition_<nFields_-1){
                    fPosition_++;
                }
                else{
                    fPosition_=0;
                }
                break;
            case UP:
                down=false;
                if(fPosition_>0){
                    fPosition_--;
                }
                else{
                    fPosition_=nFields_-1;
                }
                break;
            default:
                rc=1;
                break;
        }    
    }
    return key;
}

CDialog:: ~CDialog(){
    for(int i=0; i<C_MAX_NO_FIELDS; i++){
        if(allocated_[i]){
            delete field_[i];
        }
    }
}
CField& CDialog::operator[](unsigned int index){
    return *field_[index%nFields_];
}
}//end of namespace