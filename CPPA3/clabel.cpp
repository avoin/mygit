/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */

#include "clabel.h"
#include "consoleplus.h"
#include <cstring>

using namespace std;
namespace cio {
//A copy constructor which copies the source object's data 
//into the dynamic memory that stores the label's data	
CLabel::CLabel(const CLabel& L):CField(L){

	allocateAndCopy((char*)L.pdata());

}

void CLabel::allocateAndCopy(const char *Str){
	//allocates dynamic memory for the C-Style null-terminated string at the received address
	*data() = new char[width()+1];
	//copies the data into the newly allocated memory
	strncpy(*(char**)data(), Str, width());
	(*(char**)data())[width()] = '\0';
}

CLabel::CLabel(const char *Str, int Row, int Col, int Len) : CField(Row,Col,Len==-1?strlen(Str):Len){

allocateAndCopy(Str);

}

CLabel::CLabel(int Row, int Col, int Len) : CField(Row,Col,Len){

allocateAndCopy("");

}

CLabel::~CLabel(){
	delete[] (char*)pdata();
}

void CLabel::draw(int fn){
//Display the label 
	CField::display(0);
}

int CLabel::edit(){

draw();
return C_NOT_EDITABLE;

}
	
bool CLabel::editable()const{
	return false;
}

void CLabel::set(const void* Str){

width((int)strlen((const char*)Str));
//deallocates the dynamic memory where the label's data has been stored
delete [] (char*)pdata();
//allocates dynamic memory for that data and copies the data in the newly allocated memory
allocateAndCopy((const char*)Str);
}
}//end of namespace cio