using System.Xml;
using System.Xml.Serialization;

public class EstructuraPersona
{
	public string nombre;
	public string mensajeInicial;
	public string dialogo;
}

public class EstructuraPersonaDialogo
{
	public string nombre;
	public string dialogo;
	public string solucion1;
	public string solucion2;
	public string solucion3;
	public int solucionCorrecta;
}

public class EstructuraObjeto
{
	public string nombre;
	public string mensajeInicial;
}