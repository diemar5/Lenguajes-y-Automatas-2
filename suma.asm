;Diego Galván Martínez
;07/11/2023 11:46:45 p. m.
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
MOV AX, 0
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
print '	'
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
; IF: 2
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
JNE Eif2
print '-'
JMP FinIf2
Eif2:
print '+'
FinIf2:
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
; IF: 4
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
JNE Eif4
print '-'
JMP FinIf4
Eif4:
print '+'
FinIf4:
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
; IF: 7
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
JNE Eif7
print '-'
JMP FinIf7
Eif7:
print '+'
FinIf7:
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
; IF: 11
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
JNE Eif11
print '-'
JMP FinIf11
Eif11:
print '+'
FinIf11:
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
; IF: 16
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
JNE Eif16
print '-'
JMP FinIf16
Eif16:
print '+'
FinIf16:
INC j
JMP InicioFor7
FinFor7:
; For: 8
InicioFor8:
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
JAE FinFor8
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
JMP InicioFor8
FinFor8:
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
print '	'
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
; IF: 23
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
JNE Eif23
print '-'
JMP FinIf23
Eif23:
print '+'
FinIf23:
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
print '	'
MOV AX, 250
PUSH AX
POP AX
; Asignacion j
MOV j, AX
; Do: 2
InicioDo2:
; IF: 44
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
JNE Eif44
print '-'
JMP FinIf44
Eif44:
print '+'
FinIf44:
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
