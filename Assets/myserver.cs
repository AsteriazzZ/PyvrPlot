using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCPPP{
  class tcpp{
     private int port;
     private TcpListener server;
     // private int host
     public tcpp(){
        port = 14988;
     }
     public void start(){
        Debug.Log("Starting...");
        server = new TcpListener(IPAddress.Parse("127.0.0.1"), this.port);
        server.Start();
        Debug.Log("Started.");
        // while (true)
        // {
        //   ClientWorking cw = new ClientWorking(server.AcceptTcpClient());
        //   new Thread(new ThreadStart(cw.DoSomethingWithClient)).Start();
        // }
        // server.Stop();
      }
     public string try_connect(){
        if(server.Pending()){
          Debug.Log("Start connecting");
           ClientWorking cw = new ClientWorking(server.AcceptTcpClient());
           Debug.Log("Connected");
           return cw.work();
        }
        else return "";
     }
     public void stop(){
        Debug.Log("Destroyed");
        server.Stop();
     }
  }

  class ClientWorking{
     private Stream ClientStream;
     private TcpClient Client;
     public ClientWorking(TcpClient Client){
        this.Client = Client;
        ClientStream = Client.GetStream();
     }
     public string work(){
        StreamWriter sw = new StreamWriter(ClientStream);
        StreamReader sr = new StreamReader(sw.BaseStream);
        //sw.WriteLine("Hi. This is x2 TCP/IP easy-to-use server");
        //sw.Flush();
        string data;
        string all = "";
        try
        {
          //Debug.Log(sr.ReadLine());
          int haha = 0;
          while ((data = sr.ReadLine()) != null)
          {
            all += data;
            all += '\n';
          }
        }
        finally
        {
          sw.Close();
        }
        Debug.Log("finished");
        Debug.Log(all);
        return all;
     }
  }
}