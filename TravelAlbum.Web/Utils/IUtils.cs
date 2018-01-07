using System.Web.Mvc;

namespace TravelAlbum.Web.Utils
{
    public interface IUtils
    {
        int GetCurrentLanguage(Controller controller);

        byte[] GetFile(string address);
    }
}