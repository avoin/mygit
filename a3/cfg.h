/* Assignment 3               */
/* Frame Classes              */
/* BTP300                     */
/* Barath Kumar               */
/* Krishanthan Lingeswaran    */
/* Filza Tahir 				  */
/* __________________________ */
#ifndef cfg_h
#define cfg_h

#define C_MAX_NO_FIELDS                 100
#define C_BUTTON_HIT                      1
#define C_NOT_EDITABLE                    0
#define C_BORDER_CHARS         "/-\\|/-\\|"
#ifndef NULL
#define NULL                         (void*)0
#endif
#define C_FULL_FRAME -1
#define C_NO_FRAME    0
#define C_NO_HELPFUNC ((void(*)(CMessageStatus, CDialog&))(0))
#define C_NO_VALIDATIONFUNC ((bool(*)(const char*, CDialog&))(0))
#define C_MENU_SELECT_AND_QUIT (2)
#define C_INIT_BLOCK_DEPTH (50u)
#define C_EXPAND_DEPTH (50u)
#define C_BLOCK_WIDTH (1024u)

namespace cio {
    enum CDirection {C_STATIONARY, C_MOVED_LEFT, C_MOVED_RIGHT, C_MOVED_UP, C_MOVED_DOWN};
	enum CMessageStatus {C_CLEAR_MESSAGE, C_SET_MESSAGE};
}
#endif

