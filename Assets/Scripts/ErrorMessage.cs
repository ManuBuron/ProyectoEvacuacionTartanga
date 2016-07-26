using System.Xml;
using System.Xml.Serialization;

public class ErrorMessage{

	[XmlAttribute ("errorId")]
	public int errorId;
	public string errorMessage;

	public ErrorMessage(){
	}

	public ErrorMessage(int id,string message){
		this.errorId = id;
		this.errorMessage = message;
	}
}
