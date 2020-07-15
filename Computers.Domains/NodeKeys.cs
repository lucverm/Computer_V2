using System.Collections.Generic;

namespace Computers.Domains
{
    /// <summary>
    /// Liste des clés usuels sous forme de constantes afin de limiter leurs répétitions dans le code.
    /// </summary>
    public static class NodeKeys
    {
        public const string Name = "Name";
        public const string ImageUrl = "Image";
        public const string Description = "Description";

        public static readonly IReadOnlyCollection<string> Keys = new List<string> {Name, ImageUrl, Description};
    }
}
