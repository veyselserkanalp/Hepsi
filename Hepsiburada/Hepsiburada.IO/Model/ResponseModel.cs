using System;

namespace Hepsiburada.IO.Model
{
    public class ResponseModel : IResponseModel
    {
        public string Message { get; set; }
        public string GetInfo()
        {
            string returnedData = "";
            returnedData += Environment.NewLine;
            returnedData += "----------------------------------------------------------------------";
            returnedData += Environment.NewLine;
            returnedData += Message;
            returnedData += Environment.NewLine;
            returnedData += "----------------------------------------------------------------------";
            returnedData += Environment.NewLine;
            return returnedData;
        }
    }
}
