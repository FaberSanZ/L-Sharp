// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


using System;
using System.Collections.Generic;

namespace LSharp.IL
{
    internal class MergeSort<T>
    {
        private readonly T[] elements;
        private readonly T[] buffer;
        private readonly IComparer<T> comparer;

        private MergeSort(T[] elements, IComparer<T> comparer)
        {
            this.elements = elements;
            this.buffer = new T[elements.Length];
            Array.Copy(this.elements, this.buffer, elements.Length);
            this.comparer = comparer;
        }

        public static void Sort(T[] source, IComparer<T> comparer)
        {
            Sort(source, 0, source.Length, comparer);
        }

        public static void Sort(T[] source, int start, int length, IComparer<T> comparer)
        {
            new MergeSort<T>(source, comparer).Sort(start, length);
        }

        private void Sort(int start, int length)
        {
            TopDownSplitMerge(this.buffer, this.elements, start, length);
        }

        private void TopDownSplitMerge(T[] a, T[] b, int start, int end)
        {
            if (end - start < 2)
            {
                return;
            }

            int middle = (end + start) / 2;
            TopDownSplitMerge(b, a, start, middle);
            TopDownSplitMerge(b, a, middle, end);
            TopDownMerge(a, b, start, middle, end);
        }

        private void TopDownMerge(T[] a, T[] b, int start, int middle, int end)
        {
            for (int i = start, j = middle, k = start; k < end; k++)
            {
                if (i < middle && (j >= end || comparer.Compare(a[i], a[j]) <= 0))
                {
                    b[k] = a[i++];
                }
                else
                {
                    b[k] = a[j++];
                }
            }
        }
    }
}
