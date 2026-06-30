using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RedSocialGB.Modelos;

namespace RedSocialGB.Estructuras
{
    public class NodoB
    {
        public List<cUsuario> Claves { get; set; } = new List<cUsuario>();
        public List<NodoB> Hijos { get; set; } = new List<NodoB>();
        public bool EsHoja { get; set; }

        public NodoB(bool esHoja)
        {
            EsHoja = esHoja;
        }
    }

    public class cArbolB
    {
        private NodoB raiz;
        private int t; // grado mínimo
        public NodoB Raiz { get => raiz; set => raiz = value; }
        public int GradoMinimo { get => t; set => t = value; }
        public cArbolB(int gradoMinimo)
        {
            raiz = new NodoB(true);
            t = gradoMinimo;
        }
        public bool EstaVacia()
        {
            return raiz.Claves.Count == 0;
        }

        // Inserción
        public void Insertar(cUsuario clave)
        {
            if (raiz.Claves.Count == (2 * t - 1))
            {
                NodoB nuevaRaiz = new NodoB(false);
                nuevaRaiz.Hijos.Add(raiz);
                DividirHijo(nuevaRaiz, 0, raiz);
                raiz = nuevaRaiz;
            }
            InsertarNoLleno(raiz, clave);
        }

        private void DividirHijo(NodoB padre, int indice, NodoB hijo)
        {
            NodoB nuevo = new NodoB(hijo.EsHoja);
            for (int j = 0; j < t - 1; j++)
                nuevo.Claves.Add(hijo.Claves[j + t]);

            if (!hijo.EsHoja)
            {
                for (int j = 0; j < t; j++)
                    nuevo.Hijos.Add(hijo.Hijos[j + t]);
            }

            hijo.Claves.RemoveRange(t, hijo.Claves.Count - t);
            if (!hijo.EsHoja)
                hijo.Hijos.RemoveRange(t, hijo.Hijos.Count - t);

            padre.Hijos.Insert(indice + 1, nuevo);
            padre.Claves.Insert(indice, hijo.Claves[t - 1]);
            hijo.Claves.RemoveAt(t - 1);
        }
        private void InsertarNoLleno(NodoB nodo, cUsuario clave)
        {
            int i = nodo.Claves.Count - 1;
            if (nodo.EsHoja)
            {
                nodo.Claves.Add(clave);
                nodo.Claves.Sort((x, y) => x.Celular.CompareTo(y.Celular));
            }
            else
            {

                while (i >= 0 && clave.Celular.CompareTo(nodo.Claves[i].Celular)<0) i--;
                i++;
                if (nodo.Hijos[i].Claves.Count == (2 * t - 1))
                {
                    DividirHijo(nodo, i, nodo.Hijos[i]);
                    if (clave.Celular.CompareTo(nodo.Claves[i].Celular)>0) i++;
                }
                InsertarNoLleno(nodo.Hijos[i], clave);
            }
        }


        // Recorrido en orden
        public void Recorrer(NodoB nodo = null)
        {
            if (nodo == null) nodo = raiz;
            int i;
            for (i = 0; i < nodo.Claves.Count; i++)
            {
                if (!nodo.EsHoja) Recorrer(nodo.Hijos[i]);
                nodo.Claves[i].Escribir();
            }
            if (!nodo.EsHoja) Recorrer(nodo.Hijos[i]);
        }

        // Eliminación completa
        public void Eliminar(string clave)
        {
            EliminarInterno(raiz, clave);
            if (raiz.Claves.Count == 0 && !raiz.EsHoja)
                raiz = raiz.Hijos[0]; // Ajustar raíz si queda vacía
        }

        private void EliminarInterno(NodoB nodo, string clave)
        {
            int idx = nodo.Claves.FindIndex(c => c.Celular == clave.ToString());

            if (idx != -1) // clave encontrada en este nodo
            {
                if (nodo.EsHoja)
                {
                    nodo.Claves.RemoveAt(idx); // caso 1: eliminar en hoja
                    return; // 🔹 detener aquí para evitar seguir accediendo a hijos
                }
                else
                {
                    // caso 2: eliminar en nodo interno
                    if (idx < nodo.Hijos.Count && nodo.Hijos[idx].Claves.Count >= t)
                    {
                        cUsuario pred = ObtenerPredecesor(nodo.Hijos[idx]);
                        nodo.Claves[idx] = pred;
                        EliminarInterno(nodo.Hijos[idx], pred.Celular);
                    }
                    else if (idx + 1 < nodo.Hijos.Count && nodo.Hijos[idx + 1].Claves.Count >= t)
                    {
                        cUsuario succ = ObtenerSucesor(nodo.Hijos[idx + 1]);
                        nodo.Claves[idx] = succ;
                        EliminarInterno(nodo.Hijos[idx + 1], succ.Celular);
                    }
                    else
                    {
                        // caso 3: fusión
                        Fusionar(nodo, idx);
                        EliminarInterno(nodo.Hijos[idx], clave);
                    }
                }
            }
            else
            {
                // clave no encontrada en este nodo
                if (nodo.EsHoja) return;

                int i = 0;
                while (i < nodo.Claves.Count && clave.ToString().CompareTo(nodo.Claves[i].Celular)>0) i++;

                if (i >= nodo.Hijos.Count) return; // 🔹 validación extra

                if (nodo.Hijos[i].Claves.Count < t)
                {
                    Llenar(nodo, i); // caso 4: préstamo o fusión

                    // CORRECCIÓN: Si se fusionó con el hermano anterior (izquierdo), 
                    // el índice 'i' queda obsoleto y debemos decrementarlo en 1.
                    if (i > nodo.Claves.Count)
                        i--;
                }

                EliminarInterno(nodo.Hijos[i], clave);
            }
        }

        private cUsuario ObtenerPredecesor(NodoB nodo)
        {
            while (!nodo.EsHoja) nodo = nodo.Hijos[nodo.Hijos.Count - 1];
            return nodo.Claves[nodo.Claves.Count - 1];
        }

        private cUsuario ObtenerSucesor(NodoB nodo)
        {
            while (!nodo.EsHoja) nodo = nodo.Hijos[0];
            return nodo.Claves[0];
        }

        private void Fusionar(NodoB nodo, int idx)
        {
            NodoB hijo = nodo.Hijos[idx];
            NodoB hermano = nodo.Hijos[idx + 1];

            hijo.Claves.Add(nodo.Claves[idx]);
            hijo.Claves.AddRange(hermano.Claves);

            if (!hijo.EsHoja)
                hijo.Hijos.AddRange(hermano.Hijos);

            nodo.Claves.RemoveAt(idx);
            nodo.Hijos.RemoveAt(idx + 1);
        }

        private void Llenar(NodoB nodo, int idx)
        {
            if (idx != 0 && nodo.Hijos[idx - 1].Claves.Count >= t)
                PrestarDeAnterior(nodo, idx);
            else if (idx != nodo.Claves.Count && nodo.Hijos[idx + 1].Claves.Count >= t)
                PrestarDeSiguiente(nodo, idx);
            else
            {
                if (idx != nodo.Claves.Count)
                    Fusionar(nodo, idx);
                else
                    Fusionar(nodo, idx - 1);
            }
        }

        private void PrestarDeAnterior(NodoB nodo, int idx)
        {
            NodoB hijo = nodo.Hijos[idx];
            NodoB hermano = nodo.Hijos[idx - 1];

            hijo.Claves.Insert(0, nodo.Claves[idx - 1]);
            if (!hijo.EsHoja)
                hijo.Hijos.Insert(0, hermano.Hijos[hermano.Hijos.Count - 1]);

            nodo.Claves[idx - 1] = hermano.Claves[hermano.Claves.Count - 1];
            hermano.Claves.RemoveAt(hermano.Claves.Count - 1);
            if (!hermano.EsHoja)
                hermano.Hijos.RemoveAt(hermano.Hijos.Count - 1);
        }

        private void PrestarDeSiguiente(NodoB nodo, int idx)
        {
            NodoB hijo = nodo.Hijos[idx];
            NodoB hermano = nodo.Hijos[idx + 1];

            hijo.Claves.Add(nodo.Claves[idx]);
            if (!hijo.EsHoja)
                hijo.Hijos.Add(hermano.Hijos[0]);

            nodo.Claves[idx] = hermano.Claves[0];
            hermano.Claves.RemoveAt(0);
            if (!hermano.EsHoja)
                hermano.Hijos.RemoveAt(0);
        }
        public cUsuario Buscar(string id)
        {
            return BuscarInterno(Raiz,id);
        }

        public cUsuario BuscarInterno(NodoB nodo, string id)
        {
            int i = 0;

            while (i < nodo.Claves.Count &&
                   id.CompareTo(nodo.Claves[i].Celular)>0) //busca y aumenta a i cuando id> nodo.claves[i]
                i++;

            if (i < nodo.Claves.Count && //si es igual a clave
                nodo.Claves[i].Celular == id)
                return nodo.Claves[i];

            if (nodo.EsHoja) //ya no puede seguir buscando si es hoja
                return null;

            return BuscarInterno(nodo.Hijos[i], id);
        }
        public void Dibujar()
        {
            DibujarNodo(raiz, "", true);
        }

        private void DibujarNodo(NodoB nodo, string prefijo, bool ultimo)
        {
            if (nodo == null) return;

            Console.Write(prefijo);

            if (ultimo)
            {
                Console.Write("└── ");
                prefijo += "    ";
            }
            else
            {
                Console.Write("├── ");
                prefijo += "│   ";
            }

            Console.WriteLine("[" + string.Join(" ", nodo.Claves.Select(c => c.Celular)) + "]");

            for (int i = 0; i < nodo.Hijos.Count; i++)
            {
                DibujarNodo(
                    nodo.Hijos[i],
                    prefijo,
                    i == nodo.Hijos.Count - 1
                );
            }
        }
    }
}
