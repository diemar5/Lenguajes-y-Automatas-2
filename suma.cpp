#include <stdio.h>
#include <math.h>
#include <iostream>

int i;


void main() // Funcion principal
{
    for(i=0; i<5; i++)
   {
    printf("El valor del bucle for es: ",i);
    printf("\n");
   }
   do
   {
    printf("El valor de do-while es: ",i);
    printf("\n");
    i=i+1;
   } while (i<=9);
}