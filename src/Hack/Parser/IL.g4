grammar IL;

function    :   command+
            ;

command     :   'push' 'constant' UINT          # pushConstant
            |   'push' 'static' UINT            # pushStatic
            |   'push' SEGMENT UINT             # pushDynamic
            |   'pop' 'constant' UINT           # popConstant
            |   'pop' 'static' UINT             # popStatic
            |   'pop' SEGMENT UINT              # popDynamic
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


SEGMENT     :   'argument'
            |   'local'
            |   'this'
            |   'that'
            |   'pointer'
            |   'temp'
            ;

UINT        :   [0-9]+
            ;
            
COMMENT     :   '//' .*? '\n' -> skip ;
WS          :   [ \t\r\n] -> skip ;            