using log4net;
using System;

namespace Ecan.WaterUseAlerts.Global
{
	public static class Log
	{

        private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public static ILog Logger { get => logger; set => logger = value; }



		public static void Debug(string message)
		{
			Log.logger.Debug(message);
		}

		public static void Error(string message)
		{
			Log.logger.Error(message);
		}

		public static void Info(string message)
		{
			Log.logger.Info(message);
		}

		public static void Trace(string message)
		{
			Log.logger.Info(message);
		}

		public static void Trace(string Method, string message)
		{
			Log.logger.Info(string.Format(" - {0}: {1}", Method, message));
		}

		public static void Trace(string Method, DateTime dt)
		{
			Log.logger.Info(string.Format(" - {0}: DateTime {1}", Method, dt.ToString("dd/MMM/yyyy")));
		}


		public static void Warn(string message)
		{
			Log.logger.Warn(message);
		}
	}
}