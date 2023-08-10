using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatHubSpace
{
   
        public interface IChatHub
    {
        public  Task Send(string message);
      
    }

}

