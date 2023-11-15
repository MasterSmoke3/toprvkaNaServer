// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;


using var tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
//var tcpListener = new TcpListener(IPAddress.Any, 8888);
try
{
    // Подключение к серверу
    await tcpClient.ConnectAsync("192.168.1.104", 1777);
    while (true)
    {
    
    // чтение изображения ввиде массива
    byte[] imageData = File.ReadAllBytes("1449846366_40.jpg");
    
    //Создание сообщения
    //System.Console.WriteLine("Введите команду для сервера");
    //string command = Console.ReadLine() + '\n';

    //Первеод запроса из строкового типа данных в массив байт
    byte[] requestData = File.ReadAllBytes("./1449846366_40.jpg");

    // Отправка сообщения серверу
    await tcpClient.SendAsync(requestData, SocketFlags.None);

    // Оповещение о состоянии сообщения
    Console.WriteLine("Изображение отправленно");

    // Сообщение буфера для приема ответа от сервера
    byte[] data = new byte[512];

    // Получаем данные из потока и учитываем количество полученных байт 
    int bytes = await tcpClient.ReceiveAsync(data);

    //Декодируем полученные данные во время
    string time = Encoding.UTF8.GetString(data, 0, bytes);


    //Выводим на печать время
    Console.WriteLine($"Текущее время: {time}");  
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}