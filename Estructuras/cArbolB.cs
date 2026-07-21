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
        public List<object> Claves { get; set; } = new List<object>();
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
        public void Insertar(object clave)
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
        private void InsertarNoLleno(NodoB nodo, object clave)
        {
            int i = nodo.Claves.Count - 1;
            if (nodo.EsHoja)
            {
                nodo.Claves.Add(clave);
                nodo.Claves.Sort((x, y) => x.ToString().CompareTo(y.ToString()));
            }
            else
            {

                while (i >= 0 && clave.ToString().CompareTo(nodo.Claves[i].ToString()) < 0) i--;
                i++;
                if (nodo.Hijos[i].Claves.Count == (2 * t - 1))
                {
                    DividirHijo(nodo, i, nodo.Hijos[i]);
                    if (clave.ToString().CompareTo(nodo.Claves[i].ToString()) > 0) i++;
                }
                InsertarNoLleno(nodo.Hijos[i], clave);
            }
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
            int idx = nodo.Claves.FindIndex(c => c.ToString() == clave.ToString());

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
                        object pred = ObtenerPredecesor(nodo.Hijos[idx]);
                        nodo.Claves[idx] = pred;
                        EliminarInterno(nodo.Hijos[idx], pred.ToString());
                    }
                    else if (idx + 1 < nodo.Hijos.Count && nodo.Hijos[idx + 1].Claves.Count >= t)
                    {
                        object succ = ObtenerSucesor(nodo.Hijos[idx + 1]);
                        nodo.Claves[idx] = succ;
                        EliminarInterno(nodo.Hijos[idx + 1], succ.ToString());
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
                while (i < nodo.Claves.Count && clave.ToString().CompareTo(nodo.Claves[i].ToString()) > 0) i++;

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

        private object ObtenerPredecesor(NodoB nodo)
        {
            while (!nodo.EsHoja) nodo = nodo.Hijos[nodo.Hijos.Count - 1];
            return nodo.Claves[nodo.Claves.Count - 1];
        }

        private object ObtenerSucesor(NodoB nodo)
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
        public object Buscar(string id, Func<object,string> Selector)
        {
            return BuscarInterno(Raiz,id,Selector);
        }

        public object BuscarInterno(NodoB nodo, string id, Func<object, string> Selector)
        {
            int i = 0;

            while (i < nodo.Claves.Count &&
                   id.CompareTo(Selector(nodo.Claves[i])) > 0) //busca y aumenta a i cuando id> nodo.claves[i]
                i++;

            if (i < nodo.Claves.Count && //si es igual a clave
                Selector(nodo.Claves[i]) == id)
                return nodo.Claves[i];

            if (nodo.EsHoja) //ya no puede seguir buscando si es hoja
                return null;

            return BuscarInterno(nodo.Hijos[i], id, Selector);
        }

        //------------------------------------------------
        public void Recorrer(Action<object> accion)
        {
            RecorrerInterno(raiz, accion);
        }

        //------------------------------------------------
        private void RecorrerInterno(NodoB nodo, Action<object> accion)
        {
            if (nodo == null)
                return;

            int i;

            for (i = 0; i < nodo.Claves.Count; i++)
            {
                // Recorrer hijo izquierdo
                if (!nodo.EsHoja)
                    RecorrerInterno(nodo.Hijos[i], accion);

                // Procesar clave
                accion(nodo.Claves[i]);
            }

            // Recorrer último hijo
            if (!nodo.EsHoja)
                RecorrerInterno(nodo.Hijos[i], accion);
        }

    }
}
