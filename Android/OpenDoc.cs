using System;

namespace SecurityTestsSql.Android
{
	public class OpenDoc : IOpenDoc
	{
		public OpenDoc ()
		{
		}

		#region IOpenDoc implementation

		public void OpenPDF (string pdfName)
		{
			throw new NotImplementedException ();
		}

		#endregion
	}
}

