using System;

namespace Ecan.WaterUseAlerts.Global
{
    public class ExceptionHelper
    {
        public static void GetInnerExceptions(System.Exception ex, out string exceptionMessages)
        {
            exceptionMessages = string.Empty;

            while (ex != null)
            {
                if (string.IsNullOrEmpty(exceptionMessages))
                    exceptionMessages = ex.Message;
                else
                    exceptionMessages = exceptionMessages + Environment.NewLine + ex.Message;
                ex = ex.InnerException;
            }
        }
    }
}
