// Test Master for Assignment 3
// OOP344 - BTP300
// a3test.cpp
// 
// November 4 2011
// Fardad Soleimanloo, Chris Szalwinski
// Version 1.0

#include <cstring>
#include <string.h>
#include <cstdio>
#include "console.h"
#include "consoleplus.h"
#include "keys.h"
#include "cframe.h"      // for test 1
#include "cdialog.h"     // for test 2
#include "clabel.h"      // for test 2
#include "cline.h"       // for test 3
#include "cbutton.h"     // for test 4

#define TEST_NO 8        // select your test here

#if   TEST_NO == 5
#include "cvalidline.h"  // for test 5
#elif TEST_NO == 6
#include "cswitch.h"      // for test 6
#elif TEST_NO == 7
#include "cmenuitem.h"   // for test 7
#elif TEST_NO == 8
#include "ctext.h"       // for test 8
#endif

using namespace cio;
const int ROW_ERRORS = 22;

int  requestKey(int request);
int  reportBadKey(int request, int key, const char *msg);
void label(int key);
void testLineEditor();
void testFrame();
void testDialogLabel();
void testLineEdit();
void testButton();
void testValidationEditor();
void testSwitch();
void testMenuItem();
void testText();

int main() {
    #if TEST_NO == 0
    testLineEditor();
    #elif TEST_NO == 1
    testFrame();
    #elif TEST_NO == 2
    testDialogLabel();
    #elif TEST_NO == 3
    testLineEdit();
    #elif TEST_NO == 4
    testButton();
    #elif TEST_NO == 5
    testValidationEditor();
    #elif TEST_NO == 6
    testSwitch();
    #elif TEST_NO == 7
    testMenuItem();
    #elif TEST_NO == 8
    testText();
    #endif
    console << "Your test " << TEST_NO + '0' << " is complete.\n\r\n\r";
    console << "Press Enter key to finish ... \n\r";
    console.pause();
}

void testLineEditor() {
    bool insert = false;
    int i, j, key, rows, cols, errors = 0;
    int offset = 0, index = 0; 
    char str[81] = "abcdefghijklmnopqrstuvwxyz";
    char  in[81] = "AbcdefghiJKLmnopqrstuvwxyZ";
    char out[81] = "AbcdefghiJKLmnopqrstuvwxyZ";
    char  ed[81] = "AbCdefghiJKLmnopqrstuv12";
    char end[81] = "AbCdefg12JKLmnopqrstuv12";
    char del[81] = "AbCdef7892JKLmnopqrstuv12";

    // draw the top and left borders
    //
    rows = console.getRows();
    cols = console.getCols();
    console.clear();
    console.setPosition(0, 0);
    j = (int)'0';
    for (i = 0; i < cols; i++) {
        console << j;
        if (j == (int)'9')
            j = (int)'0';
        else
            j++;
    }
    j = (int) '0';
    for (i = 0; i < rows; i++) {
        console.setPosition(i, 0);
        console << j;
        if (j == (int)'9')
            j = (int)'0';
        else
            j++;
    }

    // display instructions
    //
    display("abcdefghijklmnopqrstuvwxyz", 12, 5, 0);
    display("Perform the following instructions in turn", 1, 1, 0);
    display("Press Right Arrow Twice, Down Arrow Twice", 3, 3, 0);
    display("Using Arrow and ASCII keys, change \"jkl\" to \"JKL\"", 4, 3, 0);
    display("Press Home, A, End, Left Arrow, Z, Home, Enter", 5, 3, 0);
    display("Press Home, a, End, Left Arrow, z, Home, Escape", 6, 3, 0);
    display("Press C, End, Backspace 4 times, 1, 2, Home, Enter", 7, 3, 0);
    display("Press End, Home, Right Arrow 7 times, 1, 2, Enter", 8, 3, 0);
    display("Press Delete Twice, Insert, 7, 8, 9, Enter", 9, 3, 0);
    
    // start the test
    //
    console.setPosition(3, 3);
    errors += requestKey(RIGHT);
    if (!errors)console.setPosition(3, 4);
    errors += requestKey(RIGHT);
    if (!errors)console.setPosition(3, 5);
    errors += requestKey(DOWN);
    if (!errors)console.setPosition(4, 5);
    errors += requestKey(DOWN);
    if (!errors)console.setPosition(5, 5);
    if (!errors) {
        key = edit(str, 14, 5, 26, 80, &insert, &offset, &index);
        if (key != ENTER) errors += reportBadKey(ENTER, key, (const char*)NULL);
        if (strcmp(str, out)) errors += reportBadKey(ENTER, 0, "Incorrect string result");
        key = edit(str, 15, 5, 26, 80, &insert, &offset, &index);
        if (key != ESCAPE) errors += reportBadKey(ESCAPE, key, (const char*)NULL);
        if (strcmp(str, in)) errors += reportBadKey(ESCAPE, 0, "Incorrect string result");
        index = 2;
        key = edit(str, 16, 5, 26, 80, &insert, &offset, &index);
        if (key != ENTER) errors += reportBadKey(ENTER, key, (const char*)NULL);
        if (strcmp(str, ed)) errors += reportBadKey(ENTER, 0, "Incorrect string result");
        index = 9;
        key = edit(str, 14, cols - 10, strlen(str), 80, &insert, &offset, &index);
        if (key != ENTER) errors += reportBadKey(ENTER, key, (const char*)NULL);
        if (strcmp(str,end)) errors += reportBadKey(ENTER, 0, "Incorrect string result");
        index = 6;
        key = edit(str, 17, 5, 26, 80, &insert, &offset, &index);
        if (key != ENTER) errors += reportBadKey(ENTER, key, (const char*)NULL);
        if (strcmp(str,del)) errors += reportBadKey(ENTER, 0, "Incorrect string result");
    }

    // finish the test
    //
    console.setPosition(ROW_ERRORS - 3, 3);
    if (errors)
        console << "Continue working! ";
    else
        console << "If no errors, prepare screen shot (include top row of numbers) ... ";

    console.setPosition(ROW_ERRORS - 2, 3);
    console << "Press Enter key to exit!";
    console.pause();
    console.clear();
}

void testFrame() {
    bool done = false;
    CFrame frame;
    CFrame outer(5, 10, 50, 15, true, "+-+|+-+|", &frame);
    CFrame inner(5, 10, 20, 5, true, C_BORDER_CHARS, &outer);

    outer.draw();
    inner.draw();
    console.setPosition(0, 0);
    console << "Press any key...";
    console.pause();

    do {
        int key;  
        console.setPosition(0, 0);
        console << "ESC: exit, F6: Move Container, F7: Move Inner border";
        console >> key;
        switch(key) {
            case ESCAPE:
                done = true;
                break;
            case F(6):
                move(outer);
                inner.draw();
                break;
            case F(7):
                move(inner);
                break;
        }
    } while(!done);

    outer.hide();
    inner.hide();
    console.clear();
}

void testDialogLabel() {
    bool i       = true;
    bool done    = false;
    int  loop    = 0;
    bool visible = false;

    // background
    for(int k = 0; k < console.getRows(); k += 2) {
        for(int m = 0; m < console.getCols() - 10; m += 10) {
            console.setPosition(k, m);
            i=!i;
            console << (i ? "OOP344" : "BTP300");
        }
    }

    CDialog screen;
    CDialog dialog(&screen, 5, 10, 53, 16, true, "+-+|+-+|");
    CLabel  label("This is a non-dynamic label", 5, 3);

    dialog.add(new CLabel("Testing Read Only Dialog", 1, 12));
    dialog << new CLabel("A trimmed dynamic label goes here, and I'm checking if it is trimmed", 3, 3, 50) << label;
    int mesIndx = dialog.add(new CLabel("Test", 7, 3, 40));
    dialog << new CLabel("ESC to exit, F6 to move, other to loop", 9, 3);
    dialog[mesIndx].set("Setting the message to see what happens");
    dialog << new CLabel("Press F6, Right Arrow Twice, Up Arrow Once, ESC", 11, 3);
    dialog << new CLabel("If there are no errors take your screen shot!", 13, 3);
    dialog.draw();

    do {
        int key = dialog.edit(mesIndx+1);
        loop++;
        std::sprintf(*(char**)dialog[mesIndx].data(), "LOOP No: %d", loop);    
        switch(key) {
            case ESCAPE:
                done = true;
                break;
            case F(6):
                move(dialog);
                break;
        }
    } while(!done);

    dialog.hide();
    console.clear();
}

void testLineEdit() {
    int  loop     = 0;
    bool i        = true;
    bool insert   = true;
    bool done     = false;
    char str[81]  = "I want to edit this thing!";

    // background
    for(int k = 0; k < console.getRows(); k += 2) {
        for(int m = 0; m < console.getCols() - 10; m += 10) {
            console.setPosition(k, m);
            i=!i;
            console << (i ? "OOP344":"BTP300");
        }
    }

    CDialog app;
    CDialog dialog(&app, 5, 10, 50, 15, true, "+-+|+-+|");
    CLabel  label("Enter some text down here:", 6, 4);

    app << new CLabel("Dialog and Line Editor Tester", 0, 0);

    dialog.add(new CLabel("Testing Label and Line edit", 0, 12));
    dialog << new CLabel("Name: ", 4, 3)
     << new CLine(4, 9, 20, 40, &insert) << label
     << new CLine(str, 7, 4, 40, 80, &insert, true);
    int mesIndx = dialog.add(new CLabel(10, 5, 40));
    dialog << new CLabel("Press ESC or F2 to exit, press F6 to Move", 2, 3);
    dialog[mesIndx].set("Setting the message to see what happens");
    dialog.draw();

    do {
        int key = dialog.edit(mesIndx + 1);
        loop++;
        std::sprintf(*(char**)dialog[mesIndx].data(), "LOOP No: %d", loop);    
        switch(key) {
            case ESCAPE:
            case F(2):
                done = true;
                break;
            case F(6):
                move(dialog);
                break;
        }
    } while(!done);

    console.clear();
    console.setPosition(10, 0);
    console << "First  Lineedit data:";
    console.setPosition(10, 23);
    console << (char*)dialog[2].pdata();
    console.setPosition(12, 0);
    console << "Second Lineedit data:";
    console.setPosition(12, 23);
    console << (char*)dialog[4].pdata();
    console.setPosition(14, 0);
}

void testButton() {
    bool done    = false;
    bool i       = true;
    int fn       = C_FULL_FRAME;

    for(int k = 0; k < console.getRows(); k += 2) {
        for(int m = 0; m < console.getCols() - 10; m += 10) {
            console.setPosition(k, m);
            i=!i;
            console << (i ? "OOP344" : "BTP300");
        }
    }

    CDialog app;
    CDialog dialog(&app, 5, 10, 50, 15, true, "+-+|+-+|");
    CButton inc("Increase", 9, 10);
    CButton dec("Decrease", 9, 30);
    dialog.add(new CLabel("Testing Buttons (bordered)", 1, 12, 30));
    dialog.add(new CLabel("Press F10 to toggle button borders visiblity", 3, 3));
    int mesIndx = dialog.add(new CLabel(7, 24, 10));

    int j = 100;
    dialog << inc << dec
      << new CLabel("Press Escape to exit", 12, 15);
    dialog[mesIndx].set("100");

    do {
        int key = dialog.edit(fn);
        switch(key){
            case ESCAPE:
                done = true;
                break;
            case F(10):
                inc.bordered(!inc.bordered());
                dec.bordered(!dec.bordered());
                dialog[0].set(inc.bordered() ? "Testing Buttons (bordered)" : "Testing Buttons (no border)");
                dialog.draw();
                break;
            case F(6):
                move(dialog);
                break;
            case C_BUTTON_HIT:
                if (&dialog.curField() == &inc) {
                    j++;
                    fn = 4;
                }
                else {
                    j--;
                    fn = 5;
                }
                std::sprintf(*(char**)dialog[mesIndx].data(), "%d", j);
                dialog[mesIndx].draw();
                break;
        }
    } while(!done);

    console.clear();
    console.setPosition(10, 0);
    console << "Final Button Value:";
    console.setPosition(10, 23);
    console << (char*)dialog[mesIndx].pdata();
    console.setPosition(12, 0);
}

#if TEST_NO == 5
static bool yes(const char* message, CDialog* owner);
static void help(CDialog* owner, int rows, int cols);
static void phoneHelp(CMessageStatus st, CDialog& owner);
static void lastNameHelp(CMessageStatus st, CDialog& owner);
static bool validPhone(const char* ph, CDialog& owner);

void testValidationEditor() {
    bool i      = true;
    bool insert = true;
    bool done   = false;

    for(int k = 0; k < console.getRows(); k += 2) {
        for(int m = 0; m < console.getCols() - 10; m += 10) {
            console.setPosition(k, m);
            i = !i;
            console << (i ? "OOP344" : "BTP300");
        }
    }
    CDialog screen;
    CDialog dialog(&screen, 5, 5, 70, 15, true);
    CLabel  phHelp(8, 34, 30);
    CLabel  lnHelp(5, 37, 30);
    CLabel  errMes(10, 2, 67);
    screen << new CLabel("F1: Help  Esc: Exit ", 0, 0);
    screen.draw();
    dialog << new CLabel("Name:", 2, 2)
           << new CLine(1, 10, 20, 40, &insert, true)
           << new CLabel("Surname:", 5, 2)
           << new CValidLine(4, 13, 20, 40, &insert, C_NO_VALIDATIONFUNC, lastNameHelp, true)
           << new CLabel("Phone Number", 8,2)
           << new CValidLine(7, 16, 15, 12, &insert, validPhone, phoneHelp, true)
           << phHelp
           << lnHelp
           << errMes
           << new CLabel("F1: Help, F6: Move, Esc: Exit", 12, 2);
    dialog.draw();

    do {
        int key = dialog.edit();
        switch(key) {
            case F(1):
                help(&dialog, (console.getRows() - 10) / 2, (console.getCols() - 40) / 2);
                break;
            case F(6):
                move(dialog);
                break;
            case ESCAPE:
                done = yes("Do you really want to quit?", &dialog);
                break;
        }
    } while(!done);

    dialog.hide();
    console.clear();
    console.setPosition(10, 0);
    console << "Name         :";
    console.setPosition(10, 15);
    console << (char*)dialog[1].pdata();
    console.setPosition(12, 0);
    console << "Surname      :";
    console.setPosition(12, 15);
    console << (char*)dialog[3].pdata();
    console.setPosition(14, 0);
    console << "Phone Number :";
    console.setPosition(14, 15);
    console << (char*)dialog[5].pdata();
    console.setPosition(16, 0);
}

#elif TEST_NO == 6

void testSwitch() {
    bool i      = true;
    bool insert = true;
    bool done   = false;

    CDialog screen;
    for(int k = 0; k < console.getRows(); k += 2) {
        for(int m = 0; m < console.getCols() - 10; m += 10) {
            screen << new CLabel((i = !i) ? "OOP344" : "BTP300", k, m, 9);
        }
    }
    screen << new CLabel("F2: Reset F6: Move  Esc: Exit", 0, 0);
    screen.draw();

    CDialog dialog(&screen, 5, 5, 35, 9, true);
    CSwitch  check(true, "[x]", "Check Box", 2, 3, 16);
    dialog << check
           << new CLabel("", 2, 17, 15)
           << new CSwitch(false, "(o)", "Radio Button", 4, 3, 16, true)
           << new CLabel("", 4, 20, 13)
           << new CLabel("Space: Toggle or Set", 6, 3);
    dialog.draw();
    CSwitch* radio = (CSwitch*)&dialog[2];

    do {
        dialog[1].set(check.selected() ? "Checked" : "Not Checked");
        dialog[3].set(bool(*((bool*)dialog[2].pdata())) ? "Checked" : "Not Checked");
        // Un-comment next line and comment line above for alternative way of accessing the checkbox's flag
        // dialog[3].set(radio->selected() ? "Checked" : "Not Checked");
        int key = dialog.edit();
        switch(key) {
            case F(2):
                check.selected(false);
                radio->selected(false);
                dialog.draw();
                break;
            case F(6):
                move(dialog);
                break;
            case ESCAPE:
                done = true;
                break;
        }
    } while(!done);

    dialog.hide();
    console.clear();
    console.setPosition(10, 0);
    console << "Check Box    :";
    console.setPosition(10, 15);
    console << (check.selected() ? "Checked" : "Not Checked");
    console.setPosition(12, 0);
    console << "Radio Button :";
    console.setPosition(12, 15);
    console << (radio->selected() ? "Checked" : "Not Checked");
    console.setPosition(14, 0);
}

#elif TEST_NO == 7

void testMenuItem() {
    int insert  = 1;
    bool i      = true;
    bool done   = false;

    CDialog screen;
    for(int k = 0; k < console.getRows(); k += 2) {
        for(int m = 0; m < console.getCols() - 10; m += 10) {
            screen << new CLabel((i = !i) ? "OOP344" : "BTP300", k, m, 9);
        }
    }
    screen << new CLabel("F6: Move  Esc: Exit", 0, 0);
    screen.draw();

    CDialog dialog(&screen, 5, 5, 38, 11, true);
    CMenuItem m1(false, "()", "One",   2, 2, 7);
    CMenuItem m2(true,  "()", "Two",   3, 2, 7);
    CMenuItem m3(false, "()", "Three", 4, 2, 7);
    CMenuItem* m[3] = {&m1, &m2, &m3};
    dialog << m1 << m2 << m3 
           << new CLabel("Space: Select, Enter: Select + Quit", 6, 2, 35) 
           << new CLabel("Selected Item : ", 8, 2) 
           << new CLabel("Two", 8, 18, 6);
    dialog.draw();

    do {
        int key = dialog.edit();
        switch(key) {
            case C_MENU_SELECT_AND_QUIT:
                done = true; // comment this for testing
            case SPACE:
                for(int i = 0; i < 3; i++) {
                    if(&dialog.curField() == m[i])
                        dialog[5].set(m[i]->text());
                    else
                        m[i]->selected(false);
                }
                break;
            case F(6):
                move(dialog);
                break;
            case ESCAPE:
                done = true;
                break;
        }
    } while(!done);

    dialog.hide();
    console.clear();
    console.setPosition(10, 0);
    console << "Selected Item :";
    console.setPosition(10, 16);
    for(int i = 0; i < 3; i++)
        if(m[i]->selected())
            console << m[i]->text(); 
    console.setPosition(12, 0);
}

#elif TEST_NO == 8

static void textHelp(CDialog* owner, int rows, int cols);
static bool yes(const char* message, CDialog* owner);

void testText() {
    char str[4096]="Homer Jay Simpson, the patriarch of the Simpson household on the"
                  " Fox series\n\"The Simpsons\" is a childish, lazy man, whose hobbies"
                  " include eating donuts,\ndrinking Duff Beer, watching television, "
                  "and sleeping.\nA victim of the \"Simpsons gene\" which allows for "
                  "only Simpson women to possess the trait of intelligence,\nHomer is "
                  "unfortunately as \"dumb as a chimp\" according to his father,\nAbe "
                  "Simpson.\nHowever, it is mainly through the analysis of his simplistic "
                  "thoughts\nand nature, that one can gain a real perspective on Homer’s "
                  "complex personality.\n\n"
                  "Spending most of his time in high school smoking,\ndrinking beer,\nand "
                  "getting into trouble,\n(He even met his wife, Marge, while serving detention.)\n"
                  "Homer’s lack of motivation for achievement grew with him into adulthood.\n"
                  "The fat, balding character ends up working in Sector 7G of the Springfield "
                  "Nuclear Power Plant,\nwhere he holds the record for most years worked at an"
                  " entry level position.\nEven in the opening credits of the show,\nhe is seen"
                  " negligently tossing aside radioactive waste as the whistle blows to end the"
                  " workday.\nIn addition to his laziness at work, his sloth is also displayed"
                  " in his free time\nwhere he is seen either lounging on his couch while"
                  " indulging in donuts\nand watching anything that comes on television or"
                  " drinking at Moe\'s Tavern\nwith his lifelong friends, "
                  "Barney, Carl, Lenny, and Moe.";

    bool insert = true;
    bool i      = true;
    bool done   = false;
    CDialog screen;

    for(int k = 0; k < console.getRows(); k += 2) {
        for(int m = 0; m < console.getCols() - 10; m += 10) {
            screen << new CLabel((i = !i) ? "OOP344" : "BTP300", k, m, 9);
        }
    }
    screen << new CLabel("F1: Help  F2: ReadOnly Toggle F6: Move  F10: Exit", 0, 0);
    screen.draw();

    CDialog dialog(&screen, 2, 3, 70, 20, true);
    CText text(str, 2, 3, 60, 14, false, &insert);
    dialog << text;
    dialog.draw(C_FULL_FRAME);

    do {
        int key = dialog.edit();
        switch(key) {
        case F(1):
            textHelp(&dialog, (console.getRows() - 10) / 2, (console.getCols() - 40) / 2);
            break;
        case F(2):
            text.readOnly(!text.readOnly());
            break;
        case F(10):
            done = yes("Do you really want to quit?", &dialog);
            break;
        case F(6):
            move(dialog);
            break;
        }
    } while(!done);

    dialog.hide();
    console.clear();

    char* string = *((char**)(text.data()));

    const int ROW_START = 10;
    int row = ROW_START;
    console.setPosition(row++, 0);
    console << "Text :";
    console.setPosition(row++, 0);
    int col = 0;
    while (*string) {
        if (col == console.getCols()) {
            col = 0;
            console.setPosition(row++, 0);
        }
        if (row == console.getRows()) {
            console << "Press Enter key to continue ... ";
            console.pause();
            console.clear();
            row = ROW_START;
            console.setPosition(row++, 0);
            console << "Text (continued) :";
            console.setPosition(row++, 0);
            col = 0;
        }
        if (*string == '\n') {
            console.setPosition(row++, 0);
            col = 0;
            *string++;
        }
        else {
            col++;
            console << *string++;
        }
    }
}

#endif

//--------------------------- test 0 functions ------------------------------
//
// Request a key press, accept the key code and
// report the difference if any
//
int requestKey(int request) {
    int key, rc = 0;

    console >> key;
    if (key != request) {
        rc = reportBadKey(request, key, 0);
    }
    return rc;
}

/* Report a faulty key press */
int reportBadKey(int request, int key, const char *msg) {
    static int row = ROW_ERRORS;

    if (row != ROW_ERRORS) {
        console.setPosition(ROW_ERRORS - 1, 8);
        console << "s!";
    } else {
        console.setPosition(ROW_ERRORS - 1, 3);
        console << "Error!";
    }
    console.setPosition(row++, 3);
    if (key != 0) {
        console << "Key requested : ";
        label(request);
        console.setPosition(row++, 3);
        console << "You pressed   : ";
        label(key);
    } else
        console << msg;
    return 1;
}

// Displays key label at the current cursor position 
//
void label(int key) {
    if (key >= ' ' && key <= '~') {
        console << key;
        console << "            ";
    } 
    else {
        switch (key) {
            case LEFT:      console << "Left Arrow   "; break;
            case RIGHT:     console << "Right Arrow  "; break;
            case HOME:      console << "Home         "; break;
            case END:       console << "End          "; break;
            case INSERT:    console << "Insert       "; break;
            case DEL:       console << "Delete       "; break;
            case BACKSPACE: console << "Backspace    "; break;
            case ESCAPE:    console << "Escape       "; break;
            case ENTER:     console << "Enter        "; break;
            case TAB:       console << "Tab          "; break;
            case UP:        console << "Up Arrow     "; break;
            case DOWN:      console << "Down Arrow   "; break;
            case PGUP:      console << "Page Up      "; break;
            case PGDN:      console << "Page Down    "; break;
            case F(1):      console << "F1           "; break;
            case F(2):      console << "F2           "; break;
            case F(3):      console << "F3           "; break;
            case F(4):      console << "F4           "; break;
            case F(5):      console << "F5           "; break;
            case F(6):      console << "F6           "; break;
            case F(7):      console << "F7           "; break;
            case F(8):      console << "F8           "; break;
            case F(9):      console << "F9           "; break;
            case F(10):     console << "F10          "; break;
            case F(11):     console << "F11          "; break;
            case F(12):     console << "F12          "; break;
            case UNKNOWN:   console << "Unknown Key  "; break;
            default:        console << "non-ASCII key";
        }
    }
}

//--------------------------- test 5-8 functions ------------------------------
//
bool yes(const char* message, CDialog* owner) {
    bool res  = false;
    bool done = false;
    CButton bt_yes("yes", 4,  4, true, "     _  ");
    CButton bt_no ("no",  4, 15, true, "     _  ");
    CDialog yesNo(owner, (console.getRows() - 10) / 2, 
     (console.getCols() - 40) / 2, 40, 10, true);

    yesNo << new CLabel(2, 2, 36) << bt_yes << bt_no;
    yesNo[0].set(message);
    yesNo.draw(C_FULL_FRAME);

    do {
        int key = yesNo.edit();
        if(key == C_BUTTON_HIT) {
            res  = &yesNo.curField() == &bt_yes;
            done = true;
        }
    } while(!done);

    yesNo.hide();
    return res;
}

void help(CDialog* owner, int row, int col){
    CDialog help(owner, row, col, 40, 10, true);
  
    help << new CLabel(2, 3,36)
         << new CLabel("Escape Key: Exit the test program.", 4, 3)
         << new CLabel("F1 Key: Open this window.", 6, 3); 
    switch(owner->curIndex()) {
        case 1:
            help[0].set("Enter the name here!");
            break;
        case 3: 
            help[0].set("Enter the Last name here!");
            break;
        case 5:
            help[0].set("Enter Phone number: 999-999-9999");
    }
    help.edit(C_FULL_FRAME);
    help.hide();
}

void phoneHelp(CMessageStatus st, CDialog& owner) {
    if(st == C_CLEAR_MESSAGE) {
        owner[6].set("                          ");
    }
    else {
        owner[6].set("Phone Format: 999-999-9999");
    }
    owner.draw(7);
}

void lastNameHelp(CMessageStatus st, CDialog& owner){
    if(st == C_CLEAR_MESSAGE) {
        owner[7].set("                            ");
    }
    else {
        owner[7].set("i.e. Middle name and Surname");
    }
    owner.draw(8);
}

bool validPhone(const char* ph , CDialog& owner){
    bool ok = true;
    int  i  = 0;

    while (i < 3 && (ok = ph[i] >= '0' && ph[i] <= '9')) i++;
    ok = ok && ph[i++] == '-';
    while (i < 7 && (ok = ok && ph[i] >= '0' && ph[i] <= '9')) i++;
    ok = ok && ph[i++] == '-';
    while (i < 12 && (ok = ok && ph[i] >= '0' && ph[i] <= '9')) i++;

    if (ok)
        owner[8].set("                                                                   ");
    else
        owner[8].set("Invalid phone number, please use the specified phone number format!");

    owner.draw(9);
    return ok;
}

void textHelp(CDialog* owner, int row, int col){
    CDialog help(owner, row, col, 40, 12, true);
  
    help << new CLabel("F1 Key: Open this window.", 2, 3)
         << new CLabel("F2 Key: Toggle readonly mode", 4, 3)
         << new CLabel("F6: Move", 6, 3)
         << new CLabel("F10: Exit the test program.", 8, 3);
    help.edit(C_FULL_FRAME);
    help.hide();
}
