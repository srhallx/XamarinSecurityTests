using System;

namespace XamarinSecurityTests
{
	public class MobileOpsEventArgs : EventArgs
	{
		public string Message;
		public MobileOpsEventArgs(string msg)
		{
			Message = msg;
		}
	}

	public class MobileOperations 
	{
		public delegate void MobileOpsEventHandler(object o, MobileOpsEventArgs e);
		public event MobileOpsEventHandler MobileOpsEvent;

		public MobileOperations ()
		{
		}

		/// <summary>
		/// Fire MobileOpsEvent and send log message to subscribing handler.
		/// </summary>
		/// <param name="entry">Entry.</param>
		protected void Log(string entry)
		{
			if (MobileOpsEvent != null)
				MobileOpsEvent (this, new MobileOpsEventArgs (entry));
		}
	}
}

