using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octokit
{
    public interface IReleasesClient
    {
        /// <summary>
        /// Retrieves every <see cref="Release"/> for the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <returns>A <see cref="IReadonlyPagedCollection{Release}"/> of <see cref="Release"/>.</returns>
        Task<IReadOnlyCollection<Release>> GetAll(string owner, string name);

        /// <summary>
        /// Create a <see cref="Release"/> for the specified repository.
        /// </summary>
        /// <param name="owner">The owner of the repository.</param>
        /// <param name="name">The name of the repository.</param>
        /// <param name="data">The data for the release.</param>
        /// <returns>A new <see cref="Release"/>.</returns>
        Task<Release> CreateRelease(string owner, string name, ReleaseUpdate data);

        /// <summary>
        /// Upload a <see cref="ReleaseAsset"/> for the specified release.
        /// </summary>
        /// <param name="release">The <see cref="Release"/> to attach the asset to.</param>
        /// <param name="data">The asset information.</param>
        /// <returns>A new <see cref="ReleaseAsset"/>.</returns>
        Task<ReleaseAsset> UploadAsset(Release release, ReleaseAssetUpload data);
    }
}
