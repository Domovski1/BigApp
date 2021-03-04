using BigApp.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Windows;

namespace BigApp.Classes
{
    public class ApiServer
    {
        static void Main()
        {
            HttpListener server = new HttpListener();
            server.Prefixes.Add("http://localhost:2117/");
            server.Start();

            JsonSerializerOptions options = new JsonSerializerOptions { Encoder = JavaScriptEncoder.Create(UnicodeRanges.All) };

            while (server.IsListening)
            {
                HttpListenerContext context = server.GetContext();
                if (context.Request.HttpMethod == "GET")
                {
                    try
                    {

                        if (context.Request.RawUrl == "/users/")
                        {
                            var ListUser = BaseConnect.db.User.ToList();

                            string response = JsonSerializer.Serialize<List<User>>(ListUser, options);

                            byte[] data = Encoding.UTF8.GetBytes(response);

                            context.Response.ContentType = "application/json;charset=utf-8";
                            using (Stream stream = context.Response.OutputStream)
                            {
                                stream.Write(data, 0, data.Length);
                            }
                            context.Response.StatusCode = 200;
                            context.Response.Close();
                        } else
                        {
                            MessageBox.Show("Возникла ошибка", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        context.Response.StatusCode = 400;
                        context.Response.Close();
                    }
                }
            }
        }
    }
}
