using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Rl.Net
{
    [Serializable]
    public class RLException : Exception
    {
        private const string ErrorCodeSerializationKey = nameof(RLException.ErrorCode);

        public int ErrorCode
        {
            get;
            set;
        } = -1;

        public RLException(ApiStatus status) : base(status.ErrorMessage)
        {
            this.ErrorCode = status.ErrorCode;
        }

        public RLException(ApiStatus status, Exception inner) : base(status.ErrorMessage, inner)
        {
            this.ErrorCode = status.ErrorCode;
        }

        protected RLException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            this.ErrorCode = info.GetInt32(ErrorCodeSerializationKey);
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue(ErrorCodeSerializationKey, this.ErrorCode);
            base.GetObjectData(info, context);
        }
    }
}