using System;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace chat.Hubs
{
    public class TestHubs : Hub
    {
        //顯示加入聊天室
        public void Hello(string name)
        {
            //這邊會傳入name參數
            //呼叫所有連線狀態中頁面上的 javascript function => hello
            //透過server端呼叫client的javascript function

            string message = "歡迎 【" + name + "】 加入聊天室";
            //Clients.All.addNewMessageToPage(name, message);
            Clients.All.hello(message);
        }

        //傳送訊息
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }
    }
}