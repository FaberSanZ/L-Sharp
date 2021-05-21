// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


using System.Collections.Generic;
using System.Text;

namespace LSharp.IL.Metadata
{
    public class StringHeap : Heap
    {
        private readonly Dictionary<uint, string> strings = new Dictionary<uint, string>();

        public StringHeap(byte[] data)
            : base(data)
        {
        }

        public string Read(uint index)
        {
            if (index == 0)
            {
                return string.Empty;
            }

            if (strings.TryGetValue(index, out string @string))
            {
                return @string;
            }

            if (index > data.Length - 1)
            {
                return string.Empty;
            }

            @string = ReadStringAt(index);
            if (@string.Length != 0)
            {
                strings.Add(index, @string);
            }

            return @string;
        }

        protected virtual string ReadStringAt(uint index)
        {
            int length = 0;
            int start = (int)index;

            for (int i = start; ; i++)
            {
                if (data[i] == 0)
                {
                    break;
                }

                length++;
            }

            return Encoding.UTF8.GetString(data, start, length);
        }
    }
}
