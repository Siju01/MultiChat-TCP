using System;
using System.Threading;
using System.Net.Sockets;
using System.Text;

namespace Client
{
    class Program {
        //inisialisasi variabel
        static string userName; //username
        private const string host = "127.0.0.1"; //ip address yang digunakan
        private const int port = 8888; //port yang digunakan
        static TcpClient client; //tcp
        static NetworkStream stream; //stream
        
        //proses untuk mencatat pesan yang masuk
        static void Main(string[] args) 
        {
            //user meng-input nama
            Console.Write("Enter your name: ");
            userName = Console.ReadLine();
            client = new TcpClient(); 
            try {
                //connect client ke server dengan menggunakan ip address dan port yang telah ditentukan
                client.Connect(host, port); 
                stream = client.GetStream(); 

                string message = userName; //message adalah username, dalam bentuk string
                byte[] data = Encoding.Unicode.GetBytes(message); //string message diubah ke byte
                stream.Write(data, 0, data.Length); //kirim data ke server

                //buat thread untuk mengirim data ke server
                Thread receiveThread = new Thread(new ThreadStart(ReceiveMessage)); 
                receiveThread.Start(); 
                
                //tampilkan di program (ex: Welcome juju)
                Console.WriteLine("Welcome, " + userName);
                SendMessage(); //kirim pesan ke server
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message); 
            }
        }
        
        //mengirim pesan ke server
        static void SendMessage() {
            Console.WriteLine("Type Your message: "); //menulis pesan

            while (true) { //jika benar lakukan pengulangan
                //user meng-input pesan
                string message = Console.ReadLine(); //ambil message dari pengirimi, dalam bentuk string
                byte[] data = Encoding.Unicode.GetBytes(message); //convert message dari string ke byte
                stream.Write(data, 0, data.Length); //kirim data ke server
            }
        }

        //memeriksa apakah ada data kiriman yang masuk dari server
        static void ReceiveMessage()
        {
            while (true) { //jika benar lakukan pengulangan
                try {
                    byte[] data = new byte[64]; //besar maksimal array byte yang diterima
                    StringBuilder builder = new StringBuilder(); //inisialisasi string builder
                    int bytes = 0; 
                    
                    do {
                        bytes = stream.Read(data, 0, data.Length); //stream dibaca, diconvert ke byte
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes)); //byte di convert ke string
                        break;
                    }
                    while (stream.DataAvailable); //pengecekan ketika ada pesan baru
                    string message = builder.ToString(); //pesan ditampilkan dalam bentuk string
                    Console.WriteLine(message); //tampilkan pesan
                }
                catch (Exception e) { //proses ketika server keluar dari program
                    Console.Write(e.Message); //tampilkan pesan (unable...)
                    break; 
                }
            }
        }
    }
}
