using QuickFix;
using System;
using System.Collections.Generic;
using System.Text;

namespace FixAcceptor
{
    public class Server
    {
        static void Main(string[] args)
        {
            try
            {
                SessionSettings settings = new SessionSettings(@"C:\Users\anhtv\Desktop\QuickFix\QuickFix\acceptor1.cfg");
                FixServerApplication application = new FixServerApplication();
                FileStoreFactory storeFactory = new FileStoreFactory(settings);
                ScreenLogFactory logFactory = new ScreenLogFactory(settings);
                MessageFactory messageFactory = new DefaultMessageFactory();
                SocketAcceptor acceptor = new SocketAcceptor(application, storeFactory, settings, logFactory, messageFactory);

                acceptor.start();
                Console.WriteLine("press enter to quit");
                Console.Read();
                acceptor.stop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }           
        }
    }

    public class FixServerApplication : MessageCracker, QuickFix.Application
    {
        public void onCreate(SessionID sessionID) { }
        public void onLogon(SessionID sessionID) { }
        public void onLogout(SessionID sessionID) { }
        public void toAdmin(Message message, SessionID sessionID) { }
        public void toApp(Message message, SessionID sessionID) { }
        public void fromAdmin(Message message, SessionID sessionID)
        { }
        public void fromApp(Message message, SessionID sessionID)
        {
            //string length = message.getHeader().getField(9);
            //string checksum = message.getHeader().getField(10);
            //string msgType = message.getHeader().getField(35);
            crack(message, sessionID);//convert to proper Message(newsingleorder, cancelorder,...)            
        }

        //35=8
        public override void onMessage(QuickFix42.ExecutionReport message, SessionID session)
        {
            ExecType et = message.getExecType();
            char type = et.getValue();
            switch (type)
            {
                case 'A':
                    OrdStatus ordS = message.getOrdStatus();
                    char status = ordS.getValue();
                    //if status == 'A'
                    //order acknowledgement
                    break;
                case '0':
                case 'B':
                case 'F':
                    break;
            }

        }

        //35=9
        public override void onMessage(QuickFix42.OrderCancelReject message, SessionID sessionID)
        {
        }

        //35=G
        public override void onMessage(QuickFix42.OrderCancelReplaceRequest message, SessionID sessionID)
        {
        }

        //35 = F
        public override void onMessage(QuickFix42.OrderCancelRequest message, SessionID sessionID)
        {
        }

        //35 = D
        public override void onMessage(QuickFix42.NewOrderSingle message, SessionID sessionID)
        {
            Console.WriteLine("Receive message " + message.getHeader().getField(35) + ", session: " + sessionID.toString());
            try
            {
                ClOrdID clordid = message.getClOrdID();
                string clord = clordid.getValue();
                Side side = message.getSide();
                char s = side.getValue();
                OrdType ordtype = message.getOrdType();
                char ord = ordtype.getValue();
                TransactTime time = message.getTransactTime();
                DateTime dt = time.getValue();

                QuickFix42.ExecutionReport executionReport = new QuickFix42.ExecutionReport(new OrderID("neworderid"), new ExecID("Hehe")
                    , new ExecTransType(ExecTransType.CORRECT), new ExecType(ExecType.NEW), new OrdStatus(OrdStatus.DONE_FOR_DAY),
                    new Symbol("VND"), new Side(Side.BUY), new LeavesQty(1), new CumQty(2), new AvgPx(100));

                bool x = Session.sendToTarget(executionReport, sessionID);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
