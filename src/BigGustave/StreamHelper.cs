﻿namespace BigGustave
{
    using System;
    using System.IO;

    internal static class StreamHelper
    {
        public static int ReadBigEndianInt32(Stream stream)
        {
            return (ReadOrTerminate(stream) << 24) + (ReadOrTerminate(stream) << 16)
                                                   + (ReadOrTerminate(stream) << 8) + ReadOrTerminate(stream);
        }

        public static int ReadBigEndianInt32(byte[] bytes, int offset)
        {
            return (bytes[0 + offset] << 24) + (bytes[1 + offset] << 16)
                                             + (bytes[2 + offset] << 8) + bytes[3 + offset];
        }

        private static byte ReadOrTerminate(Stream stream)
        {
            var b = stream.ReadByte();

            if (b == -1)
            {
                throw new InvalidOperationException($"Unexpected end of stream at {stream.Position}.");
            }

            return (byte) b;
        }

        public static bool TryReadHeaderBytes(Stream stream, out byte[] bytes)
        {
            bytes = new byte[8];
            return stream.Read(bytes, 0, 8) == 8;
        }
    }
}