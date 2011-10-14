using System.Reflection;

// Fixed version number that is advanced with major/minor releases as it breaks strongly named assembly binding (if any)
[assembly: AssemblyVersion("1.0.0.0")]
// Customer-facing marketing version used in the UI (i.e. “2011 Feature Pack 1”)
[assembly: AssemblyInformationalVersion("Q4 2011")]
// Reflects when the build was producted - yyyy.mm.dd.hhmm (UTC) - shown in the About dialog in smaller print; with only one build agent and all builds take longer than a minute conflicts should be rare/non-existent
[assembly: AssemblyFileVersion("2011.10.14.1313")]