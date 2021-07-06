using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Server
{
    class ClientObject  {
        //inisialisasi variabel
        public string Id { get; private set; } //id untuk setiap client
        public NetworkStream Stream { get; private set; } //stream
        string userName; //username
        TcpClient client; //tcp 
        ServerObject server; //server
        SaveData save = new SaveData(); //untuk menyimpan data
        
        //membuat id baru disetiap koneksi baru
        public ClientObject(TcpClient tcpClient, ServerObject serverObject) { 
            Id = Guid.NewGuid().ToString(); //id diconvert ke string
            client = tcpClient; 
            server = serverObject; 
            serverObject.AddConnection(this); //menambahkan koneksi ke server
        }

        //memproses data dari client 
        public void Process() {
            try {
                Stream = client.GetStream(); //menerima streaam client
                string message = GetMessage(); //string message = dari void GetMessage
                userName = message; //input username

                message = userName + " entered the group"; //format tampilan (ex: Juju entered the group)
                server.BroadcastMessage(message, this.Id); //message dibroadcast dengan id yang telah terkoneksi
                Console.WriteLine(message); //tampilkan message

                while (true) {
                    try {
                        message = GetMessage(); //ambil message dari void GetMessage
                        message = String.Format("{0}: {1}", userName, message); //format tampilan chat (ex= juju: haloo)
                        Console.WriteLine(message); //tampilkan message
                        server.BroadcastMessage(message, this.Id); //message dibroadcast dengan id yang telah terkoneksi
                        
                        save.writeMessage(message); //message disimpan
                    }
                    catch { //jika client keluar dari program
                        message = String.Format("{0}: left the chat", userName); //format tampilan (ex= juju: let the chat)
                        Console.WriteLine(message); //tampilkan message
                        server.BroadcastMessage(message, this.Id); //message dibroadcast dengan id yang telah terkoneksi
                        break;
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            finally {
                server.RemoveConnection(this.Id); //hapus koneksi client ketika client keluar
                Close(); //close program
            }
        }
        
        //Menyimpan data ke dalam file .txt
        class SaveData
        {
            private List<string> saveMessages = new List<string>(); //pesan di list
            public void writeMessage(string message)
            {
                //message di simpan pada file txt dibawah ini
                saveMessages.Add(message);
                File.WriteAllLines("D:/Pens/Semester 4/Arsitektur Jaringan & Komputer/FP MultiCLient/chats.txt", saveMessages);
            }
        }

        //menerima data dari user client
        private string GetMessage() {
            byte[] data = new byte[64]; //panjang array byte yang diterima
            StringBuilder builder = new StringBuilder(); //inisialisasi string builder
            int bytes = 0; 

            do {
                bytes = Stream.Read(data, 0, data.Length); //stream dibaca, diconvert ke byte
                builder.Append(Encoding.Unicode.GetString(data, 0, bytes)); //byte di convert ke string
            } 
            while (Stream.DataAvailable); //pengecekan ketika ada pesan baru

            return builder.ToString(); //kembali ke builder
        }

        //proses close program
        public void Close() {
            //jika stream / client menghilang, maka close
            if (Stream != null)
                Stream.Close();
            if (client != null)
                client.Close();
        }
    }

    class ServerObject 
    {
        static TcpListener tcpListener; //inisialisasi tcpListener
        List<ClientObject> clients = new List<ClientObject>(); //menambahkan list ketika ada client baru masuk

        //menyambungkan koneksi client 
        public void AddConnection(ClientObject clientObject) {
            clients.Add(clientObject); //menambahkan koneksi ke class client object
        }

        //memutuskan koneksi client
        public void RemoveConnection(string id) {
            ClientObject client = clients.FirstOrDefault(c => c.Id == id); //mengembalikan ke default
            //jika client menghilang, maka hapus koneksi
            if (client != null)
                clients.Remove(client);
        }

        //memeriksa client yang terhubung ke server
        public void Listen() {
            try {
                tcpListener = new TcpListener(IPAddress.Any, 8888); //menghubungkan tcpListener dengan Ip addres dan port yang sudah ditentukan
                tcpListener.Start(); //mulai server
                Console.WriteLine("Server started"); //tampilkan pada program

                while (true) {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient(); //menerima client

                    ClientObject clientObject = new ClientObject(tcpClient, this); 
                    //proses thread, setiap ada client baru akan dibuatkan thread baru
                    Thread clientThread = new Thread(new ThreadStart(clientObject.Process)); 
                    clientThread.Start(); //mulai thread
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        //mengambil data dari pengirim dan menyebarkan ke penerima
        public void BroadcastMessage(string message, string id) {
            byte[] data = Encoding.Unicode.GetBytes(message); //data diconvert ke byte

            //jika id itu bukanlah id pengirim, maka pesan di broadcast
            //artinya id pengirim dikecualikan dari broadcast
            for (int i = 0; i < clients.Count; i++) { 
                if (clients[i].Id != id) {  
                    clients[i].Stream.Write(data, 0, data.Length);
                }
            }
        }
    }

    //mencatat semua client yang masuk di fungsi main
    class Program {
        //inisialisasi variabel
        static ServerObject server; 
        static Thread listenThread; 
        static void Main(string[] args) {
            try {
                server = new ServerObject(); //membuat serverobject baru
                listenThread = new Thread(new ThreadStart(server.Listen)); //memanggil thread yang ada di fungsi listen
                listenThread.Start(); //mulai thread
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
