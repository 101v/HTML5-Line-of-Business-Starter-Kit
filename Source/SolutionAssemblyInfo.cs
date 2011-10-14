using System.Reflection;

[assembly: AssemblyCompany("i2o inc.")]
[assembly: AssemblyProduct("Fabrikam Widgets")]
[assembly: AssemblyCopyright("Copyright © i2o inc. 2011")]

#if DEBUG
[assembly: AssemblyConfiguration("DEBUG")]
#else
[assembly: AssemblyConfiguration("RELEASE")]
#endif