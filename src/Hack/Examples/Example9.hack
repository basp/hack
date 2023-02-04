/*
if (cond)
    s1
else
    s2
end

    VM code for computing ~(cond)
    if-goto L1
    VM code for computing S1
    goto L2
label L1
    VM code for computing S2
label L2
*/

