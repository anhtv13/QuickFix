//using System;
//using System.Collections.Generic;

//using System.Text;
//using QuickFix;

//namespace Server
//{
//    class Server
//    {
//        [STAThread]
//        static void Main(string[] args)
//        {
//            try
//            {
//                SessionSettings settings = new SessionSettings(@"C:\Users\anhtv\Desktop\QuickFix\QuickFix\acceptor.cfg");
//                FixServerApplication application = new FixServerApplication();
//                FileStoreFactory storeFactory = new FileStoreFactory(settings);
//                ScreenLogFactory logFactory = new ScreenLogFactory(settings);
//                MessageFactory messageFactory = new DefaultMessageFactory();
//                SocketAcceptor acceptor = new SocketAcceptor(application, storeFactory, settings, logFactory, messageFactory);

//                acceptor.start();
//                Console.WriteLine("press <enter> to quit");
//                Console.Read();
//                acceptor.stop();
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//            }
//        }
//    }
//    public class FixServerApplication : MessageCracker, QuickFix.Application
//    {
//        public void onCreate(SessionID sessionID) { }
//        public void onLogon(SessionID sessionID) { }
//        public void onLogout(SessionID sessionID) { }
//        public void toAdmin(Message message, SessionID sessionID) { }
//        public void toApp(Message message, SessionID sessionID) { }
//        public void fromAdmin(Message message, SessionID sessionID) { }
//        public void fromApp(Message message, SessionID sessionID)
//        {
//            string length = message.getHeader().getField(9);
//            string checksum = message.getHeader().getField(10);
//            string msgType = message.getHeader().getField(35);
//            crack(message, sessionID);//convert to proper Message(newsingleorder, cancelorder,...)            
//        }

//        //35=8
//        public override void onMessage(QuickFix42.ExecutionReport message, SessionID session)
//        {
//            ExecType et = message.getExecType();
//            char type = et.getValue();
//            switch (type)
//            {
//                case 'A':
//                    OrdStatus ordS = message.getOrdStatus();
//                    char status = ordS.getValue();
//                    //if status == 'A'
//                    //order acknowledgement
//                    break;
//                case '0':
//                case 'B':
//                case 'F':
//                    break;
//            }

//        }

//        //35=9
//        public override void onMessage(QuickFix42.OrderCancelReject message, SessionID sessionID)
//        {

//        }

//        //35=G
//        public override void onMessage(QuickFix42.OrderCancelReplaceRequest message, SessionID sessionID)
//        {

//        }

//        //35 = F
//        public override void onMessage(QuickFix42.OrderCancelRequest message, SessionID sessionID)
//        {

//        }

//        //35 = D
//        public override void onMessage(QuickFix42.NewOrderSingle message, SessionID sessionID)
//        {
//            ClOrdID clordid = message.getClOrdID();
//            string clord = clordid.getValue();
//            Side side = message.getSide();
//            char s = side.getValue();
//            OrdType ordtype = message.getOrdType();
//            char ord = ordtype.getValue();
//            TransactTime time = message.getTransactTime();
//            DateTime dt = time.getValue();

//            //Console.WriteLine("Got Order from session {0}", sessionID.toString());
//            //Symbol symbol = new Symbol();
//            //Side side = new Side();
//            //OrdType ordType = new OrdType();
//            //OrderQty orderQty = new OrderQty();
//            //Price price = new Price();
//            //ClOrdID clOrdID = new ClOrdID();

//            //order.get(ordType);

//            ////if (ordType.getValue() != OrdType.LIMIT)
//            ////    throw new IncorrectTagValue(ordType.getField());

//            //order.get(symbol);
//            //order.get(side);
//            //order.get(orderQty);
//            //order.get(price);
//            //order.get(clOrdID);

//            //QuickFix44.ExecutionReport executionReport = new QuickFix42.ExecutionReport
//            //                                        (genOrderID(),
//            //                                          genExecID(),
//            //                                          new ExecTransType(ExecTransType.NEW),
//            //                                          new ExecType(ExecType.FILL),
//            //                                          new OrdStatus(OrdStatus.FILLED),
//            //                                          symbol,
//            //                                          side,
//            //                                          new LeavesQty(0),
//            //                                          new CumQty(orderQty.getValue()),
//            //                                          new AvgPx(price.getValue()));

//            //executionReport.set(clOrdID);
//            //executionReport.set(orderQty);
//            //executionReport.set(new LastShares(orderQty.getValue()));
//            //executionReport.set(new LastPx(price.getValue()));

//            //if (order.isSetAccount())
//            //    executionReport.set(order.getAccount());

//            QuickFix42.ExecutionReport executionReport = new QuickFix42.ExecutionReport();
//            var header = executionReport.getHeader();
//            header.setField(new MsgType(MsgType.EXECUTION_REPORT));
//            try
//            {
//                bool x = Session.sendToTarget(executionReport, sessionID);
//            }
//            catch (SessionNotFound) { }
//        }

//        private OrderID genOrderID()
//        {
//            return new OrderID((++m_orderID).ToString());
//        }

//        private ExecID genExecID()
//        {
//            return new ExecID((++m_execID).ToString());
//        }

//        private int m_orderID;
//        private int m_execID;
//    }
//}
