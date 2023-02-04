grammar Hack;

program :  command+ ;

compute :   (dest '=')? comp (';' jump)?
        ;

address : (SYM_ADDRESS | ADDRESS) ;

instruction
        : address
        | compute
        ;

command     
        :   instruction
        |   label
        ;

label   :   '(' NAME ')'
        ;

comp    :   '0'
        |   '1'
        |   '-1'
        |   'D'
        |   'A'     |   'M'
        |   '!D'
        |   '!A'    |   '!M'
        |   '-D'
        |   '-A'    |   '-M'
        |   'D+1'
        |   'A+1'   |   'M+1'
        |   'D-1'
        |   'A-1'   |   'M-1'
        |   'D+A'   |   'D+M'
        |   'D-A'   |   'D-M'
        |   'A-D'   |   'M-D'
        |   'D&A'   |   'D&M'
        |   'D|A'   |   'D|M'
        ;

dest    :   'M'
        |   'D'
        |   'MD'
        |   'A'
        |   'AM'
        |   'AD'
        |   'AMD'
        ;

jump    :   'JGT'
        |   'JEQ'
        |   'JGE'
        |   'JLT'
        |   'JNE'
        |   'JLE'
        |   'JMP'
        ;

UINT    :   [0-9]+ ;
NAME    :   [a-zA-Z_$.:][a-zA-Z0-9_$.:]* ;

SYM_ADDRESS
        :   '@' NAME
        ;

ADDRESS :   '@' UINT
        ;

COMMENT :   '//' .*? '\n' -> skip ;
WS      :   [ \t\r\n] -> skip ;