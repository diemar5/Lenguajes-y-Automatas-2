;Diego Galván Martínez
;04/11/2023 08:22:15 p. m.
include emu8086.inc
org 100h
printn ' '
print 'Hola, esto es'
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
