// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


using LSharp.IL.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using MD = LSharp.IL.Metadata;

namespace LSharp.IL
{
    public sealed class ArrayType : TypeSpecification
    {
        private Collection<ArrayDimension> dimensions;


        public ArrayType(TypeReference type) : base(type)
        {
            Mixin.CheckType(type);
            etype = MD.ElementType.Array;
        }

        public ArrayType(TypeReference type, int rank) : this(type)
        {
            Mixin.CheckType(type);

            if (rank is 1)
            {
                return;
            }

            dimensions = new Collection<ArrayDimension>(rank);

            for (int i = 0; i < rank; i++)
            {
                dimensions.Add(new ArrayDimension());
            }

            etype = MD.ElementType.Array;
        }

        public Collection<ArrayDimension> Dimensions
        {
            get
            {
                if (dimensions is not null)
                {
                    return dimensions;
                }

                Collection<ArrayDimension> empty_dimensions = new()
                {
                    new ArrayDimension()
                };

                Interlocked.CompareExchange(ref dimensions, empty_dimensions, null);

                return dimensions;
            }
        }        

        public override string Name => base.Name + Suffix;

        public int Rank => dimensions is null ? 1 : dimensions.Count;

        public override string FullName => base.FullName + Suffix;

        public override bool IsArray => true;


        public override bool IsValueType
        {
            get => false;
            set => throw new InvalidOperationException();
        }


        public bool IsVector
        {
            get
            {
                if (dimensions is null)
                {
                    return true;
                }

                if (dimensions.Count > 1)
                {
                    return false;
                }

                ArrayDimension dimension = dimensions.First();

                return !dimension.IsSized;
            }
        }

        private string Suffix
        {
            get
            {
                if (IsVector)
                {
                    return "[]";
                }

                StringBuilder suffix = new StringBuilder();
                suffix.Append("[");
                for (int i = 0; i < dimensions.Count; i++)
                {
                    if (i > 0)
                    {
                        suffix.Append(",");
                    }

                    suffix.Append(dimensions[i].ToString());
                }
                suffix.Append("]");

                return suffix.ToString();
            }
        }


        

    }
}
