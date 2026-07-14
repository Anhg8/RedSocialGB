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
        return aRemitente.Celular == pUsuario.Celular;
    }

    public bool EsDestinatario(cUsuario pUsuario)
    {
        return aDestinatario.Celular == pUsuario.Celular;
    }

    public bool InvolucraA(cUsuario pUsuario)
    {
        return aRemitente.Celular == pUsuario.Celular || aDestinatario.Celular == pUsuario.Celular;
    }
    public override bool Equals(object obj)
    {
        if (obj is cSolicitudAmistad otra)
        {
            return
                aRemitente.Celular == otra.aRemitente.Celular &&
                aDestinatario.Celular == otra.aDestinatario.Celular;
        }

        return false;
    }
    public override string ToString()
    {
        return $"{aRemitente.NombreUsuario} -> {aDestinatario.NombreUsuario} ({aFechaEnvio:dd/MM/yyyy HH:mm})";
    }

    #endregion
}