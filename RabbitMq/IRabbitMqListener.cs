
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;

namespace RabbitRepository{

public interface IRabbitMqListener
{
 		public void Listen();
		public string GetLastMessage();
}		
}