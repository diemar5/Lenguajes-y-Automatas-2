#include <stdio.h>
#include <math.h>
#include <iostream>

char a;
int i;
float c;

void main() // Funcion principal
{
    for (i=0; i<10; i++)
    {
        printf("El valor de i en el for es: ", i);
        printf("\n");
    }
    while (i<20)
    {
        printf("El valor de i en el while es: ", i);
        printf("\n");
        i++;
    }
}