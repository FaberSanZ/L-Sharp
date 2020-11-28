// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	BinaryStreamReader.cs
====================================================================================*/

using System.IO;

namespace LSharp.IL.PE
{
    public class BinaryStreamReader : BinaryReader
    {

        public int Position
        {
            get { return (int)BaseStream.Position; }
            set { BaseStream.Position = value; }
        }

        public int Length
        {
            get { return (int)BaseStream.Length; }
        }

        public BinaryStreamReader(Stream stream)
            : base(stream)
        {
        }

        public void Advance(int bytes)
        {
            BaseStream.Seek(bytes, SeekOrigin.Current);
        }

        public void MoveTo(uint position)
        {
            BaseStream.Seek(position, SeekOrigin.Begin);
        }

        public void Align(int align)
        {
            align--;
            int position = Position;
            Advance(((position + align) & ~align) - position);
        }

        public DataDirectory ReadDataDirectory()
        {
            return new DataDirectory(ReadUInt32(), ReadUInt32());
        }
    }
}
