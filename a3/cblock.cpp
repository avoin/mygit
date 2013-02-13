/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */
#include <cstring>
#include "cblock.h"
#include <string.h>

namespace cio {

void CBlock::purge() {
    for(int i = 0; i < noLines; i++)
        if(block[i])
            delete[] block[i];
    delete [] block;
    block = 0;
}

CBlock::CBlock(int nlines, int wid) : noLines(0), lastFilledLine(0) {
    width = wid;
    expand(nlines);
}

void CBlock::set(const char* str) {
    int i;
    int j  = 0;
    int k  = 0;
    int len = strlen(str);

    for(i = 0; i < len; i++) {
        if (!block[j])
            block[j] = new char[width + 1];
        if (str[i] != '\n' && k < width) {
            block[j][k] = str[i];
            k++;
        }
        else {
            block[j][k] = 0;
            k = 0;
            j++;
        }
        if (j == noLines)
            expand(C_EXPAND_DEPTH);
    }
    if (!block[j])
        block[j] = new char[width + 1];
    block[j][k] = 0;
    lastFilledLine = j;
}

CBlock::CBlock(const CBlock& src) {
    int i;
    noLines = 0;
    expand(src.noLines);
    for(i = 0; i < noLines; i++) {
        if(src.line(i)) {
            block[i] = new char[width + 1];
            std::strcpy(block[i], src.line(i));
        }
    }
    lastFilledLine = src.lastFilledLine;
}

CBlock& CBlock::operator=(const CBlock& src) {
    if(&src != this) {
        int i;
        if(src.noLines > noLines)
            expand(src.noLines - noLines);
        for(i = 0; i < noLines; i++) {
            if(src.line(i)) {
                if(!line(i))
                    block[i] = new char[width + 1];
                std::strcpy(block[i], src.line(i));
            }
        }
        for(; i < noLines; i++) {
            if(line(i)) {
                delete [] block[i];
                block[i] = (char*)0;
            }
        }
        lastFilledLine = src.lastFilledLine;
    }

    return *this;
}

char* CBlock::operator[](int index) {

    if(index >= noLines)
        expand(index - noLines < C_EXPAND_DEPTH ? C_EXPAND_DEPTH : index - noLines);
    if(!block[index]) {
        block[index] = new char[width + 1]; 
        block[index][0] = 0;
    }
    if(index > lastFilledLine)
        lastFilledLine = index;

    return block[index];
}

const char* CBlock::line(int index) const {
    return block[index % noLines];
}

CBlock& CBlock::expand(int more) {
    int i;
    char** newBlock = new char*[noLines + more];

    for(i = 0; i < noLines; i++)
        newBlock[i] = block[i];
    for(; i < noLines + more; i++)
        newBlock[i] = (char*)0;
    if(noLines)
        delete [] block;
    block    = newBlock;
    noLines += more;
    return *this;
}

CBlock::~CBlock(){
    purge();
}

int CBlock::lastLine() const {
    return lastFilledLine;
}

void CBlock::insert(int index) {
    index = index % noLines;
    if(lastFilledLine == noLines - 1)
        expand(C_EXPAND_DEPTH);
    for(int i = lastFilledLine ; i >= index; i--)
        block[i+1] = block[i];
    lastFilledLine++;
    block[index] = (char*)0;
}

void CBlock::remove(int index) {
    if(lastFilledLine) {
        index = index % noLines;
        if(block[index])
            delete [] block[index];
        int i;
        for(i = index; i < lastFilledLine; i++)
            block[i] = block[i+1];
        block[i] = (char*)0;
        lastFilledLine--;
    }
}

} // end of cio namespace