using RedSocialGB.Modelos;
using System;

public class cSolicitudAmistad
{
    #region --- Atributos ---

    private cUsuario aRemitente;
    private cUsuario aDestinatario;
    private DateTime aFechaEnvio;

    #endregion

    #region --- Constructores ---

    public cSolicitudAmistad()
    {
    }

    public cSolicitudAmistad(cUsuario pRemitente, cUsuario pDestinatario)
    {
        aRemitente = pRemitente;
        aDestinatario = pDestinatario;
        aFechaEnvio = DateTime.Now;
    }

    #endregion

    #region --- Propiedades ---

    public cUsuario Remitente
    {
        get { return aRemitente; }
        set { aRemitente = value; }
    }

    public cUsuario Destinatario
    {
        get { return aDestinatario; }
        set { aDestinatario = value; }
    }

    public DateTime FechaEnvio
    {
        get { return aFechaEnvio; }
        set { aFechaEnvio = value; }
    }

    #endregion

    #region --- Métodos ---

    public bool EsRemitente(cUsuario pUsuario)
    {
        return aRemitente.ToString() == pUsuario.ToString();
    }

    public bool EsDestinatario(cUsuario pUsuario)
    {
        return aDestinatario.ToString() == pUsuario.ToString();
    }

    public bool InvolucraA(cUsuario pUsuario)
    {
        return aRemitente.ToString() == pUsuario.ToString() || aDestinatario.ToString() == pUsuario.ToString();
    }
    public override bool Equals(object obj)
    {
        if (obj is cSolicitudAmistad otra)
        {
            return
                aRemitente.ToString() == otra.aRemitente.ToString() &&
                aDestinatario.ToString() == otra.aDestinatario.ToString();
        }

        return false;
    }
    public override string ToString()
    {
        return $"{aRemitente.NombreUsuario} -> {aDestinatario.NombreUsuario} ({aFechaEnvio:dd/MM/yyyy HH:mm})";
    }

    #endregion
}