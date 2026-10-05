// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


namespace LSharp.IL.Metadata
{
    public abstract class Heap
    {

        public int IndexSize;

        internal readonly byte[] data;

        protected Heap(byte[] data)
        {
            this.data = data;
        }
    }
}
