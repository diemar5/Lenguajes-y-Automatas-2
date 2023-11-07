;Diego Galván Martínez
;07/11/2023 11:28:08 a. m.
include emu8086.inc
org 100h
MOV AX, 256
PUSH AX
POP AX
AND AX, 0xFF
PUSH AX
POP AX
; Asignacion k
MOV k, AX
print 'K vale'
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
END
