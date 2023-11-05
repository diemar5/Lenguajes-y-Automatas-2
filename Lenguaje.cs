using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/*
    Unidad 1
    Requerimiento 1: Mensajes del printf deben salir sin comillas
                     Incluir \n y \t como secuencias de escape ☻ ✓
    Requerimiento 2: Agregar el % al PorFactor ☻ ✓
                     Modifcar el valor de una variable con ++,--,+=,-=,*=,/=.%= ☻ ✓
    Requerimiento 3: Cada vez que se haga un match(Tipos.Identificador) verficar el
                     uso de la variable ☻ ✓
                     Icremento(), Printf(), Factor() y usar getValor y Modificar ☻
                     Levantar una excepcion en scanf() cuando se capture un string ☻ ✓
    Requerimiento 4: Implemenar la ejecución del ELSE ☻ ✓
*/
/*
    Unidad 2
    Requerimiento 1: Implemenar la ejecución del while ✓
    Requerimiento 2: Implemenar la ejecución del do-while ✓
    Requerimiento 3: Implemenar la ejecución del for ✓
    Requerimiento 4: Marcar errores semánticos ✓
    Requerimiento 5: Casteos ✓
*/
/*
    Unidad 3
    Requerimiento 1: Programar scanf ✓
    Requerimiento 2: Programar printf ✓
    Requerimiento 3: Programar ++,--,+=,-=,*=,/=,%= ✓
    Requerimiento 4: Programar else
    Requerimiento 5: Programar do para que genere una sola vez el codigo ✓
    Requerimiento 6: Programar while para que genere una sola vez el codigo ✓
    Requerimiento 7: Programar el for para que genere una sola vez el codigo ✓
    Requerimiento 8: Programar el CAST
*/

namespace Sintaxis_2
{
    public class Lenguaje : Sintaxis
    {
        List<Variable> lista;
        Stack<float> stack;
        int contIf, contFor, contWhile, contDo;
        Variable.TiposDatos tipoDatoExpresion;
        public Lenguaje()
        {
            lista = new List<Variable>();
            stack = new Stack<float>();
            tipoDatoExpresion = Variable.TiposDatos.Char;
            contIf = contFor = contWhile = contDo = 1;
        }
        public Lenguaje(string nombre) : base(nombre)
        {
            lista = new List<Variable>();
            stack = new Stack<float>();
            tipoDatoExpresion = Variable.TiposDatos.Char;
            contIf = contFor = contWhile = contDo = 1;
        }

        //Programa  -> Librerias? Variables? Main
        public void Programa()
        {
            asm.WriteLine("include emu8086.inc");
            asm.WriteLine("org 100h");
            if (getContenido() == "#")
            {
                Librerias();
            }
            if (getClasificacion() == Tipos.Datos)
            {
                Variables();
            }
            Main(true);
            asm.WriteLine("INT 20H");
            asm.WriteLine("RET");
            asm.WriteLine("define_scan_num");
            asm.WriteLine("define_print_num");
            asm.WriteLine("define_print_num_uns");
            Imprime();
            asm.WriteLine("END");
        }

        private void Imprime()
        {
            log.WriteLine("-----------------");
            log.WriteLine("V a r i a b l e s");
            log.WriteLine("-----------------");
            asm.WriteLine("; V a r i a b l e s");
            foreach (Variable v in lista)
            {
                log.WriteLine(v.getNombre() + " " + v.getTipoDato() + " = " + v.getValor());
                asm.WriteLine(v.getNombre() + " dw 0h ");
            }
            log.WriteLine("-----------------");
        }

        private bool Existe(string nombre)
        {
            foreach (Variable v in lista)
            {
                if (v.getNombre() == nombre)
                {
                    return true;
                }
            }
            return false;
        }
        private void Modifica(string nombre, float nuevoValor)
        {
            foreach (Variable v in lista)
            {
                if (v.getNombre() == nombre)
                {
                    v.setValor(nuevoValor);
                }
            }
        }
        private float getValor(string nombre)
        {
            foreach (Variable v in lista)
            {
                if (v.getNombre() == nombre)
                {
                    return v.getValor();
                }
            }
            return 0;
        }
        private Variable.TiposDatos getTipo(string nombre)
        {
            foreach (Variable v in lista)
            {
                if (v.getNombre() == nombre)
                {
                    return v.getTipoDato();
                }
            }
            return Variable.TiposDatos.Char;
        }
        private Variable.TiposDatos getTipo(float resultado)
        {
            if (resultado % 1 != 0)
            {
                return Variable.TiposDatos.Float;
            }
            else if (resultado < 256)
            {
                return Variable.TiposDatos.Char;
            }
            else if (resultado < 65536)
            {
                return Variable.TiposDatos.Int;
            }
            return Variable.TiposDatos.Float;
        }
        // Libreria -> #include<Identificador(.h)?>
        private void Libreria()
        {
            match("#");
            match("include");
            match("<");
            match(Tipos.Identificador);
            if (getContenido() == ".")
            {
                match(".");
                match("h");
            }
            match(">");
        }
        //Librerias -> Libreria Librerias?
        private void Librerias()
        {
            Libreria();
            if (getContenido() == "#")
            {
                Librerias();
            }
        }
        //Variables -> tipo_dato ListaIdentificadores; Variables?
        private void Variables()
        {
            Variable.TiposDatos tipo = Variable.TiposDatos.Char;
            switch (getContenido())
            {
                case "int": tipo = Variable.TiposDatos.Int; break;
                case "float": tipo = Variable.TiposDatos.Float; break;
            }
            match(Tipos.Datos);
            ListaIdentificadores(tipo);
            match(";");
            if (getClasificacion() == Tipos.Datos)
            {
                Variables();
            }
        }
        //ListaIdentificadores -> identificador (,ListaIdentificadores)?
        private void ListaIdentificadores(Variable.TiposDatos tipo)
        {
            if (!Existe(getContenido()))
            {
                lista.Add(new Variable(getContenido(), tipo));
            }
            else
            {
                throw new Error("de sintaxis, la variable <" + getContenido() + "> está duplicada", log, linea, columna);
            }
            match(Tipos.Identificador);
            if (getContenido() == ",")
            {
                match(",");
                ListaIdentificadores(tipo);
            }
        }
        //BloqueInstrucciones -> { ListaInstrucciones ? }
        private void BloqueInstrucciones(bool ejecuta, bool primeravez)
        {
            match("{");
            if (getContenido() != "}")
            {
                ListaInstrucciones(ejecuta, primeravez);
            }
            match("}");
        }

        //ListaInstrucciones -> Instruccion ListaInstrucciones?
        private void ListaInstrucciones(bool ejecuta, bool primeravez)
        {
            Instruccion(ejecuta, primeravez);
            if (getContenido() != "}")
            {
                ListaInstrucciones(ejecuta, primeravez);
            }
        }
        //Instruccion -> Printf | Scanf | If | While | Do | For | Asignacion
        private void Instruccion(bool ejecuta, bool primeravez)
        {
            if (getContenido() == "printf")
            {
                Printf(ejecuta, primeravez);
            }
            else if (getContenido() == "scanf")
            {
                Scanf(ejecuta, primeravez);
            }
            else if (getContenido() == "if")
            {
                If(ejecuta);
            }
            else if (getContenido() == "while")
            {
                While(ejecuta, primeravez);
            }
            else if (getContenido() == "do")
            {
                Do(ejecuta, primeravez);
            }
            else if (getContenido() == "for")
            {
                For(ejecuta, primeravez);
            }
            else
            {
                Asignacion(ejecuta, primeravez);
            }
        }
        //Asignacion -> identificador = Expresion;
        private void Asignacion(bool ejecuta, bool primeravez)
        {
            float resultado = 0;
            tipoDatoExpresion = Variable.TiposDatos.Char;
            if (!Existe(getContenido()))
            {
                throw new Error("de sintaxis, la variable <" + getContenido() + "> no está declarada", log, linea, columna);
            }
            log.Write(getContenido() + " = ");
            string variable = getContenido();
            match(Tipos.Identificador);
            if (getContenido() == "=")
            {
                match("=");
                Expresion(primeravez);
                resultado = stack.Pop();
                if (primeravez)
                {
                    asm.WriteLine("POP AX");
                    asm.WriteLine("; Asignacion " + variable);
                    asm.WriteLine("MOV " + variable + ", AX");
                }
            }
            else if (getClasificacion() == Tipos.Incremento_Termino)
            {
                if (getContenido() == "++")
                {
                    match("++");
                    if (primeravez)
                    {
                        asm.WriteLine("INC " + variable);//INC
                    }
                    resultado = getValor(variable) + 1;
                }
                else
                {
                    match("--");
                    if (primeravez)
                    {
                        asm.WriteLine("DEC " + variable);//DEC
                    }
                    resultado = getValor(variable) - 1;
                }
            }
            else if (getClasificacion() == Tipos.Incremento_Factor)
            {
                resultado = getValor(variable);
                if (getContenido() == "+=")
                {
                    match("+=");
                    Expresion(primeravez);
                    resultado += stack.Pop();
                    if (primeravez)
                    {
                        asm.WriteLine("POP AX");
                        asm.WriteLine("MOV BX," + variable);
                        asm.WriteLine("ADD AX,BX");
                    }
                }
                else if (getContenido() == "-=")
                {
                    match("-=");
                    Expresion(primeravez);
                    resultado -= stack.Pop();
                    if (primeravez)
                    {
                        asm.WriteLine("POP AX");
                        asm.WriteLine("MOV BX," + variable);
                        asm.WriteLine("SUB BX,AX");
                    }
                }

                else if (getContenido() == "*=")
                {
                    match("*=");
                    Expresion(primeravez);
                    resultado *= stack.Pop();
                    if (primeravez)
                    {
                        asm.WriteLine("POP AX");
                        asm.WriteLine("MOV BX," + variable);
                        asm.WriteLine("MULL BX");
                    }
                }
                else if (getContenido() == "/=")
                {
                    match("/=");
                    Expresion(primeravez);
                    resultado /= stack.Pop();
                    if (primeravez)
                    {
                        asm.WriteLine("POP AX");
                        asm.WriteLine("MOV BX," + variable);
                        asm.WriteLine("DIV BX");
                    }
                }
                else if (getContenido() == "%=")
                {
                    match("%=");
                    Expresion(primeravez);
                    resultado %= stack.Pop();
                    if (primeravez)
                    {
                        asm.WriteLine("POP BX");
                        asm.WriteLine("MOV AX," + variable);
                        asm.WriteLine("DIV BX");
                        asm.WriteLine("MUL BX");
                    }
                }
            }
            log.WriteLine(" = " + resultado);
            if (ejecuta)
            {

                Variable.TiposDatos tipoDatoVariable = getTipo(variable);
                Variable.TiposDatos tipoDatoResultado = getTipo(resultado);

                //Console.WriteLine(variable + " = " + tipoDatoVariable);
                //Console.WriteLine(resultado + " = " + tipoDatoResultado);
                //Console.WriteLine("expresion = " + tipoDatoExpresion);

                if (tipoDatoExpresion > tipoDatoResultado)
                {
                    tipoDatoResultado = tipoDatoExpresion;
                }
                if (tipoDatoVariable >= tipoDatoResultado)
                {
                    Modifica(variable, resultado);
                }
                else
                {
                    throw new Error("de semántica, no se puede asignar un <" + tipoDatoResultado + "> a un <" + tipoDatoVariable + ">", log, linea, columna);
                }
            }
            match(";");
        }
        //While -> while(Condicion) BloqueInstrucciones | Instruccion
        private void While(bool ejecuta, bool primeravez)
        {
            int inicia = caracter;
            int lineaInicio = linea;
            string etiquetaInicio = "InicioWhile" + contWhile;
            string etiquetaFin = "FinWhile" + contWhile++;
            log.WriteLine("while: ");
            if (primeravez)
            {
                asm.WriteLine(etiquetaInicio + ":");
            }
            do
            {
                match("while");
                match("(");
                ejecuta = Condicion(etiquetaFin, primeravez) && ejecuta;
                match(")");
                if (getContenido() == "{")
                {
                    BloqueInstrucciones(ejecuta, primeravez);
                }
                else
                {
                    Instruccion(ejecuta, primeravez);
                }
                if (ejecuta)
                {
                    archivo.DiscardBufferedData();
                    caracter = inicia - 6;
                    archivo.BaseStream.Seek(caracter, SeekOrigin.Begin);
                    nextToken();
                    linea = lineaInicio;
                }
                if (primeravez)
                {
                    asm.WriteLine("JMP " + etiquetaInicio);
                }
                primeravez = false;
            }
            while (ejecuta);
            asm.WriteLine(etiquetaFin + ":");
        }
        //Do -> do BloqueInstrucciones | Instruccion while(Condicion)
        private void Do(bool ejecuta, bool primeravez)
        {
            asm.WriteLine("; Do: " + contDo);

            string etiquetaInicio = "InicioDo" + contDo;
            string etiquetaFin = "FinDo" + contDo++;

            int inicia = caracter;
            int lineaInicio = linea;

            log.WriteLine("do: ");
            if (primeravez)
            {
                asm.WriteLine(etiquetaInicio + ":");
            }
            do
            {
                match("do");
                if (getContenido() == "{")
                {
                    BloqueInstrucciones(ejecuta, primeravez);//Cambiar el true, porque no se ha programado
                }
                else
                {
                    Instruccion(ejecuta, primeravez);
                }
                match("while");
                match("(");
                ejecuta = Condicion(etiquetaFin, primeravez) && ejecuta;//Cambiar el true, porque no se ha programado
                if (ejecuta)
                {
                    archivo.DiscardBufferedData();
                    caracter = inicia - 3;
                    archivo.BaseStream.Seek(caracter, SeekOrigin.Begin);
                    nextToken();
                    linea = lineaInicio;
                }
                if (primeravez)
                {
                    asm.WriteLine("JMP " + etiquetaInicio);
                }
                primeravez = false;
            } while (ejecuta);
            asm.WriteLine(etiquetaFin + ":");
            match(")");
            match(";");
        }
        //For -> for(Asignacion Condicion; Incremento) BloqueInstrucciones | Instruccion
        private void For(bool ejecuta, bool primeravez)
        {
            asm.WriteLine("; For: " + contFor);
            match("for");
            match("(");
            Asignacion(ejecuta, primeravez);

            string etiquetaInicio = "InicioFor" + contFor;
            string etiquetaFin = "FinFor" + contFor++;

            int inicia = caracter;
            int lineaInicio = linea;
            float resultado = 0;
            string variable = getContenido();
            primeravez = true;

            log.WriteLine("for: " + variable);
            if (primeravez)
            {
                asm.WriteLine(etiquetaInicio + ":");
            }
            do
            {
                ejecuta = Condicion(etiquetaFin, primeravez) && ejecuta;
                match(";");
                resultado = Incremento(ejecuta);
                match(")");
                if (getContenido() == "{")
                {
                    BloqueInstrucciones(ejecuta, primeravez);
                }
                else
                {
                    Instruccion(ejecuta, primeravez);
                }
                if (getValor(variable) < resultado)
                {
                    if (primeravez)
                    {
                        asm.WriteLine("INC " + variable);
                    }
                }
                else if (getValor(variable) > resultado)
                {
                    if (primeravez)
                    {
                        asm.WriteLine("DEC " + variable);
                    }
                }
                if (ejecuta)
                {
                    Variable.TiposDatos tipoDatoVariable = getTipo(variable);
                    Variable.TiposDatos tipoDatoResultado = getTipo(resultado);
                    if (tipoDatoVariable >= tipoDatoResultado)
                    {
                        Modifica(variable, resultado);
                    }
                    else
                    {
                        throw new Error("de semántica, no se puede asignar un <" + tipoDatoResultado + "> a un <" + tipoDatoVariable + ">", log, linea, columna);
                    }
                    archivo.DiscardBufferedData();
                    caracter = inicia - variable.Length - 1;
                    archivo.BaseStream.Seek(caracter, SeekOrigin.Begin);
                    nextToken();
                    linea = lineaInicio;
                }
                if (primeravez)
                {
                    asm.WriteLine("JMP " + etiquetaInicio);
                }
                primeravez = false;
            }
            while (ejecuta);
            asm.WriteLine(etiquetaFin + ":");
        }
        //Incremento -> Identificador ++ | --
        private float Incremento(bool ejecuta)
        {
            if (!Existe(getContenido()))
            {
                throw new Error("de sintaxis, la variable <" + getContenido() + "> no está declarada", log, linea, columna);
            }
            string variable = getContenido();
            match(Tipos.Identificador);
            if (getContenido() == "++")
            {
                match("++");
                return getValor(variable) + 1;
            }
            else
            {
                match("--");
                return getValor(variable) - 1;
            }

        }
        //Condicion -> Expresion OperadorRelacional Expresion
        private bool Condicion(string etiqueta, bool primeravez)
        {
            Expresion(primeravez);
            string operador = getContenido();
            match(Tipos.Operador_Relacional);
            Expresion(primeravez);
            float R1 = stack.Pop(); //Expresion 2
            float R2 = stack.Pop(); //Expresion 1
            if (primeravez)
            {
                asm.WriteLine("POP BX"); //Expresion 2
                asm.WriteLine("POP AX"); //Expresion 1
                asm.WriteLine("CMP AX, BX");
            }
            switch (operador)
            {
                case "==":
                    if (primeravez) asm.WriteLine("JNE " + etiqueta);
                    return R2 == R1;
                case ">":
                    if (primeravez) asm.WriteLine("JBE " + etiqueta);
                    return R2 > R1;
                case ">=":
                    if (primeravez) asm.WriteLine("JB " + etiqueta);
                    return R2 >= R1;
                case "<":
                    if (primeravez) asm.WriteLine("JAE " + etiqueta);
                    return R2 < R1;
                case "<=":
                    if (primeravez) asm.WriteLine("JA " + etiqueta);
                    return R2 <= R1;
                default:
                    if (primeravez) asm.WriteLine("JE " + etiqueta);
                    return R2 != R1;
            }

        }
        //If -> if (Condicion) BloqueInstrucciones | Instruccion (else BloqueInstrucciones | Instruccion)?
        private void If(bool ejecuta)
        {
            match("if");
            match("(");
            asm.WriteLine("; IF: " + contIf);
            string etiqueta = "Eif" + contIf++;
            bool evaluacion = Condicion(etiqueta, true);
            match(")");
            if (getContenido() == "{")
            {
                BloqueInstrucciones(evaluacion && ejecuta, true);
            }
            else
            {
                Instruccion(evaluacion && ejecuta, true);
            }
            asm.WriteLine(etiqueta + ":");
            if (getContenido() == "else")
            {
                match("else");
                if (getContenido() == "{")
                {
                    BloqueInstrucciones(!evaluacion && ejecuta, true);
                }
                else
                {
                    Instruccion(!evaluacion && ejecuta, true);
                }
            }
        }
        //Printf -> printf(cadena(,Identificador)?);
        private void Printf(bool ejecuta, bool primeravez)
        {
            match("printf");
            match("(");
            if (ejecuta)
            {
                string Rebeca = getContenido();
                string Lucy;
                string David = "\"\\n";
                string Z2 = "\\n\"";
                if (primeravez)
                {
                    if (Rebeca.StartsWith(David))
                    {
                        asm.WriteLine("printn ' '");
                    }
                    Lucy = Rebeca.Replace("\"", "").Replace("\\n", "\n").Replace("\n", "").Replace("\\t", "");
                    asm.WriteLine("print '" + Lucy + "'");
                    if (Rebeca.EndsWith(Z2))
                    {
                        asm.WriteLine("printn ' '");
                    }
                }
                Lucy = Rebeca.Replace("\"", "").Replace("\\n", "\n").Replace("\\t", "\t");
                Console.Write(Lucy);
            }
            match(Tipos.Cadena);
            if (getContenido() == ",")
            {
                match(",");
                if (!Existe(getContenido()))
                {
                    throw new Error("de sintaxis, la variable <" + getContenido() + "> no está declarada", log, linea, columna);
                }
                if (ejecuta)
                {
                    Console.Write(getValor(getContenido()));
                }
                match(Tipos.Identificador);
            }
            match(")");
            match(";");
        }
        //Scanf -> scanf(cadena,&Identificador);
        private void Scanf(bool ejecuta, bool primeravez)
        {
            match("scanf");
            match("(");
            match(Tipos.Cadena);
            match(",");
            match("&");
            if (!Existe(getContenido()))
            {
                throw new Error("de sintaxis, la variable <" + getContenido() + "> no está declarada", log, linea, columna);
            }
            string variable = getContenido();
            match(Tipos.Identificador);
            if (ejecuta)
            {
                string captura = "" + Console.ReadLine();
                bool CyberEsqueleto = float.TryParse(captura, out float resultado);
                Variable.TiposDatos tipoDatoVariable = getTipo(variable);
                Variable.TiposDatos tipoDatoResultado = getTipo(resultado);

                if (tipoDatoVariable >= tipoDatoResultado)
                {
                    Modifica(variable, resultado);
                }
                else
                {
                    throw new Error("de semántica, no se puede asignar un <" + tipoDatoResultado + "> a un <" + tipoDatoVariable + ">", log, linea, columna);
                }
                if (primeravez)
                {
                    asm.WriteLine("mov ax, 0");
                    asm.WriteLine("call scan_num");
                    asm.WriteLine("mov " + variable + ",CX");
                }
                if (CyberEsqueleto == true)
                {
                    stack.Push(float.Parse(captura));
                    Modifica(variable, resultado);
                }
                else
                {
                    throw new Error("de sintaxis, no puedes introducir datos de tipo <String>", log, linea, columna);
                }
                Modifica(variable, resultado);
            }
            match(")");
            match(";");
        }
        //Main -> void main() BloqueInstrucciones
        private void Main(bool ejecuta)
        {
            match("void");
            match("main");
            match("(");
            match(")");
            BloqueInstrucciones(ejecuta, true);
        }
        //Expresion -> Termino MasTermino
        private void Expresion(bool primeravez)
        {
            Termino(primeravez);
            MasTermino(primeravez);
        }
        //MasTermino -> (OperadorTermino Termino)?
        private void MasTermino(bool primeravez)
        {
            if (getClasificacion() == Tipos.Operador_Termino)
            {
                string operador = getContenido();
                match(Tipos.Operador_Termino);
                Termino(primeravez);
                log.Write(" " + operador);
                float R2 = stack.Pop();
                float R1 = stack.Pop();
                if (primeravez)
                {
                    asm.WriteLine("POP BX");
                    asm.WriteLine("POP AX");
                }
                if (operador == "+")
                {
                    stack.Push(R1 + R2);
                    if (primeravez)
                    {
                        asm.WriteLine("ADD AX, BX");
                        asm.WriteLine("PUSH AX");
                    }
                }
                else
                {
                    stack.Push(R1 - R2);
                    if (primeravez)
                    {
                        asm.WriteLine("SUB AX, BX");
                        asm.WriteLine("PUSH AX");
                    }
                }
            }
        }
        //Termino -> Factor PorFactor
        private void Termino(bool primeravez)
        {
            Factor(primeravez);
            PorFactor(primeravez);
        }
        //PorFactor -> (OperadorFactor Factor)?
        private void PorFactor(bool primeravez)
        {
            if (getClasificacion() == Tipos.Operador_Factor)
            {
                string operador = getContenido();
                match(Tipos.Operador_Factor);
                Factor(primeravez);
                log.Write(" " + operador);
                float R2 = stack.Pop();
                float R1 = stack.Pop();
                if (primeravez)
                {
                    asm.WriteLine("POP BX");
                    asm.WriteLine("POP AX");
                }
                if (operador == "*")
                {
                    stack.Push(R1 * R2);
                    if (primeravez)
                    {
                        asm.WriteLine("MUL BX");
                        asm.WriteLine("PUSH AX");
                    }
                }
                else if (operador == "/")
                {
                    stack.Push(R1 / R2);
                    if (primeravez)
                    {
                        asm.WriteLine("DIV BX");
                        asm.WriteLine("PUSH AX");
                    }
                }
                else
                    stack.Push(R1 % R2);
                if (primeravez)
                {
                    asm.WriteLine("DIV BX");
                    asm.WriteLine("PUSH DX");
                }
            }
        }
        //Factor -> numero | identificador | (Expresion)
        private void Factor(bool primeravez)
        {
            if (getClasificacion() == Tipos.Numero)
            {
                log.Write(" " + getContenido());
                if (primeravez)
                {
                    asm.WriteLine("MOV AX, " + getContenido());
                    asm.WriteLine("PUSH AX");
                }
                stack.Push(float.Parse(getContenido()));
                if (tipoDatoExpresion < getTipo(float.Parse(getContenido())))
                {
                    tipoDatoExpresion = getTipo(float.Parse(getContenido()));
                }
                match(Tipos.Numero);
            }
            else if (getClasificacion() == Tipos.Identificador)
            {
                if (!Existe(getContenido()))
                {
                    throw new Error("de sintaxis, la variable <" + getContenido() + "> no está declarada", log, linea, columna);
                }
                if (primeravez)
                {
                    asm.WriteLine("MOV AX, " + getContenido());
                    asm.WriteLine("PUSH AX");
                }
                stack.Push(getValor(getContenido()));
                match(Tipos.Identificador);
                if (tipoDatoExpresion < getTipo(getContenido()))
                {
                    tipoDatoExpresion = getTipo(getContenido());
                }
            }
            else
            {
                bool huboCast = false;
                Variable.TiposDatos tipoDatoCast = Variable.TiposDatos.Char;
                match("(");
                if (getClasificacion() == Tipos.Datos)
                {
                    huboCast = true;
                    switch (getContenido())
                    {
                        case "int":
                            tipoDatoCast = Variable.TiposDatos.Int;
                            break;
                        case "float":
                            tipoDatoCast = Variable.TiposDatos.Float;
                            break;
                    }
                    match(Tipos.Datos);
                    match(")");
                    match("(");
                }
                Expresion(primeravez);
                match(")");
                if (huboCast)
                {
                    tipoDatoExpresion = tipoDatoCast;
                    float resultado = stack.Pop();
                    if (primeravez)
                    {
                        asm.WriteLine("POP AX");
                    }
                    resultado = Castea(resultado, tipoDatoCast);
                    stack.Push(resultado);
                    Console.WriteLine(resultado);
                }
            }
        }
        //Se puede meter el casteo en Expresion() (dentro del método)
        float Castea(float resultado, Variable.TiposDatos datos)
        {
            resultado = (float)Math.Round(resultado);
            if (datos == Variable.TiposDatos.Char)
            {
                resultado %= 256;
            }
            else if (datos == Variable.TiposDatos.Int)
            {
                resultado %= 65536;
            }
            return resultado;
        }
    }
}