// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	PdbHeap.cs
====================================================================================*/

using RID = System.UInt32;

namespace LSharp.IL.Metadata
{
    public sealed class PdbHeap : Heap
    {

        public byte[] Id;
        public RID EntryPoint;
        public long TypeSystemTables;
        public uint[] TypeSystemTableRows;

        public PdbHeap(byte[] data)
            : base(data)
        {
        }

        public bool HasTable(Table table)
        {
            return (TypeSystemTables & (1L << (int)table)) != 0;
        }
    }
}
