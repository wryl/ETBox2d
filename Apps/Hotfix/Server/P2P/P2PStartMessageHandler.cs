using System;
using System.Collections.Generic;
using System.IO;

namespace ET.Server
{
	[MessageHandler(SceneType.P2PServer)]
	public class P2PStartMessageHandler : AMHandler<P2PStartMessage>
	{
		protected override async ETTask Run(Session session,  P2PStartMessage request)
		{
			session.DomainScene().GetComponent<P2PManager>().CurrentNum++;
			if (session.DomainScene().GetComponent<P2PManager>().CurrentNum>=session.DomainScene().GetComponent<P2PManager>().MaxPlayer)
			{
				List<Session> templist = new List<Session>();
				foreach (Session onesession in session.DomainScene().GetComponent<NetKcpComponent>().Children.Values)
				{
					templist.Add(onesession);
				}
				templist[0].Send(new P2PTargetMessage(){TargetAddress=templist[1].RemoteAddress.ToString() ,RemoteConn =((KService)templist[1].AService).Get(templist[1].Id).RemoteConn});
				templist[1].Send(new P2PTargetMessage(){TargetAddress=templist[0].RemoteAddress.ToString() ,RemoteConn =((KService)templist[0].AService).Get(templist[0].Id).RemoteConn});
				Log.Debug("P2PStartMessage get 2");
			}
			await ETTask.CompletedTask;
		}
	}
}