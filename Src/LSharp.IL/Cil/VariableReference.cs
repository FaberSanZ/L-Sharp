// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


namespace LSharp.IL.Cil
{

    public abstract class VariableReference
    {

        internal int index = -1;
        protected TypeReference variable_type;

        public TypeReference VariableType
        {
            get { return variable_type; }
            set { variable_type = value; }
        }

        public int Index
        {
            get { return index; }
        }

        internal VariableReference(TypeReference variable_type)
        {
            this.variable_type = variable_type;
        }

        public abstract VariableDefinition Resolve();

        public override string ToString()
        {
            if (index >= 0)
            {
                return "V_" + index;
            }

            return string.Empty;
        }
    }
}
