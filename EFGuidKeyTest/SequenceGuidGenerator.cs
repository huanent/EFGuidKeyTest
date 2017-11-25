﻿using System.Threading;

namespace System
{
    /// <summary>
    /// 可排序的Guid生成器
    /// </summary>
    public static class SequenceGuidGenerator
    {

        static object _locker = new object();
        static long _counter = DateTime.UtcNow.Ticks;

        /// <summary>
        /// 生成SqlServer可排序的guid
        /// </summary>
        /// <returns></returns>
        public static Guid SqlServerKey()
        {
            var guidBytes = Guid.NewGuid().ToByteArray();
            byte[] counterBytes;

            lock (_locker)
            {
                counterBytes = BitConverter.GetBytes(Interlocked.Increment(ref _counter));
            }

            if (!BitConverter.IsLittleEndian)
            {
                Array.Reverse(counterBytes);
            }

            guidBytes[08] = counterBytes[1];
            guidBytes[09] = counterBytes[0];
            guidBytes[10] = counterBytes[7];
            guidBytes[11] = counterBytes[6];
            guidBytes[12] = counterBytes[5];
            guidBytes[13] = counterBytes[4];
            guidBytes[14] = counterBytes[3];
            guidBytes[15] = counterBytes[2];

            return new Guid(guidBytes);
        }
    }
}
