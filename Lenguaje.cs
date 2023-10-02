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
    Requerimiento 5: Casteos
*/

namespace Sintaxis_2
{
    public class Lenguaje : Sintaxis
    {
        List<Variable> lista;
        Stack<float> stack;
        Variable.TiposDatos tipoDatoExpresion;
        public Lenguaje()
        {
            lista = new List<Variable>();
            stack = new Stack<float>();
            tipoDatoExpresion = Variable.TiposDatos.Char;
        }
        public Lenguaje(string nombre) : base(nombre)
        {
            lista = new List<Variable>();
            stack = new Stack<float>();
            tipoDatoExpresion = Variable.TiposDatos.Char;
        }

        //Programa  -> Librerias? Variables? Main
        public void Programa()
        {
            if (getContenido() == "#")
            {
                Librerias();
            }
            if (getClasificacion() == Tipos.Datos)
            {
                Variables();
            }
            Main(true);
            Imprime();
        }

        private void Imprime()
        {
            log.WriteLine("-----------------");
            log.WriteLine("V a r i a b l e s");
            log.WriteLine("-----------------");
            foreach (Variable v in lista)
            {
                log.WriteLine(v.getNombre() + " " + v.getTipoDato() + " = " + v.getValor());
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
        private void BloqueInstrucciones(bool ejecuta)
        {
            match("{");
            if (getContenido() != "}")
            {
                ListaInstrucciones(ejecuta);
            }
            match("}");
        }

        //ListaInstrucciones -> Instruccion ListaInstrucciones?
        private void ListaInstrucciones(bool ejecuta)
        {
            Instruccion(ejecuta);
            if (getContenido() != "}")
            {
                ListaInstrucciones(ejecuta);
            }
        }
        //Instruccion -> Printf | Scanf | If | While | Do | For | Asignacion
        private void Instruccion(bool ejecuta)
        {
            if (getContenido() == "printf")
            {
                Printf(ejecuta);
            }
            else if (getContenido() == "scanf")
            {
                Scanf(ejecuta);
            }
            else if (getContenido() == "if")
            {
                If(ejecuta);
            }
            else if (getContenido() == "while")
            {
                While(ejecuta);
            }
            else if (getContenido() == "do")
            {
                Do(ejecuta);
            }
            else if (getContenido() == "for")
            {
                For(ejecuta);
            }
            else
            {
                Asignacion(ejecuta);
            }
        }
        //Asignacion -> identificador = Expresion;
        private void Asignacion(bool ejecuta)
        {
            float Sandevistan = 0;
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
                Expresion();
                Sandevistan = stack.Pop();
            }
            else if (getClasificacion() == Tipos.Incremento_Termino)
            {
                if (getContenido() == "++")
                {
                    match("++");
                    Sandevistan = getValor(variable) + 1;
                }
                else
                {
                    match("--");
                    Sandevistan = getValor(variable) - 1;
                }
            }
            else if (getClasificacion() == Tipos.Incremento_Factor)
            {
                Sandevistan = getValor(variable);
                if (getContenido() == "+=")
                {
                    match("+=");
                    Expresion();
                    Sandevistan += stack.Pop();
                }
                else if (getContenido() == "-=")
                {
                    match("-=");
                    Expresion();
                    Sandevistan -= stack.Pop();
                }

                else if (getContenido() == "*=")
                {
                    match("*=");
                    Expresion();
                    Sandevistan *= stack.Pop();
                }
                else if (getContenido() == "/=")
                {
                    match("/=");
                    Expresion();
                    Sandevistan /= stack.Pop();
                }
                else if (getContenido() == "%=")
                {
                    match("%=");
                    Expresion();
                    Sandevistan %= stack.Pop();
                }
            }
            log.WriteLine(" = " + Sandevistan);
            //Saber el tipo de dato de la variable
            //Saber si el resultado se puede asignar a la variable según su tipo

            if (ejecuta)
            {
                Variable.TiposDatos tipoDatoVariable = getTipo(variable);
                Variable.TiposDatos tipoDatoResultado = getTipo(Sandevistan);

                //Console.WriteLine(variable + " = " + tipoDatoVariable);
                //Console.WriteLine(Sandevistan + " = " + tipoDatoResultado);
                //Console.WriteLine("expresion = " + tipoDatoExpresion);

                //COMPARA EL RESULTATO Y LA EXPRESION
                //Expresion();
                //float resultado = stack.Pop();
                //Variable.TiposDatos tipoDatoMayor = getTipo(resultado);
                //Console.WriteLine(resultado);
                //Console.WriteLine(tipoDatoMayor);
                if (tipoDatoVariable >= tipoDatoResultado)
                {
                    Modifica(variable, Sandevistan);
                }
                else
                {
                    throw new Error("de semántica, no se puede asignar un <" + tipoDatoResultado + "> a un <" + tipoDatoVariable + ">", log, linea, columna);
                }
            }
            match(";");
        }
        //While -> while(Condicion) BloqueInstrucciones | Instruccion
        private void While(bool ejecuta)
        {
            match("while");
            match("(");
            int inicia = caracter;
            int lineaInicio = linea;
            string variable = getContenido();
            do
            {
                ejecuta = Condicion() && ejecuta;
                match(")");
                if (getContenido() == "{")
                {
                    BloqueInstrucciones(ejecuta);
                }
                else
                {
                    Instruccion(ejecuta);
                }
                if (ejecuta)
                {
                    archivo.DiscardBufferedData();
                    //CREAR UN MÉTODO QUE DETERMINE CUÁL CICLO ESTÁ ANTES O DESPUÉS
                    caracter = inicia - variable.Length;
                    //NOTA: SI EL CICLO WHILE ESTÁ ANTES DEL FOR, FUNCIONA SÓLO SI SE LE RESTA 1,
                    //SI EL FOR ESTÁ ANTES DEL WHILE, FUNCIONA SÓLO RESTÁNDOLE variable.Length
                    archivo.BaseStream.Seek(caracter, SeekOrigin.Begin);
                    nextToken();
                    linea = lineaInicio;
                }
            }
            while (ejecuta);
        }
        //Do -> do BloqueInstrucciones | Instruccion while(Condicion)
        private void Do(bool ejecuta)
        {
            int inicia = caracter;
            int lineaInicio = linea;
            match("do");
            do
            {
                if (getContenido() == "{")
                {
                    BloqueInstrucciones(ejecuta);
                }
                else
                {
                    Instruccion(ejecuta);
                }
                match("while");
                match("(");
                ejecuta = Condicion() && ejecuta;
                match(")");
                match(";");
                if (ejecuta)
                {
                    archivo.DiscardBufferedData();
                    caracter = inicia;
                    archivo.BaseStream.Seek(caracter, SeekOrigin.Begin);
                    nextToken();
                    linea = lineaInicio;
                }
            } while (ejecuta);
        }
        //For -> for(Asignacion Condicion; Incremento) BloqueInstrucciones | Instruccion
        private void For(bool ejecuta)
        {
            match("for");
            match("(");
            Asignacion(ejecuta);

            int inicia = caracter;
            int lineaInicio = linea;
            float res;
            string variable = getContenido();

            do
            {
                ejecuta = Condicion() && ejecuta;
                match(";");
                res = Incremento(ejecuta);
                match(")");
                if (getContenido() == "{")
                {
                    BloqueInstrucciones(ejecuta);
                }
                else
                {
                    Instruccion(ejecuta);
                }
                if (ejecuta)
                {
                    Modifica(variable, res);
                    archivo.DiscardBufferedData();
                    caracter = inicia - variable.Length - 1;
                    archivo.BaseStream.Seek(caracter, SeekOrigin.Begin);
                    nextToken();
                    linea = lineaInicio;
                }
            }
            while (ejecuta);
        }
        //Incremento -> Identificador ++ | --
        private float Incremento(bool ejecuta)
        {
            float resultado;
            if (!Existe(getContenido()))
            {
                throw new Error("de sintaxis, la variable <" + getContenido() + "> no está declarada", log, linea, columna);
            }
            stack.Push(getValor(getContenido()));
            match(Tipos.Identificador);
            if (getContenido() == "++")
            {
                match("++");
                resultado = stack.Pop() + 1;
            }
            else
            {
                match("--");
                resultado = stack.Pop() - 1;
            }
            return resultado;
        }
        //Condicion -> Expresion OperadorRelacional Expresion
        private bool Condicion()
        {
            Expresion();
            string operador = getContenido();
            match(Tipos.Operador_Relacional);
            Expresion();
            float R1 = stack.Pop();
            float R2 = stack.Pop();
            switch (operador)
            {
                case "==": return R2 == R1;
                case "<": return R2 < R1;
                case ">": return R2 > R1;
                case ">=": return R2 >= R1;
                case "<=": return R2 <= R1;
                default: return R2 != R1;
            }
        }
        //If -> if (Condicion) BloqueInstrucciones | Instruccion (else BloqueInstrucciones | Instruccion)?
        private void If(bool ejecuta)
        {
            match("if");
            match("(");
            bool evaluacion = Condicion();
            match(")");
            if (getContenido() == "{")
            {
                BloqueInstrucciones(evaluacion && ejecuta);
            }
            else
            {
                Instruccion(evaluacion && ejecuta);
            }
            if (getContenido() == "else")
            {
                match("else");
                if (getContenido() == "{")
                {
                    BloqueInstrucciones(!evaluacion && ejecuta);
                }
                else
                {
                    Instruccion(!evaluacion && ejecuta);
                }
            }
        }
        //Printf -> printf(cadena(,Identificador)?);
        private void Printf(bool ejecuta)
        {
            match("printf");
            match("(");
            if (ejecuta)
            {
                string Rebeca = getContenido();
                string Lucy = Rebeca.Replace("\"", "");
                string David = Lucy.Replace("\\n", "\n");
                Console.Write(David.Replace("\\t", "\t"));
            }
            match(Tipos.Cadena);
            if (getContenido() == ",")
            {
                match(",");
                if (!Existe(getContenido()))
                {
                    throw new Error("de sintaxis, la variable <" + getContenido() + "> no está declarada", log, linea, columna);
                }
                Console.Write(getValor(getContenido()));
                match(Tipos.Identificador);
            }
            match(")");
            match(";");
        }
        //Scanf -> scanf(cadena,&Identificador);
        private void Scanf(bool ejecuta)
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
                if (CyberEsqueleto == true)
                {
                    stack.Push(float.Parse(captura));
                    Modifica(variable, resultado);
                }
                else
                {
                    throw new Error("de sintaxis, no puedes introducir datos de tipo string", log, linea, columna);
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
            BloqueInstrucciones(ejecuta);
        }
        //Expresion -> Termino MasTermino
        private void Expresion()
        {
            Termino();
            MasTermino();
        }
        //MasTermino -> (OperadorTermino Termino)?
        private void MasTermino()
        {
            if (getClasificacion() == Tipos.Operador_Termino)
            {
                string operador = getContenido();
                match(Tipos.Operador_Termino);
                Termino();
                log.Write(" " + operador);
                float R2 = stack.Pop();
                float R1 = stack.Pop();
                if (operador == "+")
                    stack.Push(R1 + R2);
                else
                    stack.Push(R1 - R2);
            }
        }
        //Termino -> Factor PorFactor
        private void Termino()
        {
            Factor();
            PorFactor();
        }
        //PorFactor -> (OperadorFactor Factor)?
        private void PorFactor()
        {
            if (getClasificacion() == Tipos.Operador_Factor)
            {
                string operador = getContenido();
                match(Tipos.Operador_Factor);
                Factor();
                log.Write(" " + operador);
                float R2 = stack.Pop();
                float R1 = stack.Pop();
                if (operador == "*")
                    stack.Push(R1 * R2);
                else if (operador == "/")
                    stack.Push(R1 / R2);
                else
                    stack.Push(R1 % R2);
            }
        }
        //Factor -> numero | identificador | (Expresion)
        private void Factor()
        {
            if (getClasificacion() == Tipos.Numero)
            {
                log.Write(" " + getContenido());
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
                Expresion();
                //Se puede meter el casteo en Expresion() (dentro del método)
                match(")");
                if (huboCast)
                {
                    tipoDatoExpresion = tipoDatoCast;
                    //float resultado = stack.Pop();
                    stack.Push(Castea(stack.Pop(), tipoDatoCast));
                }
            }
        }
        float Castea(float resultado, Variable.TiposDatos datos)
        {
            return 0;
        }
    }
}