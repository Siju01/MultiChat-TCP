# MultiChat-TCP-Socket
Multiclient using TCp Socket

# Description
Socket adalah mekanisme komunikasi yang memungkinkan terjadinya pertukaran data antar program (clients & server).
Pada repository ini menggunakan protokol TCP.

Terdapat beberapa hal yang harus diperhatikan dalam membuat program ini :
  - Server harus memiliki IP Public.
  - Server harus memiliki Port yang diketahui seluruh client.
  - Program harus menggunakan Thread yang berbeda untuk menangani setiap client yang terhubung.

# How to Use
1. Clone repository ini.
2. Buka File server.
3. Ubah lokasi file .txt 

"D:/Pens/Semester 4/Arsitektur Jaringan & Komputer/FP MultiCLient/chats.txt"

ke lokasi anda .txt file

4. Anda harus menjalankan program server terlebih dahulu, kemudian jalankan program client.
5. Anda harus input username terlebih dahulu agar client dapat terkoneksi ke server.

# Flowchart Chat
Berikut adalah flowchart chat pada server yang menangani client
![Flowchart MultiChat](https://user-images.githubusercontent.com/63763376/124835005-d3749780-dfaa-11eb-8eed-04246011e17d.png)


# Flowchart TCP
![FlowChat](https://user-images.githubusercontent.com/63763376/124850585-1ba3b200-dfcb-11eb-917c-1528e029aa30.jpg)


# Result
![ss](https://user-images.githubusercontent.com/63763376/124595744-99f93a80-de8b-11eb-9a6f-6863f2df8ec4.png)
