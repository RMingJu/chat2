using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Data;

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
        public void Send(string name, string message,string imgPath)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message, imgPath);
        }

        //加入問題
        public void AddQuesiton(int intQutionNumber)
        {
            PubClass myClass = new PubClass();
            DataTable dtQuestion =  myClass.GetQuestionByID(1);
            String Question = myClass.ConvertToJsonString(dtQuestion);
            Clients.All.addQuestion(Question);
        }


    }
}