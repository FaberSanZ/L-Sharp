// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)



namespace LSharp.IL
{
    public struct ArrayDimension
    {
        public ArrayDimension(int? lowerBound, int? upperBound)
        {
            LowerBound = lowerBound;
            UpperBound = upperBound;
        }

        public int? LowerBound { get; set; }

        public int? UpperBound { get; set; }

        public bool IsSized => LowerBound.HasValue || UpperBound.HasValue;


        public override string ToString() => !IsSized ? string.Empty : LowerBound + "..." + UpperBound;

    }
}
