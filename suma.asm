;Diego Galván Martínez
;08/11/2023 01:30:54 p. m.
include emu8086.inc
org 100h
MOV AX, 258
PUSH AX
POP AX
; Asignacion a
MOV a, AX
MOV AX, a
PUSH AX
POP AX
AND AX, 0xFF
PUSH AX
POP AX
; Asignacion a
MOV a, AX
MOV AX, 8
PUSH AX
POP AX
MOV BX, a
ADD AX, BX
MOV a, AX
MOV AX, 10
PUSH AX
POP AX
MOV BX, a
MUL BX
MOV a, AX
MOV AX, 100
PUSH AX
POP AX
MOV BX, AX
MOV AX, a
DIV BX
MOV a, AX
print 'Valor Casteado de a: '
MOV AX, 1
call print_num
printn ' '
print 'Digite el valor de altura: '
MOV AX, 0
call scan_num
MOV altura,CX
printn ' '
print 'for:'
printn ' '
; For: 1
MOV AX, 1
PUSH AX
POP AX
; Asignacion i
MOV i, AX
InicioFor1:
MOV AX, i
PUSH AX
MOV AX, altura
PUSH AX
POP BX
POP AX
CMP AX, BX
JA FinFor1
print '    '
; For: 2
MOV AX, 250
PUSH AX
POP AX
; Asignacion j
MOV j, AX
InicioFor2:
MOV AX, j
PUSH AX
MOV AX, 250
PUSH AX
MOV AX, i
PUSH AX
POP BX
POP AX
ADD AX, BX
PUSH AX
POP BX
POP AX
CMP AX, BX
JAE FinFor2
; IF: 1
MOV AX, j
PUSH AX
MOV AX, 2
PUSH AX
POP BX
POP AX
DIV BX
PUSH DX
MOV AX, 0
PUSH AX
POP BX
POP AX
CMP AX, BX
JNE Eif1
print '-'
JMP FinIf1
Eif1:
print '+'
FinIf1:
INC j
JMP InicioFor2
FinFor2:
printn ' '
print ''
printn ' '
INC i
JMP InicioFor1
; For: 3
InicioFor3:
MOV AX, j
PUSH AX
MOV AX, 250
PUSH AX
MOV AX, i
PUSH AX
POP BX
POP AX
ADD AX, BX
PUSH AX
POP BX
POP AX
CMP AX, BX
JAE FinFor3
; IF: 3
MOV AX, j
PUSH AX
MOV AX, 2
PUSH AX
POP BX
POP AX
DIV BX
PUSH DX
MOV AX, 0
PUSH AX
POP BX
POP AX
CMP AX, BX
JNE Eif3
print '-'
JMP FinIf3
Eif3:
print '+'
FinIf3:
INC j
JMP InicioFor3
FinFor3:
; For: 4
InicioFor4:
MOV AX, j
PUSH AX
MOV AX, 250
PUSH AX
MOV AX, i
PUSH AX
POP BX
POP AX
ADD AX, BX
PUSH AX
POP BX
POP AX
CMP AX, BX
JAE FinFor4
; IF: 6
MOV AX, j
PUSH AX
MOV AX, 2
PUSH AX
POP BX
POP AX
DIV BX
PUSH DX
MOV AX, 0
PUSH AX
POP BX
POP AX
CMP AX, BX
JNE Eif6
print '-'
JMP FinIf6
Eif6:
print '+'
FinIf6:
INC j
JMP InicioFor4
FinFor4:
; For: 5
InicioFor5:
MOV AX, j
PUSH AX
MOV AX, 250
PUSH AX
MOV AX, i
PUSH AX
POP BX
POP AX
ADD AX, BX
PUSH AX
POP BX
POP AX
CMP AX, BX
JAE FinFor5
; IF: 10
MOV AX, j
PUSH AX
MOV AX, 2
PUSH AX
POP BX
POP AX
DIV BX
PUSH DX
MOV AX, 0
PUSH AX
POP BX
POP AX
CMP AX, BX
JNE Eif10
print '-'
JMP FinIf10
Eif10:
print '+'
FinIf10:
INC j
JMP InicioFor5
FinFor5:
; For: 6
InicioFor6:
MOV AX, j
PUSH AX
MOV AX, 250
PUSH AX
MOV AX, i
PUSH AX
POP BX
POP AX
ADD AX, BX
PUSH AX
POP BX
POP AX
CMP AX, BX
JAE FinFor6
; IF: 15
MOV AX, j
PUSH AX
MOV AX, 2
PUSH AX
POP BX
POP AX
DIV BX
PUSH DX
MOV AX, 0
PUSH AX
POP BX
POP AX
CMP AX, BX
JNE Eif15
print '-'
JMP FinIf15
Eif15:
print '+'
FinIf15:
INC j
JMP InicioFor6
FinFor6:
; For: 7
InicioFor7:
MOV AX, j
PUSH AX
MOV AX, 250
PUSH AX
MOV AX, i
PUSH AX
POP BX
POP AX
ADD AX, BX
PUSH AX
POP BX
POP AX
CMP AX, BX
JAE FinFor7
; IF: 21
MOV AX, j
PUSH AX
MOV AX, 2
PUSH AX
POP BX
POP AX
DIV BX
PUSH DX
MOV AX, 0
PUSH AX
POP BX
POP AX
CMP AX, BX
JNE Eif21
print '-'
JMP FinIf21
Eif21:
print '+'
FinIf21:
INC j
JMP InicioFor7
FinFor7:
FinFor1:
printn ' '
print 'while:'
printn ' '
MOV AX, 1
PUSH AX
POP AX
; Asignacion i
MOV i, AX
InicioWhile1:
MOV AX, i
PUSH AX
MOV AX, altura
PUSH AX
POP BX
POP AX
CMP AX, BX
JA FinWhile1
print '    '
MOV AX, 250
PUSH AX
POP AX
; Asignacion j
MOV j, AX
InicioWhile2:
MOV AX, j
PUSH AX
MOV AX, 250
PUSH AX
MOV AX, i
PUSH AX
POP BX
POP AX
ADD AX, BX
PUSH AX
POP BX
POP AX
CMP AX, BX
JAE FinWhile2
; IF: 22
MOV AX, j
PUSH AX
MOV AX, 2
PUSH AX
POP BX
POP AX
DIV BX
PUSH DX
MOV AX, 0
PUSH AX
POP BX
POP AX
CMP AX, BX
JNE Eif22
print '-'
JMP FinIf22
Eif22:
print '+'
FinIf22:
INC j
JMP InicioWhile2
FinWhile2:
INC i
printn ' '
print ''
printn ' '
JMP InicioWhile1
FinWhile3:
FinWhile4:
FinWhile5:
FinWhile6:
FinWhile7:
FinWhile1:
printn ' '
print 'do:'
printn ' '
MOV AX, 1
PUSH AX
POP AX
; Asignacion i
MOV i, AX
; Do: 1
InicioDo1:
print '    '
MOV AX, 250
PUSH AX
POP AX
; Asignacion j
MOV j, AX
; Do: 2
InicioDo2:
; IF: 43
MOV AX, j
PUSH AX
MOV AX, 2
PUSH AX
POP BX
POP AX
DIV BX
PUSH DX
MOV AX, 0
PUSH AX
POP BX
POP AX
CMP AX, BX
JNE Eif43
print '-'
JMP FinIf43
Eif43:
print '+'
FinIf43:
INC j
MOV AX, j
PUSH AX
MOV AX, 250
PUSH AX
MOV AX, i
PUSH AX
POP BX
POP AX
ADD AX, BX
PUSH AX
POP BX
POP AX
CMP AX, BX
JAE FinDo2
JMP InicioDo2
FinDo2:
INC i
printn ' '
print ''
printn ' '
MOV AX, i
PUSH AX
MOV AX, altura
PUSH AX
POP BX
POP AX
CMP AX, BX
JA FinDo1
JMP InicioDo1
FinDo3:
FinDo4:
FinDo5:
FinDo6:
FinDo1:
INT 20H
RET
define_scan_num
define_print_num
define_print_num_uns
; V a r i a b l e s
altura dw 0h 
i dw 0h 
j dw 0h 
k dw 0h 
a dw 0h 
b dw 0h 
END
