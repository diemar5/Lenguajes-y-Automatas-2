;Diego Galván Martínez
;05/11/2023 02:38:28 p. m.
include emu8086.inc
org 100h
MOV AX, 10
PUSH AX
POP AX
POP AX
; Asignacion k
MOV k, AX
