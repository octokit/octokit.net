public class BuildVersion
{
    public string Version { get; private set; }
    public string Suffix { get; private set; }

    public BuildVersion(string version, string suffix)
    {
        Version = version;
        Suffix = suffix;

        if(string.IsNullOrWhiteSpace(Suffix))
        {
            Suffix = null;
        }
    }
}