using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NationalInstruments.VisaNS;

namespace SBJController
{
    public class VisaInstrument
    {
        private MessageBasedSession m_session;
        
        public MessageBasedSession Session
        {
            get { return m_session; }
            set { m_session = value; }
        }

        public string ResourceName {get; set;}
        
        public VisaInstrument(string resourceName)
        {
            ResourceName = resourceName;
        }

        internal void Connect(string resourceName)
        {
            try
            {
                ResourceManager manager = ResourceManager.GetLocalManager();
                m_session = (MessageBasedSession)manager.Open(resourceName);
            }
            catch (ArgumentException ex)
            {
                throw new SBJException("Error occured while trying to connect to instrument", ex);
            }
            catch (VisaException ex)
            {
                throw new SBJException("Error occured while trying to connect to instrument", ex);
            }
            catch (DllNotFoundException ex)
            {
                throw new SBJException("Error occured while trying to connect to instrument", ex);
            }
            catch (EntryPointNotFoundException ex)
            {
                throw new SBJException("Error occured while trying to connect to instrument", ex);
            }
        }

        public void Shutdown()
        {
            if (m_session != null)
            {
                m_session.Dispose();
            }
        }

        internal void Write(string command, string errorMessage)
        {
            try
            {
                m_session.Write(command);
            }
            catch (VisaException ex)
            {
                throw new SBJException(errorMessage, ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new SBJException(errorMessage, ex);
            }
            catch (DllNotFoundException ex)
            {
                throw new SBJException(errorMessage, ex);
            }
            catch (EntryPointNotFoundException ex)
            {
                throw new SBJException(errorMessage, ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new SBJException(errorMessage, ex);
            }
        }
    }
}
