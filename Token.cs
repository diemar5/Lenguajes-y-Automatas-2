using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sintaxis_2
{
    public class Token
    {
        public enum Tipos
        {
            Identificador, Numero, Asignacion, Inicializacion,
            Fin_de_Sentencia, Caracter, Operador_Logico, Operador_Relacional,
            Operador_Termino, Ternario, Operador_Factor, Incremento_Termino,
            Incremento_Factor, Cadena, Inicio, Fin, Datos, Zona, Ciclos, Condicion
        };
        private string Contenido;
        private Tipos Clasificacion;

        public Token()
        {
            Contenido = "";
            Clasificacion = Tipos.Identificador;
        }
        public void setContenido(string Contenido)
        {
            this.Contenido = Contenido;
        }
        public void setClasificacion(Tipos Clasificacion)
        {
            this.Clasificacion = Clasificacion;
        }
        public string getContenido()
        {
            return this.Contenido;
        }
        public Tipos getClasificacion()
        {
            return this.Clasificacion;
        }
    }
}