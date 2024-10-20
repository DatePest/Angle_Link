using EventSystemTool;

namespace Client
{
    public class ClientMsgEvent
    {
        EventSystemTool.Listener<EventReceiveReturnMgs> Listener;
        MsgEvent msgEvent;
        public ClientMsgEvent()
        {
            Listener = new(Send);
            msgEvent = new MsgEvent();
        }

        public void Stop()
        {
            Listener.Stop();
        }

        public void Destory()
        {
            Listener.Stop();
            Listener = null;
            msgEvent = null;    
        }
        void Send(IEventTag sendData)
        {
            msgEvent.FindRun(sendData);
        }

        class MsgEvent : EventSystemTool.IEventBase<EventTagAttribute>
        {


            [EventTag(1)]
            public void Event_02()
            {

            }

        }
    }

}
