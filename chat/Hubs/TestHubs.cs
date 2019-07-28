using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

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
            DataTable dtQuestion =  myClass.GetQuestionByID(intQutionNumber);
            String Question = myClass.ConvertToJsonString(dtQuestion);
            Clients.All.addQuestion(Question);
        }

        //回答問題
        public void AnswerQuesiton(int intQutionNumber,String userName,String answer)
        {
            PubClass myClass = new PubClass();
            bool blnAnswerQuestion = myClass.AnswerQuestion(intQutionNumber,userName, answer);
            Clients.Caller.answerQuesiton(intQutionNumber, blnAnswerQuestion);
        }

        //傳送答案
        public void SendAnswer(int intQutionNumber)
        {
            PubClass myClass = new PubClass();
            DataTable dtAnswwerImg = myClass.GetAnswerImgByID(intQutionNumber);
            String imgColleaction = myClass.ConvertToJsonString(dtAnswwerImg);
            DataTable dtUserIngfo = myClass.GetUserInfoByPhoneNumber(dtAnswwerImg.Rows[0]["phoneNumber"].ToString());
            String userInfo = myClass.ConvertToJsonString(dtUserIngfo);

            JArray ja = new JArray();
            JObject jo = new JObject();
            jo.Add(new JProperty("imgCollection", imgColleaction));
            jo.Add(new JProperty("userInfo", userInfo));
            ja.Add(jo);
            String jsonString = JsonConvert.SerializeObject(ja);

            Clients.All.sendAnswer(jsonString);
        }

        //計算分數
        public void CalcScore(int intQutionNumber)
        {
            PubClass myClass = new PubClass();
            bool blnCalcScore = myClass.CalcScore(intQutionNumber);
            Clients.All.calcScore(intQutionNumber, blnCalcScore);
        }

        //取得分數板
        public void GetScore()
        {
            PubClass myClass = new PubClass();
            DataTable dtScore = myClass.GetScore();
            String strJson = myClass.ConvertToJsonString(dtScore);
            Clients.All.getScore(strJson);
        }

        public void clearMessage() {
            Clients.All.clearMessage();
        }

    }
}