using System;
using System.Collections.Concurrent;
using MsgPack.Serialization;

namespace Inqwise.AdsCaptcha.Dal.Cache
{
    internal class MsgPackSerializer
    {
        private static readonly ConcurrentDictionary<Type, IMessagePackSingleObjectSerializer> SerializersSet =
            new ConcurrentDictionary<Type, IMessagePackSingleObjectSerializer>();
        public byte[] Serialize(object obj)
        {
            var result = SerializersSet.GetOrAdd(obj.GetType(), MessagePackSerializer.Create).PackSingleObject(obj);
            return result;
        }

        public T Deserialize<T>(byte[] dataArray)
        {
            if (null == dataArray) return default(T);
            return (T)SerializersSet.GetOrAdd(typeof (T), MessagePackSerializer.Create).UnpackSingleObject(dataArray);
        }
    }
}