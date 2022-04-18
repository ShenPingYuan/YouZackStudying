using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace WebApiSignalR.Hubs
{
    [Authorize]
    public class ChatRoomHub:Hub
    {
        public async Task SendPublicMessage(string message)
        {
            var user = Context.User;
            string connId=Context.ConnectionId;
            string msgToSend = $"{connId} {DateTime.Now}:{message}";

            await Groups.AddToGroupAsync(connId, "dev");
            //Groups.RemoveFromGroupAsync()
             
            //Clients.Caller//发送者
            //Clients.Clients();
            //Clients.Others//其他的
            //Clients.OthersInGroup("sdf")//某个组里边的其他人
            //Clients.User("2")//某个Id的用户
            await Clients.All.SendAsync("ReceivePublicMessage", msgToSend);
        }
        public async Task SendPrivateMessage(string destUserId,string message)
        {

            var userId = 5;//find user id from db by destUserId;
            string time=DateTime.Now.ToShortTimeString();
            string curUser = Context.User!.FindFirst(ClaimTypes.Name)!.Value;
            await Clients.User(destUserId).SendAsync("ReceivePrivateMessage", curUser, time, message);

            //client:await connection.invoke("SendPrivateMessage",destUserId,msg);
            //connection.on('ReceivePrivateMessage',(userFrom,time,smg)=>{...};
        }
    }
}
