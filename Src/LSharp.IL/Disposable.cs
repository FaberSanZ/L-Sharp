// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)




using System;

namespace LSharp.IL
{
    public static class Disposable
    {

        public static Disposable<T> Owned<T>(T value) where T : class, IDisposable
        {
            return new Disposable<T>(value, owned: true);
        }

        public static Disposable<T> NotOwned<T>(T value) where T : class, IDisposable
        {
            return new Disposable<T>(value, owned: false);
        }
    }

    public struct Disposable<T> : IDisposable where T : class, IDisposable
    {

        internal readonly T value;
        private readonly bool owned;

        public Disposable(T value, bool owned)
        {
            this.value = value;
            this.owned = owned;
        }

        public void Dispose()
        {
            if (value != null && owned)
            {
                value.Dispose();
            }
        }
    }
}
