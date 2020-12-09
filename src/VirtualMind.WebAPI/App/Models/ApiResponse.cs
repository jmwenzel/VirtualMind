using System.Collections.Generic;

namespace VirtualMind.WebAPI.App.Models
{
    /// <summary>
    /// Api response class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ApiResponse<T>
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public ApiResponse()
        {
            Messages = new List<ResponseMessage>();
        }

        /// <summary>
        /// Data response
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Messages
        /// </summary>
        public List<ResponseMessage> Messages { get; set; }

        /// <summary>
        /// Add Success Message
        /// </summary>
        /// <param name="message"></param>
        public void AddSuccessMessage(string message)
        {
            AddMessage(message, MessageType.Success);
        }

        /// <summary>
        /// Add Warning Message
        /// </summary>
        /// <param name="message"></param>
        public void AddWarningMessage(string message)
        {
            AddMessage(message, MessageType.Warning);
        }

        /// <summary>
        /// Add Error Message
        /// </summary>
        /// <param name="message"></param>
        public void AddErrorMessage(string message)
        {
            AddMessage(message, MessageType.Error);
        }

        /// <summary>
        /// Add Message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageType"></param>
        private void AddMessage(string message, MessageType messageType)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Messages.Add(new ResponseMessage
                {
                    MessageType = messageType,
                    Message = message
                });
            }
        }
    }

    /// <summary>
    /// Response Message class
    /// </summary>
    public class ResponseMessage
    {
        /// <summary>
        /// Message Type
        /// </summary>
        public MessageType MessageType { get; set; }
        /// <summary>
        /// Message string
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// Enum message type
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Success
        /// </summary>
        Success,
        /// <summary>
        /// Warning
        /// </summary>
        Warning,
        /// <summary>
        /// Error
        /// </summary>
        Error
    }
}
