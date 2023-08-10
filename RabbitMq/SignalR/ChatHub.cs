using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatHubSpace
{
   
  
        public class ChatHub : Hub, IChatHub
    {
          private readonly IHubContext<ChatHub> _hubContext;

        public ChatHub(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task Send(string message)
        {
            await _hubContext.Clients.All.SendAsync("Receive", message);
        }
    }

}

