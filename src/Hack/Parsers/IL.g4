grammar IL;

program     :   command+ ;

command     :   'call' NAME UINT                # call
            |   'function' NAME UINT            # function
            |   'return'                        # return
            |   'if-goto' NAME                  # ifGoto
            |   'goto' NAME                     # goto   
            |   'label' NAME                    # label
            |   'push' 'local' UINT             # pushLocal
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

UINT        :   [0-9]+ ;
NAME        :   [a-zA-Z_$.:][a-zA-Z0-9_$.:]* ;
COMMENT     :   '//' .*? '\n' -> skip ;
WS          :   [ \t\r\n] -> skip ;            