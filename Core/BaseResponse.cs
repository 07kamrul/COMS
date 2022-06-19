using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class BaseResponse
    {
        private bool isresult = false;
        public bool IsResult
        {
            get { return this.isresult; }
            set { this.isresult = value; }
        }

        private List<Message> errorMessages;
        private List<Message> ErrorMessages
        {
            get { return errorMessages; }
            set { errorMessages = value; }
        }
        public BaseResponse()
        {
            ErrorMessages = new List<Message>();
        }
    }

    public class Message
    {
        public Message()
        {
            Page = (int)PageCode.Success;
        }
        public string Code { get; set; }
        public string MessageEN { get; set; }
        public string HeadingTextEN { get; set; }
        public string HeadingTextBN { get; set; }
        public string Status { get; set; }
        public string StatusBN { get; set; }
        public string MessageBN { get; set; }
        public string TransactionId { get; set; }
        private int page { get; set; }
        public int Page
        {
            get { return this.page; }
            set { this.page = value; }
        }

    }
}
