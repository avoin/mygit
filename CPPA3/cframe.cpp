/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */
#include <cstring>
#include "console.h"
#include "consoleplus.h"
#include "cframe.h"
using namespace std;
namespace cio { // continuation of cio namespace

CFrame::CFrame(int Row, int Col, int Width, int Height, bool Visible, const char* 
               Border, CFrame* Frame){			   
		if (Height<0){
		_row=Row;
		_col=Col;
		_width=console.getCols();
		_height=console.getRows();
		_visible=false;
		_border[0]='\0';
		_frame=Frame;
		_covered=NULL;
        fullScreen_=true;
		}		
		else {
		_row=Row;
		_col=Col;
		_width=Width;
		_height=Height;
		_visible=Visible;
		strcpy(_border, Border);
		_frame=Frame;
		_covered=NULL;
        fullScreen_=false;
		}
}

CFrame:: ~CFrame() {
    release(&_covered);
}

int CFrame::absRow() const{
	int row;	
    row=_row;
	CFrame* temp=frame();
	while(temp){
		//(.)DOT DOES NOT WORK USE "->"
		row=row+temp->row();
		temp=temp->frame();
	}
	return row;   
}  


int CFrame::absCol() const{
	int col;
	col=_col;
	CFrame* temp=frame();
	while(temp){
		col=col+temp->col();
		temp=temp->frame();
	}
	return col;
}

void CFrame::setLine(char* str, char left, char fill, char right) const {
    int i;
    str[0]=left;
    for(i=1;i<_width-1;i++){
        str[i]=fill;
    }
    str[i]=right;
    str[i+1]='\0';
}

void CFrame:: goMiddle() {
	if(_visible){
        console.setPosition(absRow()+1, absCol()+_width/2);
    }
    else{
        console.setPosition(absRow(), absCol()+_width/2);
    }
}

void CFrame:: bordered(bool border){
	_visible = border;
}

bool CFrame:: bordered() const{
	return _visible;
}

void CFrame:: frame(CFrame* frame){
	_frame=frame;
}

CFrame* CFrame::frame() const {
	return (CFrame*)_frame;
}

void CFrame:: row(int row) {
	_row=row;
}

int CFrame:: row() const {
	return _row;
}
void CFrame:: col(int col){
	_col=col;
}

int CFrame::col() const{
	return _col;
}

void CFrame::height(int height){
	_height=height;   
}

int CFrame::height() const{
	return _height;
}

void CFrame::width(int width){
	_width=width;
}
int CFrame::width() const{
	return _width;
}

void CFrame::display(const char* display, int curRow){
    int width;
    int addB=0, addR=0;
    
    if(_visible){
        addB=2;
		addR=1;
    }
    width=_width-addB;
    
    if(_frame){
        if(_col+_width+addB>=_frame->width()){
            width=_frame->width()-_col-addB;
            if(_frame->bordered()){
                width--;
            }
        }
    }

	cio::display(display, absRow()+addR+curRow, absCol()+addB-1, width);   	
}

void CFrame::display(const char* format, bool selected, const char* menuItem){
	cio::display(format, absRow(), absCol(), _width, selected, menuItem);
}

int CFrame::edit(char* str, int maxStrLength, bool* insertMode, int* strOffset, int* curPosition, int curRow, bool isTextBlock, bool isReadOnly){
    int width;
    int addB=0, addR=0;
    
    if(_visible){
        addB=2;
		addR=1;
    }
    width=_width-addB;
    
    if(_frame){
        if(_col+_width+addB>=_frame->width()){
            width=_frame->width()-_col-addB;
            if(_frame->bordered()){
                width--;
            }
        }
    }
	
return cio::edit (str, absRow()+addR+curRow, absCol()+addB-1, width, maxStrLength, insertMode, strOffset, curPosition, isTextBlock, isReadOnly);
}

int CFrame::edit(const char* format, int formatLen, bool* selected, bool isRadio, const char* menuItem){
	return cio::edit(format, absRow(), absCol(), formatLen, selected, isRadio, menuItem);
}
    
void CFrame::draw(int draw){
	int i;
	char* str;
	str = new char[width()+1];
	for (i = 0; i<width()+1; i++)
	{
		str[i] = ' ';
	}
	capture();
	if (_visible && draw!=C_NO_FRAME) {
		console.setPosition(absRow(),absCol());
		setLine(str,_border[0], _border[1], _border[2]);	
		console << str;

		for (i=0; i<(height()-2); i++) {
			console.setPosition((absRow()+1+i),absCol());
			setLine(str,_border[7], ' ', _border[3]);	
			console << str;
		}
		
		console.setPosition((absRow()+1+i),absCol());
		setLine(str,_border[6],_border[5],_border[4]);
		console << str;
	}
	delete [] str;
}

void CFrame::capture () {

    if(!_covered){       
		_covered = cio::capture(absRow(),absCol(),_height,_width);
    }
} 

void CFrame::hide(cio::CDirection hide){
    cio::restore(absRow(), absCol(), _height, _width, hide, _covered);
    cio::release((void**)&_covered);
}

void CFrame::move(cio::CDirection move){
    if (move==C_MOVED_DOWN){
    	if(_frame){
        	if (bordered()){
            	if(absRow()+_height<_frame->absRow()+_frame->height()-1){
                	hide(move);
                	_row=_row+1;
                	draw();
            	}
        	}
        	else{
            	if(absRow()>_frame->absRow()&&absRow()>0){
                	hide(move);
                	_row=_row-1;
                	draw();
            	}
        	}   
    	}
    }
    else if(move==C_MOVED_UP){
    	if(_frame){
        	if (bordered()){
            	if(absRow()>_frame->absRow()+1&&absRow()>0){
                	hide(move);
                	_row=_row-1;
                	draw();
            	}
        	}
        	else{
            	if(absRow()>_frame->absRow()&&absRow()>0){
                	hide(move);
                	_row=_row-1;
                	draw();
            	}
        	}    
    	}
    }
    else if(move==C_MOVED_LEFT){
   	 if(_frame){
   	 if(bordered()){
   	 if(absCol()>_frame->absCol()+1&&absCol()>0){
            	hide(move);
   			 _col=_col-1;
            	draw();
   		 }
   	 }
   	 }else{
   		 if(absCol()>_frame->absCol()&&absCol()>0){
   			 hide(move);
   			 _col=_col-1;
            	draw();
   		 }
   	 }
    }
    else if(move==C_MOVED_RIGHT){
   	 if(_frame){
   	 if(bordered()){
   	 if(absCol()+_width<_frame->absCol()+_frame->width()-1){
   			 hide(move);
   			 _col=_col+1;
            	draw();
   	 }
   	 }else{
   		 if(absCol()+_width<_frame->absCol()+_frame->width()){
   			 hide(move);
   			 _col=_col+1;
            	draw();
   	 }
   	 }
   	 }
    }
    else if(move==C_STATIONARY){
        draw();
    }
}

void move(iFrame &mover){
	int keypress;
    console.setPosition(0, console.getCols()-8);
    console << "Moving!";
    console >> keypress;
	while(keypress!=ESCAPE){
		if(keypress==UP){
			mover.move(C_MOVED_UP);
		}
		else if(keypress==DOWN){
			mover.move(C_MOVED_DOWN);
		}
		else if(keypress==LEFT){
			mover.move(C_MOVED_LEFT);
		}
		else if(keypress==RIGHT){
			mover.move(C_MOVED_RIGHT);
		}
        console >> keypress;    
	}
    mover.draw();
}
}//end of namespace