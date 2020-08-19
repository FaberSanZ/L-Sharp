
<h1 align="center">
   L#
  <br>
  
  ##  The .NET Compiler ("L-Sharp")
  
</h1>


![Sample](https://github.com/FaberSanZ/L-Sharp/blob/master/IMG/sample.PNG)
<hr>




<br>

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://github.com/Zeckoxe/Z-Sharp/blob/master/LICENSE)

The code is licensed under MIT. Feel free to use it for whatever purpose.

<hr>
<br>


## Examples

```csharp

import Core;

namespace Sample
{
    internal class Program
    {
        public function Main(string[] args) -> void 
        {
            Core.PrintLine(Test(args));              
        }
        
        public function Test(string[] args) -> bool 
        {
            let n = "old"; // string
            var i = "car"; // string            
            i = 's'; // char is a unicode (int)         
            i = 255; // new int
            i = 3.1415; // new double
            i = 3.1415f; // new float
            
            if (args.Length == 1)
                return true;
             
            return false;
        }
    }
}
```
