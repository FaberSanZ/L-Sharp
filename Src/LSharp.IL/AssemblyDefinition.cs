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

        internal ModuleDefinition main_module;

        private AssemblyNameDefinition name;
        private Collection<ModuleDefinition> modules;
        private Collection<CustomAttribute> custom_attributes;
        private Collection<SecurityDeclaration> security_declarations;

        public AssemblyDefinition()
        {
        }


        public AssemblyNameDefinition Name
        {
            get => name;
            set => name = value;
        }

        public string FullName => name is not null ? name.FullName : string.Empty;


        public MetadataToken MetadataToken
        {
            get => new MetadataToken(TokenType.Assembly, 1); 
            set { }
        }
        public Collection<ModuleDefinition> Modules
        {
            get
            {
                if (modules is not null)
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

        public ModuleDefinition MainModule => main_module;


        public MethodDefinition EntryPoint
        {
            get => main_module.EntryPoint;
            set => main_module.EntryPoint = value;
        }

        public bool HasCustomAttributes
        {
            get
            {
                if (custom_attributes is not null)
                {
                    return custom_attributes.Count > 0;
                }

                return this.GetHasCustomAttributes(main_module);
            }
        }

        public Collection<CustomAttribute> CustomAttributes => custom_attributes ?? (this.GetCustomAttributes(ref custom_attributes, main_module));


        public bool HasSecurityDeclarations
        {
            get
            {
                if (security_declarations is not null)
                {
                    return security_declarations.Count > 0;
                }

                return this.GetHasSecurityDeclarations(main_module);
            }
        }

        public Collection<SecurityDeclaration> SecurityDeclarations => security_declarations ?? (this.GetSecurityDeclarations(ref security_declarations, main_module));


        public void Dispose()
        {
            if (this.modules is null)
            {
                main_module.Dispose();
                return;
            }

            Collection<ModuleDefinition> modules = Modules;

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
            if (assemblyName is null)
            {
                throw new ArgumentNullException("assemblyName");
            }

            if (moduleName is null)
            {
                throw new ArgumentNullException("moduleName");
            }

            Mixin.CheckParameters(parameters);

            if (parameters.Kind is ModuleKind.NetModule)
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

            if (assembly is null)
            {
                throw new ArgumentException();
            }

            return assembly;
        }


        // TODO: SaveAndRun
        public void SaveAndRun(string fileName)
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
            return string.Format($"FullName: {FullName}");
        }
    }
}
