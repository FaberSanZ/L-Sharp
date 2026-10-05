// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


using LSharp.IL.Collections.Generic;
using LSharp.IL.Metadata;
using System;
using System.Collections.Generic;

namespace LSharp.IL
{

    using Slot = Row<string, string>;

    public sealed class TypeDefinitionCollection : Collection<TypeDefinition>
    {
        private readonly ModuleDefinition container;
        private readonly Dictionary<Slot, TypeDefinition> name_cache;

        internal TypeDefinitionCollection(ModuleDefinition container)
        {
            this.container = container;
            this.name_cache = new Dictionary<Slot, TypeDefinition>(new RowEqualityComparer());
        }

        internal TypeDefinitionCollection(ModuleDefinition container, int capacity)
            : base(capacity)
        {
            this.container = container;
            this.name_cache = new Dictionary<Slot, TypeDefinition>(capacity, new RowEqualityComparer());
        }

        protected override void OnAdd(TypeDefinition item, int index)
        {
            Attach(item);
        }

        protected override void OnSet(TypeDefinition item, int index)
        {
            Attach(item);
        }

        protected override void OnInsert(TypeDefinition item, int index)
        {
            Attach(item);
        }

        protected override void OnRemove(TypeDefinition item, int index)
        {
            Detach(item);
        }

        protected override void OnClear()
        {
            foreach (TypeDefinition type in this)
            {
                Detach(type);
            }
        }

        private void Attach(TypeDefinition type)
        {
            if (type.Module != null && type.Module != container)
            {
                throw new ArgumentException("Type already attached");
            }

            type.module = container;
            type.scope = container;
            name_cache[new Slot(type.Namespace, type.Name)] = type;
        }

        private void Detach(TypeDefinition type)
        {
            type.module = null;
            type.scope = null;
            name_cache.Remove(new Slot(type.Namespace, type.Name));
        }

        public TypeDefinition GetType(string fullname)
        {
            TypeParser.SplitFullName(fullname, out string @namespace, out string name);

            return GetType(@namespace, name);
        }

        public TypeDefinition GetType(string @namespace, string name)
        {
            if (name_cache.TryGetValue(new Slot(@namespace, name), out TypeDefinition type))
            {
                return type;
            }

            return null;
        }
    }
}
