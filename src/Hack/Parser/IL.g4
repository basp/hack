grammar IL;

function    :   command+
            ;

command     :   'push' 'local' UINT             # pushLocal
            |   'push' 'argument' UINT          # pushArgument
            |   'push' 'this' UINT              # pushThis
            |   'push' 'that' UINT              # pushThat
            |   'push' 'pointer' UINT           # pushPointer
            |   'push' 'temp' UINT              # pushTemp
            |   'push' 'constant' UINT          # pushConstant
            |   'push' 'static' UINT            # pushStatic
            |   'pop' 'local' UINT              # popLocal
            |   'pop' 'argument' UINT           # popArgument
            |   'pop' 'this' UINT               # popThis
            |   'pop' 'that' UINT               # popThat
            |   'pop' 'pointer' UINT            # popPointer
            |   'pop' 'temp' UINT               # popTemp
            |   'pop' 'static' UINT             # popStatic
            |   'add'                           # add
            |   'sub'                           # sub
            |   'neg'                           # neg
            |   'eq'                            # eq
            |   'gt'                            # gt
            |   'lt'                            # lt
            |   'and'                           # and
            |   'or'                            # or
            |   'not'                           # not
            ;

UINT        :   [0-9]+
            ;
            
COMMENT     :   '//' .*? '\n' -> skip ;
WS          :   [ \t\r\n] -> skip ;            