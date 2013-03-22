#ifndef _FS_CBLOCK_H_
#define _FS_CBLOCK_H_
/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */
#include "cfg.h"

namespace cio {

class CBlock {
    char**  block;
    int    noLines;
    int    width;
    int    lastFilledLine;

    void    purge();
    CBlock& expand(int);
public:               
    CBlock(int = C_INIT_BLOCK_DEPTH, int = C_BLOCK_WIDTH);
    CBlock(const CBlock& D);
    CBlock& operator=(const CBlock& D);
    ~CBlock();
    void  set(const char* str);
    char* operator[](int index);
    const char* line(int index) const;
    int  lastLine() const;
    void  insert(int index);
    void  remove(int index);
};

} // end of cio namespace

#endif