/**
* Permite integrar processing con Unity, para leer un marcador y mover
* un objeto.
*/
using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

public class TCPClient : MonoBehaviour
{

    internal Boolean socketReady = false;
    TcpClient mySocket;
    NetworkStream theStream;
    StreamReader theReader;
    String Host = "localhost";
    Int32 Port = 5204;

    public int tcpX = 0;
    public int tcpY = 0;
    public float tcpxCorrection = 200;
    public float tcpyCorrection = 200;
    public bool tpcxBloq = false;
    public bool tpcyBloq = false;

    void Start()
    {
        abrirElSocket();
    }

    void Update()
    {
        transform.position = new Vector3(0, 0, 0);

        leerDatosProcessing();

        transform.position = new Vector3((tcpX + tcpxCorrection) * (tpcxBloq ? 0 : 1), (tcpY + tcpyCorrection) * (tpcyBloq ? 0 : 1), 0);
    }


    /**
    * Leemos los datos que llegan por el socket
    * esta informacion la envia processing.
    * */
    public void leerDatosProcessing()
    {
        string informacion = readSocket();
        if (informacion != "")
        {
            string[] partes = informacion.Split(
                new string[] { "," },
                StringSplitOptions.None
            );
            Debug.Log("X=" + partes[0] + " Y=" + partes[1]);
            tcpX = Int32.Parse(partes[0]);
            tcpY = Int32.Parse(partes[1]);
        }
    }


    /**
    * Crea el socket al puerto e Ip datos.
    * **/
    public void abrirElSocket()
    {
        try
        {
            mySocket = new TcpClient(Host, Port);
            theStream = mySocket.GetStream();
            theReader = new StreamReader(theStream);
            socketReady = true;
        }
        catch (Exception e)
        {
            Debug.Log("Socket error: " + e);
        }
    }

    /**
    * Lee datos del socket
    * **/
    public String readSocket()
    {
        if (!socketReady)
            return "";
        if (theStream.DataAvailable)
            return theReader.ReadLine();
        return "";
    }

    /**
    * Cierra el socket
    * */
    public void closeSocket()
    {
        if (!socketReady)
            return;
        theReader.Close();
        mySocket.Close();
        socketReady = false;
    }
}