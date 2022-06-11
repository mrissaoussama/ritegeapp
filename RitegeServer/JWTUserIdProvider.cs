using Microsoft.AspNetCore.SignalR;

internal class JWTUserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
     return  connection.UserIdentifier = connection.ConnectionId;
    }
}