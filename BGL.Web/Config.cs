using System.Configuration;

namespace BGL.Web
{
    public static class Config
    {
        public static int RepositoryCount
        {
            get
            {
                var defaultRepoCount = 5;
                var repoCount = 0; ;

                int.TryParse(ConfigurationManager.AppSettings["RepositoryCount"], out repoCount);

                return repoCount > 0 ? repoCount : defaultRepoCount;
            }
        }
    }
}