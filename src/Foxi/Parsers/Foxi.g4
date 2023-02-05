grammar Foxi;

prog        :   stmt+
            ;

stmt        :   forStmt                                             # ForStatement                 
            |   whileStmt                                           # WhileStatement
            |   ifStmt                                              # IfStatement
            |   tryStmt                                             # TryStatement
            |   forkStmt                                            # ForkStatement
            |   expr ';'                                            # ExpressionStatement
            ;

forkStmt    :   'fork' identifier? group
                stmt+
                'endfork'
            ;

ifStmt      :   'if' group stmt*
                ('else if' group stmt*)*
                ('else' stmt*)?
                'endif'
            ;

forStmt     :   'for' ID 'in' (range | group) 
                stmt* 
                'endfor'
            ;

whileStmt   :   'while' group 
                stmt* 
                'endwhile' 
            ;

tryStmt     :   'try'
                stmt+
                ('except' ID '(' args ')' stmt*)*
                ('finally' stmt+)?
                'endtry'
            ;

expr        :   primary                                             # PrimaryExpression
            |   call                                                # CallExpression
            |   range                                               # RangeExpression
            |   expr op='.' (ID | group)                            # PropertyReference
            |   expr op=':' (ID | group) '(' args ')'               # MethodCall
            |   op=('+'|'-') expr                                   # PrefixExpression
            |   op='!' expr                                         # PrefixExpression
            |   op='@' expr                                         # SplatExpression
            |   expr op=('*'|'/'|'%') expr                          # InfixExpression
            |   expr op=('+'|'-') expr                              # InfixExpression
            |   expr bop=('<='|'>='|'<'|'>')                        # InfixExpression
            |   expr op=('=='|'!=') expr                            # InfixExpression
            |   expr bop='&&' expr                                  # InfixExpression
            |   expr bop='||' expr                                  # InfixExpression
            |   <assoc=right> expr op='?' expr '|' expr             # IfExpression
            |   <assoc=right> expr                                  
                op=('='|'+='|'-='|'*='|'/='|'%=')
                expr                                                # Assignment
            |   <assoc=right> '`' expr op='|' error '=>' expr '\''  # TryExpression
            ;

primary     :   group                                               
            |   literal                                             
            |   error                                               
            |   identifier                                          
            ;

error       :   'E_NONE'
            |   'E_INVARG'
            |   'E_TYPE'
            |   'E_VARNF'
            |   'ANY'
            ;

literal     :   list
            |   dict
            |   OBJ
            |   STRING
            |   HEX
            |   INT
            |   FLOAT
            |   'true'
            |   'false'
            ;

identifier  :   ID ;

call        :   identifier '(' args ')' ;

group       :   '(' expr ')' ;

args        :   expr (',' expr)*
            |
            ;

params      :   ID (',' ID)*
            |
            ;

range       :   '[' expr '..' expr ']' ;

list        :   '{' elems '}' ;

elem        :   ID '?' ('=' expr)?
            |   expr
            ;

elems       :   elem (',' elem)* ','?
            |
            ;

dict        :   '[' pairs ']' ;

pair        :   expr '=>' expr ;

pairs       :   pair (',' pair)* ','?
            |
            ;

ALIAS       :   '$' ID? ;

ID          :   [a-zA-Z_][a-zA-Z0-9_]* ;

OBJ         :   '#' '-'? INT ;

HEX         :   '0' ('x' | 'X') HEXDIGIT+ ;

INT         :   DIGIT+ ;

FLOAT       :   DIGIT+ '.' DIGIT+ EXP?
            |   DIGIT+ EXP?
            ;

STRING      :   '"' (ESC | ~[\\"])*? '"' ;

WS          :   [ \t\r\n] -> skip ;

fragment    HEXDIGIT    :   [0-9a-fA-F] ;
fragment    DIGIT       :   [0-9] ;
fragment    LETTER      :   [a-zA-Z] ;
fragment    EXP         :   ('E' | 'e') ('+' | '-')? INT ;
fragment    ESC         :   '\\' ([tnr]|'"'|'\'') ;