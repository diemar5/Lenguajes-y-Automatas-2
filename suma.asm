;Diego Galván Martínez
;07/11/2023 12:58:11 p. m.
include emu8086.inc
org 100h
printn ' '
print 'Altura: '
MOV AX, 0
call scan_num
MOV altura,CX
printn ' '
print 'Altura vale: '
MOV AX, 5
call print_num
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
