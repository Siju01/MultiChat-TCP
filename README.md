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

# Flowchart
Berikut adalah alur pengiriman data pada multiclient socket :
![FlowChat](https://user-images.githubusercontent.com/63763376/124770218-5b837e80-df64-11eb-8dd6-749818a12ce1.jpg)

# Result
![ss](https://user-images.githubusercontent.com/63763376/124595744-99f93a80-de8b-11eb-9a6f-6863f2df8ec4.png)
