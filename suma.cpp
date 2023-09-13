#include<stdio.h>
#include<math.h>
#include<iostream>

float a,b,c;

void main() // Funcion principal
{
    a=(3+5)*8-(10-4)/2; //a=61
    b=8;
    printf("Valor de c = ");
    scanf("%f",&c);
    if (c%2==0)
    {
        printf("\nc es par\t\tITQ");
        if (c==10)
            printf("El valor de c = ",c);
    }
    else
    {
        printf("\nc es impar\t\tITQ\n");
    }
    b++; 
    c--;
    c+=(10-b); //c = 4 + (10-b); c = 4 +(10-9); c = 4 + 1; c = 5
    b-=9;
    printf("El valor de a = ",a);
    printf("El valor de b = ",b);
    printf("El valor de d = ",d);
}
