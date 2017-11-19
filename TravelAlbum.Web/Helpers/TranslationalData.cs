namespace TravelAlbum.Web.Helpers
{
    public struct TranslatedData
    {
        public string Title { get; set; }

        public string Descrption { get; set; }

        public TranslatedData(string title, string description)
        {
            this.Title = title;
            this.Descrption = description;
        }       
    }
}