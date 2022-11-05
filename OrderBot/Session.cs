using System;

namespace OrderBot
{
    public class Session
    {
        private enum State
        {
            WELCOMING, PROPERTY_FINALIZED, FINALIZED , APPOINTMENT_PROPERTY_WATCHED , APPOINTMENT_PROPERTY_NOTWATCHED
        }

        private State nCur = State.WELCOMING;
        private Order oOrder;

        public Session(string sPhone)
        {
            this.oOrder = new Order();
            this.oOrder.Phone = sPhone;
        }

        public List<String> OnMessage(String sInMessage)
        {
            List<String> aMessages = new List<string>();
            switch (this.nCur)
            {
                case State.WELCOMING:
                    aMessages.Add("Welcome to GTA Properties!");
                    aMessages.Add("My name is Alice, How may I help you Today?");
                    this.nCur = State.PROPERTY_FINALIZED;
                    break;
                case State.PROPERTY_FINALIZED:
                    string message = sInMessage;
                    this.oOrder.Save();
                    aMessages.Add("Have you finalized the property? "+"\nYes\\No");
                    this.nCur = State.FINALIZED;
                    break;
                case State.FINALIZED:
                
                 string FINALIZED = sInMessage;
                    this.oOrder.Save();
                    if(FINALIZED.ToUpper()== "YES"){
                         aMessages.Add("As You already finalized the property.Should I book your appointment?"+ "\nYes\\No");
                      this.nCur = State.APPOINTMENT_PROPERTY_WATCHED;
                    break;
                }
                    else if(FINALIZED.ToUpper()== "NO"){ 
                        aMessages.Add("We have plenty of properties available. Should I book your appointment?"+ "\nYes\\No");
                    this.nCur = State.APPOINTMENT_PROPERTY_NOTWATCHED;
                    break;
                    }else
                    break;

                   case State.APPOINTMENT_PROPERTY_WATCHED:
                   string APPOINTMENT_PROPERTY_WATCHED = sInMessage;
                    this.oOrder.Save();
                     if(APPOINTMENT_PROPERTY_WATCHED.ToUpper()== "YES"){
                         aMessages.Add("Choose one from below options:" + "\n1. New Appointment"+ "\n2. Reschedule Appointment" + "\n3. Cancel Appointment");
                    break;
                }
                    else if(APPOINTMENT_PROPERTY_WATCHED.ToUpper()== "NO"){ 
                        aMessages.Add("blank");
                    break;
                    }else
                    break;
                    


            }
            aMessages.ForEach(delegate (String sMessage)
            {
                System.Diagnostics.Debug.WriteLine(sMessage);
            });
            return aMessages;
        }

    }
}
