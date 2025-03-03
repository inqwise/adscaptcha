using System;
using Jayrock.Json;

namespace Inqwise.AdsCaptcha.Common
{
    public class AdsCaptchaOperationResult<T> : AdsCaptchaOperationResult
    {
        protected AdsCaptchaOperationResult() : base()
        {
           
        }

        public T Value { get; private set; }

        public bool HasValue
        {
            get { return !HasError; }
        }

        public static implicit operator AdsCaptchaOperationResult<T>(T value)
        {
           return ToValueOrNotExist(value);
        }

        public new static AdsCaptchaOperationResult<T> ToError(AdsCaptchaErrors error, Guid? ticketId = null,
                                                        string description = null)
        {
            return new AdsCaptchaOperationResult<T>
            {
                Description = description,
                Error = error,
                TicketId = ticketId ?? Guid.NewGuid()
            };
        }

        public new static AdsCaptchaOperationResult<T> ToError(AdsCaptchaOperationResult result)
        {
            return new AdsCaptchaOperationResult<T>
            {
                Description = result.Description,
                Error = result.Error,
                TicketId = result.TicketId,
            };
        }

        public static AdsCaptchaOperationResult<T> ToValueOrNotExist(T value)
        {
            if (Equals(default(T), value) && !typeof(T).IsValueType)
            {
                return ToError(AdsCaptchaErrors.NoResults);
            }

            return new AdsCaptchaOperationResult<T> { Value = value };
        }
    }

    public class AdsCaptchaOperationResult
    {
        public static JsonObject JsonOk = new JsonObject {{"result","success"}};
        public static readonly AdsCaptchaOperationResult Ok = new AdsCaptchaOperationResult();
        public AdsCaptchaErrors? Error { get; protected set; }
        public string Description { get; set; }
        public Guid TicketId { get; protected set; }
        public bool HasError
        {
            get { return Error.HasValue; }
        }

        public JsonObject ToJson()
        {
            JsonObject result;
            if (HasError)
            {
                var jo = new JsonObject {{"result", Error.ToString().UnCamelCase("-")}};

                if (null != Description)
                {
                    jo.Add("description", Description);
                }
                result = jo;
            }
            else
            {
                result = JsonOk;
            }

            return result;
        }

        public static AdsCaptchaOperationResult ToError(AdsCaptchaErrors error, Guid? ticketId = null,
                                                        string description = null)
        {
            return new AdsCaptchaOperationResult
                {
                    Description = description,
                    Error = error,
                    TicketId = ticketId ?? Guid.NewGuid()
                };
        }

        protected AdsCaptchaOperationResult()
        {
        }

        public static AdsCaptchaOperationResult ToError(AdsCaptchaOperationResult result)
        {
            return new AdsCaptchaOperationResult
            {
                Description = result.Description,
                Error = result.Error,
                TicketId = result.TicketId,
            };
        }
    }
}