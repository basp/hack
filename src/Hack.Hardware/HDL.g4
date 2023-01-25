grammar HDL;

chip    : 'CHIP' chipName '{'
          'IN' inputs ';'
          'OUT' outputs ';'
          body
          '}'
        ;

body    : 'PARTS:' part+ ;

part    : partName'(' connections ')' ';'
        ;

partName: ID ;
chipName: ID ;
pinName : ID ;
pinWidth: INT ;

connections
        : conn (',' conn)* ;

conn    : pin '=' pin ;

inputs  : pin (',' pin)*
        ;

outputs : pin (',' pin)*
        ;

pin     : pinName ('[' pinWidth ']')? ;

ID      : [a-zA-Z_][a-zA-Z0-9_]* ;

INT     : DIGIT+ ;

LETTER  : [a-zA-Z] ;

DIGIT   : [0-9] ;

WS      : [ \t\r\n] -> skip ;