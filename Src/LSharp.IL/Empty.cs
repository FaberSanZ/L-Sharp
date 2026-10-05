// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)



using LSharp.IL.Collections.Generic;
using System;

namespace LSharp.IL
{
    internal static class Empty<T>
    {

        public static readonly T[] Array = new T[0];
    }

    internal class ArgumentNullOrEmptyException : ArgumentException
    {

        public ArgumentNullOrEmptyException(string paramName)
            : base("Argument null or empty", paramName)
        {
        }
    }
}

namespace LSharp.IL
{
    internal static partial class Mixin
    {

        public static bool IsNullOrEmpty<T>(this T[] self)
        {
            return self == null || self.Length == 0;
        }

        public static bool IsNullOrEmpty<T>(this Collection<T> self)
        {
            return self == null || self.size == 0;
        }

        public static T[] Resize<T>(this T[] self, int length)
        {
            Array.Resize(ref self, length);
            return self;
        }

        public static T[] Add<T>(this T[] self, T item)
        {
            if (self == null)
            {
                self = new[] { item };
                return self;
            }

            self = self.Resize(self.Length + 1);
            self[self.Length - 1] = item;
            return self;
        }
    }
}
