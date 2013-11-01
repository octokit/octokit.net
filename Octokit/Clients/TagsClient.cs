using System.Threading.Tasks;

namespace Octokit
{
    public class TagsClient : ApiClient, ITagsClient
    {
        public TagsClient(IApiConnection apiConnection) 
            : base(apiConnection)
        {
        }

        public Task<Tag> Get(string owner, string name, string reference)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNullOrEmptyString(reference, "reference");

            return ApiConnection.Get<Tag>(ApiUrls.Tag(owner, name, reference));
        }

        public Task<Tag> Create(string owner, string name, NewTag tag)
        {
            Ensure.ArgumentNotNullOrEmptyString(owner, "owner");
            Ensure.ArgumentNotNullOrEmptyString(name, "name");
            Ensure.ArgumentNotNull(tag, "tag");

            return ApiConnection.Post<Tag>(ApiUrls.CreateTag(owner, name), tag);
        }
    }
}