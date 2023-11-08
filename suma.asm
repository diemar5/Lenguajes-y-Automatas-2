;Diego Galván Martínez
;07/11/2023 05:59:04 p. m.
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
INC a
DEC a
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
MULL BX
MOVa, AX
MOV AX, 100
PUSH AX
POP AX
MOV BX, AX
MOV AX, a
DIV BX
MOVa, AX
INT 20H
RET
define_scan_num
define_print_num
define_print_num_uns
; V a r i a b l e s
a dw 0h 
altura dw 0h 
i dw 0h 
j dw 0h 
k dw 0h 
END
