//using System;
//using System.Collections.Generic;

//using System.Windows.Forms;
//using QuickFix;
//using System.Threading;
//using System.Text;


//namespace Client
//{
//    class Client
//    {
//        /// <summary>
//        /// The main entry point for the application.
//        /// </summary>
//        //[STAThread]
//        static void Main()
//        {
//            ClientInitiator app = new ClientInitiator();
//            SessionSettings settings = new SessionSettings(@"C:\Users\anhtv\Desktop\QuickFix\QuickFix\initiator.cfg");
//            QuickFix.Application application = new ClientInitiator();
//            FileStoreFactory storeFactory = new FileStoreFactory(settings);
//            ScreenLogFactory logFactory = new ScreenLogFactory(settings);
//            MessageFactory messageFactory = new DefaultMessageFactory();

//            SocketInitiator initiator = new SocketInitiator(application, storeFactory, settings, logFactory, messageFactory);
//            initiator.start();
//            Thread.Sleep(3000);
//            System.Collections.ArrayList list = initiator.getSessions();
//            SessionID sessionID = (SessionID)list[0];

//            QuickFix42.NewOrderSingle order = new QuickFix42.NewOrderSingle(new ClOrdID("Hello"), new HandlInst(HandlInst.AUTOMATED_EXECUTION_ORDER_PRIVATE), new Symbol("ABC"), new Side(Side.SELL), new TransactTime(DateTime.Now), new OrdType(OrdType.MARKET));
//            //QuickFix44.NewOrderSingle order = new QuickFix44.NewOrderSingle(new ClOrdID("Hello"), new Side(Side.SELL), new TransactTime(DateTime.MinValue), new OrdType(OrdType.FOREX_MARKET));
//            //order.getHeader().setField(new SenderCompID("CLIENT"));
//            //order.getHeader().setField(new TargetCompID("SERVER"));
//            var header = order.getHeader();
//            var trailer = order.getTrailer();

//            //QuickFix42.Message order = new QuickFix42.Message();
//            //order.getHeader().setField(new MsgType("D"));
//            //order.setField(new ClOrdID("Hello"));
//            //order.setField(new Side(Side.SELL));
//            //order.setField(new TransactTime(DateTime.Now));
//            //order.setField(new OrdType(OrdType.MARKET));
//            //order.setField(new OrderQty(OrderQty.FIELD));


//            bool sent = Session.sendToTarget(order, sessionID);
//            if (sent)
//                Console.WriteLine("Sent Order to Server");

//            Console.ReadLine();

//            initiator.stop();
//        }
//    }

//    public class ClientInitiator : QuickFix.Application
//    {

//        public void onCreate(QuickFix.SessionID value)
//        {
//            //Console.WriteLine("Message OnCreate" + value.toString());
//        }

//        public void onLogon(QuickFix.SessionID value)
//        {
//            //Console.WriteLine("OnLogon" + value.toString());
//        }

//        public void onLogout(QuickFix.SessionID value)
//        {
//            // Console.WriteLine("Log out Session" + value.toString());
//        }

//        public void toAdmin(QuickFix.Message value, QuickFix.SessionID session)
//        {
//            //Console.WriteLine("Called Admin :" + value.ToString());
//        }

//        public void toApp(QuickFix.Message value, QuickFix.SessionID session)
//        {
//            //  Console.WriteLine("Called toApp :" + value.ToString());
//        }

//        //nhan  message logon(A), testrequest(2), heartbeat(0), 
//        public void fromAdmin(QuickFix.Message value, SessionID session)
//        {
//            // Console.WriteLine("Got message from Admin" + value.ToString());
//        }

//        //nhan message neworder(D), reject(j)
//        public void fromApp(QuickFix.Message value, SessionID session)
//        {
//            ////confirm receiving order
//            //Console.WriteLine("Got Execution Report from Server \n" + value.ToString());
//        }
//    }
//}
