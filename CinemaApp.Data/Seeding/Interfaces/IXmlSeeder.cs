namespace CinemaApp.Data.Seeding.Interfaces
{
    using CinemaApp.Data.Utilities.Interfaces;

    public interface IXmlSeeder
    {
        public string RootName { get; }

        public IXmlHelper XmlHelper { get; }
    }
}
