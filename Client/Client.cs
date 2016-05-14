using QuickFix;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FixInitiator
{
    public class Client
    {
        static void Main()
        {
            try
            {
                ClientInitiator app = new ClientInitiator();
                SessionSettings settings = new SessionSettings(@"C:\Users\anhtv\Desktop\QuickFix\QuickFix\initiator.cfg");
                QuickFix.Application application = new ClientInitiator();
                FileStoreFactory storeFactory = new FileStoreFactory(settings);
                ScreenLogFactory logFactory = new ScreenLogFactory(settings);
                MessageFactory messageFactory = new DefaultMessageFactory();

                SocketInitiator initiator = new SocketInitiator(application, storeFactory, settings, logFactory, messageFactory);
                initiator.start();
                Thread.Sleep(3000);
                System.Collections.ArrayList list = initiator.getSessions();
                SessionID sessionID = (SessionID)list[0];
                Console.WriteLine("Press any key: ");
                string x = Console.ReadLine();
                QuickFix42.NewOrderSingle order = new QuickFix42.NewOrderSingle(new ClOrdID("Hello"), new HandlInst(HandlInst.AUTOMATED_EXECUTION_ORDER_PRIVATE), new Symbol("ABC"), new Side(Side.SELL), new TransactTime(DateTime.Now), new OrdType(OrdType.MARKET));
                bool sent = Session.sendToTarget(order, sessionID);
                Console.ReadLine();
                initiator.stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadLine();
            }
            
        }
    }

    public class ClientInitiator : QuickFix.Application
    {
        public void onCreate(QuickFix.SessionID value)
        {
        }

        public void onLogon(QuickFix.SessionID value)
        {
        }

        public void onLogout(QuickFix.SessionID value)
        {
        }

        public void toAdmin(QuickFix.Message value, QuickFix.SessionID session)
        {
        }

        public void toApp(QuickFix.Message value, QuickFix.SessionID session)
        {
        }

        //nhan message logon(A), testrequest(2), heartbeat(0), 
        public void fromAdmin(QuickFix.Message value, SessionID session)
        {
        }

        //nhan message neworder(D)
        public void fromApp(QuickFix.Message value, SessionID session)
        {
            Console.WriteLine("Receive message " + value.ToString() + ", session: " + session + "\n");
        }
    }
}
