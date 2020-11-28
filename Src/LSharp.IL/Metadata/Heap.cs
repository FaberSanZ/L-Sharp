// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	Heap.cs
====================================================================================*/

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
