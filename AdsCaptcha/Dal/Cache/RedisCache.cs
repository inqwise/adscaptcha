using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using BookSleeve;
using Inqwise.AdsCaptcha.SystemFramework;
using NLog;

namespace Inqwise.AdsCaptcha.Dal.Cache
{
    public class RedisCache : ILocker, IPublisher
    {
        //private const int DEFAULT_CACHE_DB_ID = 12;
        private const int DEFAULT_LOCKER_DB_ID = 13;
        private const string DEFAULT_LOCK_KEY_PREFIX = "l_";
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();

        private const string DEFAULT_HOST = "127.0.0.1";
        private const int DEFAULT_PORT = 16379;
        private const int DEFAULT_OBJECT_EXPIRY_IN_SECONDS = 600;
        private TimeSpan _objectExpiry;
        private string host;
        private int port;
        private MsgPackSerializer serializer;
        private ExpirationType _expirationType;
        
        public RedisCache(ExpirationType expirationType = ExpirationType.Absolute)
        {
            host = ConfigurationManager.AppSettings["Redis.ServerIP"] ?? DEFAULT_HOST;
            if (!int.TryParse(ConfigurationManager.AppSettings["Redis.ServerPort"], out port))
            {
                port = DEFAULT_PORT;
            }

            int expiryInSeconds;
            if (!int.TryParse(ConfigurationManager.AppSettings["Redis.ExpiryInSeconds"], out expiryInSeconds))
            {
                expiryInSeconds = DEFAULT_OBJECT_EXPIRY_IN_SECONDS;
            }

            _objectExpiry = TimeSpan.FromSeconds(expiryInSeconds);
            serializer = new MsgPackSerializer();
            this._expirationType = expirationType;
        }

        private struct RedisConnectionBox : IDisposable
        {
            private RedisConnection connection;
            private Action<RedisConnection> disposeCallback;
            private Action resetCallback;

            public RedisConnectionBox(RedisConnection connection, Action<RedisConnection> disposeCallback, Action resetCallback)
            {
                this.connection = connection;
                this.disposeCallback = disposeCallback;
                this.resetCallback = resetCallback;
            }

            public void Dispose()
            {
                if (null != disposeCallback)
                {
                    disposeCallback(connection);
                }
            }

            public RedisConnection Get()
            {
                return connection;
            }

            public void Reset()
            {
                if (null != resetCallback)
                {
                    resetCallback();
                }
            }
        }

        /*
        private RedisConnection GetClient()
        {
            var client = new RedisConnection(host, port);
            client.Wait(client.Open());
            return client;
        }
         * */

        private RedisConnection connection;
        private RedisConnectionBox GetClientBox()
        {

            CreateClient(ref connection);
            return new RedisConnectionBox(connection, c => { }, () => connection = null);
        }

        private Task GetClientAsync(Action<RedisConnection> callback)
        {
            var client = new RedisConnection(host, port);
            return client.Open().ContinueWith(c => {
                                                       using (client)
                                                       {
                                                           callback(client);
                                                       }
            });
        }

        private void ReCreateClient()
        {
            RedisConnection newConnection = null;
            CreateClient(ref newConnection);

            if (null != connection)
            {
                if(Monitor.TryEnter(typeof (RedisConnectionBox)))
                {
                    try
                    {
                        var tmpConnection = connection;
                        connection = newConnection;
                        tmpConnection.Close(false);
                    }
                    finally
                    {
                        Monitor.Exit(typeof(RedisConnectionBox));
                    }
                }
            }
        }

        private void CreateClient(ref RedisConnection connection)
        {
            if (null == connection)
            {
                lock (typeof(RedisConnection))
                {
                    if (null == connection)
                    {
                        connection = new RedisConnection(host, port);
                        connection.Wait(connection.Open());
                    }
                }
            }
        }

        private void ResetClient()
        {
            connection = null;
        }

        private bool TestConnection()
        {
            try
            {
                using (var client = GetClientBox())
                {
                    return client.Get().Wait(client.Get().Keys.Remove(DEFAULT_LOCKER_DB_ID, "1"));
                }
            }
            catch (RedisException ex)
            {
                Log.Error(ex);
                return false;
            }
        }

        public TValue Get<TValue>(object key, TimeSpan? slidingExpiry, CacheName cacheName)
        {
            try
            {
                using (var client = GetClientBox())
                {
                    object value = client.Get().Wait(client.Get().Strings.Get((int)cacheName, (string)key));
                    if (null != value && _expirationType == ExpirationType.Sliding)
                    {
                        ProlongExpiration(client.Get(), cacheName, (string)key, slidingExpiry);
                    }
                    return ((typeof (TValue) is byte[])
                                ? (TValue) value
                                : serializer.Deserialize<TValue>(value as byte[]));
                }
            }
            catch (RedisException ex)
            {
                Log.Error(ex);
                ResetClient();
                return default(TValue);
            }
        }

        private void ProlongExpiration(RedisConnection client, CacheName cacheName, string key, TimeSpan? expiry)
        {
            client.Keys.Expire((int)cacheName, key, (int)(expiry ?? _objectExpiry).TotalSeconds);
        }

        public TValue GetOrAdd<TValue>(object key, Func<TValue> getFunc, TimeSpan? expiry, CacheName cacheName)
        {
            var result = Get<TValue>(key, expiry, cacheName);
            if (Equals(default(TValue), result))
            {
                result = Add(key, getFunc(), expiry, cacheName);
            }
            return result;
        }

        public TValue Add<TValue>(object key, TValue value, TimeSpan? expiry, CacheName cacheName)
        {
            try
            {
                using (var client = GetClientBox())
                {
                    byte[] binaryVaue;
                    if (typeof (TValue) is byte[])
                    {
                        binaryVaue = value as byte[];
                    }
                    else if(null == value)
                    {
                        binaryVaue = null;
                    }
                    else
                    {
                        binaryVaue = serializer.Serialize(value);
                    }

                    if (null != binaryVaue)
                    {
                        client.Get().Strings.Set((int)cacheName, (string)key, binaryVaue,
                                           (long) (expiry ?? _objectExpiry).TotalSeconds).Wait();
                    }
                    else
                    {
                        Log.Warn("null value received. Key '{0}' not created. CacheName: '{1}'", key, cacheName);
                    }
                }
                return value;
            }
            catch (RedisException ex)
            {
                Log.Error(ex);
                return default(TValue);
            }
        }

        public bool AddIfNotExists<TValue>(object key, TValue value, TimeSpan? expiry, CacheName cacheName)
        {
            try
            {
                using (var client = GetClientBox())
                {
                    bool result = false;
                    byte[] binaryVaue;
                    if (typeof (TValue) is byte[])
                    {
                        binaryVaue = value as byte[];
                    }
                    else if(null == value)
                    {
                        binaryVaue = null;
                    }
                    else
                    {
                        binaryVaue = serializer.Serialize(value);
                    }

                    if (null != binaryVaue)
                    {

                        result = client.Get().Wait(client.Get().Strings.SetIfNotExists((int)cacheName, (string)key, binaryVaue));
                    }
                    else
                    {
                        Log.Warn("null value received. Key '{0}' not created. CacheName: '{1}'", key, cacheName);
                    }
                    
                    if (result)
                    {
                        ProlongExpiration(client.Get(), cacheName, (string)key, expiry);
                    }
                    
                    return result;
                }
                
            }
            catch (RedisException ex)
            {
                Log.Error(ex);
                return false;
            }
        }

        public bool Remove(object key, CacheName cacheName)
        {
            try
            {
                using (var client = GetClientBox())
                {
                    return client.Get().Wait(client.Get().Keys.Remove((int)cacheName, (string)key)); 
                }
            }
            catch (RedisException ex)
            {
                Log.Error(ex);
                return false;
            }
        }

        public void AddAsync<TValue>(object key, TValue value, Action<TValue> callback, TimeSpan? expiry, CacheName cacheName)
        {
            TValue result;
            try
            {
                using (var client = GetClientBox())
                {
                    byte[] binaryVaue = ((typeof(TValue) is byte[]) ? value as byte[] : serializer.Serialize(value));
                    client.Get().Strings.Set((int)cacheName, (string)key, binaryVaue, (long)((expiry ?? _objectExpiry).TotalSeconds)).ContinueWith(t =>
                        { if (callback != null) callback(value); });
                }
            }
            catch (RedisException ex)
            {
                Log.Error(ex);
                if (callback != null) callback(default(TValue));
            }
        }

        private struct LockerWrapper :IDisposable
        {
            private string key;
            private Func<RedisConnectionBox> getClientBox;
            public LockerWrapper(string key, Func<RedisConnectionBox> getClientBox)
            {
                this.key = key;
                this.getClientBox = getClientBox;
            }

            public void Dispose()
            {
                if (null != key)
                {
                    using (var client = getClientBox())
                    {
                        try
                        {
                            client.Get().Strings.ReleaseLock(DEFAULT_LOCKER_DB_ID, key).Wait();
                        }
                        catch (Exception ex)
                        {
                            Log.ErrorException("Unexpected error in ReleaseLock for the key: " + key, ex);
                        }
                    }
                }
            }
        }

        public IDisposable AcquireLock(string key, TimeSpan timeout)
        {
            return AcquireLock(key, timeout as TimeSpan?);
        }

        public IDisposable AcquireLock(string key)
        {
            return AcquireLock(key, null);
        }

        public static readonly TimeSpan DefaultLockerTimeout = TimeSpan.FromSeconds(2);

        private IDisposable AcquireLock(string key, TimeSpan? timeout)
        {
            try
            {
                using (var client = GetClientBox())
                {
                    var lockerKey = DEFAULT_LOCK_KEY_PREFIX + key;
                    var actualTimeout = timeout.GetValueOrDefault(DefaultLockerTimeout);
                    var sw = Stopwatch.StartNew();
                    bool hasExclusiveAccess = false;
                    while (sw.Elapsed <= actualTimeout &&
                           !(hasExclusiveAccess = client.Get().Wait(client.Get().Strings.TakeLock(DEFAULT_LOCKER_DB_ID, lockerKey,
                                                                                      string.Empty,
                                                                                      (int) actualTimeout.TotalSeconds))))
                    {
                        Thread.Sleep(500);
                    }

                    sw.Stop();
                    if (!hasExclusiveAccess) return new LockerWrapper(null, null);

                    return new LockerWrapper(lockerKey, GetClientBox);
                }
            }
            catch (TimeoutException ex)
            {
                return new LockerWrapper(null, null);
            }
            catch (RedisException ex)
            {
                Log.Error(ex);
                return new LockerWrapper(null, null);
            }
        }

        public void PublishAsync(string key, string value, Action callback)
        {
            using (var client = GetClientBox())
            {
                client.Get().Publish(key, value).ContinueWith((t) => { if (null != callback) callback(); });
            }
        }

        public void Publish(string key, string value)
        {
            using (var client = GetClientBox())
            {
                client.Get().Publish(key, value).Wait();
            }
        }
    }
}