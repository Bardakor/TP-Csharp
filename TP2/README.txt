Debugger :

Ex 1 : You needed to replace the 42 in the line "bool isDivisor = Misc.IsDivisorOf (666, 42);" 
by the previously defined variable div. Also, you had to remove the "&" opertor from "&=" since 
this operator concatenates two strings. 

Ex 2: You needed to change the type of i so it could reach -1. We change it's type from a uint to 
an int. Then, we change the stop case of the for loop from " i >= 0 " to " i != -1"

Ex 3: SubFunction 2 is uspposed to verify if an array is ordered. Yet, the function crashes 
beacuse i is initialised wrongfully. We need it to be the length of the array. We also need to 
invert the comparison to "arr[i-1] <= arr[i]" to manage the growing order. There is also need to 
change the stop case to 1. Also, the final return must be the boolean i == 1. SubFunction 1 has 
no problem whatsoever. In the function Exo 3, we must shift it in initializing i to 0 and calling 
Subfunction1 with an i. Otherwise, it wouldn't correctly sort the last element. 

This Tp was made in C# by Liam ABOUROUSSE   



