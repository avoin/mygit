/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */

//This assignment focuses on editing and displaying a string passed through by another function. 
//This source code can be considered an addon to console.cpp written by Chris Szalwinski. 
#include <iostream>
#include <cstring>
#include "consoleplus.h"
#include "console.h"
#include "keys.h" 


using namespace std;
namespace cio{
//The following function displays the string passed to it using the criteria given for assignment 1.
//Additional information can be found at: https://cs.senecac.on.ca/~btp300/pages/assignments/a1.html
void display(const char *str, int row, int col, int fieldLen){
    int maxRow; //Holds the maximum rows allowed, defined by console.cpp
    int maxCol; //Holds the maximum columns allowed, defined by console.cpp
    int strLength; //Holds the strLength of *str
    int i; //Counter, normally declared in for statements but is needed for some calculations

    maxRow = console.getRows();
    maxCol = console.getCols();
    console.setPosition(row, col);
    strLength = strlen(str);
    if (fieldLen <= 0){
        console << str;           
        console.setPosition(row, col+strLength-1);
    }       
    else if (strLength < fieldLen){
        for (i=0; i <= strLength && (i+col) < maxCol; i++){
            console << str[i];
        }
        console.setPosition(row, col+i-1);
        if (col+i<maxCol){
            for (i=0; strLength+i <= fieldLen-1; i++){
                console << ' ';
            }
        }
    }
    else if (strLength >= fieldLen){        
        if ((strLength + col) <= maxCol){
            for(i=0; i<fieldLen; i++){
            console<<str[i];
            }
        }
        else{
            for(i=0; (i+col) < maxCol && i <fieldLen; i++){
                console << str[i];
            }   
            console.setPosition(row, col+i-2);
        }    
    }    
	
	
}
//massively rewritten with help of simon
int edit(char* str, int row, int col, int fieldLength, int maxStrLength, 
 bool* insertMode, int* strOffset, int* curPosition,bool isTextEditor, bool isReadOnly) {
    int tabsize=4; //tab
    int i; //counter
    bool done=false; //exit for while loop
    int key=0; //key entered by user
    int maxCol=console.getCols()-1; //maximum line space avaliable
    int strLength=str?strlen(str):0; //string length of str
    int cPosition=curPosition?*curPosition:0; //cursor position relative to screen
    int offset=strOffset?*strOffset:0; //how much the string is offset when displayed
    char* strBackup=NULL; //backup if user exits

    if(cPosition>fieldLength-1){
        cPosition=fieldLength-1;
    }
    if(cPosition>strLength-offset){
        cPosition=strLength-offset;
    }
    if(offset > strLength){
        offset = strLength;
    }
    if(!isTextEditor){
        strBackup = new char[strlen(str)+1];
        strcpy(strBackup,str);
    }
    while(!done){
        strLength=strlen(str);
        display(str+offset, row, col, fieldLength);
        console.setPosition(row, col+cPosition);
        console>>key;
        if(offset>strLength){
            done=true;
        }
        switch(key){
            case RIGHT:
                if(cPosition<fieldLength-1&&offset+cPosition<strLength){
                    if(offset+cPosition<maxCol){
                        cPosition++;
                    }
                    else if(offset>0){
                        cPosition++;
                        offset--;
                        if(isTextEditor){
                            done=true;
                        }    
                    }
                }
                else if (offset+cPosition<strLength){                    
                    offset++;
                    if(isTextEditor){
                        done=true;
                    }
                }
                break;
            case LEFT:
                if(cPosition>0){
                    cPosition--;
                }
                else if(offset>0){
                    offset--;
                    if(isTextEditor){
                        done=true;
                    }
                }                
                break;
            case HOME:
                if(offset){
                    offset=0;
                }
                cPosition=0;
                break;
            case END:
                if (strLength<offset+fieldLength){
                    cPosition=strLength-offset;
                }
                else{
                    offset=strLength-fieldLength+1;
                    cPosition=fieldLength-1;
                    if(isTextEditor){
                        done=true;
                    }
                }
                if(cPosition+offset>maxCol){
                    cPosition=maxCol-offset;
                }
                break;
            case BACKSPACE:
                if(!isReadOnly){
                    if (cPosition>0){
                        cPosition--;
                        for (i=cPosition+offset;str[i];i++){
                            str[i]=str[i+1];
                        }
                        if (cPosition==0){
                            if (offset<tabsize){
                                cPosition+=tabsize+offset;
                                offset=0;
                            }
                            else{
                                cPosition+=tabsize;
                                offset-=tabsize;
                            }
                        }
                    }
                }
                else{
                    if(cPosition!=0){
                        done=true;
                    }
                }
                break;
            case DEL:
                if(!isReadOnly){
                    for(i=cPosition+offset;str[i];i++)
                        str[i]=str[i+1];
                    }
                break;
            case INSERT:
                *insertMode=!*insertMode;
                break;
            case ESCAPE:
                if(!isTextEditor){
                strcpy(str, strBackup);
                }
                done=true;
                break;
            //thanks to simon for this, automatically goes down
            case F(1):
            case F(2):
            case F(3):
            case F(4):
            case F(5):
            case F(6):
            case F(7):
            case F(8):
            case F(9):
            case F(10):
            case F(11):
            case F(12):
            case TAB:
            case ENTER:
            case UP:
            case DOWN:
            case PGUP:
            case PGDN:
                done = true;
                break;
            default:
                if(!isReadOnly){
                    if((*insertMode)){
                        if(strLength<maxStrLength){
                            for(i=strLength+1;i>offset+cPosition;i--){
                                str[i]=str[i-1];
                            }
                            str[i]=key;
                            if(cPosition==fieldLength-1){
                                offset++;
                                if(isTextEditor){
                                    done=true;
                                }
                            }
                            else{
                                cPosition++;
                            }
                        }
                    }
                    else{
                        if(strLength<maxStrLength||(offset+cPosition)<strLength){
                            if(cPosition==fieldLength-1){
                                cPosition--;
                                offset++;
                                if(isTextEditor){
                                    done=true;
                                }
                            }
                            if(str[offset+cPosition]==0){
                                str[offset+cPosition+1]=0;
                            }
                            str[offset+cPosition]=key;
                            if (cPosition!=fieldLength-1){
                                cPosition++;
                            }
                            else{
                                offset++;
                                if(isTextEditor){
                                    done=true;
                                }
                            }
                        }
                    }               
                }
        }
    }
    if (key!=ESCAPE&&strOffset){
        *strOffset=offset;
    }
    if (key!=ESCAPE&&curPosition){
        *curPosition=cPosition;
    }
    if(strBackup){
        delete [] strBackup;
    }
    return key;
}
}//end of cio namespace