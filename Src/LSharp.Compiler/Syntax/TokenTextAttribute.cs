// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ

/*===================================================================================
	TokenTextAttribute.cs
====================================================================================*/


using System;


namespace LSharp.Compiler.Syntax
{

    [AttributeUsage(AttributeTargets.Field)]
    internal class TokenTextAttribute : Attribute
    {
        public TokenTextAttribute(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}
