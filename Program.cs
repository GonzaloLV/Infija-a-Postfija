using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infija_a_Postfija
{
        class Nodo
        {
            public char dato;
            public Nodo sgte;
        }
        class Pila
        {
            private Nodo inicio;
            public Pila()
            {
                inicio = null;
            }
            public Nodo getInicio()
            {
                return inicio;
            }
            public void push(char valor)
            {
                Nodo nuevoNodo;
                nuevoNodo = new Nodo();
                nuevoNodo.dato = valor;
                nuevoNodo.sgte = inicio;
                inicio = nuevoNodo;
            }
            public char pop()
            {
                Nodo aux = inicio;
                char valor = aux.dato;
                inicio = inicio.sgte;
                aux = null;
                return valor;
            }
            public char top()
            {
                char valor;
                valor = inicio.dato;
                return valor;
            }
        }
        internal class Program
        {
            static int prioridad(char operador)
            {
                if (operador == '^')
                {
                    return 3;
                }
                else if (operador == '*' || operador == '/')
                {
                    return 2;
                }
                else if (operador == '+' || operador == '-')
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            static bool esOperador(char caracter)
            {
                return (caracter == '^' || caracter == '*' || caracter == '/'
               || caracter == '+' || caracter == '-');
            }
            static string infijoAPostfijo(string infijo)
            {
                Pila pilaOperadores = new Pila();
                char[] postfijo = new char[100];
                char caracter;
                int posPostfijo = 0;
                for (int i = 0; i <= infijo.Length - 1; i++)
                {
                    if (infijo[i] == '(')
                    {
                        pilaOperadores.push(infijo[i]);
                    }
                    else if (infijo[i] == ')')
                    {
                        if (pilaOperadores.getInicio() != null)
                        {
                            do
                            {
                                caracter = pilaOperadores.pop();
                                if (caracter != '(')
                                {
                                    postfijo[posPostfijo++] = caracter;
                                }
                            } while (pilaOperadores.getInicio() != null &&
                           caracter != '(');
                        }
                    }
                    else if (esOperador(infijo[i]))
                    {
                        if (pilaOperadores.getInicio() == null)
                        {
                            pilaOperadores.push(infijo[i]);
                        }
                        else if (prioridad(infijo[i]) >
                        prioridad(pilaOperadores.top()))
                        {
                            pilaOperadores.push(infijo[i]);
                        }
                        else
                        {
                            do
                            {
                                caracter = pilaOperadores.pop();
                                postfijo[posPostfijo++] = caracter;
                            } while (pilaOperadores.getInicio() != null &&
                           prioridad(infijo[i]) <= prioridad(pilaOperadores.top()));
                            pilaOperadores.push(infijo[i]);
                        }
                    }
                    else
                    {
                        postfijo[posPostfijo++] = infijo[i];
                    }
                }
                while (pilaOperadores.getInicio() != null)
                {
                    caracter = pilaOperadores.pop();
                    postfijo[posPostfijo++] = caracter;
                }
                return new String(postfijo);
            }
            static void Main(string[] args)
            {
                string postfijo;
                string infijo;
                Console.WriteLine("Ingresa expresión en notación infija:");
                infijo = Console.ReadLine();
                postfijo = infijoAPostfijo(infijo);
                for(int i =0; i < infijo.Length; i++)
            {
                char c = infijo[i];
                Console.WriteLine("Cararter: "+c);
            }
                Console.WriteLine("Expresión en notación postfija:");
                Console.WriteLine(postfijo);
            }
        }
    }