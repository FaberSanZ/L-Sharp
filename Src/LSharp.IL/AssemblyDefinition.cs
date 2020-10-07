// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	AssemblyDefinition.cs
====================================================================================*/

using LSharp.IL.Collections.Generic;
using System;
using System.IO;
using System.Threading;

namespace LSharp.IL
{

    public sealed class AssemblyDefinition : ICustomAttributeProvider, ISecurityDeclarationProvider, IDisposable
    {
        private AssemblyNameDefinition name;

        internal ModuleDefinition main_module;
        private Collection<ModuleDefinition> modules;
        private Collection<CustomAttribute> custom_attributes;
        private Collection<SecurityDeclaration> security_declarations;

        public AssemblyNameDefinition Name
        {
            get { return name; }
            set { name = value; }
        }

        public string FullName
        {
            get { return name != null ? name.FullName : string.Empty; }
        }

        public MetadataToken MetadataToken
        {
            get { return new MetadataToken(TokenType.Assembly, 1); }
            set { }
        }

        public Collection<ModuleDefinition> Modules
        {
            get
            {
                if (modules != null)
                {
                    return modules;
                }

                if (main_module.HasImage)
                {
                    return main_module.Read(ref modules, this, (_, reader) => reader.ReadModules());
                }

                Interlocked.CompareExchange(ref modules, new Collection<ModuleDefinition>(1) { main_module }, null);
                return modules;
            }
        }

        public ModuleDefinition MainModule
        {
            get { return main_module; }
        }

        public MethodDefinition EntryPoint
        {
            get { return main_module.EntryPoint; }
            set { main_module.EntryPoint = value; }
        }

        public bool HasCustomAttributes
        {
            get
            {
                if (custom_attributes != null)
                {
                    return custom_attributes.Count > 0;
                }

                return this.GetHasCustomAttributes(main_module);
            }
        }

        public Collection<CustomAttribute> CustomAttributes
        {
            get { return custom_attributes ?? (this.GetCustomAttributes(ref custom_attributes, main_module)); }
        }

        public bool HasSecurityDeclarations
        {
            get
            {
                if (security_declarations != null)
                {
                    return security_declarations.Count > 0;
                }

                return this.GetHasSecurityDeclarations(main_module);
            }
        }

        public Collection<SecurityDeclaration> SecurityDeclarations
        {
            get { return security_declarations ?? (this.GetSecurityDeclarations(ref security_declarations, main_module)); }
        }

        internal AssemblyDefinition()
        {
        }

        public void Dispose()
        {
            if (this.modules == null)
            {
                main_module.Dispose();
                return;
            }

            Collection<ModuleDefinition> modules = this.Modules;
            for (int i = 0; i < modules.Count; i++)
            {
                modules[i].Dispose();
            }
        }
        public static AssemblyDefinition CreateAssembly(AssemblyNameDefinition assemblyName, string moduleName, ModuleKind kind)
        {
            return CreateAssembly(assemblyName, moduleName, new ModuleParameters { Kind = kind });
        }

        public static AssemblyDefinition CreateAssembly(AssemblyNameDefinition assemblyName, string moduleName, ModuleParameters parameters)
        {
            if (assemblyName == null)
            {
                throw new ArgumentNullException("assemblyName");
            }

            if (moduleName == null)
            {
                throw new ArgumentNullException("moduleName");
            }

            Mixin.CheckParameters(parameters);
            if (parameters.Kind == ModuleKind.NetModule)
            {
                throw new ArgumentException("kind");
            }

            AssemblyDefinition assembly = ModuleDefinition.CreateModule(moduleName, parameters).Assembly;
            assembly.Name = assemblyName;

            return assembly;
        }

        public static AssemblyDefinition ReadAssembly(string fileName)
        {
            return ReadAssembly(ModuleDefinition.ReadModule(fileName));
        }

        public static AssemblyDefinition ReadAssembly(string fileName, ReaderParameters parameters)
        {
            return ReadAssembly(ModuleDefinition.ReadModule(fileName, parameters));
        }

        public static AssemblyDefinition ReadAssembly(Stream stream)
        {
            return ReadAssembly(ModuleDefinition.ReadModule(stream));
        }

        public static AssemblyDefinition ReadAssembly(Stream stream, ReaderParameters parameters)
        {
            return ReadAssembly(ModuleDefinition.ReadModule(stream, parameters));
        }

        private static AssemblyDefinition ReadAssembly(ModuleDefinition module)
        {
            AssemblyDefinition assembly = module.Assembly;
            if (assembly == null)
            {
                throw new ArgumentException();
            }

            return assembly;
        }
        
        public void SaveAndRun()
        {

        }

        public void Save(string fileName)
        {
            Save(fileName, new WriterParameters());
        }

        public void Save(string fileName, WriterParameters parameters)
        {
            main_module.Write(fileName, parameters);
        }

        public void Save()
        {
            main_module.Write();
        }

        public void Save(WriterParameters parameters)
        {
            main_module.Write(parameters);
        }

        public void Save(Stream stream)
        {
            Save(stream, new WriterParameters());
        }

        public void Save(Stream stream, WriterParameters parameters)
        {
            main_module.Write(stream, parameters);
        }

        public override string ToString()
        {
            return this.FullName;
        }
    }
}
